using System;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    public partial class TaskDialog : DarkForm
    {
        public TaskItem Result { get; private set; }

        internal static readonly (string Label, int Minutes)[] AlertOptions =
        {
            ("Never",          -1),
            ("At due time",     0),
            ("30 min before",  30),
            ("1 hour before",  60),
            ("2 hours before", 120),
            ("4 hours before", 240),
            ("8 hours before", 480),
            ("1 day before",   1440),
            ("2 days before",  2880),
            ("1 week before",  10080),
        };

        private const int CollapsedHeight = 420;
        private const int ExpandedHeight  = 578;   // 420 + pnlJira.Height(150) + gap(8)
        private const int BtnYCollapsed   = 382;
        private const int BtnYExpanded    = 540;   // 378 + 150 + 12

        // ── Constructors ──────────────────────────────────────────────────────
        public TaskDialog() : this(null) { }

        public TaskDialog(TaskItem existing)
        {
            Result = existing ?? new TaskItem();
            InitializeComponent();
            RegisterTitleBar(pnlTitleBar, showMin: false, showMax: false);

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            Populate();
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

            foreach (var opt in AlertOptions)
                _cmbAlert.Items.Add(opt.Label);

            for (int i = 0; i < AlertOptions.Length; i++)
            {
                if (AlertOptions[i].Minutes == Result.AlertLeadMinutes)
                { _cmbAlert.SelectedIndex = i; break; }
            }
            if (_cmbAlert.SelectedIndex < 0) _cmbAlert.SelectedIndex = 0;

            // Jira fields
            cbJiraImportable.Checked  = Result.JiraImportable;
            _txtStoryPoints.Text      = Result.JiraStoryPoints?.ToString() ?? "";
            _txtProject.Text          = Result.JiraProject  ?? "";
            _txtFeature.Text          = Result.JiraFeature  ?? "";
            _txtAssignee.Text         = Result.JiraAssignee ?? "";
            _txtReporter.Text         = Result.JiraReporter ?? "";

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
            Result.AlertLeadMinutes = AlertOptions[_cmbAlert.SelectedIndex].Minutes;
            Result.DueDateEnabled   = cbDueDateEnabled.Checked;

            Result.JiraImportable  = cbJiraImportable.Checked;
            Result.JiraStoryPoints = int.TryParse(_txtStoryPoints.Text.Trim(), out int sp) ? sp : (int?)null;
            Result.JiraProject     = _txtProject.Text.Trim();
            Result.JiraFeature     = _txtFeature.Text.Trim();
            Result.JiraAssignee    = _txtAssignee.Text.Trim();
            Result.JiraReporter    = _txtReporter.Text.Trim();

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
