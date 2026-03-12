using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Test
{
    internal sealed partial class JiraExportDialog : Form
    {
        // ── Fields ────────────────────────────────────────────────────────────
        private readonly List<JiraIssue> _searchIssues;
        private readonly List<JiraIssue> _masterIssues;
        private readonly string          _baseUrl;

        // Stats tracked during FetchAndBuildRows
        private int    _statWorklogCount;
        private int    _statCommentCount;
        private double _statTotalHours;

        // Per-provider key memory (swapped as user changes provider)
        private string _claudeKey       = "";
        private string _openAiKey       = "";
        private string _azureKey        = "";
        private string _azureEndpoint   = "";

        private static readonly string ConfigFile =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jira_config.json");

        // ── Constructors ──────────────────────────────────────────────────────
        public JiraExportDialog()
            : this(new List<JiraIssue>(), new List<JiraIssue>(), "") { }

        public JiraExportDialog(List<JiraIssue> searchResults,
                                List<JiraIssue> masterItems,
                                string          baseUrl)
        {
            _searchIssues = searchResults ?? new List<JiraIssue>();
            _masterIssues = masterItems   ?? new List<JiraIssue>();
            _baseUrl      = (baseUrl      ?? "").TrimEnd('/');

            InitializeComponent();

            _radResults.Text = $"Search Results  ({_searchIssues.Count})";
            _radMaster.Text  = $"Master List  ({_masterIssues.Count})";

            LoadAiSettings();
            UpdateStatusLabel();
        }

        // ── Source radio ──────────────────────────────────────────────────────
        private void RadSource_Changed(object sender, EventArgs e) => UpdateStatusLabel();

        private void UpdateStatusLabel()
        {
            var list = _radResults.Checked ? _searchIssues : _masterIssues;
            SetStatus($"{list.Count} issue(s) selected as source.", false);
        }

        // ── Provider combo ────────────────────────────────────────────────────
        private enum AiProvider { Claude, OpenAI, Azure }

        private AiProvider GetSelectedProvider() =>
            _cmbProvider.SelectedIndex == 1 ? AiProvider.OpenAI :
            _cmbProvider.SelectedIndex == 2 ? AiProvider.Azure  :
                                              AiProvider.Claude;

        internal void CmbProvider_Changed(object sender, EventArgs e)
        {
            // Save current key back to in-memory slot before switching
            StoreCurrentKey();

            var provider = GetSelectedProvider();

            // Update key label and populate key field from memory
            switch (provider)
            {
                case AiProvider.OpenAI:
                    _lblAiKey.Text   = "OPENAI API KEY";
                    _txtAiKey.Text   = _openAiKey;
                    break;
                case AiProvider.Azure:
                    _lblAiKey.Text   = "AZURE OPENAI API KEY";
                    _txtAiKey.Text   = _azureKey;
                    _txtAzureEndpoint.Text = _azureEndpoint;
                    break;
                default: // Claude
                    _lblAiKey.Text   = "ANTHROPIC API KEY";
                    _txtAiKey.Text   = _claudeKey;
                    break;
            }

            // Show/hide Azure endpoint row
            bool isAzure = provider == AiProvider.Azure;
            _lblAzureEndpoint.Visible  = isAzure;
            _txtAzureEndpoint.Visible  = isAzure;
        }

        private void StoreCurrentKey()
        {
            switch (GetSelectedProvider())
            {
                case AiProvider.OpenAI: _openAiKey     = _txtAiKey.Text.Trim(); break;
                case AiProvider.Azure:  _azureKey      = _txtAiKey.Text.Trim();
                                        _azureEndpoint = _txtAzureEndpoint.Text.Trim(); break;
                default:                _claudeKey     = _txtAiKey.Text.Trim(); break;
            }
        }

        // ── AI settings persistence ───────────────────────────────────────────
        private void LoadAiSettings()
        {
            try
            {
                if (!File.Exists(ConfigFile)) return;
                var obj = JObject.Parse(File.ReadAllText(ConfigFile));

                _claudeKey     = obj["anthropic_key"]?.ToString()         ?? "";
                _openAiKey     = obj["openai_key"]?.ToString()             ?? "";
                _azureKey      = obj["azure_openai_key"]?.ToString()       ?? "";
                _azureEndpoint = obj["azure_openai_endpoint"]?.ToString()  ?? "";

                // Restore last-used provider — suppress the event so StoreCurrentKey()
                // doesn't overwrite the keys we just loaded with empty strings.
                int providerIdx = obj["ai_provider"]?.ToObject<int>() ?? 0;
                if (providerIdx >= 0 && providerIdx < _cmbProvider.Items.Count)
                {
                    _cmbProvider.SelectedIndexChanged -= CmbProvider_Changed;
                    _cmbProvider.SelectedIndex = providerIdx;
                    _cmbProvider.SelectedIndexChanged += CmbProvider_Changed;
                }

                // Populate key field for currently selected provider
                _txtAiKey.Text = GetSelectedProvider() == AiProvider.OpenAI ? _openAiKey
                               : GetSelectedProvider() == AiProvider.Azure   ? _azureKey
                               : _claudeKey;
                _txtAzureEndpoint.Text = _azureEndpoint;

                // Show/hide Azure endpoint row to match restored provider
                bool isAzure = GetSelectedProvider() == AiProvider.Azure;
                _lblAzureEndpoint.Visible = isAzure;
                _txtAzureEndpoint.Visible = isAzure;
            }
            catch { }
        }

        private void SaveAiSettings()
        {
            StoreCurrentKey();
            try
            {
                var obj = File.Exists(ConfigFile)
                    ? JObject.Parse(File.ReadAllText(ConfigFile))
                    : new JObject();

                obj["ai_provider"]          = _cmbProvider.SelectedIndex;
                obj["anthropic_key"]        = _claudeKey;
                obj["openai_key"]           = _openAiKey;
                obj["azure_openai_key"]     = _azureKey;
                obj["azure_openai_endpoint"]= _azureEndpoint;

                File.WriteAllText(ConfigFile, obj.ToString());
            }
            catch { }
        }

        // ── ADF → plain text ──────────────────────────────────────────────────
        private static string AdfToText(JToken token)
        {
            if (token == null || token.Type == JTokenType.Null) return "";
            if (token.Type == JTokenType.String)               return token.ToString();
            if (token.Type != JTokenType.Object)               return "";

            var obj     = (JObject)token;
            var content = obj["content"] as JArray;
            return content == null ? "" : ExtractAdf(content).Trim();
        }

        private static string ExtractAdf(JArray nodes)
        {
            if (nodes == null) return "";
            var sb = new StringBuilder();
            foreach (var node in nodes)
            {
                if (node?.Type != JTokenType.Object) continue;
                var obj  = (JObject)node;
                var type = obj["type"]?.ToString();
                if (type == "text")
                    sb.Append(obj["text"]?.ToString() ?? "");
                else
                {
                    var children = obj["content"] as JArray;
                    if (children != null) sb.Append(ExtractAdf(children));
                }
                if (type == "paragraph" || type == "heading" ||
                    type == "listItem"  || type == "codeBlock")
                    sb.Append("\n");
            }
            return sb.ToString();
        }

        // ── Data fetch ────────────────────────────────────────────────────────
        private async Task<(List<string> headers, List<List<string>> rows)> FetchAndBuildRows(
            List<JiraIssue> issues)
        {
            var headers = new List<string>();
            if (_chkColKey.Checked)        headers.Add("Key");
            if (_chkColSummary.Checked)    headers.Add("Summary");
            if (_chkColType.Checked)       headers.Add("Type");
            if (_chkColStatus.Checked)     headers.Add("Status");
            if (_chkColPriority.Checked)   headers.Add("Priority");
            if (_chkColProject.Checked)    headers.Add("Project");
            if (_chkColAssignee.Checked)   headers.Add("Assignee");
            if (_chkColDueDate.Checked)    headers.Add("Due Date");
            if (_chkColRecordType.Checked) headers.Add("Record Type");
            if (_chkColDate.Checked)       headers.Add("Date");
            if (_chkColAuthor.Checked)     headers.Add("Author");
            if (_chkColHours.Checked)      headers.Add("Hours");
            if (_chkColText.Checked)       headers.Add("Text");

            var rows = new List<List<string>>();
            _statWorklogCount = 0;
            _statCommentCount = 0;
            _statTotalHours   = 0;

            for (int i = 0; i < issues.Count; i++)
            {
                var issue = issues[i];
                _lblStatus.Text = $"Fetching {i + 1}/{issues.Count}: {issue.Key}\u2026";
                Application.DoEvents();

                // ── Worklogs ──────────────────────────────────────────────────
                if (_chkWorklogs.Checked)
                {
                    try
                    {
                        var resp = await JiraForm.Http.GetAsync(
                            _baseUrl + $"/rest/api/3/issue/{issue.Key}/worklog");
                        if (resp.IsSuccessStatusCode)
                        {
                            var data     = JObject.Parse(await resp.Content.ReadAsStringAsync());
                            var worklogs = data["worklogs"] as JArray;
                            if (worklogs != null)
                            {
                                foreach (var wl in worklogs)
                                {
                                    string date   = wl["started"]?.ToString() ?? "";
                                    string author = (wl["author"] as JObject)?["displayName"]?.ToString() ?? "";
                                    double secs   = wl["timeSpentSeconds"]?.ToObject<double>() ?? 0;
                                    string hours  = (secs / 3600.0).ToString("N2");
                                    string text   = AdfToText(wl["comment"]);

                                    _statWorklogCount++;
                                    _statTotalHours += secs / 3600.0;

                                    rows.Add(BuildRow(issue, "Worklog", date, author, hours, text));
                                }
                            }
                        }
                    }
                    catch { /* skip failed fetch */ }
                }

                // ── Comments ──────────────────────────────────────────────────
                if (_chkComments.Checked)
                {
                    try
                    {
                        var resp = await JiraForm.Http.GetAsync(
                            _baseUrl + $"/rest/api/3/issue/{issue.Key}/comment");
                        if (resp.IsSuccessStatusCode)
                        {
                            var data     = JObject.Parse(await resp.Content.ReadAsStringAsync());
                            var comments = data["comments"] as JArray;
                            if (comments != null)
                            {
                                foreach (var cm in comments)
                                {
                                    string date   = cm["created"]?.ToString() ?? "";
                                    string author = (cm["author"] as JObject)?["displayName"]?.ToString() ?? "";
                                    string text   = AdfToText(cm["body"]);

                                    _statCommentCount++;
                                    rows.Add(BuildRow(issue, "Comment", date, author, "", text));
                                }
                            }
                        }
                    }
                    catch { /* skip failed fetch */ }
                }
            }

            return (headers, rows);
        }

        private List<string> BuildRow(JiraIssue issue, string recordType,
                                      string date, string author,
                                      string hours, string text)
        {
            var row = new List<string>();
            if (_chkColKey.Checked)        row.Add(issue.Key      ?? "");
            if (_chkColSummary.Checked)    row.Add(issue.Summary  ?? "");
            if (_chkColType.Checked)       row.Add(issue.Type     ?? "");
            if (_chkColStatus.Checked)     row.Add(issue.Status   ?? "");
            if (_chkColPriority.Checked)   row.Add(issue.Priority ?? "");
            if (_chkColProject.Checked)    row.Add(issue.Project  ?? "");
            if (_chkColAssignee.Checked)   row.Add(issue.Assignee ?? "");
            if (_chkColDueDate.Checked)    row.Add(issue.DueDate  ?? "");
            if (_chkColRecordType.Checked) row.Add(recordType     ?? "");
            if (_chkColDate.Checked)       row.Add(date           ?? "");
            if (_chkColAuthor.Checked)     row.Add(author         ?? "");
            if (_chkColHours.Checked)      row.Add(hours          ?? "");
            if (_chkColText.Checked)       row.Add(text           ?? "");
            return row;
        }

        // ── AI Summary ────────────────────────────────────────────────────────
        private string BuildSummaryPrompt(List<string> headers, List<List<string>> rows, int issueCount)
        {
            int iKey    = headers.IndexOf("Key");
            int iDate   = headers.IndexOf("Date");
            int iAuthor = headers.IndexOf("Author");
            int iRec    = headers.IndexOf("Record Type");
            int iHours  = headers.IndexOf("Hours");
            int iText   = headers.IndexOf("Text");

            var body      = new StringBuilder();
            int charBudget = 80_000;

            foreach (var row in rows)
            {
                string rec    = iRec    >= 0 && iRec    < row.Count ? row[iRec]    : "";
                string key    = iKey    >= 0 && iKey    < row.Count ? row[iKey]    : "";
                string date   = iDate   >= 0 && iDate   < row.Count ? row[iDate]   : "";
                string author = iAuthor >= 0 && iAuthor < row.Count ? row[iAuthor] : "";
                string hours  = iHours  >= 0 && iHours  < row.Count ? row[iHours]  : "";
                string text   = iText   >= 0 && iText   < row.Count ? row[iText]   : "";

                if (string.IsNullOrWhiteSpace(text)) continue;

                string line = rec == "Worklog"
                    ? $"[Worklog] {key} | {date} | {author} | {hours}h | {text}\n"
                    : $"[Comment] {key} | {date} | {author} | {text}\n";

                if (body.Length + line.Length > charBudget)
                {
                    body.Append("... (additional entries truncated due to length)\n");
                    break;
                }
                body.Append(line);
            }

            return
                $"You are reviewing Jira work logs and comments for a team status summary.\n\n" +
                $"Statistics:\n" +
                $"  Issues processed: {issueCount}\n" +
                $"  Worklogs: {_statWorklogCount} ({_statTotalHours:N2} hours total)\n" +
                $"  Comments: {_statCommentCount}\n\n" +
                $"Entries:\n{body}\n" +
                $"Please provide a concise professional summary covering:\n" +
                $"1. Key work accomplished\n" +
                $"2. Any blockers or issues raised\n" +
                $"3. Notable time investments\n\n" +
                $"Keep it suitable for a status report.";
        }

        private async Task<string> SummarizeWithAI(List<string> headers,
                                                    List<List<string>> rows,
                                                    int issueCount)
        {
            string prompt = BuildSummaryPrompt(headers, rows, issueCount);
            switch (GetSelectedProvider())
            {
                case AiProvider.OpenAI: return await CallOpenAI(prompt);
                case AiProvider.Azure:  return await CallAzureOpenAI(prompt);
                default:                return await CallClaude(prompt);
            }
        }

        private async Task<string> CallClaude(string prompt)
        {
            if (string.IsNullOrEmpty(_claudeKey))
                throw new Exception("Enter an Anthropic API key to use Claude summarization.");

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("x-api-key", _claudeKey);
                http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
                http.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var body = new JObject
                {
                    ["model"]      = "claude-opus-4-6",
                    ["max_tokens"] = 2048,
                    ["messages"]   = new JArray(new JObject
                    {
                        ["role"]    = "user",
                        ["content"] = prompt
                    })
                };

                var response = await http.PostAsync(
                    "https://api.anthropic.com/v1/messages",
                    new StringContent(body.ToString(), Encoding.UTF8, "application/json"));

                var responseText = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception(
                        $"Claude API error {(int)response.StatusCode}: " +
                        responseText.Substring(0, Math.Min(300, responseText.Length)));

                var json = JObject.Parse(responseText);
                return json["content"]?[0]?["text"]?.ToString() ?? "(no response returned)";
            }
        }

        private async Task<string> CallOpenAI(string prompt)
        {
            if (string.IsNullOrEmpty(_openAiKey))
                throw new Exception("Enter an OpenAI API key to use ChatGPT summarization.");

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _openAiKey);
                http.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var body = new JObject
                {
                    ["model"]      = "gpt-4o",
                    ["max_tokens"] = 2048,
                    ["messages"]   = new JArray(new JObject
                    {
                        ["role"]    = "user",
                        ["content"] = prompt
                    })
                };

                var response = await http.PostAsync(
                    "https://api.openai.com/v1/chat/completions",
                    new StringContent(body.ToString(), Encoding.UTF8, "application/json"));

                var responseText = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception(
                        $"OpenAI API error {(int)response.StatusCode}: " +
                        responseText.Substring(0, Math.Min(300, responseText.Length)));

                var json = JObject.Parse(responseText);
                return json["choices"]?[0]?["message"]?["content"]?.ToString() ?? "(no response returned)";
            }
        }

        private async Task<string> CallAzureOpenAI(string prompt)
        {
            if (string.IsNullOrEmpty(_azureKey))
                throw new Exception("Enter an Azure OpenAI API key to use Copilot summarization.");
            if (string.IsNullOrEmpty(_azureEndpoint))
                throw new Exception("Enter the Azure OpenAI endpoint URL.");

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("api-key", _azureKey);
                http.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var body = new JObject
                {
                    ["max_tokens"] = 2048,
                    ["messages"]   = new JArray(new JObject
                    {
                        ["role"]    = "user",
                        ["content"] = prompt
                    })
                };

                var response = await http.PostAsync(
                    _azureEndpoint,
                    new StringContent(body.ToString(), Encoding.UTF8, "application/json"));

                var responseText = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception(
                        $"Azure OpenAI error {(int)response.StatusCode}: " +
                        responseText.Substring(0, Math.Min(300, responseText.Length)));

                var json = JObject.Parse(responseText);
                return json["choices"]?[0]?["message"]?["content"]?.ToString() ?? "(no response returned)";
            }
        }

        private List<List<string>> BuildSummarySheetRows(int issueCount, string summaryText)
        {
            var rows = new List<List<string>>();
            rows.Add(new List<string> { "STATISTICS" });
            rows.Add(new List<string> { $"Issues processed: {issueCount}" });
            rows.Add(new List<string> { $"Worklogs found: {_statWorklogCount}" });
            rows.Add(new List<string> { $"Comments found: {_statCommentCount}" });
            rows.Add(new List<string> { $"Total hours logged: {_statTotalHours:N2}" });
            rows.Add(new List<string> { $"Provider: {_cmbProvider.SelectedItem}" });
            rows.Add(new List<string> { "" });
            rows.Add(new List<string> { "AI SUMMARY" });
            rows.Add(new List<string> { "" });

            foreach (var line in summaryText.Replace("\r\n", "\n").Split('\n'))
                rows.Add(new List<string> { line.TrimEnd() });

            return rows;
        }

        // ── Preview ───────────────────────────────────────────────────────────
        private async void BtnLoadPreview_Click(object sender, EventArgs e)
        {
            var sourceList = _radResults.Checked ? _searchIssues : _masterIssues;
            if (sourceList.Count == 0)
            {
                _lblPreviewInfo.Text = "No issues in selected source.";
                return;
            }

            if (!_chkWorklogs.Checked && !_chkComments.Checked)
            {
                _lblPreviewInfo.Text = "Select at least one of: Worklogs, Comments.";
                return;
            }

            _btnLoadPreview.Enabled = false;
            _lblPreviewInfo.Text    = "Loading\u2026";
            _dgvPreview.Columns.Clear();
            _dgvPreview.Rows.Clear();

            try
            {
                int limit        = Math.Min(5, sourceList.Count);
                var previewIssues = sourceList.GetRange(0, limit);

                var result  = await FetchAndBuildRows(previewIssues);
                var headers = result.headers;
                var rows    = result.rows;

                // Build columns — give Text and Summary extra width
                foreach (var h in headers)
                {
                    var col = new System.Windows.Forms.DataGridViewTextBoxColumn
                    {
                        HeaderText = h,
                        SortMode   = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable,
                        Width      = (h == "Text" || h == "Summary") ? 280 : 100
                    };
                    _dgvPreview.Columns.Add(col);
                }

                foreach (var row in rows)
                    _dgvPreview.Rows.Add(row.ToArray());

                string issueWord = limit == 1 ? "issue" : "issues";
                _lblPreviewInfo.Text = $"{rows.Count} row(s) from first {limit} {issueWord}" +
                                       (sourceList.Count > limit
                                            ? $" (of {sourceList.Count} total — full data exported)"
                                            : ".");
                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                _lblPreviewInfo.Text = $"Preview failed: {ex.Message}";
            }
            finally
            {
                _btnLoadPreview.Enabled = true;
            }
        }

        // ── Export button ─────────────────────────────────────────────────────
        private async void BtnExport_Click(object sender, EventArgs e)
        {
            var sourceList = _radResults.Checked ? _searchIssues : _masterIssues;

            if (sourceList.Count == 0)
            { SetStatus("No issues in selected source.", true); return; }

            if (!_chkWorklogs.Checked && !_chkComments.Checked)
            { SetStatus("Select at least one of: Worklogs, Comments.", true); return; }

            if (_chkSummarize.Checked)
            {
                StoreCurrentKey();
                var provider = GetSelectedProvider();
                if (provider == AiProvider.Claude && string.IsNullOrWhiteSpace(_claudeKey))
                { SetStatus("Enter an Anthropic API key to use Claude summarization.", true); return; }
                if (provider == AiProvider.OpenAI && string.IsNullOrWhiteSpace(_openAiKey))
                { SetStatus("Enter an OpenAI API key to use ChatGPT summarization.", true); return; }
                if (provider == AiProvider.Azure  && string.IsNullOrWhiteSpace(_azureKey))
                { SetStatus("Enter an Azure OpenAI API key to use Copilot summarization.", true); return; }
                if (provider == AiProvider.Azure  && string.IsNullOrWhiteSpace(_azureEndpoint))
                { SetStatus("Enter the Azure OpenAI endpoint URL.", true); return; }
            }

            string savePath;
            using (var sfd = new SaveFileDialog
            {
                Title        = "Save Excel Export",
                Filter       = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName     = $"jira_export_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                DefaultExt   = "xlsx",
                AddExtension = true
            })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK) return;
                savePath = sfd.FileName;
            }

            _btnExport.Enabled = false;
            SetStatus("Fetching data\u2026", false);

            try
            {
                var result  = await FetchAndBuildRows(sourceList);
                var headers = result.headers;
                var rows    = result.rows;

                if (_chkSummarize.Checked)
                {
                    SetStatus($"Generating AI summary ({_cmbProvider.SelectedItem})\u2026", false);

                    string summaryText = await SummarizeWithAI(headers, rows, sourceList.Count);

                    SetStatus("Writing file\u2026", false);
                    XlsxWriter.Write(savePath, new List<XlsxWriter.SheetSpec>
                    {
                        new XlsxWriter.SheetSpec { Name = "Data",    Headers = headers, Rows = rows },
                        new XlsxWriter.SheetSpec { Name = "Summary", Headers = new List<string>(),
                                                   Rows = BuildSummarySheetRows(sourceList.Count, summaryText) }
                    });
                }
                else
                {
                    SetStatus("Writing file\u2026", false);
                    XlsxWriter.Write(savePath, "Jira Export", headers, rows);
                }

                SetStatus($"Done \u2014 {rows.Count} row(s) exported.", false);
                _lblStatus.ForeColor = Color.FromArgb(88, 196, 88);

                MessageBox.Show(
                    $"Export complete.\n\n{rows.Count} row(s) written to:\n{savePath}" +
                    (_chkSummarize.Checked ? "\n\nAI summary is on the 'Summary' sheet." : ""),
                    "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SetStatus($"Export failed: {ex.Message}", true);
                MessageBox.Show($"Export failed:\n{ex.Message}", "Export Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _btnExport.Enabled = true;
            }
        }

        // ── Status helper ─────────────────────────────────────────────────────
        private void SetStatus(string msg, bool isError)
        {
            _lblStatus.Text      = msg;
            _lblStatus.ForeColor = isError
                ? Color.FromArgb(222, 80, 80)
                : Color.FromArgb(130, 130, 140);
        }

        private void _btnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveAiSettings();
        }
    }
}
