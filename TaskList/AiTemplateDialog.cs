using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Test
{
    internal sealed partial class AiTemplateDialog : DarkForm
    {
        private readonly List<SummaryTemplate> _original;
        private readonly List<SummaryTemplate> _working;
        private bool _suppress;

        public AiTemplateDialog() : this(new List<SummaryTemplate>()) { }

        public AiTemplateDialog(List<SummaryTemplate> templates)
        {
            _original = templates;
            _working  = templates.Select(t => new SummaryTemplate { Name = t.Name, Template = t.Template }).ToList();
            _resizable = true;

            InitializeComponent();
            RegisterTitleBar(pnlHeader, showMin: true, showMax: false);

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            RefreshList(-1);
            if (_lvTemplates.Items.Count > 0)
                _lvTemplates.Items[0].Selected = true;
        }

        // ── List management ───────────────────────────────────────────────────
        private void RefreshList(int selectIdx)
        {
            _suppress = true;
            _lvTemplates.BeginUpdate();
            _lvTemplates.Items.Clear();
            foreach (var t in _working)
                _lvTemplates.Items.Add(new ListViewItem(t.Name) { Tag = t });
            _lvTemplates.EndUpdate();
            _suppress = false;

            if (selectIdx >= 0 && selectIdx < _lvTemplates.Items.Count)
            {
                _lvTemplates.Items[selectIdx].Selected = true;
                _lvTemplates.Items[selectIdx].EnsureVisible();
            }
            UpdateButtonStates();
        }

        // ── Event handlers ────────────────────────────────────────────────────
        private void LvTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppress) return;
            if (_lvTemplates.SelectedItems.Count == 0) { ClearEdit(); return; }
            var t         = (SummaryTemplate)_lvTemplates.SelectedItems[0].Tag;
            _suppress         = true;
            _txtName.Text     = t.Name;
            _txtTemplate.Text = t.Template;
            _suppress         = false;
            UpdateButtonStates();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            _working.Add(new SummaryTemplate { Name = "New Template", Template = "" });
            RefreshList(_working.Count - 1);
            _txtName.Focus();
            _txtName.SelectAll();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_lvTemplates.SelectedItems.Count == 0) return;
            int idx = _lvTemplates.SelectedIndices[0];
            _working.RemoveAt(idx);
            RefreshList(Math.Min(idx, _working.Count - 1));
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (_lvTemplates.SelectedItems.Count == 0) return;
            int idx           = _lvTemplates.SelectedIndices[0];
            if (idx == 0) return;
            var tmp           = _working[idx - 1];
            _working[idx - 1] = _working[idx];
            _working[idx]     = tmp;
            RefreshList(idx - 1);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (_lvTemplates.SelectedItems.Count == 0) return;
            int idx = _lvTemplates.SelectedIndices[0];
            if (idx == _working.Count - 1) return;
            var tmp           = _working[idx + 1];
            _working[idx + 1] = _working[idx];
            _working[idx]     = tmp;
            RefreshList(idx + 1);
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (_lvTemplates.SelectedItems.Count == 0) return;
            int idx                        = _lvTemplates.SelectedIndices[0];
            _working[idx].Name             = _txtName.Text.Trim();
            _working[idx].Template         = _txtTemplate.Text;
            _lvTemplates.Items[idx].Text   = _working[idx].Name;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private void ClearEdit()
        {
            _txtName.Text     = "";
            _txtTemplate.Text = "";
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool sel = _lvTemplates.SelectedItems.Count > 0;
            int  idx = sel ? _lvTemplates.SelectedIndices[0] : -1;
            _btnDelete.Enabled = sel;
            _btnApply.Enabled  = sel;
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
