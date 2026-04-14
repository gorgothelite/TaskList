using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Test
{
    public partial class JiraForm : DarkForm
    {
        // ── Service ───────────────────────────────────────────────────────────
        private readonly JiraService _jiraService =
            new JiraService(AppDomain.CurrentDomain.BaseDirectory);

        // ── Master list backing store ─────────────────────────────────────────
        private readonly List<JiraIssue>  _masterItems = new List<JiraIssue>();
        private readonly List<JiraPreset> _presets     = new List<JiraPreset>();

        // ── Results sort state ────────────────────────────────────────────────
        private int  _resultsSortCol = -1;
        private bool _resultsSortAsc = true;

        /// <summary>
        /// Raised when the user clicks "Import to Tasks".
        /// Payload is the list of TaskItems to be added to MainForm.
        /// </summary>
        public event Action<List<TaskItem>> TasksImported;

        // ── Constructor ───────────────────────────────────────────────────────
        public JiraForm()
        {
            _resizable = true;
            InitializeComponent();
            RegisterTitleBar(pnlTitleBar, showMin: true, showMax: true);

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
        private void LoadConfig()
        {
            var config        = _jiraService.LoadConfig();
            _txtJiraUrl.Text  = config.Url;
            _txtUserName.Text = config.Email;
            _txtPassword.Text = config.Token;
        }

        private void SaveConfig()
        {
            _jiraService.SaveConfig(_txtJiraUrl.Text.Trim(), _txtUserName.Text.Trim(), _txtPassword.Text.Trim());
        }

        // ── Presets ───────────────────────────────────────────────────────────
        private void LoadPresets()
        {
            _presets.Clear();
            _presets.AddRange(_jiraService.LoadPresets());
            PopulatePresetsCombo();
        }

        internal void SavePresets()
        {
            _jiraService.SavePresets(_presets);
            PopulatePresetsCombo();
        }

        private void PopulatePresetsCombo()
        {
            _cmbPresets.Items.Clear();
            _cmbPresets.Items.Add("— select a preset —");
            foreach (var p in _presets) _cmbPresets.Items.Add(p.Name);
            _cmbPresets.SelectedIndex = 0;
        }

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
                if (!_jiraService.SetupAuth(_txtJiraUrl.Text.Trim(), _txtUserName.Text.Trim(), _txtPassword.Text.Trim()))
                { SetStatus("Fill in all connection fields first.", true); return; }
                var (success, displayName, error) = await _jiraService.TestConnectionAsync(_txtJiraUrl.Text.Trim());
                if (success)
                {
                    _lblStatus.ForeColor = Color.FromArgb(88, 196, 88);
                    SetStatus($"✓  Connected as: {displayName}", false);
                }
                else SetStatus(error, true);
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
                if (!_jiraService.SetupAuth(_txtJiraUrl.Text.Trim(), _txtUserName.Text.Trim(), _txtPassword.Text.Trim()))
                { SetStatus("Configure connection settings first.", true); return; }

                var result = await _jiraService.SearchAsync(_txtJiraUrl.Text.Trim(), jql);
                if (!result.Success) { SetStatus(result.Error, true); return; }

                _lvResults.BeginUpdate();
                try { foreach (var issue in result.Issues) _lvResults.Items.Add(IssueToListItem(issue)); }
                finally { _lvResults.EndUpdate(); }

                string skipNote = result.Skipped > 0 ? $"  ({result.Skipped} skipped due to parse errors)" : "";
                SetStatus($"{result.Issues.Count} of {result.Total} returned.  Double-click to open in browser.{skipNote}", false);
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
            int idx = _cmbPresets.SelectedIndex - 1;
            if (idx < 0 || idx >= _presets.Count) return;
            _txtJql.Text = _presets[idx].Jql;
        }

        private void LvResults_DoubleClick(object sender, EventArgs e)
        {
            if (_lvResults.SelectedItems.Count == 0) return;
            string key   = ((JiraIssue)_lvResults.SelectedItems[0].Tag)?.Key;
            string base_ = _txtJiraUrl.Text.Trim().TrimEnd('/');
            if (key == null || string.IsNullOrEmpty(base_)) return;
            try { System.Diagnostics.Process.Start($"{base_}/browse/{key}"); }
            catch (Exception ex) { MessageBox.Show($"Could not open browser:\n{ex.Message}"); }
        }

        private void LvResults_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (_resultsSortCol == e.Column) _resultsSortAsc = !_resultsSortAsc;
            else { _resultsSortCol = e.Column; _resultsSortAsc = true; }
            _lvResults.ListViewItemSorter = new JiraListSorter(_resultsSortCol, _resultsSortAsc);
            _lvResults.Sort();
        }

        // ── Quick Run ─────────────────────────────────────────────────────────
        private async void BtnQuickRun_Click(object sender, EventArgs e)
        {
            if (!_jiraService.IsQuickRunConfigured())
            {
                using (var dlg = new JiraQuickRunDialog())
                    dlg.ShowDialog(this);
                return;
            }
            _btnQuickRun.Enabled      = false;
            _btnQuickRunSetup.Enabled = false;
            try
            {
                using (var runner = new JiraQuickRunDialog())
                {
                    runner.ExternalStatusCallback = (msg, isErr) =>
                    {
                        _lblStatus.Text      = msg;
                        _lblStatus.ForeColor = isErr
                            ? Color.FromArgb(222, 80, 80)
                            : Color.FromArgb(130, 130, 140);
                    };
                    await runner.RunAsync();
                }
            }
            finally
            {
                _btnQuickRun.Enabled      = true;
                _btnQuickRunSetup.Enabled = true;
            }
        }

        private void BtnQuickRunSetup_Click(object sender, EventArgs e)
        {
            using (var dlg = new JiraQuickRunDialog())
                dlg.ShowDialog(this);
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
                AddToMaster((JiraIssue)li.Tag);
        }

        // ── Master list – Add Parent / Epic ───────────────────────────────────
        private async void BtnAddParent_Click(object sender, EventArgs e)
        {
            if (_lvResults.SelectedItems.Count == 0)
            { SetStatus("Select one or more issues first.", true); return; }
            if (!_jiraService.SetupAuth(_txtJiraUrl.Text.Trim(), _txtUserName.Text.Trim(), _txtPassword.Text.Trim()))
            { SetStatus("Configure connection settings first.", true); return; }

            SetStatus("Fetching parent / epic…", false);
            _btnAddParent.Enabled = false;
            int errors = 0;
            foreach (ListViewItem li in _lvResults.SelectedItems)
            {
                var issue = (JiraIssue)li.Tag;
                var (parentIssue, errorTag) = await _jiraService.FetchParentOrEpicAsync(_txtJiraUrl.Text.Trim(), issue);
                if (parentIssue != null)
                    AddToMaster(parentIssue);
                else if (errorTag?.StartsWith("no-link:") == true)
                    SetStatus($"{issue.Key} has no parent or epic link.", true);
                else
                    errors++;
            }
            _btnAddParent.Enabled = true;
            if (errors > 0) SetStatus($"Done (with {errors} fetch error(s)).", true);
            else            SetStatus("Parent / epic added to master list.", false);
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

            var newTasks = JiraService.ConvertToTasks(selected);
            TasksImported?.Invoke(newTasks);
            SetStatus($"{newTasks.Count} task(s) imported.", false);
            _lblStatus.ForeColor = Color.FromArgb(88, 196, 88);
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private void AddToMaster(JiraIssue issue)
        {
            if (issue == null || string.IsNullOrEmpty(issue.Key)) return;
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

        private void UpdateMasterCount()
        {
            _lblMasterCount.Text = $"Master list: {_masterItems.Count} item(s)";
        }

        private void SetStatus(string msg, bool isError)
        {
            _lblStatus.Text      = msg;
            _lblStatus.ForeColor = isError ? Color.FromArgb(222, 80, 80) : Color.FromArgb(130, 130, 140);
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

    // ── ListView column sorter (WinForms-specific — stays in UI layer) ─────────
    internal class JiraListSorter : System.Collections.IComparer
    {
        private static readonly Dictionary<string, int> PriorityRank = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["blocker"]  = 0, ["critical"] = 0, ["highest"] = 0,
            ["major"]    = 1, ["high"]     = 1,
            ["medium"]   = 2, ["normal"]   = 2,
            ["minor"]    = 3, ["low"]      = 3,
            ["trivial"]  = 4, ["lowest"]   = 4,
        };

        private readonly int  _col;
        private readonly bool _asc;

        public JiraListSorter(int col, bool asc) { _col = col; _asc = asc; }

        public int Compare(object x, object y)
        {
            var a  = (ListViewItem)x;
            var b  = (ListViewItem)y;
            string va = _col == 0 ? a.Text : a.SubItems.Count > _col ? a.SubItems[_col].Text : "";
            string vb = _col == 0 ? b.Text : b.SubItems.Count > _col ? b.SubItems[_col].Text : "";

            int result;
            if (_col == 4)
            {
                int ra = PriorityRank.TryGetValue(va, out int tmp) ? tmp : 99;
                int rb = PriorityRank.TryGetValue(vb, out tmp)     ? tmp : 99;
                result = ra.CompareTo(rb);
            }
            else if (_col == 0)
            {
                result = CompareKeys(va, vb);
            }
            else
            {
                result = string.Compare(va, vb, StringComparison.OrdinalIgnoreCase);
            }
            return _asc ? result : -result;
        }

        private static int CompareKeys(string a, string b)
        {
            int ia = a.LastIndexOf('-'), ib = b.LastIndexOf('-');
            if (ia > 0 && ib > 0)
            {
                string prefA = a.Substring(0, ia), prefB = b.Substring(0, ib);
                int cmp = string.Compare(prefA, prefB, StringComparison.OrdinalIgnoreCase);
                if (cmp != 0) return cmp;
                if (int.TryParse(a.Substring(ia + 1), out int na) &&
                    int.TryParse(b.Substring(ib + 1), out int nb))
                    return na.CompareTo(nb);
            }
            return string.Compare(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}
