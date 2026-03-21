using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Test
{
    internal sealed partial class EmailRecipientsDialog : Form
    {
        private readonly List<EmailRecipient> _original;
        private readonly List<EmailRecipient> _working;
        private bool _suppress;

        public EmailRecipientsDialog() : this(new List<EmailRecipient>()) { }

        public EmailRecipientsDialog(List<EmailRecipient> recipients)
        {
            _original = recipients;
            _working  = recipients.Select(r => new EmailRecipient
            {
                Name      = r.Name,
                Email     = r.Email,
                Position  = r.Position,
                IsDefault = r.IsDefault
            }).ToList();

            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            _txtName.TextChanged += (s, e) =>
            {
                if (_suppress || _lvRecipients.SelectedItems.Count == 0) return;
                var item = _lvRecipients.SelectedItems[0];
                item.Text = _txtName.Text;
                ((EmailRecipient)item.Tag).Name = _txtName.Text;
            };

            _txtEmail.TextChanged += (s, e) =>
            {
                if (_suppress || _lvRecipients.SelectedItems.Count == 0) return;
                var item = _lvRecipients.SelectedItems[0];
                item.SubItems[1].Text = _txtEmail.Text;
                ((EmailRecipient)item.Tag).Email = _txtEmail.Text;
            };

            _txtPosition.TextChanged += (s, e) =>
            {
                if (_suppress || _lvRecipients.SelectedItems.Count == 0) return;
                var item = _lvRecipients.SelectedItems[0];
                item.SubItems[2].Text = _txtPosition.Text;
                ((EmailRecipient)item.Tag).Position = _txtPosition.Text;
            };

            _chkDefault.CheckedChanged += (s, e) =>
            {
                if (_suppress || _lvRecipients.SelectedItems.Count == 0) return;
                var item = _lvRecipients.SelectedItems[0];
                ((EmailRecipient)item.Tag).IsDefault = _chkDefault.Checked;
                item.SubItems[3].Text = _chkDefault.Checked ? "\u2605" : "";
            };

            RefreshList(-1);
            if (_lvRecipients.Items.Count > 0)
                _lvRecipients.Items[0].Selected = true;
        }

        // ── List management ───────────────────────────────────────────────────
        private void RefreshList(int selectIdx)
        {
            _suppress = true;
            _lvRecipients.BeginUpdate();
            _lvRecipients.Items.Clear();
            foreach (var r in _working)
            {
                var item = new ListViewItem(r.Name) { Tag = r };
                item.SubItems.Add(r.Email);
                item.SubItems.Add(r.Position ?? "");
                item.SubItems.Add(r.IsDefault ? "\u2605" : "");
                _lvRecipients.Items.Add(item);
            }
            _lvRecipients.EndUpdate();
            _suppress = false;

            if (selectIdx >= 0 && selectIdx < _lvRecipients.Items.Count)
            {
                _lvRecipients.Items[selectIdx].Selected = true;
                _lvRecipients.Items[selectIdx].EnsureVisible();
            }
            UpdateButtonStates();
        }

        // ── Event handlers ────────────────────────────────────────────────────
        private void LvRecipients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppress) return;
            if (_lvRecipients.SelectedItems.Count == 0) { ClearEdit(); return; }
            var r = (EmailRecipient)_lvRecipients.SelectedItems[0].Tag;
            _suppress            = true;
            _txtName.Text        = r.Name;
            _txtEmail.Text       = r.Email;
            _txtPosition.Text    = r.Position ?? "";
            _chkDefault.Checked  = r.IsDefault;
            _suppress            = false;
            UpdateButtonStates();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            _working.Add(new EmailRecipient { Name = "New Recipient", Email = "", Position = "" });
            RefreshList(_working.Count - 1);
            _txtName.Focus();
            _txtName.SelectAll();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_lvRecipients.SelectedItems.Count == 0) return;
            int idx = _lvRecipients.SelectedIndices[0];
            _working.RemoveAt(idx);
            RefreshList(Math.Min(idx, _working.Count - 1));
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (_lvRecipients.SelectedItems.Count == 0) return;
            int idx           = _lvRecipients.SelectedIndices[0];
            if (idx == 0) return;
            var tmp           = _working[idx - 1];
            _working[idx - 1] = _working[idx];
            _working[idx]     = tmp;
            RefreshList(idx - 1);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (_lvRecipients.SelectedItems.Count == 0) return;
            int idx = _lvRecipients.SelectedIndices[0];
            if (idx == _working.Count - 1) return;
            var tmp           = _working[idx + 1];
            _working[idx + 1] = _working[idx];
            _working[idx]     = tmp;
            RefreshList(idx + 1);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            // Commit any pending edits for the currently selected row
            if (_lvRecipients.SelectedItems.Count > 0)
            {
                int idx = _lvRecipients.SelectedIndices[0];
                _working[idx].Name      = _txtName.Text.Trim();
                _working[idx].Email     = _txtEmail.Text.Trim();
                _working[idx].Position  = _txtPosition.Text.Trim();
                _working[idx].IsDefault = _chkDefault.Checked;
            }

            // Validate all rows: Name and Email are mandatory
            for (int i = 0; i < _working.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(_working[i].Name) ||
                    string.IsNullOrWhiteSpace(_working[i].Email))
                {
                    RefreshList(i);
                    MessageBox.Show($"Row {i + 1}: Name and Email are required.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private void ClearEdit()
        {
            _txtName.Text       = "";
            _txtEmail.Text      = "";
            _txtPosition.Text   = "";
            _chkDefault.Checked = false;
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool sel = _lvRecipients.SelectedItems.Count > 0;
            int  idx = sel ? _lvRecipients.SelectedIndices[0] : -1;
            _btnDelete.Enabled = sel;
            _btnUp.Enabled     = sel && idx > 0;
            _btnDown.Enabled   = sel && idx < _working.Count - 1;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (DialogResult == DialogResult.OK)
            {
                _original.Clear();
                _original.AddRange(_working);
            }
        }
    }
}
