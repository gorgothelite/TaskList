using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class ExportDialog : Form
    {
        private readonly List<TaskItem> _tasks;

        public ExportDialog() : this(new List<TaskItem>()) { }

        public ExportDialog(List<TaskItem> tasks)
        {
            _tasks = tasks ?? new List<TaskItem>();
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            UpdatePreview();
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private IEnumerable<TaskItem> GetFiltered()
        {
            var q = _tasks.AsEnumerable();

            // Status
            if (_radActive.Checked)     q = q.Where(t => !t.IsDone);
            else if (_radDone.Checked)  q = q.Where(t =>  t.IsDone);

            // Priority checkboxes
            var allowed = new List<TaskPriority>();
            if (_chkLow.Checked)      allowed.Add(TaskPriority.Low);
            if (_chkMedium.Checked)   allowed.Add(TaskPriority.Medium);
            if (_chkHigh.Checked)     allowed.Add(TaskPriority.High);
            if (_chkCritical.Checked) allowed.Add(TaskPriority.Critical);
            q = q.Where(t => allowed.Contains(t.Priority));

            return q.OrderByDescending(t => (int)t.Priority).ThenBy(t => t.DueDate);
        }

        private string BuildCsv()
        {
            var sb = new StringBuilder();

            // Header row – only include selected columns
            var cols = new List<string>();
            if (_chkColName.Checked)     cols.Add("Name");
            if (_chkColPriority.Checked) cols.Add("Priority");
            if (_chkColDue.Checked)      cols.Add("Due Date");
            if (_chkColStatus.Checked)   cols.Add("Status");
            if (_chkColNotes.Checked)    cols.Add("Notes");
            sb.AppendLine(string.Join(",", cols));

            foreach (var t in GetFiltered())
            {
                var row = new List<string>();
                if (_chkColName.Checked)     row.Add(CsvEscape(t.Name));
                if (_chkColPriority.Checked) row.Add(t.Priority.ToString());
                if (_chkColDue.Checked)      row.Add(t.DueDate.ToString("yyyy-MM-dd HH:mm"));
                if (_chkColStatus.Checked)   row.Add(t.IsDone ? "Done" : (t.DueDate < DateTime.Now ? "Overdue" : "Active"));
                if (_chkColNotes.Checked)    row.Add(CsvEscape(t.Notes));
                sb.AppendLine(string.Join(",", row));
            }

            return sb.ToString();
        }

        private static string CsvEscape(string s)
        {
            if (s == null) return "\"\"";
            s = s.Replace("\"", "\"\"");
            return $"\"{s}\"";
        }

        private void UpdatePreview()
        {
            var rows = GetFiltered().ToList();
            _lblCount.Text = $"{rows.Count} task(s) will be exported";
            _txtPreview.Text = rows.Count == 0 ? "(no tasks match the selected filters)" : BuildCsv();
        }

        // ── Event handlers ────────────────────────────────────────────────────
        private void Filter_Changed(object sender, EventArgs e) => UpdatePreview();

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (!GetFiltered().Any())
            {
                MessageBox.Show("No tasks match the current filters.", "Nothing to Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Title            = "Export Tasks as CSV",
                Filter           = "CSV File (*.csv)|*.csv",
                FileName         = $"tasks_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                DefaultExt       = "csv"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    File.WriteAllText(sfd.FileName, BuildCsv(), Encoding.UTF8);
                    MessageBox.Show($"Exported {GetFiltered().Count()} task(s) to:\n{sfd.FileName}",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Export failed:\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
