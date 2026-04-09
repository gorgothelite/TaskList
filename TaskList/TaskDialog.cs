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

        // -- Constructors -----------------------------------------------------
        // Parameterless constructor used by the VS designer
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

        // -- Logic ------------------------------------------------------------
        private void Populate()
        {
            Text = string.IsNullOrEmpty(Result.Name) ? "Add Task" : "Edit Task";
            _txtName.Text              = Result.Name;
            _rtbNotes.Text             = Result.Notes;
            _cmbPriority.SelectedIndex = (int)Result.Priority;
            cbDueDateEnabled.Checked = Result.DueDateEnabled;
            try
            {
                _dtpDate.Value = Result.DueDate.Date;
                _dtpTime.Value = Result.DueDate;
            }
            catch { }

            foreach (var opt in AlertOptions)
                _cmbAlert.Items.Add(opt.Label);

            // Select the option whose minutes match; fall back to "1 day before"
            int alertIdx = -1; // default: Never
            for (int i = 0; i < AlertOptions.Length; i++)
            {
                if (AlertOptions[i].Minutes == Result.AlertLeadMinutes)
                { alertIdx = i; break; }
            }
            _cmbAlert.SelectedIndex = 0;
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
            DialogResult            = DialogResult.OK;
        }

        private void cbDueDateEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDueDateEnabled.Checked)
            {
                _cmbAlert.Enabled = true;
                _dtpDate.Enabled = true;
                _dtpTime.Enabled = true;
            }
            else
            {
                _cmbAlert.Enabled = false;
                if(_cmbAlert.Items.Count > 0)
                    _cmbAlert.SelectedIndex = 0;
                _dtpDate.Enabled = false;
                _dtpTime.Enabled = false;
            }
        }
    }
}
