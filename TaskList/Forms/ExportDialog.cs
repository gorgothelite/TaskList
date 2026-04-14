using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class ExportDialog : DarkForm
    {
        private readonly List<TaskItem>       _tasks;
        private readonly List<EmailRecipient> _recipients = new List<EmailRecipient>();

        private readonly ExportService _exportService =
            new ExportService(AppDomain.CurrentDomain.BaseDirectory);

        public ExportDialog() : this(new List<TaskItem>()) { }

        public ExportDialog(List<TaskItem> tasks)
        {
            _tasks = tasks ?? new List<TaskItem>();
            InitializeComponent();
            RegisterTitleBar(pnlTitleBar, showMin: false, showMax: false);

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            _recipients.AddRange(_exportService.LoadRecipients());
            UpdatePreview();
        }

        // ── UI state → service parameters ────────────────────────────────────
        private ExportService.ExportFilter GetCurrentFilter() => new ExportService.ExportFilter
        {
            ActiveOnly      = _radActive.Checked,
            DoneOnly        = _radDone.Checked,
            IncludeLow      = _chkLow.Checked,
            IncludeMedium   = _chkMedium.Checked,
            IncludeHigh     = _chkHigh.Checked,
            IncludeCritical = _chkCritical.Checked,
        };

        private ExportService.ColumnSelection GetCurrentColumns() => new ExportService.ColumnSelection
        {
            Name     = _chkColName.Checked,
            Priority = _chkColPriority.Checked,
            Due      = _chkColDue.Checked,
            Status   = _chkColStatus.Checked,
            Notes    = _chkColNotes.Checked,
        };

        // ── Preview ───────────────────────────────────────────────────────────
        private void UpdatePreview()
        {
            var filter   = GetCurrentFilter();
            var cols     = GetCurrentColumns();
            var filtered = ExportService.ApplyFilter(_tasks, filter).ToList();
            _lblCount.Text   = $"{filtered.Count} task(s) will be exported";
            _txtPreview.Text = filtered.Count == 0
                ? "(no tasks match the selected filters)"
                : ExportService.BuildCsv(filtered, cols);
        }

        // ── Event handlers ────────────────────────────────────────────────────
        private void Filter_Changed(object sender, EventArgs e) => UpdatePreview();

        private void BtnExport_Click(object sender, EventArgs e)
        {
            var filter   = GetCurrentFilter();
            var cols     = GetCurrentColumns();
            var filtered = ExportService.ApplyFilter(_tasks, filter).ToList();

            if (!filtered.Any())
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
                    File.WriteAllText(sfd.FileName, ExportService.BuildCsv(filtered, cols), Encoding.UTF8);
                    MessageBox.Show($"Exported {filtered.Count} task(s) to:\n{sfd.FileName}",
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
                    _exportService.SaveRecipients(_recipients);
            }
        }

        private void BtnSendOutlook_Click(object sender, EventArgs e)
        {
            var filter   = GetCurrentFilter();
            var cols     = GetCurrentColumns();
            var filtered = ExportService.ApplyFilter(_tasks, filter).ToList();

            if (!filtered.Any())
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

            string tempPath = Path.Combine(Path.GetTempPath(), $"tasks_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
            try
            {
                XlsxWriter.Write(tempPath, "Tasks", ExportService.BuildHeaders(cols), ExportService.BuildRows(filtered, cols));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to build Excel file:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                mail    = outlook.CreateItem(0);
                mail.Subject = $"Task List — {DateTime.Now:yyyy-MM-dd}";
                foreach (var r in _recipients)
                {
                    dynamic recip = mail.Recipients.Add(r.Email);
                    recip.Type = 1;
                }
                mail.Attachments.Add(tempPath, 1, 1, Path.GetFileName(tempPath));
                mail.Display(false);
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
