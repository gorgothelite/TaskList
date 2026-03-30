using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test
{
    public partial class ExportDialog : DarkForm
    {
        private readonly List<TaskItem>        _tasks;
        private readonly List<EmailRecipient>  _recipients = new List<EmailRecipient>();

        private static readonly string RecipientsFile =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "email_recipients.json");

        public ExportDialog() : this(new List<TaskItem>()) { }

        public ExportDialog(List<TaskItem> tasks)
        {
            _tasks = tasks ?? new List<TaskItem>();
            InitializeComponent();
            RegisterTitleBar(pnlTitleBar, showMin: false, showMax: false);

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            LoadRecipients();
            UpdatePreview();
        }

        // ── Recipients persistence ────────────────────────────────────────────
        private void LoadRecipients()
        {
            try
            {
                if (!File.Exists(RecipientsFile)) return;
                var list = JsonConvert.DeserializeObject<List<EmailRecipient>>(
                               File.ReadAllText(RecipientsFile));
                if (list != null) _recipients.AddRange(list);
            }
            catch { /* ignore corrupt file */ }
        }

        private void SaveRecipients()
        {
            try
            {
                File.WriteAllText(RecipientsFile,
                    JsonConvert.SerializeObject(_recipients, Formatting.Indented));
            }
            catch { /* ignore */ }
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

            var filtered = q.ToList();
            var topLevel = filtered.Where(t => t.ParentId == null)
                                   .OrderByDescending(t => (int)t.Priority).ThenBy(t => t.DueDate)
                                   .ToList();
            var subtasks = filtered.Where(t => t.ParentId != null)
                                   .OrderByDescending(t => (int)t.Priority).ThenBy(t => t.DueDate)
                                   .ToList();

            var result   = new List<TaskItem>();
            var addedIds = new HashSet<string>();
            foreach (var parent in topLevel)
            {
                result.Add(parent);
                addedIds.Add(parent.Id);
                foreach (var child in subtasks.Where(s => s.ParentId == parent.Id))
                {
                    result.Add(child);
                    addedIds.Add(child.Id);
                }
            }
            // Subtasks whose parent was filtered out
            foreach (var child in subtasks.Where(s => !addedIds.Contains(s.Id)))
                result.Add(child);
            return result;
        }

        private List<string> BuildHeaders()
        {
            var cols = new List<string>();
            if (_chkColName.Checked)     cols.Add("Name");
            if (_chkColPriority.Checked) cols.Add("Priority");
            if (_chkColDue.Checked)      cols.Add("Due Date");
            if (_chkColStatus.Checked)   cols.Add("Status");
            if (_chkColNotes.Checked)    cols.Add("Notes");
            return cols;
        }

        private List<List<string>> BuildRows()
        {
            var result = new List<List<string>>();
            foreach (var t in GetFiltered())
            {
                var row = new List<string>();
                if (_chkColName.Checked)     row.Add(t.ParentId != null ? "  ↳ " + t.Name : t.Name);
                if (_chkColPriority.Checked) row.Add(t.Priority.ToString());
                if (_chkColDue.Checked)      row.Add(t.DueDate.ToString("yyyy-MM-dd HH:mm"));
                if (_chkColStatus.Checked)   row.Add(t.IsDone ? "Done" : (t.DueDate < DateTime.Now ? "Overdue" : "Active"));
                if (_chkColNotes.Checked)    row.Add(t.Notes);
                result.Add(row);
            }
            return result;
        }

        private string BuildCsv()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Join(",", BuildHeaders().Select(CsvEscape)));
            foreach (var row in BuildRows())
                sb.AppendLine(string.Join(",", row.Select(CsvEscape)));
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
                Title      = "Export Tasks as CSV",
                Filter     = "CSV File (*.csv)|*.csv",
                FileName   = $"tasks_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                DefaultExt = "csv"
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

        private void BtnEditRecipients_Click(object sender, EventArgs e)
        {
            using (var dlg = new EmailRecipientsDialog(_recipients))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    SaveRecipients();
            }
        }

        private void BtnSendOutlook_Click(object sender, EventArgs e)
        {
            if (!GetFiltered().Any())
            {
                MessageBox.Show("No tasks match the current filters.", "Nothing to Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_recipients.Count == 0)
            {
                MessageBox.Show("No recipients configured. Use \"Edit Recipients…\" to add recipients first.",
                    "No Recipients", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Write xlsx to a temp file
            string tempPath = Path.Combine(Path.GetTempPath(),
                $"tasks_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
            try
            {
                XlsxWriter.Write(tempPath, "Tasks", BuildHeaders(), BuildRows());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to build Excel file:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Launch Outlook draft via late binding (no interop reference needed)
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
                mail    = outlook.CreateItem(0); // 0 = olMailItem

                mail.Subject = $"Task List — {DateTime.Now:yyyy-MM-dd}";

                foreach (var r in _recipients)
                {
                    dynamic recip = mail.Recipients.Add(r.Email);
                    recip.Type = 1; // 1 = olTo
                }

                // olByValue = 1, position = 1
                mail.Attachments.Add(tempPath, 1, 1, Path.GetFileName(tempPath));

                mail.Display(false); // show draft; user manually sends
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create Outlook email:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (mail    != null) { try { Marshal.ReleaseComObject(mail);    } catch { } }
                if (outlook != null) { try { Marshal.ReleaseComObject(outlook); } catch { } }
            }
        }
    }
}
