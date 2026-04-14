using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Test
{
    internal sealed partial class JiraExportDialog : DarkForm
    {
        // ── Services ──────────────────────────────────────────────────────────
        private readonly JiraService      _jiraService  =
            new JiraService(AppDomain.CurrentDomain.BaseDirectory);
        private readonly ExportService    _exportService =
            new ExportService(AppDomain.CurrentDomain.BaseDirectory);
        private readonly AiSummaryService _aiService    = new AiSummaryService();

        // ── Fields ────────────────────────────────────────────────────────────
        private readonly List<JiraIssue> _searchIssues;
        private readonly List<JiraIssue> _masterIssues;
        private readonly string          _baseUrl;

        // Stats tracked during FetchAndBuildRows
        private int    _statWorklogCount;
        private int    _statCommentCount;
        private double _statTotalHours;

        // Per-provider key memory (swapped as user changes provider)
        private string _claudeKey     = "";
        private string _openAiKey     = "";
        private string _azureKey      = "";
        private string _azureEndpoint = "";
        private string _forgeAiKey    = "";

        // Prompt templates
        private readonly List<SummaryTemplate> _templates         = new List<SummaryTemplate>();
        private string                          _savedTemplateName = "";

        // Email recipients
        private readonly List<EmailRecipient> _recipients = new List<EmailRecipient>();

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
            _resizable    = true;

            InitializeComponent();
            _cmbProvider.SelectedIndex = 0;
            RegisterTitleBar(pnlHeader, showMin: true, showMax: false);

            _radResults.Text = $"Search Results  ({_searchIssues.Count})";
            _radMaster.Text  = $"Master List  ({_masterIssues.Count})";

            LoadAiSettings();
            LoadTemplates();
            LoadRecipients();
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
        private AiSummaryService.AiProvider GetSelectedProvider() => AiSummaryService.AiProvider.Forge;
            //_cmbProvider.SelectedIndex == 1 ? AiSummaryService.AiProvider.OpenAI :
            //_cmbProvider.SelectedIndex == 2 ? AiSummaryService.AiProvider.Azure  :
            //_cmbProvider.SelectedIndex == 3 ? AiSummaryService.AiProvider.Forge  :
            //                                  AiSummaryService.AiProvider.Claude;

        internal void CmbProvider_Changed(object sender, EventArgs e)
        {
            // Save current key back to in-memory slot before switching
            StoreCurrentKey();

            var provider = GetSelectedProvider();

            switch (provider)
            {
                case AiSummaryService.AiProvider.OpenAI:
                    _lblAiKey.Text = "OPENAI API KEY";
                    _txtAiKey.Text = _openAiKey;
                    break;
                case AiSummaryService.AiProvider.Azure:
                    _lblAiKey.Text         = "AZURE OPENAI API KEY";
                    _txtAiKey.Text         = _azureKey;
                    _txtAzureEndpoint.Text = _azureEndpoint;
                    break;
                case AiSummaryService.AiProvider.Forge:
                    _lblAiKey.Text = "FORGE API KEY";
                    _txtAiKey.Text = _forgeAiKey;
                    break;
                default:
                    _lblAiKey.Text = "ANTHROPIC API KEY";
                    _txtAiKey.Text = _claudeKey;
                    break;
            }

            bool isAzure = provider == AiSummaryService.AiProvider.Azure;
            _lblAzureEndpoint.Visible = isAzure;
            _txtAzureEndpoint.Visible = isAzure;
        }

        private void StoreCurrentKey()
        {
            switch (GetSelectedProvider())
            {
                case AiSummaryService.AiProvider.OpenAI: _openAiKey     = _txtAiKey.Text.Trim(); break;
                case AiSummaryService.AiProvider.Forge:  _forgeAiKey    = _txtAiKey.Text.Trim(); break;
                case AiSummaryService.AiProvider.Azure:  _azureKey      = _txtAiKey.Text.Trim();
                                                         _azureEndpoint = _txtAzureEndpoint.Text.Trim(); break;
                default:                                 _claudeKey     = _txtAiKey.Text.Trim(); break;
            }
        }

        // ── AI settings persistence ───────────────────────────────────────────
        private void LoadAiSettings()
        {
            var s = _jiraService.LoadAiSettings();
            _claudeKey         = s.ClaudeKey;
            _openAiKey         = s.OpenAiKey;
            _azureKey          = s.AzureKey;
            _azureEndpoint     = s.AzureEndpoint;
            _forgeAiKey        = s.ForgeKey;
            _savedTemplateName = s.SelectedTemplate;

            // Restore last-used provider — suppress event so StoreCurrentKey()
            // doesn't overwrite the keys we just loaded with empty strings.
            if (s.ProviderIndex >= 0 && s.ProviderIndex < _cmbProvider.Items.Count)
            {
                _cmbProvider.SelectedIndexChanged -= CmbProvider_Changed;
                _cmbProvider.SelectedIndex = s.ProviderIndex;
                _cmbProvider.SelectedIndexChanged += CmbProvider_Changed;
            }

            _txtAiKey.Text = GetSelectedProvider() == AiSummaryService.AiProvider.OpenAI ? _openAiKey
                           : GetSelectedProvider() == AiSummaryService.AiProvider.Azure   ? _azureKey
                           : GetSelectedProvider() == AiSummaryService.AiProvider.Forge   ? _forgeAiKey
                           : _claudeKey;
            _txtAzureEndpoint.Text = _azureEndpoint;

            bool isAzure = GetSelectedProvider() == AiSummaryService.AiProvider.Azure;
            _lblAzureEndpoint.Visible = isAzure;
            _txtAzureEndpoint.Visible = isAzure;
        }

        private void SaveAiSettings()
        {
            StoreCurrentKey();
            _jiraService.SaveAiSettings(new AiSettings
            {
                ProviderIndex    = _cmbProvider.SelectedIndex,
                ClaudeKey        = _claudeKey,
                OpenAiKey        = _openAiKey,
                AzureKey         = _azureKey,
                AzureEndpoint    = _azureEndpoint,
                ForgeKey         = _forgeAiKey,
                SelectedTemplate = GetSelectedTemplate()?.Name ?? "",
            });
        }

        // ── Prompt templates ──────────────────────────────────────────────────
        private void LoadTemplates()
        {
            var loaded = _jiraService.LoadTemplates();
            _templates.Clear();
            _templates.AddRange(loaded);
            if (_templates.Count == 0)
                _templates.Add(new SummaryTemplate { Name = "Default", Template = AiSummaryService.DefaultTemplateText });
            PopulateTemplatesCombo(_savedTemplateName);
        }

        internal void SaveTemplates()
        {
            _jiraService.SaveTemplates(_templates);
        }

        private void PopulateTemplatesCombo(string selectName = null)
        {
            _cmbTemplate.Items.Clear();
            foreach (var t in _templates) _cmbTemplate.Items.Add(t.Name);
            if (_templates.Count == 0) return;
            int idx = selectName != null ? _templates.FindIndex(t => t.Name == selectName) : -1;
            _cmbTemplate.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private SummaryTemplate GetSelectedTemplate()
        {
            int idx = _cmbTemplate.SelectedIndex;
            return idx >= 0 && idx < _templates.Count ? _templates[idx] : null;
        }

        private void CmbTemplate_Changed(object sender, EventArgs e)
        {
            SaveAiSettings();
        }

        private void BtnEditTemplates_Click(object sender, EventArgs e)
        {
            string prevName = GetSelectedTemplate()?.Name;
            using (var dlg = new AiTemplateDialog(_templates))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    SaveTemplates();
            }
            int idx = prevName != null ? _templates.FindIndex(t => t.Name == prevName) : -1;
            PopulateTemplatesCombo(idx >= 0 ? prevName : null);
        }

        // ── Email recipients ──────────────────────────────────────────────────
        private void LoadRecipients()
        {
            _recipients.Clear();
            _recipients.AddRange(_exportService.LoadRecipients());
        }

        private void SaveRecipients()
        {
            _exportService.SaveRecipients(_recipients);
        }

        private void BtnEditRecipients_Click(object sender, EventArgs e)
        {
            using (var dlg = new EmailRecipientsDialog(_recipients))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    SaveRecipients();
            }
        }

        private void BtnSendOutlookCustom_Click(object sender, EventArgs e)
        {
            EmailRecipients(false);
        }

        private void BtnSendOutlookDefault_Click(object sender, EventArgs e)
        {
            EmailRecipients(true);
        }

        private async void EmailRecipients(bool iIsDefault)
        {
            var sourceList = _radResults.Checked ? _searchIssues : _masterIssues;

            if (sourceList.Count == 0)
            { SetStatus("No issues in selected source.", true); return; }

            if (!_chkWorklogs.Checked && !_chkComments.Checked)
            { SetStatus("Select at least one of: Worklogs, Comments.", true); return; }

            if (_recipients.Count == 0)
            {
                MessageBox.Show(
                    "No recipients configured. Use \"Edit Recipients\u2026\" to add recipients first.",
                    "No Recipients", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRecipients = PickRecipients(iIsDefault);
            if (selectedRecipients == null || selectedRecipients.Count == 0) return;

            if (_chkSummarize.Checked)
            {
                StoreCurrentKey();
                var provider = GetSelectedProvider();
                if (provider == AiSummaryService.AiProvider.Claude && string.IsNullOrWhiteSpace(_claudeKey))
                { SetStatus("Enter an Anthropic API key to use Claude summarization.", true); return; }
                if (provider == AiSummaryService.AiProvider.OpenAI && string.IsNullOrWhiteSpace(_openAiKey))
                { SetStatus("Enter an OpenAI API key to use ChatGPT summarization.", true); return; }
                if (provider == AiSummaryService.AiProvider.Azure  && string.IsNullOrWhiteSpace(_azureKey))
                { SetStatus("Enter an Azure OpenAI API key to use Copilot summarization.", true); return; }
                if (provider == AiSummaryService.AiProvider.Azure  && string.IsNullOrWhiteSpace(_azureEndpoint))
                { SetStatus("Enter the Azure OpenAI endpoint URL.", true); return; }
            }

            string tempPath = Path.Combine(Path.GetTempPath(),
                $"jira_export_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");

            _btnSendOutlookDefault.Enabled = false;
            _btnExport.Enabled             = false;
            SetStatus("Fetching data\u2026", false);

            try
            {
                var cols    = GetCurrentColumns();
                var result  = await FetchAndBuildRows(sourceList, cols, includeEmpty: _chkIncludeEmpty.Checked);
                var headers = result.headers;
                var rows    = result.rows;

                if (_chkSummarize.Checked)
                {
                    SetStatus($"Generating AI summary ({_cmbProvider.SelectedItem})\u2026", false);
                    string summaryText = await SummarizeWithAI(headers, rows, sourceList.Count);
                    SetStatus("Writing file\u2026", false);
                    XlsxWriter.Write(tempPath, new List<XlsxWriter.SheetSpec>
                    {
                        new XlsxWriter.SheetSpec { Name = "Data",    Headers = headers, Rows = rows },
                        new XlsxWriter.SheetSpec { Name = "Summary", Headers = new List<string>(),
                                                   Rows = BuildSummarySheetRows(sourceList.Count, summaryText) }
                    });
                }
                else
                {
                    SetStatus("Writing file\u2026", false);
                    XlsxWriter.Write(tempPath, "Jira Export", headers, rows);
                }

                SetStatus("Opening Outlook\u2026", false);

                dynamic outlook = null;
                dynamic mail    = null;
                try
                {
                    var outlookType = Type.GetTypeFromProgID("Outlook.Application");
                    if (outlookType == null)
                    {
                        MessageBox.Show("Microsoft Outlook does not appear to be installed.",
                            "Outlook Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    outlook = Activator.CreateInstance(outlookType);
                    mail    = outlook.CreateItem(0);
                    mail.Subject = $"Jira Export \u2014 {DateTime.Now:yyyy-MM-dd}";

                    foreach (var r in selectedRecipients)
                    {
                        dynamic recip = mail.Recipients.Add(r.Email);
                        recip.Type = 1;
                    }

                    mail.Attachments.Add(tempPath, 1, 1, Path.GetFileName(tempPath));
                    mail.Display(false);

                    SetStatus($"Done \u2014 Outlook draft opened with {rows.Count} row(s).", false);
                    _lblStatus.ForeColor = Color.FromArgb(88, 196, 88);
                }
                catch (Exception ex)
                {
                    SetStatus($"Outlook error: {ex.Message}", true);
                    MessageBox.Show($"Failed to create Outlook email:\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (mail    != null) { try { Marshal.ReleaseComObject(mail);    } catch { } }
                    if (outlook != null) { try { Marshal.ReleaseComObject(outlook); } catch { } }
                }
            }
            catch (Exception ex)
            {
                SetStatus($"Export failed: {ex.Message}", true);
                MessageBox.Show($"Export failed:\n{ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _btnSendOutlookDefault.Enabled = true;
                _btnExport.Enabled             = true;
            }
        }

        // ── Recipient picker ──────────────────────────────────────────────────
        private List<EmailRecipient> PickRecipients(bool iIsDefault)
        {
            if (!iIsDefault)
            {
                using (var dlg = new Form())
                {
                    dlg.Text            = "Select Recipients";
                    dlg.BackColor       = Color.FromArgb(37, 37, 38);
                    dlg.ForeColor       = Color.White;
                    dlg.Font            = new System.Drawing.Font("Segoe UI", 9.5f);
                    dlg.ClientSize      = new System.Drawing.Size(420, 310);
                    dlg.StartPosition   = FormStartPosition.CenterParent;
                    dlg.MaximizeBox     = false;
                    dlg.MinimizeBox     = false;
                    dlg.FormBorderStyle = FormBorderStyle.FixedDialog;

                    var lbl = new Label
                    {
                        Text      = "Select recipients for this email:",
                        AutoSize  = true,
                        Location  = new System.Drawing.Point(12, 12),
                        ForeColor = Color.FromArgb(175, 175, 185)
                    };

                    var clb = new CheckedListBox
                    {
                        BackColor    = Color.FromArgb(28, 28, 30),
                        ForeColor    = Color.White,
                        BorderStyle  = BorderStyle.None,
                        Location     = new System.Drawing.Point(12, 36),
                        Size         = new System.Drawing.Size(396, 220),
                        CheckOnClick = true
                    };
                    foreach (var r in _recipients)
                        clb.Items.Add(r, r.IsDefault);

                    var btnSend = new Button
                    {
                        Text         = "Send",
                        DialogResult = DialogResult.OK,
                        BackColor    = Color.FromArgb(0, 100, 180),
                        FlatStyle    = FlatStyle.Flat,
                        ForeColor    = Color.White,
                        Location     = new System.Drawing.Point(258, 270),
                        Size         = new System.Drawing.Size(70, 28)
                    };
                    btnSend.FlatAppearance.BorderSize = 0;

                    var btnCancel = new Button
                    {
                        Text         = "Cancel",
                        DialogResult = DialogResult.Cancel,
                        BackColor    = Color.FromArgb(70, 70, 78),
                        FlatStyle    = FlatStyle.Flat,
                        ForeColor    = Color.White,
                        Location     = new System.Drawing.Point(338, 270),
                        Size         = new System.Drawing.Size(70, 28)
                    };
                    btnCancel.FlatAppearance.BorderSize = 0;

                    dlg.Controls.AddRange(new Control[] { lbl, clb, btnSend, btnCancel });
                    dlg.AcceptButton = btnSend;
                    dlg.CancelButton = btnCancel;

                    if (dlg.ShowDialog(this) != DialogResult.OK) return null;

                    var selected = new List<EmailRecipient>();
                    foreach (var item in clb.CheckedItems)
                        selected.Add((EmailRecipient)item);
                    return selected;
                }
            }
            else
            {
                return _recipients.Where(r => r.IsDefault).ToList();
            }
        }

        // ── AI Summary ────────────────────────────────────────────────────────
        private async Task<string> SummarizeWithAI(List<string> headers,
                                                    List<List<string>> rows,
                                                    int issueCount)
        {
            string templateText = GetSelectedTemplate()?.Template;
            string prompt = _aiService.BuildSummaryPrompt(
                headers, rows, issueCount,
                _statWorklogCount, _statCommentCount, _statTotalHours,
                templateText);

            return await _aiService.SummarizeAsync(
                GetSelectedProvider(),
                _claudeKey, _openAiKey, _azureKey, _azureEndpoint, _forgeAiKey,
                prompt);
        }

        private List<List<string>> BuildSummarySheetRows(int issueCount, string summaryText)
        {
            var rows = new List<List<string>>();
            foreach (var line in summaryText.Replace("\r\n", "\n").Split('\n'))
                rows.Add(new List<string> { line.TrimEnd() });
            return rows;
        }

        // ── Column selection ──────────────────────────────────────────────────
        private JiraExportColumns GetCurrentColumns() => new JiraExportColumns
        {
            Key        = _chkColKey.Checked,
            Summary    = _chkColSummary.Checked,
            Type       = _chkColType.Checked,
            Status     = _chkColStatus.Checked,
            Priority   = _chkColPriority.Checked,
            Project    = _chkColProject.Checked,
            Assignee   = _chkColAssignee.Checked,
            DueDate    = _chkColDueDate.Checked,
            RecordType = _chkColRecordType.Checked,
            Date       = _chkColDate.Checked,
            Author     = _chkColAuthor.Checked,
            Hours      = _chkColHours.Checked,
            Text       = _chkColText.Checked,
        };

        // ── Data fetch ────────────────────────────────────────────────────────
        private async Task<(List<string> headers, List<List<string>> rows)> FetchAndBuildRows(
            List<JiraIssue> issues, JiraExportColumns cols, bool includeEmpty = false)
        {
            var headers = JiraService.BuildExportHeaders(cols);
            var rows    = new List<List<string>>();
            _statWorklogCount = 0;
            _statCommentCount = 0;
            _statTotalHours   = 0;

            for (int i = 0; i < issues.Count; i++)
            {
                var issue = issues[i];
                _lblStatus.Text = $"Fetching {i + 1}/{issues.Count}: {issue.Key}\u2026";
                Application.DoEvents();

                int rowsBefore = rows.Count;

                // ── Worklogs ──────────────────────────────────────────────────
                if (_chkWorklogs.Checked)
                {
                    try
                    {
                        var resp = await JiraService.Http.GetAsync(
                            _baseUrl + $"/rest/api/2/issue/{issue.Key}/worklog");
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
                                    string text   = JiraService.AdfToText(wl["comment"]);

                                    _statWorklogCount++;
                                    _statTotalHours += secs / 3600.0;

                                    rows.Add(JiraService.BuildExportRow(issue, "Worklog", date, author, hours, text, cols));
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
                        var resp = await JiraService.Http.GetAsync(
                            _baseUrl + $"/rest/api/2/issue/{issue.Key}/comment");
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
                                    string text   = JiraService.AdfToText(cm["body"]);

                                    _statCommentCount++;
                                    rows.Add(JiraService.BuildExportRow(issue, "Comment", date, author, "", text, cols));
                                }
                            }
                        }
                    }
                    catch { /* skip failed fetch */ }
                }

                if (includeEmpty && rows.Count == rowsBefore)
                    rows.Add(JiraService.BuildExportRow(issue, "", "", "", "", "", cols));
            }

            return (headers, rows);
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

            _btnLoadPreview.Enabled = false;
            _lblPreviewInfo.Text    = "Loading\u2026";
            _dgvPreview.Columns.Clear();
            _dgvPreview.Rows.Clear();

            try
            {
                var cols    = GetCurrentColumns();
                var result  = await FetchAndBuildRows(sourceList, cols, includeEmpty: true);
                var headers = result.headers;
                var rows    = result.rows;

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

                _lblPreviewInfo.Text = $"{rows.Count} row(s) from {sourceList.Count} issue(s).";
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
                if (provider == AiSummaryService.AiProvider.Claude && string.IsNullOrWhiteSpace(_claudeKey))
                { SetStatus("Enter an Anthropic API key to use Claude summarization.", true); return; }
                if (provider == AiSummaryService.AiProvider.OpenAI && string.IsNullOrWhiteSpace(_openAiKey))
                { SetStatus("Enter an OpenAI API key to use ChatGPT summarization.", true); return; }
                if (provider == AiSummaryService.AiProvider.Azure  && string.IsNullOrWhiteSpace(_azureKey))
                { SetStatus("Enter an Azure OpenAI API key to use Copilot summarization.", true); return; }
                if (provider == AiSummaryService.AiProvider.Azure  && string.IsNullOrWhiteSpace(_azureEndpoint))
                { SetStatus("Enter the Azure OpenAI endpoint URL.", true); return; }
                if (provider == AiSummaryService.AiProvider.Forge  && string.IsNullOrWhiteSpace(_forgeAiKey))
                { SetStatus("Enter a Forge API key to use Forge summarization.", true); return; }
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
                var cols    = GetCurrentColumns();
                var result  = await FetchAndBuildRows(sourceList, cols, includeEmpty: _chkIncludeEmpty.Checked);
                var headers = result.headers;
                var rows    = result.rows;

                if (_chkSummarize.Checked)
                {
                    SetStatus($"Generating AI summary ({_cmbProvider.SelectedItem})\u2026", false);
                    SaveAiSettings();

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
