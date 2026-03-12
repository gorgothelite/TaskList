namespace Test
{
    partial class JiraPresetsDialog
    {
        private System.ComponentModel.IContainer components = null;

        // ── Header ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel  pnlHeader;
        private System.Windows.Forms.Label  _lblTitle;

        // ── Bottom button bar ─────────────────────────────────────────────────
        private System.Windows.Forms.Panel  _pnlBottom;
        private System.Windows.Forms.Button _btnNew;
        private System.Windows.Forms.Button _btnDelete;
        private System.Windows.Forms.Button _btnUp;
        private System.Windows.Forms.Button _btnDown;
        private System.Windows.Forms.Button _btnOk;
        private System.Windows.Forms.Button _btnCancel;

        // ── Main split ────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlMain;

        // Left – preset list
        private System.Windows.Forms.Panel    pnlLeft;
        private System.Windows.Forms.Label    _lblListCap;
        private System.Windows.Forms.ListView _lvPresets;

        // Right – edit pane
        private System.Windows.Forms.Panel   pnlRight;
        private System.Windows.Forms.Label   _lblNameCap;
        private System.Windows.Forms.TextBox _txtName;
        private System.Windows.Forms.Label   _lblJqlCap;
        private System.Windows.Forms.TextBox _txtJql;
        private System.Windows.Forms.Button  _btnApply;
        private System.Windows.Forms.Label   _lblHint;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader   = new System.Windows.Forms.Panel();
            this._lblTitle   = new System.Windows.Forms.Label();
            this._pnlBottom  = new System.Windows.Forms.Panel();
            this._btnNew     = new System.Windows.Forms.Button();
            this._btnDelete  = new System.Windows.Forms.Button();
            this._btnUp      = new System.Windows.Forms.Button();
            this._btnDown    = new System.Windows.Forms.Button();
            this._btnOk      = new System.Windows.Forms.Button();
            this._btnCancel  = new System.Windows.Forms.Button();
            this.pnlMain     = new System.Windows.Forms.Panel();
            this.pnlLeft     = new System.Windows.Forms.Panel();
            this._lblListCap = new System.Windows.Forms.Label();
            this._lvPresets  = new System.Windows.Forms.ListView();
            this.pnlRight    = new System.Windows.Forms.Panel();
            this._lblNameCap = new System.Windows.Forms.Label();
            this._txtName    = new System.Windows.Forms.TextBox();
            this._lblJqlCap  = new System.Windows.Forms.Label();
            this._txtJql     = new System.Windows.Forms.TextBox();
            this._btnApply   = new System.Windows.Forms.Button();
            this._lblHint    = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this._pnlBottom.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();

            // ── pnlHeader ────────────────────────────────────────────────────
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(46)))));
            this.pnlHeader.Controls.Add(this._lblTitle);
            this.pnlHeader.Dock     = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name     = "pnlHeader";
            this.pnlHeader.Size     = new System.Drawing.Size(720, 42);
            this.pnlHeader.TabIndex = 0;

            // _lblTitle
            this._lblTitle.AutoSize  = true;
            this._lblTitle.BackColor = System.Drawing.Color.Transparent;
            this._lblTitle.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.White;
            this._lblTitle.Location  = new System.Drawing.Point(12, 10);
            this._lblTitle.Name      = "_lblTitle";
            this._lblTitle.TabIndex  = 0;
            this._lblTitle.Text      = "Manage JQL Presets";

            // ── _pnlBottom ───────────────────────────────────────────────────
            this._pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this._pnlBottom.Controls.Add(this._btnNew);
            this._pnlBottom.Controls.Add(this._btnDelete);
            this._pnlBottom.Controls.Add(this._btnUp);
            this._pnlBottom.Controls.Add(this._btnDown);
            this._pnlBottom.Controls.Add(this._btnOk);
            this._pnlBottom.Controls.Add(this._btnCancel);
            this._pnlBottom.Dock     = System.Windows.Forms.DockStyle.Bottom;
            this._pnlBottom.Location = new System.Drawing.Point(0, 450);
            this._pnlBottom.Name     = "_pnlBottom";
            this._pnlBottom.Size     = new System.Drawing.Size(720, 50);
            this._pnlBottom.TabIndex = 2;

            // _btnNew
            this._btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(60)))));
            this._btnNew.FlatAppearance.BorderSize = 0;
            this._btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnNew.ForeColor = System.Drawing.Color.White;
            this._btnNew.Location  = new System.Drawing.Point(10, 10);
            this._btnNew.Name      = "_btnNew";
            this._btnNew.Size      = new System.Drawing.Size(100, 30);
            this._btnNew.TabIndex  = 0;
            this._btnNew.Text      = "+ New";
            this._btnNew.UseVisualStyleBackColor = false;
            this._btnNew.Click    += new System.EventHandler(this.BtnNew_Click);

            // _btnDelete
            this._btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this._btnDelete.FlatAppearance.BorderSize = 0;
            this._btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDelete.ForeColor = System.Drawing.Color.White;
            this._btnDelete.Location  = new System.Drawing.Point(118, 10);
            this._btnDelete.Name      = "_btnDelete";
            this._btnDelete.Size      = new System.Drawing.Size(100, 30);
            this._btnDelete.TabIndex  = 1;
            this._btnDelete.Text      = "Delete";
            this._btnDelete.UseVisualStyleBackColor = false;
            this._btnDelete.Click    += new System.EventHandler(this.BtnDelete_Click);

            // _btnUp
            this._btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnUp.FlatAppearance.BorderSize = 0;
            this._btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnUp.ForeColor = System.Drawing.Color.White;
            this._btnUp.Location  = new System.Drawing.Point(226, 10);
            this._btnUp.Name      = "_btnUp";
            this._btnUp.Size      = new System.Drawing.Size(100, 30);
            this._btnUp.TabIndex  = 2;
            this._btnUp.Text      = "\u25b2  Up";
            this._btnUp.UseVisualStyleBackColor = false;
            this._btnUp.Click    += new System.EventHandler(this.BtnUp_Click);

            // _btnDown
            this._btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnDown.FlatAppearance.BorderSize = 0;
            this._btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDown.ForeColor = System.Drawing.Color.White;
            this._btnDown.Location  = new System.Drawing.Point(334, 10);
            this._btnDown.Name      = "_btnDown";
            this._btnDown.Size      = new System.Drawing.Size(100, 30);
            this._btnDown.TabIndex  = 3;
            this._btnDown.Text      = "\u25bc  Down";
            this._btnDown.UseVisualStyleBackColor = false;
            this._btnDown.Click    += new System.EventHandler(this.BtnDown_Click);

            // _btnOk
            this._btnOk.Anchor    = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this._btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._btnOk.FlatAppearance.BorderSize = 0;
            this._btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnOk.ForeColor = System.Drawing.Color.White;
            this._btnOk.Location  = new System.Drawing.Point(502, 10);
            this._btnOk.Name      = "_btnOk";
            this._btnOk.Size      = new System.Drawing.Size(100, 30);
            this._btnOk.TabIndex  = 4;
            this._btnOk.Text      = "OK";
            this._btnOk.UseVisualStyleBackColor = false;
            this._btnOk.Click    += new System.EventHandler(this.BtnOk_Click);

            // _btnCancel
            this._btnCancel.Anchor       = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            this._btnCancel.BackColor    = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(78)))));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.FlatAppearance.BorderSize = 0;
            this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.ForeColor = System.Drawing.Color.White;
            this._btnCancel.Location  = new System.Drawing.Point(610, 10);
            this._btnCancel.Name      = "_btnCancel";
            this._btnCancel.Size      = new System.Drawing.Size(100, 30);
            this._btnCancel.TabIndex  = 5;
            this._btnCancel.Text      = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = false;

            // ── pnlMain ──────────────────────────────────────────────────────
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.pnlMain.Controls.Add(this.pnlRight);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Dock     = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 42);
            this.pnlMain.Name     = "pnlMain";
            this.pnlMain.Size     = new System.Drawing.Size(720, 408);
            this.pnlMain.TabIndex = 1;

            // ── pnlLeft ──────────────────────────────────────────────────────
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.pnlLeft.Controls.Add(this._lvPresets);
            this.pnlLeft.Controls.Add(this._lblListCap);
            this.pnlLeft.Dock     = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name     = "pnlLeft";
            this.pnlLeft.Size     = new System.Drawing.Size(230, 408);
            this.pnlLeft.TabIndex = 0;

            // _lblListCap
            this._lblListCap.AutoSize  = false;
            this._lblListCap.BackColor = System.Drawing.Color.Transparent;
            this._lblListCap.Dock      = System.Windows.Forms.DockStyle.Top;
            this._lblListCap.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblListCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblListCap.Location  = new System.Drawing.Point(0, 0);
            this._lblListCap.Name      = "_lblListCap";
            this._lblListCap.Padding   = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._lblListCap.Size      = new System.Drawing.Size(230, 24);
            this._lblListCap.TabIndex  = 0;
            this._lblListCap.Text      = "PRESETS";
            this._lblListCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // _lvPresets
            this._lvPresets.BackColor     = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this._lvPresets.BorderStyle   = System.Windows.Forms.BorderStyle.None;
            this._lvPresets.Dock          = System.Windows.Forms.DockStyle.Fill;
            this._lvPresets.Font          = new System.Drawing.Font("Segoe UI", 9F);
            this._lvPresets.ForeColor     = System.Drawing.Color.White;
            this._lvPresets.FullRowSelect = true;
            this._lvPresets.HeaderStyle   = System.Windows.Forms.ColumnHeaderStyle.None;
            this._lvPresets.HideSelection = false;
            this._lvPresets.MultiSelect   = false;
            this._lvPresets.Name          = "_lvPresets";
            this._lvPresets.TabIndex      = 1;
            this._lvPresets.View          = System.Windows.Forms.View.Details;
            this._lvPresets.Columns.Add("Name", 228);
            this._lvPresets.SelectedIndexChanged += new System.EventHandler(this.LvPresets_SelectedIndexChanged);

            // ── pnlRight ─────────────────────────────────────────────────────
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.pnlRight.Controls.Add(this._lblNameCap);
            this.pnlRight.Controls.Add(this._txtName);
            this.pnlRight.Controls.Add(this._lblJqlCap);
            this.pnlRight.Controls.Add(this._txtJql);
            this.pnlRight.Controls.Add(this._btnApply);
            this.pnlRight.Controls.Add(this._lblHint);
            this.pnlRight.Dock     = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(230, 0);
            this.pnlRight.Name     = "pnlRight";
            this.pnlRight.Padding  = new System.Windows.Forms.Padding(14, 8, 10, 8);
            this.pnlRight.Size     = new System.Drawing.Size(490, 408);
            this.pnlRight.TabIndex = 1;

            // _lblNameCap
            this._lblNameCap.AutoSize  = true;
            this._lblNameCap.BackColor = System.Drawing.Color.Transparent;
            this._lblNameCap.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblNameCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblNameCap.Location  = new System.Drawing.Point(14, 16);
            this._lblNameCap.Name      = "_lblNameCap";
            this._lblNameCap.TabIndex  = 0;
            this._lblNameCap.Text      = "PRESET NAME";

            // _txtName
            this._txtName.Anchor      = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this._txtName.BackColor   = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtName.ForeColor   = System.Drawing.Color.White;
            this._txtName.Location    = new System.Drawing.Point(14, 32);
            this._txtName.Name        = "_txtName";
            this._txtName.Size        = new System.Drawing.Size(452, 24);
            this._txtName.TabIndex    = 1;

            // _lblJqlCap
            this._lblJqlCap.AutoSize  = true;
            this._lblJqlCap.BackColor = System.Drawing.Color.Transparent;
            this._lblJqlCap.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblJqlCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblJqlCap.Location  = new System.Drawing.Point(14, 68);
            this._lblJqlCap.Name      = "_lblJqlCap";
            this._lblJqlCap.TabIndex  = 2;
            this._lblJqlCap.Text      = "JQL QUERY";

            // _txtJql
            this._txtJql.AcceptsReturn = true;
            this._txtJql.Anchor        = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this._txtJql.BackColor     = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtJql.BorderStyle   = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtJql.ForeColor     = System.Drawing.Color.White;
            this._txtJql.Location      = new System.Drawing.Point(14, 84);
            this._txtJql.Multiline     = true;
            this._txtJql.Name          = "_txtJql";
            this._txtJql.ScrollBars    = System.Windows.Forms.ScrollBars.Vertical;
            this._txtJql.Size          = new System.Drawing.Size(452, 110);
            this._txtJql.TabIndex      = 3;

            // _btnApply
            this._btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(175)))));
            this._btnApply.FlatAppearance.BorderSize = 0;
            this._btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnApply.ForeColor = System.Drawing.Color.White;
            this._btnApply.Location  = new System.Drawing.Point(14, 206);
            this._btnApply.Name      = "_btnApply";
            this._btnApply.Size      = new System.Drawing.Size(140, 28);
            this._btnApply.TabIndex  = 4;
            this._btnApply.Text      = "Apply Changes";
            this._btnApply.UseVisualStyleBackColor = false;
            this._btnApply.Click    += new System.EventHandler(this.BtnApply_Click);

            // _lblHint
            this._lblHint.AutoSize  = true;
            this._lblHint.BackColor = System.Drawing.Color.Transparent;
            this._lblHint.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this._lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
            this._lblHint.Location  = new System.Drawing.Point(14, 246);
            this._lblHint.Name      = "_lblHint";
            this._lblHint.TabIndex  = 5;
            this._lblHint.Text      = "Select a preset to edit its name and JQL.\r\nClick \"Apply Changes\" to save edits to the list.";

            // ── JiraPresetsDialog ─────────────────────────────────────────────
            this.AcceptButton        = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.CancelButton        = this._btnCancel;
            this.ClientSize          = new System.Drawing.Size(720, 500);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this._pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor       = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize     = new System.Drawing.Size(580, 400);
            this.Name            = "JiraPresetsDialog";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Manage JQL Presets";

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this._pnlBottom.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
