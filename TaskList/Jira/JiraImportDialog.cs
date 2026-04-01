using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Test
{
    public partial class JiraImportDialog : DarkForm
    {
        private readonly List<TaskItem> _tasks;
        private string _jiraBaseUrl = "";

        private static readonly string ConfigFile =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jira_config.json");
        private static readonly string SettingsFile =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jira_import_settings.json");

        public JiraImportDialog(List<TaskItem> tasks)
        {
            _resizable = true;
            _tasks = tasks ?? new List<TaskItem>();
            InitializeComponent();
            RegisterTitleBar(pnlTitleBar, showMin: true, showMax: true);

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            SetupGrid();
            LoadSettings();
            PopulateGrid();
        }

        // ── Grid column setup (done in code, not designer) ────────────────────

        private void SetupGrid()
        {
            _dgvTasks.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colSelect", HeaderText = "", Width = 30, MinimumWidth = 30,
                Resizable = DataGridViewTriState.False
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCreated", HeaderText = "Created", Width = 84,
                ReadOnly = true
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colType", HeaderText = "Type", Width = 70, ReadOnly = true
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSummary", HeaderText = "Summary", Width = 200
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFeature", HeaderText = "Feature", Width = 110
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStoryPts", HeaderText = "SP", Width = 44
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDueDate", HeaderText = "Due Date", Width = 90
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFeatureLink", HeaderText = "Feature Link", Width = 110
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colParentKey", HeaderText = "Parent Key", Width = 90
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colReporter", HeaderText = "Reporter", Width = 100
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAssignee", HeaderText = "Assignee", Width = 100
            });
            _dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDescription", HeaderText = "Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 100
            });
        }

        // ── Settings ──────────────────────────────────────────────────────────

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(ConfigFile))
                {
                    var obj = JObject.Parse(File.ReadAllText(ConfigFile));
                    _lblJiraUrl.Text = obj["url"]?.ToString()?.Trim() ?? "(not configured)";
                }
                else _lblJiraUrl.Text = "(not configured — set up in the Jira window first)";
            }
            catch { _lblJiraUrl.Text = "(error reading jira_config.json)"; }

            _txtSpField.Text = "customfield_10016";
            _txtFlField.Text = "customfield_10014";

            try
            {
                if (!File.Exists(SettingsFile)) return;
                var s = JObject.Parse(File.ReadAllText(SettingsFile));
                _txtProject.Text            = s["project"]?.ToString()           ?? "";
                _txtSpField.Text            = s["spField"]?.ToString()            ?? "customfield_10016";
                _txtFeatField.Text          = s["featField"]?.ToString()          ?? "";
                _txtFlField.Text            = s["flField"]?.ToString()            ?? "customfield_10014";
                _txtDefaultFeatureLink.Text = s["defaultFeatureLink"]?.ToString() ?? "";
                _txtDefaultReporter.Text    = s["defaultReporter"]?.ToString()    ?? "";
                _txtDefaultAssignee.Text    = s["defaultAssignee"]?.ToString()    ?? "";
                int sp = s["defaultStoryPts"]?.ToObject<int>() ?? 0;
                _numDefaultStoryPts.Value   = Math.Max(0, Math.Min((int)_numDefaultStoryPts.Maximum, sp));
            }
            catch { }
        }

        private void SaveSettings()
        {
            try
            {
                File.WriteAllText(SettingsFile, new JObject
                {
                    ["project"]            = _txtProject.Text.Trim(),
                    ["spField"]            = _txtSpField.Text.Trim(),
                    ["featField"]          = _txtFeatField.Text.Trim(),
                    ["flField"]            = _txtFlField.Text.Trim(),
                    ["defaultFeatureLink"] = _txtDefaultFeatureLink.Text.Trim(),
                    ["defaultReporter"]    = _txtDefaultReporter.Text.Trim(),
                    ["defaultAssignee"]    = _txtDefaultAssignee.Text.Trim(),
                    ["defaultStoryPts"]    = (int)_numDefaultStoryPts.Value,
                }.ToString());
            }
            catch { }
        }

        // ── Grid population ───────────────────────────────────────────────────

        private void PopulateGrid()
        {
            _dgvTasks.Rows.Clear();

            var topLevel = _tasks.Where(t => t.ParentId == null)
                                 .OrderByDescending(t => (int)t.Priority).ThenBy(t => t.DueDate)
                                 .ToList();
            var subtasks = _tasks.Where(t => t.ParentId != null)
                                 .OrderByDescending(t => (int)t.Priority).ThenBy(t => t.DueDate)
                                 .ToList();
            var ordered = new List<TaskItem>();
            var added   = new HashSet<string>();
            foreach (var parent in topLevel)
            {
                ordered.Add(parent); added.Add(parent.Id);
                foreach (var sub in subtasks.Where(s => s.ParentId == parent.Id))
                { ordered.Add(sub); added.Add(sub.Id); }
            }
            foreach (var orphan in subtasks.Where(s => !added.Contains(s.Id)))
                ordered.Add(orphan);

            foreach (var task in ordered)
                AddRow(task);
        }

        private void AddRow(TaskItem task)
        {
            bool isSub = task.ParentId != null;
            int idx = _dgvTasks.Rows.Add();
            var row = _dgvTasks.Rows[idx];
            row.Tag = task;

            row.Cells["colSelect"].Value      = true;
            row.Cells["colCreated"].Value     = "";
            row.Cells["colType"].Value        = isSub ? "Sub-task" : "Story";
            row.Cells["colSummary"].Value     = task.Name;
            row.Cells["colFeature"].Value     = "";
            row.Cells["colStoryPts"].Value    = (int)_numDefaultStoryPts.Value;
            row.Cells["colDueDate"].Value     = task.DueDate.ToString("yyyy-MM-dd");
            row.Cells["colFeatureLink"].Value = isSub ? "" : _txtDefaultFeatureLink.Text.Trim();
            row.Cells["colParentKey"].Value   = "";
            row.Cells["colReporter"].Value    = _txtDefaultReporter.Text.Trim();
            row.Cells["colAssignee"].Value    = _txtDefaultAssignee.Text.Trim();
            row.Cells["colDescription"].Value = task.Notes ?? "";
        }

        // ── Grid events ───────────────────────────────────────────────────────

        private void DgvTasks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var task = _dgvTasks.Rows[e.RowIndex].Tag as TaskItem;
            if (task == null) return;

            bool isSub = task.ParentId != null;
            string col = _dgvTasks.Columns[e.ColumnIndex].Name;

            // Gray out cells that don't apply to this row type
            bool locked = (isSub && col == "colFeatureLink") || (!isSub && col == "colParentKey");
            if (locked)
            {
                e.CellStyle.BackColor          = Color.FromArgb(38, 38, 42);
                e.CellStyle.ForeColor          = Color.FromArgb(62, 62, 70);
                e.CellStyle.SelectionBackColor = Color.FromArgb(38, 38, 42);
                e.CellStyle.SelectionForeColor = Color.FromArgb(62, 62, 70);
                return;
            }

            // Colour the colCreated cell based on outcome
            if (col == "colCreated")
            {
                string val = e.Value as string ?? "";
                if (val == "FAILED")
                    e.CellStyle.ForeColor = Color.FromArgb(222, 80, 80);
                else if (!string.IsNullOrEmpty(val))
                    e.CellStyle.ForeColor = Color.FromArgb(88, 196, 88);
            }
        }

        private void DgvTasks_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var task = _dgvTasks.Rows[e.RowIndex].Tag as TaskItem;
            if (task == null) return;
            bool isSub = task.ParentId != null;
            string col = _dgvTasks.Columns[e.ColumnIndex].Name;

            if (col == "colType" || col == "colCreated")               { e.Cancel = true; return; }
            if (isSub  && col == "colFeatureLink")                     { e.Cancel = true; return; }
            if (!isSub && col == "colParentKey")                       { e.Cancel = true; }
        }

        // ── Button handlers ───────────────────────────────────────────────────

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in _dgvTasks.Rows)
                r.Cells["colSelect"].Value = true;
        }

        private void BtnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in _dgvTasks.Rows)
                r.Cells["colSelect"].Value = false;
        }

        private void BtnApplyDefaults_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in _dgvTasks.Rows)
            {
                var task = r.Tag as TaskItem;
                if (task == null) continue;
                bool isSub = task.ParentId != null;
                if (!isSub) r.Cells["colFeatureLink"].Value = _txtDefaultFeatureLink.Text.Trim();
                r.Cells["colReporter"].Value = _txtDefaultReporter.Text.Trim();
                r.Cells["colAssignee"].Value = _txtDefaultAssignee.Text.Trim();
                r.Cells["colStoryPts"].Value = (int)_numDefaultStoryPts.Value;
            }
        }

        private async void BtnImport_Click(object sender, EventArgs e)
        {
            _btnImport.Enabled = false;
            SetStatus("Importing…", false);
            SaveSettings();
            try   { await ImportAsync(); }
            catch (Exception ex) { SetStatus($"Unexpected error: {ex.Message}", true); }
            finally { _btnImport.Enabled = true; }
        }

        // ── Import logic ──────────────────────────────────────────────────────

        private async Task ImportAsync()
        {
            string project = _txtProject.Text.Trim();
            if (string.IsNullOrEmpty(project))
            { SetStatus("Project key is required.", true); return; }

            if (!InitHttpClient())
            { SetStatus("Jira not configured — set up the connection in the Jira window first.", true); return; }

            string spField   = _txtSpField.Text.Trim();
            string featField = _txtFeatField.Text.Trim();
            string flField   = _txtFlField.Text.Trim();

            var selected = _dgvTasks.Rows.Cast<DataGridViewRow>()
                                    .Where(r => r.Tag != null && true == (r.Cells["colSelect"].Value as bool?))
                                    .ToList();
            if (selected.Count == 0) { SetStatus("No tasks selected.", true); return; }

            var stories  = selected.Where(r => CellStr(r, "colType") == "Story").ToList();
            var subtasks = selected.Where(r => CellStr(r, "colType") == "Sub-task").ToList();

            // localTaskId → Jira key (for auto-linking subtasks to parents created in this run)
            var keyMap = new Dictionary<string, string>();
            int ok = 0, fail = 0;

            // ── Pass 1: stories ───────────────────────────────────────────────
            foreach (var row in stories)
            {
                var task    = (TaskItem)row.Tag;
                var payload = BuildPayload(row, project, spField, featField, flField, null);
                try
                {
                    var resp = await JiraForm.Http.PostAsync(
                        BuildUrl("/rest/api/2/issue"),
                        new System.Net.Http.StringContent(payload.ToString(), Encoding.UTF8, "application/json"));
                    string body = await resp.Content.ReadAsStringAsync();
                    if (resp.IsSuccessStatusCode)
                    {
                        string key = JObject.Parse(body)["key"]?.ToString() ?? "";
                        keyMap[task.Id] = key;
                        row.Cells["colCreated"].Value = key;
                        ok++;
                    }
                    else
                    {
                        row.Cells["colCreated"].Value = "FAILED";
                        fail++;
                        SetStatus($"Story failed ({CellStr(row, "colSummary")}): {ParseError(body)}", true);
                    }
                }
                catch (Exception ex)
                {
                    row.Cells["colCreated"].Value = "FAILED";
                    fail++;
                    SetStatus($"Error: {ex.Message}", true);
                }
                _dgvTasks.InvalidateRow(row.Index);
            }

            // ── Pass 2: sub-tasks ─────────────────────────────────────────────
            foreach (var row in subtasks)
            {
                var task = (TaskItem)row.Tag;

                // Prefer the manually-entered parent key; fall back to auto-resolved from pass 1
                string parentKey = CellStr(row, "colParentKey");
                if (string.IsNullOrEmpty(parentKey) && task.ParentId != null)
                    keyMap.TryGetValue(task.ParentId, out parentKey);

                if (string.IsNullOrEmpty(parentKey))
                {
                    row.Cells["colCreated"].Value = "FAILED";
                    fail++;
                    SetStatus($"Skipped '{CellStr(row, "colSummary")}': Parent Key is empty — fill in the Parent Key column.", true);
                    _dgvTasks.InvalidateRow(row.Index);
                    continue;
                }

                var payload = BuildPayload(row, project, spField, featField, flField, parentKey);
                try
                {
                    var resp = await JiraForm.Http.PostAsync(
                        BuildUrl("/rest/api/2/issue"),
                        new System.Net.Http.StringContent(payload.ToString(), Encoding.UTF8, "application/json"));
                    string body = await resp.Content.ReadAsStringAsync();
                    if (resp.IsSuccessStatusCode)
                    {
                        string key = JObject.Parse(body)["key"]?.ToString() ?? "";
                        row.Cells["colCreated"].Value = key;
                        ok++;
                    }
                    else
                    {
                        row.Cells["colCreated"].Value = "FAILED";
                        fail++;
                        SetStatus($"Sub-task failed ({CellStr(row, "colSummary")}): {ParseError(body)}", true);
                    }
                }
                catch (Exception ex)
                {
                    row.Cells["colCreated"].Value = "FAILED";
                    fail++;
                    SetStatus($"Error: {ex.Message}", true);
                }
                _dgvTasks.InvalidateRow(row.Index);
            }

            string msg = fail > 0
                ? $"Done: {ok} created, {fail} failed."
                : $"Done: {ok} issue(s) created successfully.";
            SetStatus(msg, fail > 0);
            if (fail == 0) _lblStatus.ForeColor = Color.FromArgb(88, 196, 88);
        }

        // ── Payload builder ───────────────────────────────────────────────────

        private JObject BuildPayload(DataGridViewRow row, string project,
            string spField, string featField, string flField, string parentKey)
        {
            bool isSub = parentKey != null;

            var fields = new JObject
            {
                ["project"]     = new JObject { ["key"] = project },
                ["summary"]     = CellStr(row, "colSummary"),
                ["issuetype"]   = new JObject { ["name"] = isSub ? "Sub-task" : "Story" },
                ["description"] = CellStr(row, "colDescription"),
            };

            string due = CellStr(row, "colDueDate");
            if (!string.IsNullOrEmpty(due))
                fields["duedate"] = due;

            string reporter = CellStr(row, "colReporter");
            if (!string.IsNullOrEmpty(reporter))
                fields["reporter"] = new JObject { ["name"] = reporter };

            string assignee = CellStr(row, "colAssignee");
            if (!string.IsNullOrEmpty(assignee))
                fields["assignee"] = new JObject { ["name"] = assignee };

            if (!string.IsNullOrEmpty(spField) &&
                int.TryParse(CellStr(row, "colStoryPts"), out int sp) && sp >= 0)
                fields[spField] = sp;

            string feature = CellStr(row, "colFeature");
            if (!string.IsNullOrEmpty(featField) && !string.IsNullOrEmpty(feature))
                fields[featField] = feature;

            // Feature link only for stories
            if (!isSub && !string.IsNullOrEmpty(flField))
            {
                string fl = CellStr(row, "colFeatureLink");
                if (!string.IsNullOrEmpty(fl))
                    fields[flField] = fl;
            }

            if (isSub)
                fields["parent"] = new JObject { ["key"] = parentKey };

            return new JObject { ["fields"] = fields };
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static string CellStr(DataGridViewRow row, string col) =>
            row.Cells[col].Value?.ToString()?.Trim() ?? "";

        private bool InitHttpClient()
        {
            try
            {
                if (!File.Exists(ConfigFile)) return false;
                var obj   = JObject.Parse(File.ReadAllText(ConfigFile));
                string url   = obj["url"]?.ToString()?.Trim()     ?? "";
                string email = obj["username"]?.ToString()?.Trim() ?? "";
                string token = DpapiCrypto.UnprotectFromBase64(
                    obj["password"]?.ToString() ?? "", DataProtectionScope.CurrentUser);

                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                    return false;

                string cred = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{email}:{token}"));
                JiraForm.Http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", cred);
                JiraForm.Http.DefaultRequestHeaders.Accept.Clear();
                JiraForm.Http.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                _jiraBaseUrl = url.TrimEnd('/');
                return true;
            }
            catch { return false; }
        }

        private string BuildUrl(string path) => _jiraBaseUrl + path;

        private void SetStatus(string msg, bool isError)
        {
            _lblStatus.Text      = msg;
            _lblStatus.ForeColor = isError
                ? Color.FromArgb(222, 80, 80)
                : Color.FromArgb(130, 130, 140);
        }

        private static string ParseError(string json)
        {
            try
            {
                var obj = JObject.Parse(json);
                return obj["errorMessages"]?.First?.ToString()
                    ?? obj["message"]?.ToString()
                    ?? json.Substring(0, Math.Min(150, json.Length));
            }
            catch { return json.Substring(0, Math.Min(150, json.Length)); }
        }
    }
}
