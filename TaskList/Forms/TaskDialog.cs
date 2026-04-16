using System;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    public partial class TaskDialog : DarkForm
    {
        public TaskItem Result { get; private set; }


        private const int CollapsedHeight = 420;
        private const int ExpandedHeight  = 612;   // pnlJira at Y=378, height 192 → bottom 570, buttons +4, +28+10
        private const int BtnYCollapsed   = 382;
        private const int BtnYExpanded    = 574;

        // ── Constructors ──────────────────────────────────────────────────────
        public TaskDialog() : this(null) { }

        public TaskDialog(TaskItem existing, bool isSubtask = false)
        {
            Result = existing ?? new TaskItem();
            InitializeComponent();
            RegisterTitleBar(pnlTitleBar, showMin: false, showMax: false);

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            Populate();

            if (isSubtask)
            {
                _txtFeature.Enabled   = false;
                _txtFeature.Text      = "";
                _lblFeatureCaption.Enabled = false;
            }
        }

        // ── Populate / Save ───────────────────────────────────────────────────
        private void Populate()
        {
            Text = string.IsNullOrEmpty(Result.Name) ? "Add Task" : "Edit Task";
            _txtName.Text              = Result.Name;
            _rtbNotes.Text             = Result.Notes;
            _cmbPriority.SelectedIndex = (int)Result.Priority;
            cbDueDateEnabled.Checked   = Result.DueDateEnabled;
            try
            {
                _dtpDate.Value = Result.DueDate.Date;
                _dtpTime.Value = Result.DueDate;
            }
            catch { }

            foreach (var opt in AlertPresets.Options)
                _cmbAlert.Items.Add(opt.Label);

            for (int i = 0; i < AlertPresets.Options.Length; i++)
            {
                if (AlertPresets.Options[i].Minutes == Result.AlertLeadMinutes)
                { _cmbAlert.SelectedIndex = i; break; }
            }
            if (_cmbAlert.SelectedIndex < 0) _cmbAlert.SelectedIndex = 0;

            // Jira fields
            cbJiraImportable.Checked  = Result.JiraImportable;
            _txtStoryPoints.Text      = Result.JiraStoryPoints?.ToString() ?? "";
            _txtProject.Text          = Result.JiraProject    ?? "";
            _txtFeature.Text          = Result.JiraFeature    ?? "";
            _txtAssignee.Text         = Result.JiraAssignee   ?? "";
            _txtReporter.Text         = Result.JiraReporter   ?? "";
            _txtIssueType.Text        = string.IsNullOrWhiteSpace(Result.JiraIssueType) ? "Story" : Result.JiraIssueType;

            if (Result.JiraImportable)
                ExpandJiraPanel();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtName.Text))
            {
                MessageBox.Show("Task name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }
            Result.Name             = _txtName.Text.Trim();
            Result.Priority         = (TaskPriority)_cmbPriority.SelectedIndex;
            Result.DueDate          = _dtpDate.Value.Date + _dtpTime.Value.TimeOfDay;
            Result.Notes            = _rtbNotes.Text.Trim();
            Result.AlertLeadMinutes = AlertPresets.Options[_cmbAlert.SelectedIndex].Minutes;
            Result.DueDateEnabled   = cbDueDateEnabled.Checked;

            Result.JiraImportable  = cbJiraImportable.Checked;
            Result.JiraStoryPoints = int.TryParse(_txtStoryPoints.Text.Trim(), out int sp) ? sp : (int?)null;
            Result.JiraProject     = _txtProject.Text.Trim();
            Result.JiraFeature     = _txtFeature.Text.Trim();
            Result.JiraAssignee    = _txtAssignee.Text.Trim();
            Result.JiraReporter    = _txtReporter.Text.Trim();
            Result.JiraIssueType   = string.IsNullOrWhiteSpace(_txtIssueType.Text) ? "Story" : _txtIssueType.Text.Trim();

            DialogResult = DialogResult.OK;
        }

        // ── Expand / Collapse ─────────────────────────────────────────────────
        private void CbJiraImportable_CheckedChanged(object sender, EventArgs e)
        {
            if (cbJiraImportable.Checked)
                ExpandJiraPanel();
            else
                CollapseJiraPanel();
        }

        private void ExpandJiraPanel()
        {
            pnlJira.Visible  = true;
            ClientSize       = new Size(ClientSize.Width, ExpandedHeight);
            _btnSave.Top     = BtnYExpanded;
            _btnCancel.Top   = BtnYExpanded;
        }

        private void CollapseJiraPanel()
        {
            pnlJira.Visible  = false;
            ClientSize       = new Size(ClientSize.Width, CollapsedHeight);
            _btnSave.Top     = BtnYCollapsed;
            _btnCancel.Top   = BtnYCollapsed;
        }

        // ── Due date toggle ───────────────────────────────────────────────────
        private void cbDueDateEnabled_CheckedChanged(object sender, EventArgs e)
        {
            _cmbAlert.Enabled = cbDueDateEnabled.Checked;
            _dtpDate.Enabled  = cbDueDateEnabled.Checked;
            _dtpTime.Enabled  = cbDueDateEnabled.Checked;
            if (!cbDueDateEnabled.Checked && _cmbAlert.Items.Count > 0)
                _cmbAlert.SelectedIndex = 0;
        }
    }
}
