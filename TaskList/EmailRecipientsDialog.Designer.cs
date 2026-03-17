namespace Test
{
    partial class EmailRecipientsDialog
    {
        private System.ComponentModel.IContainer components = null;

        // ── Header ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel  pnlHeader;
        private System.Windows.Forms.Label  _lblTitle;

        // ── Left panel (recipient list) ───────────────────────────────────────
        private System.Windows.Forms.Panel    pnlLeft;
        private System.Windows.Forms.ListView _lvRecipients;
        private System.Windows.Forms.Button   _btnNew;
        private System.Windows.Forms.Button   _btnDelete;
        private System.Windows.Forms.Button   _btnUp;
        private System.Windows.Forms.Button   _btnDown;

        // ── Right panel (editor) ──────────────────────────────────────────────
        private System.Windows.Forms.Panel   pnlRight;
        private System.Windows.Forms.Label   _lblNameCaption;
        private System.Windows.Forms.TextBox _txtName;
        private System.Windows.Forms.Label   _lblEmailCaption;
        private System.Windows.Forms.TextBox _txtEmail;
        private System.Windows.Forms.Label   _lblPositionCaption;
        private System.Windows.Forms.TextBox _txtPosition;

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
            this._lvRecipients = new System.Windows.Forms.ListView();
            this._btnNew = new System.Windows.Forms.Button();
            this._btnDelete = new System.Windows.Forms.Button();
            this._btnUp = new System.Windows.Forms.Button();
            this._btnDown = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this._lblNameCaption = new System.Windows.Forms.Label();
            this._txtName = new System.Windows.Forms.TextBox();
            this._lblEmailCaption = new System.Windows.Forms.Label();
            this._txtEmail = new System.Windows.Forms.TextBox();
            this._lblPositionCaption = new System.Windows.Forms.Label();
            this._txtPosition = new System.Windows.Forms.TextBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this._btnOk = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
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
            this.pnlHeader.Size = new System.Drawing.Size(860, 42);
            this.pnlHeader.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.White;
            this._lblTitle.Location = new System.Drawing.Point(12, 10);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(183, 20);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "Manage Email Recipients";
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.pnlLeft.Controls.Add(this._lvRecipients);
            this.pnlLeft.Controls.Add(this._btnNew);
            this.pnlLeft.Controls.Add(this._btnDelete);
            this.pnlLeft.Controls.Add(this._btnUp);
            this.pnlLeft.Controls.Add(this._btnDown);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 42);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Size = new System.Drawing.Size(480, 474);
            this.pnlLeft.TabIndex = 1;
            // 
            // _lvRecipients
            // 
            this._lvRecipients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lvRecipients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this._lvRecipients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lvRecipients.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._lvRecipients.ForeColor = System.Drawing.Color.White;
            this._lvRecipients.FullRowSelect = true;
            this._lvRecipients.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._lvRecipients.HideSelection = false;
            this._lvRecipients.Location = new System.Drawing.Point(10, 10);
            this._lvRecipients.MultiSelect = false;
            this._lvRecipients.Name = "_lvRecipients";
            this._lvRecipients.Size = new System.Drawing.Size(458, 390);
            this._lvRecipients.TabIndex = 0;
            this._lvRecipients.UseCompatibleStateImageBehavior = false;
            this._lvRecipients.View = System.Windows.Forms.View.Details;
            this._lvRecipients.Columns.Add("Name",     150);
            this._lvRecipients.Columns.Add("Email",    196);
            this._lvRecipients.Columns.Add("Position", 112);
            this._lvRecipients.SelectedIndexChanged += new System.EventHandler(this.LvRecipients_SelectedIndexChanged);
            // 
            // _btnNew
            // 
            this._btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._btnNew.FlatAppearance.BorderSize = 0;
            this._btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnNew.ForeColor = System.Drawing.Color.White;
            this._btnNew.Location = new System.Drawing.Point(10, 408);
            this._btnNew.Name = "_btnNew";
            this._btnNew.Size = new System.Drawing.Size(56, 26);
            this._btnNew.TabIndex = 1;
            this._btnNew.Text = "New";
            this._btnNew.UseVisualStyleBackColor = false;
            this._btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // _btnDelete
            // 
            this._btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._btnDelete.Enabled = false;
            this._btnDelete.FlatAppearance.BorderSize = 0;
            this._btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDelete.ForeColor = System.Drawing.Color.White;
            this._btnDelete.Location = new System.Drawing.Point(74, 408);
            this._btnDelete.Name = "_btnDelete";
            this._btnDelete.Size = new System.Drawing.Size(56, 26);
            this._btnDelete.TabIndex = 2;
            this._btnDelete.Text = "Delete";
            this._btnDelete.UseVisualStyleBackColor = false;
            this._btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // _btnUp
            // 
            this._btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnUp.Enabled = false;
            this._btnUp.FlatAppearance.BorderSize = 0;
            this._btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnUp.ForeColor = System.Drawing.Color.White;
            this._btnUp.Location = new System.Drawing.Point(10, 442);
            this._btnUp.Name = "_btnUp";
            this._btnUp.Size = new System.Drawing.Size(56, 26);
            this._btnUp.TabIndex = 3;
            this._btnUp.Text = "↑ Up";
            this._btnUp.UseVisualStyleBackColor = false;
            this._btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // _btnDown
            // 
            this._btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnDown.Enabled = false;
            this._btnDown.FlatAppearance.BorderSize = 0;
            this._btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDown.ForeColor = System.Drawing.Color.White;
            this._btnDown.Location = new System.Drawing.Point(74, 442);
            this._btnDown.Name = "_btnDown";
            this._btnDown.Size = new System.Drawing.Size(62, 26);
            this._btnDown.TabIndex = 4;
            this._btnDown.Text = "↓ Down";
            this._btnDown.UseVisualStyleBackColor = false;
            this._btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.pnlRight.Controls.Add(this._lblNameCaption);
            this.pnlRight.Controls.Add(this._txtName);
            this.pnlRight.Controls.Add(this._lblEmailCaption);
            this.pnlRight.Controls.Add(this._txtEmail);
            this.pnlRight.Controls.Add(this._lblPositionCaption);
            this.pnlRight.Controls.Add(this._txtPosition);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(480, 42);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.pnlRight.Size = new System.Drawing.Size(380, 474);
            this.pnlRight.TabIndex = 2;
            // 
            // _lblNameCaption
            // 
            this._lblNameCaption.AutoSize = true;
            this._lblNameCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblNameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblNameCaption.Location = new System.Drawing.Point(14, 14);
            this._lblNameCaption.Name = "_lblNameCaption";
            this._lblNameCaption.Size = new System.Drawing.Size(96, 12);
            this._lblNameCaption.TabIndex = 0;
            this._lblNameCaption.Text = "NAME  —  required";
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
            this._txtName.Size = new System.Drawing.Size(356, 24);
            this._txtName.TabIndex = 1;
            // 
            // _lblEmailCaption
            // 
            this._lblEmailCaption.AutoSize = true;
            this._lblEmailCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblEmailCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblEmailCaption.Location = new System.Drawing.Point(14, 62);
            this._lblEmailCaption.Name = "_lblEmailCaption";
            this._lblEmailCaption.Size = new System.Drawing.Size(96, 12);
            this._lblEmailCaption.TabIndex = 2;
            this._lblEmailCaption.Text = "EMAIL  —  required";
            // 
            // _txtEmail
            // 
            this._txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtEmail.ForeColor = System.Drawing.Color.White;
            this._txtEmail.Location = new System.Drawing.Point(14, 76);
            this._txtEmail.Name = "_txtEmail";
            this._txtEmail.Size = new System.Drawing.Size(356, 24);
            this._txtEmail.TabIndex = 3;
            // 
            // _lblPositionCaption
            // 
            this._lblPositionCaption.AutoSize = true;
            this._lblPositionCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblPositionCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblPositionCaption.Location = new System.Drawing.Point(14, 110);
            this._lblPositionCaption.Name = "_lblPositionCaption";
            this._lblPositionCaption.Size = new System.Drawing.Size(114, 12);
            this._lblPositionCaption.TabIndex = 4;
            this._lblPositionCaption.Text = "POSITION  —  optional";
            // 
            // _txtPosition
            // 
            this._txtPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtPosition.ForeColor = System.Drawing.Color.White;
            this._txtPosition.Location = new System.Drawing.Point(14, 124);
            this._txtPosition.Name = "_txtPosition";
            this._txtPosition.Size = new System.Drawing.Size(356, 24);
            this._txtPosition.TabIndex = 5;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.pnlBottom.Controls.Add(this._btnOk);
            this.pnlBottom.Controls.Add(this._btnCancel);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 516);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(860, 44);
            this.pnlBottom.TabIndex = 3;
            // 
            // _btnOk
            // 
            this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._btnOk.FlatAppearance.BorderSize = 0;
            this._btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnOk.ForeColor = System.Drawing.Color.White;
            this._btnOk.Location = new System.Drawing.Point(716, 8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(70, 28);
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
            this._btnCancel.Location = new System.Drawing.Point(796, 8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(54, 28);
            this._btnCancel.TabIndex = 1;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = false;
            // 
            // EmailRecipientsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(860, 560);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(720, 500);
            this.Name = "EmailRecipientsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Email Recipients";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
