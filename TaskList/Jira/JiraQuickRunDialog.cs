using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Test
{
    internal sealed partial class JiraQuickRunDialog : DarkForm
    {
        // ── File paths ────────────────────────────────────────────────────────
        private static readonly string ConfigFile    = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jira_config.json");
        private static readonly string QuickFile     = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jira_quickrun.json");
        private static readonly string PresetsFile   = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jira_presets.json");
        private static readonly string TemplatesFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ai_prompt_templates.json");

        // ── In-memory data ────────────────────────────────────────────────────
        private readonly List<JiraPreset>      _presets   = new List<JiraPreset>();
        private readonly List<SummaryTemplate> _templates = new List<SummaryTemplate>();

        // AI keys (read from jira_config.json — shared with JiraExportDialog)
        private string _forgeKey   = "";
        private string _claudeKey  = "";
        private string _openAiKey  = "";
        private int    _aiProvider = 3; // 0=Claude, 1=OpenAI, 2=Azure, 3=Forge

        // Per-run stats for the AI prompt
        private int    _statWorklogCount;
        private int    _statCommentCount;
        private double _statTotalHours;

        private const string DefaultTemplate =
            "You are reviewing Jira work logs and comments for a team status summary.\n\n" +
            "{DATA}\n\n" +
            "Please provide a concise professional summary covering:\n" +
            "1. Key work accomplished\n" +
            "2. Any blockers or issues raised\n" +
            "3. Notable time investments\n\n" +
            "Keep it suitable for a status report.";

        /// <summary>
        /// When set, status updates are forwarded here in addition to (or instead of)
        /// the dialog's own status label. Used when running headlessly from JiraForm.
        /// </summary>
        internal Action<string, bool> ExternalStatusCallback { get; set; }

        /// <summary>
        /// Returns true if jira_quickrun.json exists and has the minimum fields to run
        /// (a preset name and a save folder).
        /// </summary>
        internal static bool IsConfigured()
        {
            try
            {
                if (!File.Exists(QuickFile)) return false;
                var obj = JObject.Parse(File.ReadAllText(QuickFile));
                return !string.IsNullOrWhiteSpace(obj["preset_name"]?.ToString())
                    && !string.IsNullOrWhiteSpace(obj["save_folder"]?.ToString());
            }
            catch { return false; }
        }

        // ── Constructor ───────────────────────────────────────────────────────
        public JiraQuickRunDialog()
        {
            _resizable = false;
            InitializeComponent();
            RegisterTitleBar(pnlTitleBar, showMin: false, showMax: false);

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            LoadPresets();
            LoadTemplates();
            LoadAiKeys();
            LoadQuickConfig();
        }

        // ── Load / Save ───────────────────────────────────────────────────────

        private void LoadPresets()
        {
            _presets.Clear();
            if (File.Exists(PresetsFile))
            {
                try
                {
                    var arr = JArray.Parse(File.ReadAllText(PresetsFile));
                    foreach (var item in arr)
                        _presets.Add(new JiraPreset
                        {
                            Name = item["name"]?.ToString() ?? "",
                            Jql  = item["jql"]?.ToString()  ?? ""
                        });
                }
                catch { }
            }

            _cmbPreset.Items.Clear();
            foreach (var p in _presets) _cmbPreset.Items.Add(p.Name);
            if (_cmbPreset.Items.Count > 0) _cmbPreset.SelectedIndex = 0;
        }

        private void LoadTemplates()
        {
            _templates.Clear();
            if (File.Exists(TemplatesFile))
            {
                try
                {
                    var arr = JArray.Parse(File.ReadAllText(TemplatesFile));
                    foreach (var item in arr)
                        _templates.Add(new SummaryTemplate
                        {
                            Name     = item["name"]?.ToString()     ?? "",
                            Template = item["template"]?.ToString() ?? ""
                        });
                }
                catch { }
            }

            if (_templates.Count == 0)
                _templates.Add(new SummaryTemplate { Name = "Default", Template = DefaultTemplate });

            _cmbTemplate.Items.Clear();
            foreach (var t in _templates) _cmbTemplate.Items.Add(t.Name);
            if (_cmbTemplate.Items.Count > 0) _cmbTemplate.SelectedIndex = 0;
        }

        private void LoadAiKeys()
        {
            if (!File.Exists(ConfigFile)) return;
            try
            {
                var obj     = JObject.Parse(File.ReadAllText(ConfigFile));
                _claudeKey  = obj["anthropic_key"]?.ToString() ?? "";
                _openAiKey  = obj["openai_key"]?.ToString()    ?? "";
                _forgeKey   = obj["forge_key"]?.ToString()     ?? "";
                _aiProvider = obj["ai_provider"]?.ToObject<int>() ?? 3;
            }
            catch { }
        }

        private void LoadQuickConfig()
        {
            if (!File.Exists(QuickFile)) return;
            try
            {
                var obj = JObject.Parse(File.ReadAllText(QuickFile));

                string presetName = obj["preset_name"]?.ToString() ?? "";
                int pi = _presets.FindIndex(p => p.Name == presetName);
                if (pi >= 0) _cmbPreset.SelectedIndex = pi;

                string tmplName = obj["template_name"]?.ToString() ?? "";
                int ti = _templates.FindIndex(t => t.Name == tmplName);
                if (ti >= 0) _cmbTemplate.SelectedIndex = ti;

                _txtFolder.Text  = obj["save_folder"]?.ToString()       ?? "";
                _txtPattern.Text = obj["file_name_pattern"]?.ToString() ?? "jira_export_{date}.xlsx";

                _chkWorklogs.Checked  = obj["include_worklogs"]?.ToObject<bool>()  ?? true;
                _chkComments.Checked  = obj["include_comments"]?.ToObject<bool>()  ?? true;
                _chkAiSummary.Checked = obj["include_ai_summary"]?.ToObject<bool>() ?? true;

                _chkKey.Checked        = obj["col_key"]?.ToObject<bool>()         ?? true;
                _chkSummary.Checked    = obj["col_summary"]?.ToObject<bool>()     ?? true;
                _chkType.Checked       = obj["col_type"]?.ToObject<bool>()        ?? true;
                _chkStatus.Checked     = obj["col_status"]?.ToObject<bool>()      ?? true;
                _chkPriority.Checked   = obj["col_priority"]?.ToObject<bool>()    ?? true;
                _chkProject.Checked    = obj["col_project"]?.ToObject<bool>()     ?? true;
                _chkAssignee.Checked   = obj["col_assignee"]?.ToObject<bool>()    ?? true;
                _chkDueDate.Checked    = obj["col_due_date"]?.ToObject<bool>()    ?? true;
                _chkRecordType.Checked = obj["col_record_type"]?.ToObject<bool>() ?? true;
                _chkDate.Checked       = obj["col_date"]?.ToObject<bool>()        ?? true;
                _chkAuthor.Checked     = obj["col_author"]?.ToObject<bool>()      ?? true;
                _chkHours.Checked      = obj["col_hours"]?.ToObject<bool>()       ?? true;
                _chkText.Checked       = obj["col_text"]?.ToObject<bool>()        ?? true;
            }
            catch { }
        }

        private void SaveQuickConfig()
        {
            try
            {
                var obj = new JObject
                {
                    ["preset_name"]        = _cmbPreset.SelectedItem?.ToString()   ?? "",
                    ["template_name"]      = _cmbTemplate.SelectedItem?.ToString() ?? "",
                    ["save_folder"]        = _txtFolder.Text.Trim(),
                    ["file_name_pattern"]  = _txtPattern.Text.Trim(),
                    ["include_worklogs"]   = _chkWorklogs.Checked,
                    ["include_comments"]   = _chkComments.Checked,
                    ["include_ai_summary"] = _chkAiSummary.Checked,
                    ["col_key"]            = _chkKey.Checked,
                    ["col_summary"]        = _chkSummary.Checked,
                    ["col_type"]           = _chkType.Checked,
                    ["col_status"]         = _chkStatus.Checked,
                    ["col_priority"]       = _chkPriority.Checked,
                    ["col_project"]        = _chkProject.Checked,
                    ["col_assignee"]       = _chkAssignee.Checked,
                    ["col_due_date"]       = _chkDueDate.Checked,
                    ["col_record_type"]    = _chkRecordType.Checked,
                    ["col_date"]           = _chkDate.Checked,
                    ["col_author"]         = _chkAuthor.Checked,
                    ["col_hours"]          = _chkHours.Checked,
                    ["col_text"]           = _chkText.Checked,
                };
                File.WriteAllText(QuickFile, obj.ToString());
            }
            catch { }
        }

        // ── Button handlers ───────────────────────────────────────────────────

        private void BtnReloadPresets_Click(object sender, EventArgs e)
        {
            string cur = _cmbPreset.SelectedItem?.ToString();
            LoadPresets();
            int idx = cur != null ? _presets.FindIndex(p => p.Name == cur) : -1;
            if (idx >= 0) _cmbPreset.SelectedIndex = idx;
            SetStatus("Presets reloaded.", false);
        }

        private void BtnEditTemplates_Click(object sender, EventArgs e)
        {
            string cur = _cmbTemplate.SelectedItem?.ToString();
            using (var dlg = new AiTemplateDialog(_templates))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        var arr = new JArray(_templates.Select(t => new JObject
                        {
                            ["name"]     = t.Name,
                            ["template"] = t.Template
                        }));
                        File.WriteAllText(TemplatesFile, arr.ToString());
                    }
                    catch { }
                }
            }
            _cmbTemplate.Items.Clear();
            foreach (var t in _templates) _cmbTemplate.Items.Add(t.Name);
            int idx = cur != null ? _templates.FindIndex(t => t.Name == cur) : -1;
            _cmbTemplate.SelectedIndex = idx >= 0 ? idx : (_templates.Count > 0 ? 0 : -1);
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select folder to save exports";
                if (!string.IsNullOrEmpty(_txtFolder.Text) && Directory.Exists(_txtFolder.Text))
                    dlg.SelectedPath = _txtFolder.Text;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    _txtFolder.Text = dlg.SelectedPath;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveQuickConfig();
            SetStatus("Settings saved.", false);
            _lblStatus.ForeColor = Color.FromArgb(88, 196, 88);
        }

        private async void BtnRun_Click(object sender, EventArgs e)
        {
            SaveQuickConfig();

            if (_cmbPreset.SelectedIndex < 0 || _cmbPreset.SelectedIndex >= _presets.Count)
            { SetStatus("Select a JQL preset first.", true); return; }

            if (string.IsNullOrWhiteSpace(_txtFolder.Text))
            { SetStatus("Choose a save folder first.", true); return; }

            if (!Directory.Exists(_txtFolder.Text.Trim()))
            { SetStatus("Save folder does not exist.", true); return; }

            if (!_chkWorklogs.Checked && !_chkComments.Checked)
            { SetStatus("Enable at least one of: Worklogs, Comments.", true); return; }

            if (_chkAiSummary.Checked && string.IsNullOrWhiteSpace(GetAiKey()))
            { SetStatus("No AI API key found. Open the Export Dialog to configure a key.", true); return; }

            _btnRun.Enabled = false;
            _btnSave.Enabled = false;
            try { await RunAsync(); }
            finally
            {
                _btnRun.Enabled  = true;
                _btnSave.Enabled = true;
            }
        }

        // ── Run pipeline ──────────────────────────────────────────────────────

        internal async Task RunAsync()
        {
            // 1. Load Jira connection
            string jiraUrl, email, token;
            try
            {
                if (!File.Exists(ConfigFile)) throw new Exception("jira_config.json not found.");
                var obj = JObject.Parse(File.ReadAllText(ConfigFile));
                jiraUrl = (obj["url"]?.ToString() ?? "").TrimEnd('/');
                email   = obj["username"]?.ToString() ?? "";
                token   = DpapiCrypto.UnprotectFromBase64(obj["password"]?.ToString() ?? "",
                              DataProtectionScope.CurrentUser) ?? "";
            }
            catch (Exception ex)
            { SetStatus($"Cannot read Jira config: {ex.Message}", true); return; }

            if (string.IsNullOrEmpty(jiraUrl) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            { SetStatus("Jira connection not configured — set it in the Jira Query window.", true); return; }

            // 2. Authenticate
            string cred = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{email}:{token}"));
            JiraService.Http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", cred);
            JiraService.Http.DefaultRequestHeaders.Accept.Clear();
            JiraService.Http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // 3. Build JQL
            var preset = _presets[_cmbPreset.SelectedIndex];
            string jql = preset.Jql;
            SetStatus($"Searching: {jql.Substring(0, Math.Min(70, jql.Length))}\u2026", false);

            // 4. Search Jira
            List<JiraIssue> issues;
            try
            {
                var payload = new JObject
                {
                    ["jql"]        = jql,
                    ["maxResults"] = 100,
                    ["fields"]     = new JArray("summary", "status", "priority", "issuetype",
                                                "assignee", "project", "duedate")
                };
                var resp = await JiraService.Http.PostAsync(
                    jiraUrl + "/rest/api/2/search",
                    new StringContent(payload.ToString(), Encoding.UTF8, "application/json"));
                var body = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                { SetStatus($"Search failed: {(int)resp.StatusCode} — {ParseError(body)}", true); return; }

                var data = JObject.Parse(body);
                var arr  = data["issues"] as JArray;
                if (arr == null) { SetStatus("No 'issues' array in response.", true); return; }
                issues = arr.Select(i => ParseIssue(i)).ToList();
            }
            catch (Exception ex)
            { SetStatus($"Search error: {ex.Message}", true); return; }

            SetStatus($"Found {issues.Count} issue(s). Fetching details\u2026", false);

            // 5. Fetch worklogs/comments and build rows
            List<string>       headers;
            List<List<string>> rows;
            try
            {
                var result = await FetchAndBuildRows(jiraUrl, issues);
                headers = result.Item1;
                rows    = result.Item2;
            }
            catch (Exception ex)
            { SetStatus($"Fetch error: {ex.Message}", true); return; }

            // 6. Optionally generate AI summary
            string summaryText = null;
            if (_chkAiSummary.Checked)
            {
                SetStatus("Generating AI summary\u2026", false);
                try { summaryText = await SummarizeAsync(headers, rows, issues.Count); }
                catch (Exception ex) { SetStatus($"AI error: {ex.Message}", true); return; }
            }

            // 7. Build file path
            string pattern  = string.IsNullOrWhiteSpace(_txtPattern.Text)
                              ? "jira_export_{date}.xlsx"
                              : _txtPattern.Text.Trim();
            string fileName = pattern.Replace("{date}", DateTime.Now.ToString("yyyyMMdd_HHmm"));
            if (!fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                fileName += ".xlsx";
            string savePath = Path.Combine(_txtFolder.Text.Trim(), fileName);

            // 8. Write xlsx
            SetStatus("Writing file\u2026", false);
            try
            {
                if (summaryText != null)
                {
                    var summaryRows = summaryText.Replace("\r\n", "\n").Split('\n')
                                                 .Select(l => new List<string> { l.TrimEnd() })
                                                 .ToList();
                    XlsxWriter.Write(savePath, new List<XlsxWriter.SheetSpec>
                    {
                        new XlsxWriter.SheetSpec { Name = "Data",    Headers = headers, Rows = rows },
                        new XlsxWriter.SheetSpec { Name = "Summary", Headers = new List<string>(),
                                                   Rows = summaryRows }
                    });
                }
                else
                {
                    XlsxWriter.Write(savePath, "Jira Export", headers, rows);
                }
            }
            catch (Exception ex)
            { SetStatus($"Write failed: {ex.Message}", true); return; }

            SetStatus($"Done \u2014 {rows.Count} row(s) written to {fileName}", false);
            _lblStatus.ForeColor = Color.FromArgb(88, 196, 88);

            MessageBox.Show(
                $"Export complete.\n\n" +
                $"Issues: {issues.Count}    Rows: {rows.Count}\n\n" +
                $"Saved to:\n{savePath}" +
                (summaryText != null ? "\n\nAI summary is on the 'Summary' sheet." : ""),
                "Quick Run Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ── Data fetching ─────────────────────────────────────────────────────

        private async Task<Tuple<List<string>, List<List<string>>>> FetchAndBuildRows(
            string baseUrl, List<JiraIssue> issues)
        {
            var headers = new List<string>();
            if (_chkKey.Checked)        headers.Add("Key");
            if (_chkSummary.Checked)    headers.Add("Summary");
            if (_chkType.Checked)       headers.Add("Type");
            if (_chkStatus.Checked)     headers.Add("Status");
            if (_chkPriority.Checked)   headers.Add("Priority");
            if (_chkProject.Checked)    headers.Add("Project");
            if (_chkAssignee.Checked)   headers.Add("Assignee");
            if (_chkDueDate.Checked)    headers.Add("Due Date");
            if (_chkRecordType.Checked) headers.Add("Record Type");
            if (_chkDate.Checked)       headers.Add("Date");
            if (_chkAuthor.Checked)     headers.Add("Author");
            if (_chkHours.Checked)      headers.Add("Hours");
            if (_chkText.Checked)       headers.Add("Text");

            var rows = new List<List<string>>();
            _statWorklogCount = 0;
            _statCommentCount = 0;
            _statTotalHours   = 0;

            for (int i = 0; i < issues.Count; i++)
            {
                var issue = issues[i];
                SetStatus($"Fetching {i + 1}/{issues.Count}: {issue.Key}\u2026", false);
                Application.DoEvents();

                if (_chkWorklogs.Checked)
                {
                    try
                    {
                        var resp = await JiraService.Http.GetAsync(
                            baseUrl + $"/rest/api/2/issue/{issue.Key}/worklog");
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

                if (_chkComments.Checked)
                {
                    try
                    {
                        var resp = await JiraService.Http.GetAsync(
                            baseUrl + $"/rest/api/2/issue/{issue.Key}/comment");
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

            return Tuple.Create(headers, rows);
        }

        private List<string> BuildRow(JiraIssue issue, string recordType,
                                      string date, string author, string hours, string text)
        {
            var row = new List<string>();
            if (_chkKey.Checked)        row.Add(issue.Key      ?? "");
            if (_chkSummary.Checked)    row.Add(issue.Summary  ?? "");
            if (_chkType.Checked)       row.Add(issue.Type     ?? "");
            if (_chkStatus.Checked)     row.Add(issue.Status   ?? "");
            if (_chkPriority.Checked)   row.Add(issue.Priority ?? "");
            if (_chkProject.Checked)    row.Add(issue.Project  ?? "");
            if (_chkAssignee.Checked)   row.Add(issue.Assignee ?? "");
            if (_chkDueDate.Checked)    row.Add(issue.DueDate  ?? "");
            if (_chkRecordType.Checked) row.Add(recordType     ?? "");
            if (_chkDate.Checked)       row.Add(date           ?? "");
            if (_chkAuthor.Checked)     row.Add(author         ?? "");
            if (_chkHours.Checked)      row.Add(hours          ?? "");
            if (_chkText.Checked)       row.Add(text           ?? "");
            return row;
        }

        // ── AI summary ────────────────────────────────────────────────────────

        private string GetAiKey()
        {
            switch (_aiProvider)
            {
                case 1:  return _openAiKey;
                case 0:  return _forgeKey;
                default: return _claudeKey;
            }
        }

        private async Task<string> SummarizeAsync(List<string> headers,
                                                   List<List<string>> rows,
                                                   int issueCount)
        {
            string prompt = BuildPrompt(headers, rows, issueCount);
            switch (_aiProvider)
            {
                case 1:  return await CallOpenAI(prompt);
                case 0:  return await CallForge(prompt);
                default: return await CallClaude(prompt);
            }
        }

        private string BuildPrompt(List<string> headers, List<List<string>> rows, int issueCount)
        {
            int iKey     = headers.IndexOf("Key");
            int iProject = headers.IndexOf("Project");
            int iDate    = headers.IndexOf("Date");
            int iAuthor  = headers.IndexOf("Author");
            int iRec     = headers.IndexOf("Record Type");
            int iHours   = headers.IndexOf("Hours");
            int iText    = headers.IndexOf("Text");

            var body       = new StringBuilder();
            int charBudget = 80_000;

            foreach (var row in rows)
            {
                string rec     = iRec     >= 0 && iRec     < row.Count ? row[iRec]     : "";
                string project = iProject >= 0 && iProject < row.Count ? row[iProject] : "";
                string key     = iKey     >= 0 && iKey     < row.Count ? row[iKey]     : "";
                string date    = iDate    >= 0 && iDate    < row.Count ? row[iDate]    : "";
                string author  = iAuthor  >= 0 && iAuthor  < row.Count ? row[iAuthor]  : "";
                string hours   = iHours   >= 0 && iHours   < row.Count ? row[iHours]   : "";
                string text    = iText    >= 0 && iText    < row.Count ? row[iText]    : "";

                if (string.IsNullOrWhiteSpace(text)) continue;

                string line = rec == "Worklog"
                    ? $"[Worklog] Key: {key} | Project: {project} | Date: {date} | Author: {author} | Hours: {hours}h | Summary: {text}\n"
                    : $"[Comment] Key: {key} | Project: {project} | Date: {date} | Author: {author} | Summary: {text}\n";

                if (body.Length + line.Length > charBudget)
                {
                    body.Append("... (additional entries truncated)\n");
                    break;
                }
                body.Append(line);
            }

            string data =
                $"Statistics:\n" +
                $"  Issues processed: {issueCount}\n" +
                $"  Worklogs: {_statWorklogCount} ({_statTotalHours:N2} hours total)\n" +
                $"  Comments: {_statCommentCount}\n\n" +
                $"Entries:\n{body}";

            string templateText = _cmbTemplate.SelectedIndex >= 0
                ? _templates[_cmbTemplate.SelectedIndex].Template
                : DefaultTemplate;

            if (string.IsNullOrWhiteSpace(templateText)) templateText = DefaultTemplate;

            return templateText.Contains("{DATA}")
                ? templateText.Replace("{DATA}", data)
                : templateText + "\n\n" + data;
        }

        private async Task<string> CallClaude(string prompt)
        {
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
                        ["role"] = "user", ["content"] = prompt
                    })
                };
                var resp = await http.PostAsync(
                    "https://api.anthropic.com/v1/messages",
                    new StringContent(body.ToString(), Encoding.UTF8, "application/json"));
                var text = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                    throw new Exception(
                        $"Claude error {(int)resp.StatusCode}: " +
                        text.Substring(0, Math.Min(200, text.Length)));
                return JObject.Parse(text)["content"]?[0]?["text"]?.ToString() ?? "(no response)";
            }
        }

        private async Task<string> CallOpenAI(string prompt)
        {
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
                        ["role"] = "user", ["content"] = prompt
                    })
                };
                var resp = await http.PostAsync(
                    "https://api.openai.com/v1/chat/completions",
                    new StringContent(body.ToString(), Encoding.UTF8, "application/json"));
                var text = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                    throw new Exception(
                        $"OpenAI error {(int)resp.StatusCode}: " +
                        text.Substring(0, Math.Min(200, text.Length)));
                return JObject.Parse(text)["choices"]?[0]?["message"]?["content"]?.ToString()
                       ?? "(no response)";
            }
        }

        private async Task<string> CallForge(string prompt)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _forgeKey);
                http.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var body = new JObject
                {
                    ["model"]      = "gpt-oss-20b-Q6_K.gguf",
                    ["max_tokens"] = 2048,
                    ["messages"]   = new JArray(new JObject
                    {
                        ["role"] = "user", ["content"] = prompt
                    })
                };
                var resp = await http.PostAsync(
                    "https://forge-dev.vdl.cluster.caemilusa.us/api/chat/completions",
                    new StringContent(body.ToString(), Encoding.UTF8, "application/json"));
                var text = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                    throw new Exception(
                        $"Forge error {(int)resp.StatusCode}: " +
                        text.Substring(0, Math.Min(200, text.Length)));
                return JObject.Parse(text)["choices"]?[0]?["message"]?["content"]?.ToString()
                       ?? "(no response)";
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static JiraIssue ParseIssue(JToken issue)
        {
            var f = issue["fields"];
            return new JiraIssue
            {
                Key      = issue["key"]?.ToString()                                     ?? "",
                Summary  = f["summary"]?.ToString()                                     ?? "",
                Type     = (f["issuetype"] as JObject)?["name"]?.ToString()             ?? "",
                Status   = (f["status"]    as JObject)?["name"]?.ToString()             ?? "",
                Priority = (f["priority"]  as JObject)?["name"]?.ToString()             ?? "",
                Assignee = (f["assignee"]  as JObject)?["displayName"]?.ToString()      ?? "Unassigned",
                Project  = (f["project"]   as JObject)?["name"]?.ToString()             ?? "",
                DueDate  = FormatDate(f["duedate"]?.ToString()),
            };
        }

        private static string AdfToText(JToken token)
        {
            if (token == null || token.Type == JTokenType.Null) return "";
            if (token.Type == JTokenType.String)               return token.ToString();
            if (token.Type != JTokenType.Object)               return "";
            var content = ((JObject)token)["content"] as JArray;
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
                if (type == "text") sb.Append(obj["text"]?.ToString() ?? "");
                else { var ch = obj["content"] as JArray; if (ch != null) sb.Append(ExtractAdf(ch)); }
                if (type == "paragraph" || type == "heading" ||
                    type == "listItem"  || type == "codeBlock")
                    sb.Append("\n");
            }
            return sb.ToString();
        }

        private static string FormatDate(string iso)
        {
            if (string.IsNullOrEmpty(iso)) return "";
            return DateTime.TryParse(iso, out var d) ? d.ToString("yyyy-MM-dd") : iso;
        }

        private static string ParseError(string json)
        {
            try
            {
                var obj = JObject.Parse(json);
                return obj["errorMessages"]?.First?.ToString()
                    ?? obj["message"]?.ToString()
                    ?? json.Substring(0, Math.Min(120, json.Length));
            }
            catch { return json.Substring(0, Math.Min(120, json.Length)); }
        }

        private void SetStatus(string msg, bool isError)
        {
            _lblStatus.Text      = msg;
            _lblStatus.ForeColor = isError
                ? Color.FromArgb(222, 80, 80)
                : Color.FromArgb(130, 130, 140);
            ExternalStatusCallback?.Invoke(msg, isError);
        }
    }
}
