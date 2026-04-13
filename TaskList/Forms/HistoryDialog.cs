using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Test
{
    public partial class HistoryDialog : DarkForm
    {
        public event Action HistoryCleared;

        private List<RevisionEntry> _entries;
        private readonly string     _backupDir;
        private int                 _lastCount    = -1;
        private string              _selectedRevId = null;

        private static readonly (string Label, Color Col)[] LegendItems =
        {
            ("Added",       Color.FromArgb( 88, 196,  88)),
            ("Edited",      Color.FromArgb( 90, 190, 255)),
            ("Deleted",     Color.FromArgb(222,  80,  80)),
            ("Status",      Color.FromArgb(214, 188,  50)),
            ("Alert/Other", Color.FromArgb(180, 180, 190)),
        };

        // Parameterless constructor used by the VS designer
        public HistoryDialog() : this(null, null) { }

        public HistoryDialog(List<RevisionEntry> entries, string backupDir)
        {
            _entries   = entries ?? new List<RevisionEntry>();
            _backupDir = backupDir ?? string.Empty;
            _resizable = true;

            InitializeComponent();
            RegisterTitleBar(pnlToolbar, showMin: true, showMax: true);
            BuildLegend();

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            PopulateList();

            _refreshTimer.Interval = 1000;
            _refreshTimer.Tick    += (s, e) => { if (_entries.Count != _lastCount) PopulateList(); };
            _refreshTimer.Start();
        }

        // Called directly by MainForm after every AddRevision for instant update
        public void Refresh() => PopulateList();

        // ── Legend ───────────────────────────────────────────────────────────
        private void BuildLegend()
        {
            int lx = 12;
            var title = new Label { Text = "Legend:", Location = new Point(lx, 7), AutoSize = true, Font = new Font("Segoe UI", 8f, FontStyle.Bold), ForeColor = Color.FromArgb(130, 130, 140), BackColor = Color.Transparent };
            pnlLegend.Controls.Add(title);
            lx += 58;

            foreach (var item in LegendItems)
            {
                var dot = new Panel { Location = new Point(lx, 10), Size = new Size(10, 10), BackColor = item.Col };
                pnlLegend.Controls.Add(dot);
                lx += 14;

                var lbl = new Label { Text = item.Label, Location = new Point(lx, 7), AutoSize = true, Font = new Font("Segoe UI", 8.5f), ForeColor = Color.FromArgb(200, 200, 210), BackColor = Color.Transparent };
                pnlLegend.Controls.Add(lbl);
                lx += TextRenderer.MeasureText(item.Label, lbl.Font).Width + 18;
            }
        }

        // ── List population ───────────────────────────────────────────────────
        private void PopulateList()
        {
            if (_lvHistory.SelectedItems.Count > 0)
                _selectedRevId = ((RevisionEntry)_lvHistory.SelectedItems[0].Tag)?.RevisionId;

            _lastCount     = _entries.Count;
            _lblCount.Text = $"({_lastCount} entries)";

            _lvHistory.BeginUpdate();
            _lvHistory.Items.Clear();

            foreach (var r in _entries.OrderByDescending(x => x.Timestamp))
            {
                var li = new ListViewItem(r.Timestamp.ToString("yyyy-MM-dd  HH:mm:ss")) { Tag = r };
                li.SubItems.Add(ActionLabel(r.Action));
                li.SubItems.Add(r.TaskName ?? "");
                li.SubItems.Add(string.IsNullOrEmpty(r.BackupFile) ? "" : "✓");
                li.ForeColor = ActionColor(r.Action);
                _lvHistory.Items.Add(li);
            }

            _lvHistory.EndUpdate();

            if (_selectedRevId != null)
            {
                foreach (ListViewItem li in _lvHistory.Items)
                {
                    if (((RevisionEntry)li.Tag)?.RevisionId == _selectedRevId)
                    {
                        li.Selected = true;
                        li.EnsureVisible();
                        break;
                    }
                }
            }
        }

        // ── Event handlers ────────────────────────────────────────────────────
        private void OnHistorySelect(object sender, EventArgs e)
        {
            _lvChanges.Items.Clear();
            if (_lvHistory.SelectedItems.Count == 0) { _lblSummary.Text = ""; return; }

            var rev = (RevisionEntry)_lvHistory.SelectedItems[0].Tag;
            _selectedRevId   = rev.RevisionId;
            _lblSummary.Text = rev.Summary ?? "";

            foreach (var c in rev.Changes)
            {
                var li = new ListViewItem(c.Field);
                li.SubItems.Add(c.OldValue ?? "");
                li.SubItems.Add(c.NewValue ?? "");
                _lvChanges.Items.Add(li);
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Title            = "Select Backup to Restore",
                InitialDirectory = Directory.Exists(_backupDir) ? _backupDir : AppDomain.CurrentDomain.BaseDirectory,
                Filter           = "JSON Backup (*.json)|*.json"
            })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;
                if (MessageBox.Show(
                        $"Restore from:\n{Path.GetFileName(ofd.FileName)}\n\nThis overwrites tasks.json.\nContinue?",
                        "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                try
                {
                    File.Copy(ofd.FileName, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.json"), true);
                    MessageBox.Show("Backup restored.\nRestart the application to load the restored data.",
                        "Restored", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Restore failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all revision history?\nBackup files will NOT be deleted.",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            _entries.Clear();
            _selectedRevId   = null;
            _lblSummary.Text = "";
            _lvChanges.Items.Clear();
            PopulateList();
            HistoryCleared?.Invoke();
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private static string ActionLabel(RevisionAction a)
        {
            switch (a)
            {
                case RevisionAction.TaskAdded:     return "Added";
                case RevisionAction.TaskEdited:    return "Edited";
                case RevisionAction.TaskDeleted:   return "Deleted";
                case RevisionAction.StatusChanged: return "Status";
                case RevisionAction.AlertSnoozed:  return "Snoozed";
                case RevisionAction.AlertIgnored:  return "Alert Off";
                case RevisionAction.DataLoaded:    return "Loaded";
                default:                           return a.ToString();
            }
        }

        private static Color ActionColor(RevisionAction a)
        {
            switch (a)
            {
                case RevisionAction.TaskAdded:     return Color.FromArgb( 88, 196,  88);
                case RevisionAction.TaskDeleted:   return Color.FromArgb(222,  80,  80);
                case RevisionAction.TaskEdited:    return Color.FromArgb( 90, 190, 255);
                case RevisionAction.StatusChanged: return Color.FromArgb(214, 188,  50);
                default:                           return Color.FromArgb(180, 180, 190);
            }
        }
    }
}
