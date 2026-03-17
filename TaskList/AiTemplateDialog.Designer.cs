namespace Test
{
    partial class AiTemplateDialog
    {
        private System.ComponentModel.IContainer components = null;

        // ── Header ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel  pnlHeader;
        private System.Windows.Forms.Label  _lblTitle;

        // ── Left panel (template list) ────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlLeft;
        private System.Windows.Forms.ListView _lvTemplates;
        private System.Windows.Forms.Button   _btnNew;
        private System.Windows.Forms.Button   _btnDelete;
        private System.Windows.Forms.Button   _btnUp;
        private System.Windows.Forms.Button   _btnDown;

        // ── Right panel (editor) ──────────────────────────────────────────────
        private System.Windows.Forms.Panel   pnlRight;
        private System.Windows.Forms.Label   _lblNameCaption;
        private System.Windows.Forms.TextBox _txtName;
        private System.Windows.Forms.Label   _lblTemplateCaption;
        private System.Windows.Forms.TextBox _txtTemplate;

        // ── Bottom strip ──────────────────────────────────────────────────────
        private System.Windows.Forms.Panel  pnlBottom;
        private System.Windows.Forms.Button _btnOk;
        private System.Windows.Forms.Button _btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader           = new System.Windows.Forms.Panel();
            this._lblTitle           = new System.Windows.Forms.Label();
            this.pnlLeft             = new System.Windows.Forms.Panel();
            this._lvTemplates        = new System.Windows.Forms.ListView();
            this._btnNew             = new System.Windows.Forms.Button();
            this._btnDelete          = new System.Windows.Forms.Button();
            this._btnUp              = new System.Windows.Forms.Button();
            this._btnDown            = new System.Windows.Forms.Button();
            this.pnlRight            = new System.Windows.Forms.Panel();
            this._lblNameCaption     = new System.Windows.Forms.Label();
            this._txtName            = new System.Windows.Forms.TextBox();
            this._lblTemplateCaption = new System.Windows.Forms.Label();
            this._txtTemplate        = new System.Windows.Forms.TextBox();
            this.pnlBottom           = new System.Windows.Forms.Panel();
            this._btnOk              = new System.Windows.Forms.Button();
            this._btnCancel          = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(44, 44, 46);
            this.pnlHeader.Controls.Add(this._lblTitle);
            this.pnlHeader.Dock     = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Name     = "pnlHeader";
            this.pnlHeader.Size     = new System.Drawing.Size(780, 42);
            this.pnlHeader.TabIndex = 0;

            // _lblTitle
            this._lblTitle.AutoSize  = true;
            this._lblTitle.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.White;
            this._lblTitle.Location  = new System.Drawing.Point(12, 10);
            this._lblTitle.Name      = "_lblTitle";
            this._lblTitle.TabIndex  = 0;
            this._lblTitle.Text      = "Manage AI Prompt Templates";

            // pnlBottom
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(36, 36, 40);
            this.pnlBottom.Controls.Add(this._btnOk);
            this.pnlBottom.Controls.Add(this._btnCancel);
            this.pnlBottom.Dock     = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Name     = "pnlBottom";
            this.pnlBottom.Size     = new System.Drawing.Size(780, 44);
            this.pnlBottom.TabIndex = 3;

            // _btnOk
            this._btnOk.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this._btnOk.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this._btnOk.FlatAppearance.BorderSize = 0;
            this._btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnOk.ForeColor = System.Drawing.Color.White;
            this._btnOk.Location  = new System.Drawing.Point(636, 8);
            this._btnOk.Name      = "_btnOk";
            this._btnOk.Size      = new System.Drawing.Size(70, 28);
            this._btnOk.TabIndex  = 0;
            this._btnOk.Text      = "OK";
            this._btnOk.UseVisualStyleBackColor = false;
            this._btnOk.Click    += new System.EventHandler(this.BtnOk_Click);

            // _btnCancel
            this._btnCancel.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this._btnCancel.BackColor = System.Drawing.Color.FromArgb(70, 70, 78);
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.FlatAppearance.BorderSize = 0;
            this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.ForeColor = System.Drawing.Color.White;
            this._btnCancel.Location  = new System.Drawing.Point(716, 8);
            this._btnCancel.Name      = "_btnCancel";
            this._btnCancel.Size      = new System.Drawing.Size(54, 28);
            this._btnCancel.TabIndex  = 1;
            this._btnCancel.Text      = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = false;

            // pnlLeft
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(36, 36, 40);
            this.pnlLeft.Controls.Add(this._lvTemplates);
            this.pnlLeft.Controls.Add(this._btnNew);
            this.pnlLeft.Controls.Add(this._btnDelete);
            this.pnlLeft.Controls.Add(this._btnUp);
            this.pnlLeft.Controls.Add(this._btnDown);
            this.pnlLeft.Dock     = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Name     = "pnlLeft";
            this.pnlLeft.Padding  = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Size     = new System.Drawing.Size(234, 474);
            this.pnlLeft.TabIndex = 1;

            // _lvTemplates
            this._lvTemplates.Anchor      = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                          | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this._lvTemplates.BackColor   = System.Drawing.Color.FromArgb(28, 28, 30);
            this._lvTemplates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lvTemplates.Font        = new System.Drawing.Font("Segoe UI", 9.5F);
            this._lvTemplates.ForeColor   = System.Drawing.Color.White;
            this._lvTemplates.FullRowSelect = true;
            this._lvTemplates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this._lvTemplates.HideSelection = false;
            this._lvTemplates.Location    = new System.Drawing.Point(10, 10);
            this._lvTemplates.MultiSelect = false;
            this._lvTemplates.Name        = "_lvTemplates";
            this._lvTemplates.Size        = new System.Drawing.Size(212, 390);
            this._lvTemplates.TabIndex    = 0;
            this._lvTemplates.UseCompatibleStateImageBehavior = false;
            this._lvTemplates.View        = System.Windows.Forms.View.Details;
            this._lvTemplates.Columns.Add("Name", 210);
            this._lvTemplates.SelectedIndexChanged += new System.EventHandler(this.LvTemplates_SelectedIndexChanged);

            // _btnNew
            this._btnNew.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this._btnNew.FlatAppearance.BorderSize = 0;
            this._btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnNew.ForeColor = System.Drawing.Color.White;
            this._btnNew.Location  = new System.Drawing.Point(10, 408);
            this._btnNew.Name      = "_btnNew";
            this._btnNew.Size      = new System.Drawing.Size(56, 26);
            this._btnNew.TabIndex  = 1;
            this._btnNew.Text      = "New";
            this._btnNew.UseVisualStyleBackColor = false;
            this._btnNew.Click    += new System.EventHandler(this.BtnNew_Click);

            // _btnDelete
            this._btnDelete.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);
            this._btnDelete.Enabled   = false;
            this._btnDelete.FlatAppearance.BorderSize = 0;
            this._btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDelete.ForeColor = System.Drawing.Color.White;
            this._btnDelete.Location  = new System.Drawing.Point(74, 408);
            this._btnDelete.Name      = "_btnDelete";
            this._btnDelete.Size      = new System.Drawing.Size(56, 26);
            this._btnDelete.TabIndex  = 2;
            this._btnDelete.Text      = "Delete";
            this._btnDelete.UseVisualStyleBackColor = false;
            this._btnDelete.Click    += new System.EventHandler(this.BtnDelete_Click);

            // _btnUp
            this._btnUp.BackColor = System.Drawing.Color.FromArgb(60, 60, 70);
            this._btnUp.Enabled   = false;
            this._btnUp.FlatAppearance.BorderSize = 0;
            this._btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnUp.ForeColor = System.Drawing.Color.White;
            this._btnUp.Location  = new System.Drawing.Point(10, 442);
            this._btnUp.Name      = "_btnUp";
            this._btnUp.Size      = new System.Drawing.Size(56, 26);
            this._btnUp.TabIndex  = 3;
            this._btnUp.Text      = "↑ Up";
            this._btnUp.UseVisualStyleBackColor = false;
            this._btnUp.Click    += new System.EventHandler(this.BtnUp_Click);

            // _btnDown
            this._btnDown.BackColor = System.Drawing.Color.FromArgb(60, 60, 70);
            this._btnDown.Enabled   = false;
            this._btnDown.FlatAppearance.BorderSize = 0;
            this._btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDown.ForeColor = System.Drawing.Color.White;
            this._btnDown.Location  = new System.Drawing.Point(74, 442);
            this._btnDown.Name      = "_btnDown";
            this._btnDown.Size      = new System.Drawing.Size(62, 26);
            this._btnDown.TabIndex  = 4;
            this._btnDown.Text      = "↓ Down";
            this._btnDown.UseVisualStyleBackColor = false;
            this._btnDown.Click    += new System.EventHandler(this.BtnDown_Click);

            // pnlRight
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            this.pnlRight.Controls.Add(this._lblNameCaption);
            this.pnlRight.Controls.Add(this._txtName);
            this.pnlRight.Controls.Add(this._lblTemplateCaption);
            this.pnlRight.Controls.Add(this._txtTemplate);
            this.pnlRight.Dock     = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Name     = "pnlRight";
            this.pnlRight.Padding  = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.pnlRight.TabIndex = 2;

            // _lblNameCaption
            this._lblNameCaption.AutoSize  = true;
            this._lblNameCaption.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblNameCaption.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this._lblNameCaption.Location  = new System.Drawing.Point(14, 14);
            this._lblNameCaption.Name      = "_lblNameCaption";
            this._lblNameCaption.TabIndex  = 0;
            this._lblNameCaption.Text      = "NAME";

            // _txtName
            this._txtName.Anchor      = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right;
            this._txtName.BackColor   = System.Drawing.Color.FromArgb(55, 55, 60);
            this._txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtName.ForeColor   = System.Drawing.Color.White;
            this._txtName.Location    = new System.Drawing.Point(14, 28);
            this._txtName.Name        = "_txtName";
            this._txtName.Size        = new System.Drawing.Size(520, 24);
            this._txtName.TabIndex    = 1;

            // _lblTemplateCaption
            this._lblTemplateCaption.AutoSize  = true;
            this._lblTemplateCaption.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblTemplateCaption.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this._lblTemplateCaption.Location  = new System.Drawing.Point(14, 62);
            this._lblTemplateCaption.Name      = "_lblTemplateCaption";
            this._lblTemplateCaption.TabIndex  = 2;
            this._lblTemplateCaption.Text      = "TEMPLATE  \u2014  use {DATA} where statistics & log entries should appear";

            // _txtTemplate — anchored to all four sides so it fills the remaining space
            this._txtTemplate.AcceptsReturn = true;
            this._txtTemplate.Anchor        = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                            | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this._txtTemplate.BackColor     = System.Drawing.Color.FromArgb(55, 55, 60);
            this._txtTemplate.BorderStyle   = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtTemplate.ForeColor     = System.Drawing.Color.White;
            this._txtTemplate.Location      = new System.Drawing.Point(14, 76);
            this._txtTemplate.Multiline     = true;
            this._txtTemplate.Name          = "_txtTemplate";
            this._txtTemplate.ScrollBars    = System.Windows.Forms.ScrollBars.Vertical;
            this._txtTemplate.Size          = new System.Drawing.Size(520, 388);
            this._txtTemplate.TabIndex      = 3;

            // AiTemplateDialog
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(37, 37, 38);
            this.CancelButton        = this._btnCancel;
            this.ClientSize          = new System.Drawing.Size(780, 560);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Font                = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor           = System.Drawing.Color.White;
            this.MaximizeBox         = false;
            this.MinimumSize         = new System.Drawing.Size(640, 500);
            this.Name                = "AiTemplateDialog";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text                = "AI Prompt Templates";

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
