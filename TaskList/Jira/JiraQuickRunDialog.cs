using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Test
{
    internal sealed partial class JiraQuickRunDialog : DarkForm
    {
        // ── Services ──────────────────────────────────────────────────────────
        private readonly JiraService      _jiraService =
            new JiraService(AppDomain.CurrentDomain.BaseDirectory);
        private readonly AiSummaryService _aiService   = new AiSummaryService();

        // ── In-memory data ────────────────────────────────────────────────────
        private readonly List<JiraPreset>      _presets   = new List<JiraPreset>();
        private readonly List<SummaryTemplate> _templates = new List<SummaryTemplate>();

        // AI keys loaded from config
        private string _forgeKey  = "";
        private string _claudeKey = "";
        private string _openAiKey = "";
        private int    _aiProvider = 3; // matches AiSummaryService.AiProvider enum values

        // Per-run stats for the AI prompt
        private int    _statWorklogCount;
        private int    _statCommentCount;
        private double _statTotalHours;

        /// <summary>
        /// When set, status updates are forwarded here in addition to the dialog's own label.
        /// Used when running headlessly from JiraForm.
        /// </summary>
        internal Action<string, bool> ExternalStatusCallback { get; set; }

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
            _presets.AddRange(_jiraService.LoadPresets());

            _cmbPreset.Items.Clear();
            foreach (var p in _presets) _cmbPreset.Items.Add(p.Name);
            if (_cmbPreset.Items.Count > 0) _cmbPreset.SelectedIndex = 0;
        }

        private void LoadTemplates()
        {
            _templates.Clear();
            _templates.AddRange(_jiraService.LoadTemplates());
            if (_templates.Count == 0)
                _templates.Add(new SummaryTemplate { Name = "Default", Template = AiSummaryService.DefaultTemplateText });

            _cmbTemplate.Items.Clear();
            foreach (var t in _templates) _cmbTemplate.Items.Add(t.Name);
            if (_cmbTemplate.Items.Count > 0) _cmbTemplate.SelectedIndex = 0;
        }

        private void LoadAiKeys()
        {
            var s      = _jiraService.LoadAiSettings();
            _claudeKey = s.ClaudeKey;
            _openAiKey = s.OpenAiKey;
            _forgeKey  = s.ForgeKey;
            _aiProvider = s.ProviderIndex;
        }

        private void LoadQuickConfig()
        {
            var c = _jiraService.LoadQuickRunConfig();

            int pi = _presets.FindIndex(p => p.Name == c.PresetName);
            if (pi >= 0) _cmbPreset.SelectedIndex = pi;

            int ti = _templates.FindIndex(t => t.Name == c.TemplateName);
            if (ti >= 0) _cmbTemplate.SelectedIndex = ti;

            _txtFolder.Text  = c.SaveFolder;
            _txtPattern.Text = c.FileNamePattern;

            _chkWorklogs.Checked  = c.IncludeWorklogs;
            _chkComments.Checked  = c.IncludeComments;
            _chkAiSummary.Checked = c.IncludeAiSummary;

            var cols = c.Columns;
            _chkKey.Checked        = cols.Key;
            _chkSummary.Checked    = cols.Summary;
            _chkType.Checked       = cols.Type;
            _chkStatus.Checked     = cols.Status;
            _chkPriority.Checked   = cols.Priority;
            _chkProject.Checked    = cols.Project;
            _chkAssignee.Checked   = cols.Assignee;
            _chkDueDate.Checked    = cols.DueDate;
            _chkRecordType.Checked = cols.RecordType;
            _chkDate.Checked       = cols.Date;
            _chkAuthor.Checked     = cols.Author;
            _chkHours.Checked      = cols.Hours;
            _chkText.Checked       = cols.Text;
        }

        private void SaveQuickConfig()
        {
            _jiraService.SaveQuickRunConfig(new JiraQuickRunConfig
            {
                PresetName       = _cmbPreset.SelectedItem?.ToString()   ?? "",
                TemplateName     = _cmbTemplate.SelectedItem?.ToString() ?? "",
                SaveFolder       = _txtFolder.Text.Trim(),
                FileNamePattern  = _txtPattern.Text.Trim(),
                IncludeWorklogs  = _chkWorklogs.Checked,
                IncludeComments  = _chkComments.Checked,
                IncludeAiSummary = _chkAiSummary.Checked,
                Columns = new JiraExportColumns
                {
                    Key        = _chkKey.Checked,
                    Summary    = _chkSummary.Checked,
                    Type       = _chkType.Checked,
                    Status     = _chkStatus.Checked,
                    Priority   = _chkPriority.Checked,
                    Project    = _chkProject.Checked,
                    Assignee   = _chkAssignee.Checked,
                    DueDate    = _chkDueDate.Checked,
                    RecordType = _chkRecordType.Checked,
                    Date       = _chkDate.Checked,
                    Author     = _chkAuthor.Checked,
                    Hours      = _chkHours.Checked,
                    Text       = _chkText.Checked,
                }
            });
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
                    _jiraService.SaveTemplates(_templates);
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

            _btnRun.Enabled  = false;
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
            // 1. Load Jira connection config
            var config = _jiraService.LoadConfig();
            if (string.IsNullOrEmpty(config.Url) || string.IsNullOrEmpty(config.Email) || string.IsNullOrEmpty(config.Token))
            { SetStatus("Jira connection not configured — set it in the Jira Query window.", true); return; }

            // 2. Authenticate
            if (!_jiraService.SetupAuth(config.Url, config.Email, config.Token))
            { SetStatus("Jira connection not configured — set it in the Jira Query window.", true); return; }

            // 3. Build JQL
            var preset = _presets[_cmbPreset.SelectedIndex];
            string jql = preset.Jql;
            SetStatus($"Searching: {jql.Substring(0, Math.Min(70, jql.Length))}\u2026", false);

            // 4. Search Jira
            var searchResult = await _jiraService.SearchAsync(config.Url, jql);
            if (!searchResult.Success)
            { SetStatus(searchResult.Error, true); return; }

            var issues = searchResult.Issues;
            SetStatus($"Found {issues.Count} issue(s). Fetching details\u2026", false);

            // 5. Fetch worklogs/comments and build rows
            var cols = GetCurrentColumns();
            List<string>       headers;
            List<List<string>> rows;
            try
            {
                var result = await FetchAndBuildRows(config.Url, issues, cols);
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
                try
                {
                    string templateText = _cmbTemplate.SelectedIndex >= 0
                        ? _templates[_cmbTemplate.SelectedIndex].Template
                        : AiSummaryService.DefaultTemplateText;
                    string prompt = _aiService.BuildSummaryPrompt(
                        headers, rows, issues.Count,
                        _statWorklogCount, _statCommentCount, _statTotalHours,
                        templateText);
                    summaryText = await _aiService.SummarizeAsync(
                        (AiSummaryService.AiProvider)_aiProvider,
                        _claudeKey, _openAiKey, "", "", _forgeKey,
                        prompt);
                }
                catch (Exception ex)
                { SetStatus($"AI error: {ex.Message}", true); return; }
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

        // ── Column selection ──────────────────────────────────────────────────
        private JiraExportColumns GetCurrentColumns() => new JiraExportColumns
        {
            Key        = _chkKey.Checked,
            Summary    = _chkSummary.Checked,
            Type       = _chkType.Checked,
            Status     = _chkStatus.Checked,
            Priority   = _chkPriority.Checked,
            Project    = _chkProject.Checked,
            Assignee   = _chkAssignee.Checked,
            DueDate    = _chkDueDate.Checked,
            RecordType = _chkRecordType.Checked,
            Date       = _chkDate.Checked,
            Author     = _chkAuthor.Checked,
            Hours      = _chkHours.Checked,
            Text       = _chkText.Checked,
        };

        // ── Data fetching ─────────────────────────────────────────────────────
        private async Task<Tuple<List<string>, List<List<string>>>> FetchAndBuildRows(
            string baseUrl, List<JiraIssue> issues, JiraExportColumns cols)
        {
            var headers = JiraService.BuildExportHeaders(cols);
            var rows    = new List<List<string>>();
            _statWorklogCount = 0;
            _statCommentCount = 0;
            _statTotalHours   = 0;

            for (int i = 0; i < issues.Count; i++)
            {
                var issue = issues[i];
                SetStatus($"Fetching {i + 1}/{issues.Count}: {issue.Key}\u2026", false);
                System.Windows.Forms.Application.DoEvents();

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
                                    string text   = JiraService.AdfToText(cm["body"]);
                                    _statCommentCount++;
                                    rows.Add(JiraService.BuildExportRow(issue, "Comment", date, author, "", text, cols));
                                }
                            }
                        }
                    }
                    catch { /* skip failed fetch */ }
                }
            }

            return Tuple.Create(headers, rows);
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private string GetAiKey()
        {
            switch (_aiProvider)
            {
                case 1:  return _openAiKey;
                case 0:  return _forgeKey;
                default: return _claudeKey;
            }
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
