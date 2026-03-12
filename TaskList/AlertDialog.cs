using System;
using System.Windows.Forms;

namespace Test
{
    public partial class AlertDialog : Form
    {
        public SnoozeChoice Choice { get; private set; } = SnoozeChoice.Dismiss;

        // Parameterless constructor used by the VS designer
        public AlertDialog() { InitializeComponent(); }

        public AlertDialog(TaskItem task)
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            bool overdue = task.DueDate < DateTime.Now;
            _lblTaskInfo.Text = $"\"{task.Name}\"\n" +
                                (overdue ? "is OVERDUE!" : "is due  " + task.DueDate.ToString("g"));
        }

        private void BtnDismiss_Click(object sender, EventArgs e)
        {
            Choice = SnoozeChoice.Dismiss;
            Close();
        }

        private void BtnIgnoreForever_Click(object sender, EventArgs e)
        {
            Choice = SnoozeChoice.IgnoreAlways;
            Close();
        }

        private void BtnSnooze1h_Click(object sender, EventArgs e)
        {
            Choice = SnoozeChoice.Snooze1Hour;
            Close();
        }

        private void BtnSnooze4h_Click(object sender, EventArgs e)
        {
            Choice = SnoozeChoice.Snooze4Hours;
            Close();
        }

        private void BtnSnooze1d_Click(object sender, EventArgs e)
        {
            Choice = SnoozeChoice.Snooze1Day;
            Close();
        }
    }
}
