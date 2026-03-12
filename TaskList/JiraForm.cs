using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Test
{
    public partial class JiraForm : Form
    {
        // ── Static HTTP client (one per app lifetime) ─────────────────────────
        internal static readonly HttpClient Http        = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
        private static readonly string      ConfigFile  = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jira_config.json");
        private static readonly string      PresetsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jira_presets.json");

        // ── Master list backing store ─────────────────────────────────────────
        private readonly List<JiraIssue>  _masterItems = new List<JiraIssue>();
        private readonly List<JiraPreset> _presets     = new List<JiraPreset>();

        /// <summary>
        /// Raised when the user clicks "Import to Tasks".
        /// Payload is the list of TaskItems to be added to MainForm.
        /// </summary>
        public event Action<List<TaskItem>> TasksImported;

        // ── Constructors ──────────────────────────────────────────────────────
        public JiraForm()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            AddColumns(_lvResults);
            AddColumns(_lvMaster);
            LoadConfig();
            LoadPresets();
        }

        private static void AddColumns(System.Windows.Forms.ListView lv)
        {
            lv.Columns.Add("Key",      80);
            lv.Columns.Add("Summary",  340);
            lv.Columns.Add("Type",     90);
            lv.Columns.Add("Status",   110);
            lv.Columns.Add("Priority", 80);
            lv.Columns.Add("Project",  120);
            lv.Columns.Add("Due Date", 90);
        }

        // ── Config ────────────────────────────────────────────────────────────
        private static string EncryptPassword(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";
            byte[] encrypted = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(plainText), null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        private static string DecryptPassword(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";
            try
            {
                byte[] decrypted = ProtectedData.Unprotect(
                    Convert.FromBase64String(cipherText), null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decrypted);
            }
            catch { return ""; }
        }

        private void LoadConfig()
        {
            if (!File.Exists(ConfigFile)) return;
            try
            {
                var obj = JObject.Parse(File.ReadAllText(ConfigFile));
                _txtJiraUrl.Text  = obj["url"]?.ToString()   ?? "";
                _txtEmail.Text    = obj["email"]?.ToString() ?? "";
                _txtApiToken.Text = DecryptPassword(obj["token"]?.ToString() ?? "");
            }
            catch { }
        }

        private void SaveConfig()
        {
            try
            {
                File.WriteAllText(ConfigFile, new JObject
                {
                    ["url"]   = _txtJiraUrl.Text.Trim(),
                    ["email"] = _txtEmail.Text.Trim(),
                    ["token"] = EncryptPassword(_txtApiToken.Text.Trim())
                }.ToString());
            }
            catch { }
        }

        // ── Presets ───────────────────────────────────────────────────────────
        private void LoadPresets()
        {
            if (File.Exists(PresetsFile))
            {
                try
                {
                    var arr = JArray.Parse(File.ReadAllText(PresetsFile));
                    _presets.Clear();
                    foreach (var item in arr)
                        _presets.Add(new JiraPreset
                        {
                            Name = item["name"]?.ToString() ?? "",
                            Jql  = item["jql"]?.ToString()  ?? ""
                        });
                }
                catch { }
            }

            if (_presets.Count == 0)
                _presets.AddRange(DefaultPresets());

            PopulatePresetsCombo();
        }

        internal void SavePresets()
        {
            try
            {
                var arr = new JArray(_presets.Select(p => new JObject { ["name"] = p.Name, ["jql"] = p.Jql }));
                File.WriteAllText(PresetsFile, arr.ToString());
            }
            catch { }
            PopulatePresetsCombo();
        }

        private void PopulatePresetsCombo()
        {
            _cmbPresets.Items.Clear();
            _cmbPresets.Items.Add("— select a preset —");
            foreach (var p in _presets) _cmbPresets.Items.Add(p.Name);
            _cmbPresets.SelectedIndex = 0;
        }

        private static List<JiraPreset> DefaultPresets() => new List<JiraPreset>
        {
            new JiraPreset { Name = "My open issues",        Jql = "assignee = currentUser() AND resolution = Unresolved ORDER BY priority DESC" },
            new JiraPreset { Name = "My work last week",     Jql = "assignee = currentUser() AND updated >= -1w ORDER BY updated DESC" },
            new JiraPreset { Name = "Open bugs by priority", Jql = "issuetype = Bug AND status != Done ORDER BY priority DESC" },
            new JiraPreset { Name = "In progress",           Jql = "status = \"In Progress\" ORDER BY updated DESC" },
            new JiraPreset { Name = "Open stories & epics",  Jql = "issuetype in (Story, Epic) AND status != Done ORDER BY priority DESC" },
            new JiraPreset { Name = "Due this week",         Jql = "duedate <= endOfWeek() AND resolution = Unresolved ORDER BY duedate ASC" },
            new JiraPreset { Name = "All epics",             Jql = "project is not EMPTY AND issuetype = Epic ORDER BY created DESC" },
            new JiraPreset { Name = "Search all text\u2026", Jql = "text ~ \"\" ORDER BY updated DESC" },
        };

        private void BtnManagePresets_Click(object sender, EventArgs e)
        {
            using (var dlg = new JiraPresetsDialog(_presets))
            {
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    SavePresets();
            }
        }

        // ── Connection handlers ───────────────────────────────────────────────
        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveConfig();
            SetStatus("Settings saved.", false);
        }

        private async void BtnTestConnection_Click(object sender, EventArgs e)
        {
            SetStatus("Testing connection…", false);
            _btnTestConnection.Enabled = false;
            try
            {
                if (BuildClient() == null) { SetStatus("Fill in all connection fields first.", true); return; }
                var resp = await Http.GetAsync(BuildUrl("/rest/api/3/myself"));
                if (resp.IsSuccessStatusCode)
                {
                    var json = JObject.Parse(await resp.Content.ReadAsStringAsync());
                    _lblStatus.ForeColor = Color.FromArgb(88, 196, 88);
                    SetStatus($"✓  Connected as: {json["displayName"]}", false);
                }
                else SetStatus($"Connection failed: {(int)resp.StatusCode} {resp.ReasonPhrase}", true);
            }
            catch (Exception ex) { SetStatus($"Error: {ex.Message}", true); }
            finally { _btnTestConnection.Enabled = true; }
        }

        // ── Search handlers ───────────────────────────────────────────────────
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            string jql = _txtJql.Text.Trim();
            if (string.IsNullOrEmpty(jql)) { SetStatus("Enter a JQL query.", true); return; }

            SetStatus("Searching…", false);
            _btnSearch.Enabled = false;
            _lvResults.Items.Clear();

            try
            {
                if (BuildClient() == null) { SetStatus("Configure connection settings first.", true); return; }

                var payload = new JObject
                {
                    ["jql"]        = jql,
                    ["maxResults"] = 100,
                    ["fields"]     = new JArray("summary", "status", "priority", "issuetype",
                                                "assignee", "project", "duedate", "parent",
                                                "customfield_10014")  // epic link (older Jira)
                };

                var resp = await Http.PostAsync(
                    BuildUrl("/rest/api/3/search/jql"),
                    new StringContent(payload.ToString(), Encoding.UTF8, "application/json"));

                var body = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                    { SetStatus($"Search failed: {(int)resp.StatusCode} – {ParseError(body)}", true); return; }

                var data   = JObject.Parse(body);
                var issues = data["issues"] as JArray;

                if (issues == null)
                    { SetStatus($"Unexpected response – no 'issues' array found.", true); return; }

                int skipped = 0;
                _lvResults.BeginUpdate();
                try
                {
                    foreach (var issue in issues)
                    {
                        try { _lvResults.Items.Add(IssueToListItem(ParseIssue(issue))); }
                        catch { skipped++; }
                    }
                }
                finally { _lvResults.EndUpdate(); }

                int total = data["total"]?.ToObject<int>() ?? issues.Count;
                string skipNote = skipped > 0 ? $"  ({skipped} skipped due to parse errors)" : "";
                SetStatus($"{issues.Count - skipped} of {total} returned.  Double-click to open in browser.{skipNote}", false);
            }
            catch (Exception ex) { SetStatus($"Error: {ex.Message}", true); }
            finally { _btnSearch.Enabled = true; }
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            _lvResults.Items.Clear();
            _txtJql.Clear();
            SetStatus("", false);
        }

        private void CmbPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = _cmbPresets.SelectedIndex - 1; // offset by 1 for placeholder
            if (idx < 0 || idx >= _presets.Count) return;
            _txtJql.Text = _presets[idx].Jql;
            _cmbPresets.SelectedIndex = 0;
        }

        private void LvResults_DoubleClick(object sender, EventArgs e)
        {
            if (_lvResults.SelectedItems.Count == 0) return;
            string key  = ((JiraIssue)_lvResults.SelectedItems[0].Tag)?.Key;
            string base_ = _txtJiraUrl.Text.Trim().TrimEnd('/');
            if (key == null || string.IsNullOrEmpty(base_)) return;
            try { System.Diagnostics.Process.Start($"{base_}/browse/{key}"); }
            catch (Exception ex) { MessageBox.Show($"Could not open browser:\n{ex.Message}"); }
        }

        // ── Export to Excel ───────────────────────────────────────────────────
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            var results = _lvResults.Items.Cast<System.Windows.Forms.ListViewItem>()
                                    .Select(li => (JiraIssue)li.Tag).ToList();
            using (var dlg = new JiraExportDialog(results, _masterItems, _txtJiraUrl.Text.Trim()))
                dlg.ShowDialog(this);
        }

        // ── Master list – Add Issue ───────────────────────────────────────────
        private void BtnAddIssue_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in _lvResults.SelectedItems)
            {
                var issue = (JiraIssue)li.Tag;
                AddToMaster(issue);
            }
        }

        // ── Master list – Add Parent / Epic ───────────────────────────────────
        private async void BtnAddParent_Click(object sender, EventArgs e)
        {
            if (_lvResults.SelectedItems.Count == 0)
            { SetStatus("Select one or more issues first.", true); return; }

            if (BuildClient() == null) { SetStatus("Configure connection settings first.", true); return; }

            SetStatus("Fetching parent / epic…", false);
            _btnAddParent.Enabled = false;

            var errors = 0;
            foreach (ListViewItem li in _lvResults.SelectedItems)
            {
                var issue = (JiraIssue)li.Tag;
                try
                {
                    // Fetch the issue detail to get parent or epic link
                    var resp = await Http.GetAsync(
                        BuildUrl($"/rest/api/3/issue/{issue.Key}?fields=parent,customfield_10014,summary,status,priority,issuetype,assignee,project,duedate"));

                    if (!resp.IsSuccessStatusCode) { errors++; continue; }

                    var data = JObject.Parse(await resp.Content.ReadAsStringAsync());
                    var f    = data["fields"];

                    // parent field (Jira Cloud next-gen / company-managed)
                    var parent = f["parent"];
                    if (parent != null && parent.Type != JTokenType.Null)
                    {
                        var pf = parent["fields"];
                        AddToMaster(new JiraIssue
                        {
                            Key      = parent["key"]?.ToString()                                     ?? "",
                            Summary  = pf?["summary"]?.ToString()                                    ?? parent["key"]?.ToString() ?? "",
                            Type     = (pf?["issuetype"] as JObject)?["name"]?.ToString()            ?? "Parent",
                            Status   = (pf?["status"]    as JObject)?["name"]?.ToString()            ?? "",
                            Priority = (pf?["priority"]  as JObject)?["name"]?.ToString()            ?? "",
                            Project  = issue.Project,
                            DueDate  = ""
                        });
                        continue;
                    }

                    // customfield_10014 = epic link (classic Jira)
                    string epicKey = f["customfield_10014"]?.ToString();
                    if (!string.IsNullOrEmpty(epicKey))
                    {
                        var epicResp = await Http.GetAsync(
                            BuildUrl($"/rest/api/3/issue/{epicKey}?fields=summary,status,priority,issuetype,assignee,project,duedate"));
                        if (epicResp.IsSuccessStatusCode)
                        {
                            var epicData = JObject.Parse(await epicResp.Content.ReadAsStringAsync());
                            AddToMaster(ParseIssue(epicData));
                        }
                        else { errors++; }
                        continue;
                    }

                    SetStatus($"{issue.Key} has no parent or epic link.", true);
                }
                catch { errors++; }
            }

            _btnAddParent.Enabled = true;
            if (errors > 0) SetStatus($"Done (with {errors} fetch error(s)).", true);
            else SetStatus($"Parent / epic added to master list.", false);
        }

        // ── Master list – Remove & Clear ──────────────────────────────────────
        private void BtnRemoveMaster_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in _lvMaster.SelectedItems)
            {
                _masterItems.RemoveAll(x => x.Key == ((JiraIssue)li.Tag)?.Key);
                _lvMaster.Items.Remove(li);
            }
            UpdateMasterCount();
        }

        private void BtnClearMaster_Click(object sender, EventArgs e)
        {
            _masterItems.Clear();
            _lvMaster.Items.Clear();
            UpdateMasterCount();
        }

        // ── Master list – Import to Tasks ─────────────────────────────────────
        private void BtnImportTasks_Click(object sender, EventArgs e)
        {
            var selected = _lvMaster.SelectedItems.Count > 0
                ? _lvMaster.SelectedItems.Cast<ListViewItem>().Select(li => (JiraIssue)li.Tag).ToList()
                : _masterItems.ToList();

            if (selected.Count == 0) { SetStatus("Master list is empty.", true); return; }

            var newTasks = new List<TaskItem>();
            foreach (var ji in selected)
            {
                DateTime due = DateTime.Now.AddDays(7);
                if (!string.IsNullOrEmpty(ji.DueDate) && DateTime.TryParse(ji.DueDate, out var d)) due = d;

                TaskPriority pri = TaskPriority.Medium;
                switch ((ji.Priority ?? "").ToLowerInvariant())
                {
                    case "highest": case "critical": pri = TaskPriority.Critical; break;
                    case "high":                     pri = TaskPriority.High;     break;
                    case "low": case "lowest":       pri = TaskPriority.Low;      break;
                }

                newTasks.Add(new TaskItem
                {
                    Name     = $"[{ji.Key}] {ji.Summary}",
                    Notes    = $"Jira: {ji.Key}\nProject: {ji.Project}\nType: {ji.Type}\nStatus: {ji.Status}\nAssignee: {ji.Assignee}",
                    Priority = pri,
                    DueDate  = due
                });
            }

            TasksImported?.Invoke(newTasks);
            SetStatus($"{newTasks.Count} task(s) imported.", false);
            _lblStatus.ForeColor = Color.FromArgb(88, 196, 88);
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private void AddToMaster(JiraIssue issue)
        {
            if (issue == null || string.IsNullOrEmpty(issue.Key)) return;
            // Deduplicate by key
            if (_masterItems.Any(x => x.Key == issue.Key)) return;
            _masterItems.Add(issue);
            _lvMaster.Items.Add(IssueToListItem(issue));
            UpdateMasterCount();
        }

        private static ListViewItem IssueToListItem(JiraIssue issue)
        {
            var li = new ListViewItem(issue.Key) { Tag = issue };
            li.SubItems.Add(issue.Summary);
            li.SubItems.Add(issue.Type);
            li.SubItems.Add(issue.Status);
            li.SubItems.Add(issue.Priority);
            li.SubItems.Add(issue.Project);
            li.SubItems.Add(issue.DueDate);
            li.ForeColor = StatusColor(issue.Status);
            return li;
        }

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

        private void UpdateMasterCount()
        {
            _lblMasterCount.Text = $"Master list: {_masterItems.Count} item(s)";
        }

        private HttpClient BuildClient()
        {
            string url   = _txtJiraUrl.Text.Trim();
            string email = _txtEmail.Text.Trim();
            string token = _txtApiToken.Text.Trim();
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token)) return null;

            string cred = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{email}:{token}"));
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", cred);
            Http.DefaultRequestHeaders.Accept.Clear();
            Http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return Http;
        }

        private string BuildUrl(string path) => _txtJiraUrl.Text.Trim().TrimEnd('/') + path;

        private void SetStatus(string msg, bool isError)
        {
            _lblStatus.Text      = msg;
            _lblStatus.ForeColor = isError ? Color.FromArgb(222, 80, 80) : Color.FromArgb(130, 130, 140);
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

        private static string FormatDate(string iso)
        {
            if (string.IsNullOrEmpty(iso)) return "";
            return DateTime.TryParse(iso, out var d) ? d.ToString("yyyy-MM-dd") : iso;
        }

        private static Color StatusColor(string status)
        {
            if (status == null) return Color.FromArgb(180, 180, 190);
            switch (status.ToLowerInvariant())
            {
                case "done": case "resolved": case "closed": return Color.FromArgb(100, 100, 108);
                case "in progress": case "in review":        return Color.FromArgb(90, 190, 255);
                case "to do": case "open": case "new":       return Color.FromArgb(180, 180, 190);
                default:                                     return Color.FromArgb(214, 188, 50);
            }
        }
    }

    // ── Lightweight DTO stored as Tag on both ListViews ───────────────────────
    internal class JiraIssue
    {
        public string Key      { get; set; }
        public string Summary  { get; set; }
        public string Type     { get; set; }
        public string Status   { get; set; }
        public string Priority { get; set; }
        public string Assignee { get; set; }
        public string Project  { get; set; }
        public string DueDate  { get; set; }   // yyyy-MM-dd or ""
    }

    // ── Named JQL preset ──────────────────────────────────────────────────────
    internal class JiraPreset
    {
        public string Name { get; set; }
        public string Jql  { get; set; }
    }
}
