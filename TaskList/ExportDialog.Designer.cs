namespace Test
{
    partial class ExportDialog
    {
        private System.ComponentModel.IContainer components = null;

        // ── Status filter ─────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox   grpStatus;
        private System.Windows.Forms.RadioButton _radAll;
        private System.Windows.Forms.RadioButton _radActive;
        private System.Windows.Forms.RadioButton _radDone;

        // ── Priority filter ───────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox   grpPriority;
        private System.Windows.Forms.CheckBox   _chkLow;
        private System.Windows.Forms.CheckBox   _chkMedium;
        private System.Windows.Forms.CheckBox   _chkHigh;
        private System.Windows.Forms.CheckBox   _chkCritical;

        // ── Column selection ──────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox   grpColumns;
        private System.Windows.Forms.CheckBox   _chkColName;
        private System.Windows.Forms.CheckBox   _chkColPriority;
        private System.Windows.Forms.CheckBox   _chkColDue;
        private System.Windows.Forms.CheckBox   _chkColStatus;
        private System.Windows.Forms.CheckBox   _chkColNotes;

        // ── Preview ───────────────────────────────────────────────────────────
        private System.Windows.Forms.Label      _lblCount;
        private System.Windows.Forms.TextBox    _txtPreview;

        // ── Buttons ───────────────────────────────────────────────────────────
        private System.Windows.Forms.Button _btnExport;
        private System.Windows.Forms.Button _btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components      = new System.ComponentModel.Container();
            this.grpStatus       = new System.Windows.Forms.GroupBox();
            this._radAll         = new System.Windows.Forms.RadioButton();
            this._radActive      = new System.Windows.Forms.RadioButton();
            this._radDone        = new System.Windows.Forms.RadioButton();
            this.grpPriority     = new System.Windows.Forms.GroupBox();
            this._chkLow         = new System.Windows.Forms.CheckBox();
            this._chkMedium      = new System.Windows.Forms.CheckBox();
            this._chkHigh        = new System.Windows.Forms.CheckBox();
            this._chkCritical    = new System.Windows.Forms.CheckBox();
            this.grpColumns      = new System.Windows.Forms.GroupBox();
            this._chkColName     = new System.Windows.Forms.CheckBox();
            this._chkColPriority = new System.Windows.Forms.CheckBox();
            this._chkColDue      = new System.Windows.Forms.CheckBox();
            this._chkColStatus   = new System.Windows.Forms.CheckBox();
            this._chkColNotes    = new System.Windows.Forms.CheckBox();
            this._lblCount       = new System.Windows.Forms.Label();
            this._txtPreview     = new System.Windows.Forms.TextBox();
            this._btnExport      = new System.Windows.Forms.Button();
            this._btnCancel      = new System.Windows.Forms.Button();

            this.grpStatus.SuspendLayout();
            this.grpPriority.SuspendLayout();
            this.grpColumns.SuspendLayout();
            this.SuspendLayout();

            // ── grpStatus ────────────────────────────────────────────────────
            this.grpStatus.Controls.Add(this._radAll);
            this.grpStatus.Controls.Add(this._radActive);
            this.grpStatus.Controls.Add(this._radDone);
            this.grpStatus.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this.grpStatus.Location  = new System.Drawing.Point(14, 14);
            this.grpStatus.Name      = "grpStatus";
            this.grpStatus.Size      = new System.Drawing.Size(180, 98);
            this.grpStatus.TabIndex  = 0;
            this.grpStatus.Text      = "Status";

            this._radAll.AutoSize  = true;
            this._radAll.Checked   = true;
            this._radAll.ForeColor = System.Drawing.Color.White;
            this._radAll.Location  = new System.Drawing.Point(14, 24);
            this._radAll.Name      = "_radAll";
            this._radAll.TabIndex  = 0;
            this._radAll.Text      = "All";
            this._radAll.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            this._radActive.AutoSize  = true;
            this._radActive.ForeColor = System.Drawing.Color.White;
            this._radActive.Location  = new System.Drawing.Point(14, 48);
            this._radActive.Name      = "_radActive";
            this._radActive.TabIndex  = 1;
            this._radActive.Text      = "Active only";
            this._radActive.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            this._radDone.AutoSize  = true;
            this._radDone.ForeColor = System.Drawing.Color.White;
            this._radDone.Location  = new System.Drawing.Point(14, 70);
            this._radDone.Name      = "_radDone";
            this._radDone.TabIndex  = 2;
            this._radDone.Text      = "Done only";
            this._radDone.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            // ── grpPriority ───────────────────────────────────────────────────
            this.grpPriority.Controls.Add(this._chkLow);
            this.grpPriority.Controls.Add(this._chkMedium);
            this.grpPriority.Controls.Add(this._chkHigh);
            this.grpPriority.Controls.Add(this._chkCritical);
            this.grpPriority.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this.grpPriority.Location  = new System.Drawing.Point(206, 14);
            this.grpPriority.Name      = "grpPriority";
            this.grpPriority.Size      = new System.Drawing.Size(180, 98);
            this.grpPriority.TabIndex  = 1;
            this.grpPriority.Text      = "Priority";

            this._chkLow.AutoSize  = true;
            this._chkLow.Checked   = true;
            this._chkLow.ForeColor = System.Drawing.Color.FromArgb(88, 196, 88);
            this._chkLow.Location  = new System.Drawing.Point(14, 24);
            this._chkLow.Name      = "_chkLow";
            this._chkLow.TabIndex  = 0;
            this._chkLow.Text      = "Low";
            this._chkLow.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            this._chkMedium.AutoSize  = true;
            this._chkMedium.Checked   = true;
            this._chkMedium.ForeColor = System.Drawing.Color.FromArgb(214, 188, 50);
            this._chkMedium.Location  = new System.Drawing.Point(80, 24);
            this._chkMedium.Name      = "_chkMedium";
            this._chkMedium.TabIndex  = 1;
            this._chkMedium.Text      = "Medium";
            this._chkMedium.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            this._chkHigh.AutoSize  = true;
            this._chkHigh.Checked   = true;
            this._chkHigh.ForeColor = System.Drawing.Color.FromArgb(232, 116, 40);
            this._chkHigh.Location  = new System.Drawing.Point(14, 52);
            this._chkHigh.Name      = "_chkHigh";
            this._chkHigh.TabIndex  = 2;
            this._chkHigh.Text      = "High";
            this._chkHigh.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            this._chkCritical.AutoSize  = true;
            this._chkCritical.Checked   = true;
            this._chkCritical.ForeColor = System.Drawing.Color.FromArgb(222, 52, 52);
            this._chkCritical.Location  = new System.Drawing.Point(80, 52);
            this._chkCritical.Name      = "_chkCritical";
            this._chkCritical.TabIndex  = 3;
            this._chkCritical.Text      = "Critical";
            this._chkCritical.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            // ── grpColumns ────────────────────────────────────────────────────
            this.grpColumns.Controls.Add(this._chkColName);
            this.grpColumns.Controls.Add(this._chkColPriority);
            this.grpColumns.Controls.Add(this._chkColDue);
            this.grpColumns.Controls.Add(this._chkColStatus);
            this.grpColumns.Controls.Add(this._chkColNotes);
            this.grpColumns.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this.grpColumns.Location  = new System.Drawing.Point(398, 14);
            this.grpColumns.Name      = "grpColumns";
            this.grpColumns.Size      = new System.Drawing.Size(294, 98);
            this.grpColumns.TabIndex  = 2;
            this.grpColumns.Text      = "Columns to export";

            this._chkColName.AutoSize  = true;
            this._chkColName.Checked   = true;
            this._chkColName.ForeColor = System.Drawing.Color.White;
            this._chkColName.Location  = new System.Drawing.Point(14, 24);
            this._chkColName.Name      = "_chkColName";
            this._chkColName.TabIndex  = 0;
            this._chkColName.Text      = "Name";
            this._chkColName.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            this._chkColPriority.AutoSize  = true;
            this._chkColPriority.Checked   = true;
            this._chkColPriority.ForeColor = System.Drawing.Color.White;
            this._chkColPriority.Location  = new System.Drawing.Point(80, 24);
            this._chkColPriority.Name      = "_chkColPriority";
            this._chkColPriority.TabIndex  = 1;
            this._chkColPriority.Text      = "Priority";
            this._chkColPriority.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            this._chkColDue.AutoSize  = true;
            this._chkColDue.Checked   = true;
            this._chkColDue.ForeColor = System.Drawing.Color.White;
            this._chkColDue.Location  = new System.Drawing.Point(162, 24);
            this._chkColDue.Name      = "_chkColDue";
            this._chkColDue.TabIndex  = 2;
            this._chkColDue.Text      = "Due Date";
            this._chkColDue.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            this._chkColStatus.AutoSize  = true;
            this._chkColStatus.Checked   = true;
            this._chkColStatus.ForeColor = System.Drawing.Color.White;
            this._chkColStatus.Location  = new System.Drawing.Point(14, 52);
            this._chkColStatus.Name      = "_chkColStatus";
            this._chkColStatus.TabIndex  = 3;
            this._chkColStatus.Text      = "Status";
            this._chkColStatus.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            this._chkColNotes.AutoSize  = true;
            this._chkColNotes.Checked   = true;
            this._chkColNotes.ForeColor = System.Drawing.Color.White;
            this._chkColNotes.Location  = new System.Drawing.Point(80, 52);
            this._chkColNotes.Name      = "_chkColNotes";
            this._chkColNotes.TabIndex  = 4;
            this._chkColNotes.Text      = "Notes";
            this._chkColNotes.CheckedChanged += new System.EventHandler(this.Filter_Changed);

            // ── _lblCount ─────────────────────────────────────────────────────
            this._lblCount.AutoSize  = true;
            this._lblCount.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this._lblCount.Location  = new System.Drawing.Point(14, 124);
            this._lblCount.Name      = "_lblCount";
            this._lblCount.TabIndex  = 3;
            this._lblCount.Text      = "0 task(s) will be exported";

            // ── _txtPreview ───────────────────────────────────────────────────
            this._txtPreview.Anchor      = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this._txtPreview.BackColor   = System.Drawing.Color.FromArgb(28, 28, 30);
            this._txtPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtPreview.Font        = new System.Drawing.Font("Consolas", 8.5F);
            this._txtPreview.ForeColor   = System.Drawing.Color.FromArgb(180, 180, 190);
            this._txtPreview.Location    = new System.Drawing.Point(14, 144);
            this._txtPreview.Multiline   = true;
            this._txtPreview.Name        = "_txtPreview";
            this._txtPreview.ReadOnly    = true;
            this._txtPreview.ScrollBars  = System.Windows.Forms.ScrollBars.Both;
            this._txtPreview.Size        = new System.Drawing.Size(678, 248);
            this._txtPreview.TabIndex    = 4;
            this._txtPreview.WordWrap    = false;

            // ── _btnExport ────────────────────────────────────────────────────
            this._btnExport.Anchor    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this._btnExport.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this._btnExport.FlatAppearance.BorderSize = 0;
            this._btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnExport.ForeColor = System.Drawing.Color.White;
            this._btnExport.Location  = new System.Drawing.Point(504, 406);
            this._btnExport.Name      = "_btnExport";
            this._btnExport.Size      = new System.Drawing.Size(100, 28);
            this._btnExport.TabIndex  = 5;
            this._btnExport.Text      = "Export CSV…";
            this._btnExport.Click    += new System.EventHandler(this.BtnExport_Click);

            // ── _btnCancel ────────────────────────────────────────────────────
            this._btnCancel.Anchor       = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this._btnCancel.BackColor    = System.Drawing.Color.FromArgb(70, 70, 78);
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.FlatAppearance.BorderSize = 0;
            this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.ForeColor = System.Drawing.Color.White;
            this._btnCancel.Location  = new System.Drawing.Point(614, 406);
            this._btnCancel.Name      = "_btnCancel";
            this._btnCancel.Size      = new System.Drawing.Size(78, 28);
            this._btnCancel.TabIndex  = 6;
            this._btnCancel.Text      = "Cancel";

            // ── ExportDialog ──────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(37, 37, 38);
            this.CancelButton        = this._btnCancel;
            this.ClientSize          = new System.Drawing.Size(706, 448);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.grpPriority);
            this.Controls.Add(this.grpColumns);
            this.Controls.Add(this._lblCount);
            this.Controls.Add(this._txtPreview);
            this.Controls.Add(this._btnExport);
            this.Controls.Add(this._btnCancel);
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor       = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "ExportDialog";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Export Tasks to CSV";

            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpPriority.ResumeLayout(false);
            this.grpPriority.PerformLayout();
            this.grpColumns.ResumeLayout(false);
            this.grpColumns.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
