using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Test
{
    internal sealed partial class JiraPresetsDialog : DarkForm
    {
        private readonly List<JiraPreset> _original;
        private readonly List<JiraPreset> _working;
        private bool _suppress;

        // Parameterless constructor for the VS designer
        public JiraPresetsDialog() : this(new List<JiraPreset>()) { }

        public JiraPresetsDialog(List<JiraPreset> presets)
        {

            _original = presets;
            _working  = presets.Select(p => new JiraPreset { Name = p.Name, Jql = p.Jql }).ToList();
            _resizable = true;

            InitializeComponent();
            this._lvPresets.Columns.Add("Name", 228);
            RegisterTitleBar(pnlHeader, showMin: true, showMax: true);

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;

            RefreshList(-1);
            if (_lvPresets.Items.Count > 0)
                _lvPresets.Items[0].Selected = true;
        }

        // ── List management ───────────────────────────────────────────────────
        private void RefreshList(int selectIdx)
        {
            _suppress = true;
            _lvPresets.BeginUpdate();
            _lvPresets.Items.Clear();
            foreach (var p in _working)
                _lvPresets.Items.Add(new ListViewItem(p.Name) { Tag = p });
            _lvPresets.EndUpdate();
            _suppress = false;

            if (selectIdx >= 0 && selectIdx < _lvPresets.Items.Count)
            {
                _lvPresets.Items[selectIdx].Selected = true;
                _lvPresets.Items[selectIdx].EnsureVisible();
            }
            UpdateButtonStates();
        }

        // ── Event handlers ────────────────────────────────────────────────────
        private void LvPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppress) return;
            if (_lvPresets.SelectedItems.Count == 0) { ClearEdit(); return; }
            var p         = (JiraPreset)_lvPresets.SelectedItems[0].Tag;
            _suppress     = true;
            _txtName.Text = p.Name;
            _txtJql.Text  = p.Jql;
            _suppress     = false;
            UpdateButtonStates();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (_lvPresets.SelectedItems.Count == 0) return;
            int idx                    = _lvPresets.SelectedIndices[0];
            _working[idx].Name         = _txtName.Text.Trim();
            _working[idx].Jql          = _txtJql.Text.Trim();
            _lvPresets.Items[idx].Text = _working[idx].Name;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            _working.Add(new JiraPreset { Name = "New Preset", Jql = "" });
            RefreshList(_working.Count - 1);
            _txtName.Focus();
            _txtName.SelectAll();
        }
        private void BtnCopy_Click(object sender, EventArgs e)
        {
            if (_lvPresets.SelectedItems.Count == 0) return;
            int idx = _lvPresets.SelectedIndices[0];
            _working.Add(new JiraPreset { Name = _working[idx].Name, Jql = _working[idx].Jql });
            RefreshList(_working.Count - 1);
            _txtName.Focus();
            _txtName.SelectAll();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_lvPresets.SelectedItems.Count == 0) return;
            int idx = _lvPresets.SelectedIndices[0];
            _working.RemoveAt(idx);
            RefreshList(Math.Min(idx, _working.Count - 1));
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (_lvPresets.SelectedItems.Count == 0) return;
            int idx           = _lvPresets.SelectedIndices[0];
            if (idx == 0) return;
            var tmp           = _working[idx - 1];
            _working[idx - 1] = _working[idx];
            _working[idx]     = tmp;
            RefreshList(idx - 1);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (_lvPresets.SelectedItems.Count == 0) return;
            int idx = _lvPresets.SelectedIndices[0];
            if (idx == _working.Count - 1) return;
            var tmp           = _working[idx + 1];
            _working[idx + 1] = _working[idx];
            _working[idx]     = tmp;
            RefreshList(idx + 1);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private void ClearEdit()
        {
            _txtName.Text = "";
            _txtJql.Text  = "";
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool sel       = _lvPresets.SelectedItems.Count > 0;
            int  idx       = sel ? _lvPresets.SelectedIndices[0] : -1;
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
