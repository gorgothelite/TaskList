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

        // ── Right panel (editor) ──────────────────────────────────────────────
        private System.Windows.Forms.Panel   pnlRight;
        private System.Windows.Forms.Label   _lblNameCaption;
        private System.Windows.Forms.TextBox _txtName;
        private System.Windows.Forms.Label   _lblTemplateCaption;
        private System.Windows.Forms.TextBox _txtTemplate;
        private System.Windows.Forms.Button  _btnApply;

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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this._lblTitle = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this._lvTemplates = new System.Windows.Forms.ListView();
            this.pnlRight = new System.Windows.Forms.Panel();
            this._lblNameCaption = new System.Windows.Forms.Label();
            this._txtName = new System.Windows.Forms.TextBox();
            this._lblTemplateCaption = new System.Windows.Forms.Label();
            this._txtTemplate = new System.Windows.Forms.TextBox();
            this._btnApply = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this._btnOk = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnNew = new System.Windows.Forms.Button();
            this._btnDelete = new System.Windows.Forms.Button();
            this._btnCopy = new System.Windows.Forms.Button();
            this._btnUp = new System.Windows.Forms.Button();
            this._btnDown = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(46)))));
            this.pnlHeader.Controls.Add(this._lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(780, 42);
            this.pnlHeader.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.White;
            this._lblTitle.Location = new System.Drawing.Point(12, 10);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(218, 20);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "Manage AI Prompt Templates";
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.pnlLeft.Controls.Add(this._lvTemplates);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 42);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Size = new System.Drawing.Size(234, 431);
            this.pnlLeft.TabIndex = 1;
            // 
            // _lvTemplates
            // 
            this._lvTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lvTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this._lvTemplates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lvTemplates.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._lvTemplates.ForeColor = System.Drawing.Color.White;
            this._lvTemplates.FullRowSelect = true;
            this._lvTemplates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this._lvTemplates.HideSelection = false;
            this._lvTemplates.Location = new System.Drawing.Point(10, 10);
            this._lvTemplates.MultiSelect = false;
            this._lvTemplates.Name = "_lvTemplates";
            this._lvTemplates.Size = new System.Drawing.Size(212, 408);
            this._lvTemplates.TabIndex = 0;
            this._lvTemplates.UseCompatibleStateImageBehavior = false;
            this._lvTemplates.View = System.Windows.Forms.View.Details;
            this._lvTemplates.SelectedIndexChanged += new System.EventHandler(this.LvTemplates_SelectedIndexChanged);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.pnlRight.Controls.Add(this._lblNameCaption);
            this.pnlRight.Controls.Add(this._txtName);
            this.pnlRight.Controls.Add(this._lblTemplateCaption);
            this.pnlRight.Controls.Add(this._txtTemplate);
            this.pnlRight.Controls.Add(this._btnApply);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(234, 42);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.pnlRight.Size = new System.Drawing.Size(546, 431);
            this.pnlRight.TabIndex = 2;
            // 
            // _lblNameCaption
            // 
            this._lblNameCaption.AutoSize = true;
            this._lblNameCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblNameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblNameCaption.Location = new System.Drawing.Point(14, 14);
            this._lblNameCaption.Name = "_lblNameCaption";
            this._lblNameCaption.Size = new System.Drawing.Size(35, 12);
            this._lblNameCaption.TabIndex = 0;
            this._lblNameCaption.Text = "NAME";
            // 
            // _txtName
            // 
            this._txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtName.ForeColor = System.Drawing.Color.White;
            this._txtName.Location = new System.Drawing.Point(14, 28);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(522, 24);
            this._txtName.TabIndex = 1;
            // 
            // _lblTemplateCaption
            // 
            this._lblTemplateCaption.AutoSize = true;
            this._lblTemplateCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblTemplateCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblTemplateCaption.Location = new System.Drawing.Point(14, 62);
            this._lblTemplateCaption.Name = "_lblTemplateCaption";
            this._lblTemplateCaption.Size = new System.Drawing.Size(324, 12);
            this._lblTemplateCaption.TabIndex = 2;
            this._lblTemplateCaption.Text = "TEMPLATE  —  use {DATA} where statistics & log entries should appear";
            // 
            // _txtTemplate
            // 
            this._txtTemplate.AcceptsReturn = true;
            this._txtTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtTemplate.ForeColor = System.Drawing.Color.White;
            this._txtTemplate.Location = new System.Drawing.Point(14, 76);
            this._txtTemplate.Multiline = true;
            this._txtTemplate.Name = "_txtTemplate";
            this._txtTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtTemplate.Size = new System.Drawing.Size(522, 303);
            this._txtTemplate.TabIndex = 3;
            //
            // _btnApply
            //
            this._btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(175)))));
            this._btnApply.FlatAppearance.BorderSize = 0;
            this._btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnApply.ForeColor = System.Drawing.Color.White;
            this._btnApply.Location = new System.Drawing.Point(14, 389);
            this._btnApply.Name = "_btnApply";
            this._btnApply.Size = new System.Drawing.Size(140, 28);
            this._btnApply.TabIndex = 4;
            this._btnApply.Text = "Apply Changes";
            this._btnApply.UseVisualStyleBackColor = false;
            this._btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            //
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.pnlBottom.Controls.Add(this._btnNew);
            this.pnlBottom.Controls.Add(this._btnDelete);
            this.pnlBottom.Controls.Add(this._btnCopy);
            this.pnlBottom.Controls.Add(this._btnUp);
            this.pnlBottom.Controls.Add(this._btnDown);
            this.pnlBottom.Controls.Add(this._btnOk);
            this.pnlBottom.Controls.Add(this._btnCancel);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 473);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(780, 87);
            this.pnlBottom.TabIndex = 3;
            // 
            // _btnOk
            // 
            this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._btnOk.FlatAppearance.BorderSize = 0;
            this._btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnOk.ForeColor = System.Drawing.Color.White;
            this._btnOk.Location = new System.Drawing.Point(561, 6);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(100, 30);
            this._btnOk.TabIndex = 0;
            this._btnOk.Text = "OK";
            this._btnOk.UseVisualStyleBackColor = false;
            this._btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(78)))));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.FlatAppearance.BorderSize = 0;
            this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.ForeColor = System.Drawing.Color.White;
            this._btnCancel.Location = new System.Drawing.Point(670, 6);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(100, 30);
            this._btnCancel.TabIndex = 1;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = false;
            // 
            // _btnNew
            // 
            this._btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(60)))));
            this._btnNew.FlatAppearance.BorderSize = 0;
            this._btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnNew.ForeColor = System.Drawing.Color.White;
            this._btnNew.Location = new System.Drawing.Point(16, 9);
            this._btnNew.Name = "_btnNew";
            this._btnNew.Size = new System.Drawing.Size(100, 30);
            this._btnNew.TabIndex = 4;
            this._btnNew.Text = "+ New";
            this._btnNew.UseVisualStyleBackColor = false;
            this._btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // _btnDelete
            // 
            this._btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this._btnDelete.FlatAppearance.BorderSize = 0;
            this._btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDelete.ForeColor = System.Drawing.Color.White;
            this._btnDelete.Location = new System.Drawing.Point(124, 9);
            this._btnDelete.Name = "_btnDelete";
            this._btnDelete.Size = new System.Drawing.Size(100, 30);
            this._btnDelete.TabIndex = 5;
            this._btnDelete.Text = "Delete";
            this._btnDelete.UseVisualStyleBackColor = false;
            this._btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            //
            // _btnCopy
            //
            this._btnCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(60)))));
            this._btnCopy.FlatAppearance.BorderSize = 0;
            this._btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCopy.ForeColor = System.Drawing.Color.White;
            this._btnCopy.Location = new System.Drawing.Point(232, 9);
            this._btnCopy.Name = "_btnCopy";
            this._btnCopy.Size = new System.Drawing.Size(100, 30);
            this._btnCopy.TabIndex = 8;
            this._btnCopy.Text = "++ Copy";
            this._btnCopy.UseVisualStyleBackColor = false;
            this._btnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            //
            // _btnUp
            // 
            this._btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnUp.FlatAppearance.BorderSize = 0;
            this._btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnUp.ForeColor = System.Drawing.Color.White;
            this._btnUp.Location = new System.Drawing.Point(16, 45);
            this._btnUp.Name = "_btnUp";
            this._btnUp.Size = new System.Drawing.Size(100, 30);
            this._btnUp.TabIndex = 6;
            this._btnUp.Text = "▲  Up";
            this._btnUp.UseVisualStyleBackColor = false;
            this._btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // _btnDown
            // 
            this._btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnDown.FlatAppearance.BorderSize = 0;
            this._btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDown.ForeColor = System.Drawing.Color.White;
            this._btnDown.Location = new System.Drawing.Point(124, 45);
            this._btnDown.Name = "_btnDown";
            this._btnDown.Size = new System.Drawing.Size(100, 30);
            this._btnDown.TabIndex = 7;
            this._btnDown.Text = "▼  Down";
            this._btnDown.UseVisualStyleBackColor = false;
            this._btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // AiTemplateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(780, 560);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 500);
            this.Name = "AiTemplateDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AI Prompt Templates";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button _btnNew;
        private System.Windows.Forms.Button _btnDelete;
        private System.Windows.Forms.Button _btnCopy;
        private System.Windows.Forms.Button _btnUp;
        private System.Windows.Forms.Button _btnDown;
    }
}
