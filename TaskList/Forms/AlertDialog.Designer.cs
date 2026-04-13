namespace Test
{
    partial class AlertDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel  pnlTitleBar;
        private System.Windows.Forms.Label  _lblWarningIcon;
        private System.Windows.Forms.Label  _lblTaskInfo;
        private System.Windows.Forms.Button _btnDismiss;
        private System.Windows.Forms.Button _btnIgnoreForever;
        private System.Windows.Forms.Button _btnSnooze1h;
        private System.Windows.Forms.Button _btnSnooze4h;
        private System.Windows.Forms.Button _btnSnooze1d;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this._lblWarningIcon = new System.Windows.Forms.Label();
            this._lblTaskInfo = new System.Windows.Forms.Label();
            this._btnDismiss = new System.Windows.Forms.Button();
            this._btnIgnoreForever = new System.Windows.Forms.Button();
            this._btnSnooze1h = new System.Windows.Forms.Button();
            this._btnSnooze4h = new System.Windows.Forms.Button();
            this._btnSnooze1d = new System.Windows.Forms.Button();
            this.pnlTitleBar.SuspendLayout();
            this.SuspendLayout();

            // pnlTitleBar
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(44, 44, 46);
            this.pnlTitleBar.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location  = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name      = "pnlTitleBar";
            this.pnlTitleBar.Size      = new System.Drawing.Size(414, 32);
            this.pnlTitleBar.TabIndex  = 10;
            // 
            // _lblWarningIcon
            // 
            this._lblWarningIcon.AutoSize = true;
            this._lblWarningIcon.Font = new System.Drawing.Font("Segoe UI", 22F);
            this._lblWarningIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(185)))), ((int)(((byte)(0)))));
            this._lblWarningIcon.Location = new System.Drawing.Point(12, 44);
            this._lblWarningIcon.Name = "_lblWarningIcon";
            this._lblWarningIcon.Size = new System.Drawing.Size(59, 41);
            this._lblWarningIcon.TabIndex = 0;
            this._lblWarningIcon.Text = "⚠";
            // 
            // _lblTaskInfo
            // 
            this._lblTaskInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblTaskInfo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this._lblTaskInfo.Location = new System.Drawing.Point(77, 48);
            this._lblTaskInfo.Name = "_lblTaskInfo";
            this._lblTaskInfo.Size = new System.Drawing.Size(336, 52);
            this._lblTaskInfo.TabIndex = 1;
            this._lblTaskInfo.Text = "\"Task Name\"\nis due soon";
            // 
            // _btnDismiss
            // 
            this._btnDismiss.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(78)))));
            this._btnDismiss.FlatAppearance.BorderSize = 0;
            this._btnDismiss.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDismiss.ForeColor = System.Drawing.Color.White;
            this._btnDismiss.Location = new System.Drawing.Point(15, 116);
            this._btnDismiss.Name = "_btnDismiss";
            this._btnDismiss.Size = new System.Drawing.Size(185, 28);
            this._btnDismiss.TabIndex = 2;
            this._btnDismiss.Text = "Dismiss (this time)";
            this._btnDismiss.UseVisualStyleBackColor = false;
            this._btnDismiss.Click += new System.EventHandler(this.BtnDismiss_Click);
            // 
            // _btnIgnoreForever
            // 
            this._btnIgnoreForever.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._btnIgnoreForever.FlatAppearance.BorderSize = 0;
            this._btnIgnoreForever.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnIgnoreForever.ForeColor = System.Drawing.Color.White;
            this._btnIgnoreForever.Location = new System.Drawing.Point(210, 116);
            this._btnIgnoreForever.Name = "_btnIgnoreForever";
            this._btnIgnoreForever.Size = new System.Drawing.Size(185, 28);
            this._btnIgnoreForever.TabIndex = 3;
            this._btnIgnoreForever.Text = "Ignore Forever";
            this._btnIgnoreForever.UseVisualStyleBackColor = false;
            this._btnIgnoreForever.Click += new System.EventHandler(this.BtnIgnoreForever_Click);
            // 
            // _btnSnooze1h
            // 
            this._btnSnooze1h.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(175)))));
            this._btnSnooze1h.FlatAppearance.BorderSize = 0;
            this._btnSnooze1h.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSnooze1h.ForeColor = System.Drawing.Color.White;
            this._btnSnooze1h.Location = new System.Drawing.Point(15, 156);
            this._btnSnooze1h.Name = "_btnSnooze1h";
            this._btnSnooze1h.Size = new System.Drawing.Size(120, 28);
            this._btnSnooze1h.TabIndex = 4;
            this._btnSnooze1h.Text = "+1 Hour";
            this._btnSnooze1h.UseVisualStyleBackColor = false;
            this._btnSnooze1h.Click += new System.EventHandler(this.BtnSnooze1h_Click);
            // 
            // _btnSnooze4h
            // 
            this._btnSnooze4h.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(175)))));
            this._btnSnooze4h.FlatAppearance.BorderSize = 0;
            this._btnSnooze4h.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSnooze4h.ForeColor = System.Drawing.Color.White;
            this._btnSnooze4h.Location = new System.Drawing.Point(145, 156);
            this._btnSnooze4h.Name = "_btnSnooze4h";
            this._btnSnooze4h.Size = new System.Drawing.Size(120, 28);
            this._btnSnooze4h.TabIndex = 5;
            this._btnSnooze4h.Text = "+4 Hours";
            this._btnSnooze4h.UseVisualStyleBackColor = false;
            this._btnSnooze4h.Click += new System.EventHandler(this.BtnSnooze4h_Click);
            // 
            // _btnSnooze1d
            // 
            this._btnSnooze1d.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(175)))));
            this._btnSnooze1d.FlatAppearance.BorderSize = 0;
            this._btnSnooze1d.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSnooze1d.ForeColor = System.Drawing.Color.White;
            this._btnSnooze1d.Location = new System.Drawing.Point(275, 156);
            this._btnSnooze1d.Name = "_btnSnooze1d";
            this._btnSnooze1d.Size = new System.Drawing.Size(120, 28);
            this._btnSnooze1d.TabIndex = 6;
            this._btnSnooze1d.Text = "+1 Day";
            this._btnSnooze1d.UseVisualStyleBackColor = false;
            this._btnSnooze1d.Click += new System.EventHandler(this.BtnSnooze1d_Click);
            // 
            // AlertDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(414, 204);
            this.Controls.Add(this._lblWarningIcon);
            this.Controls.Add(this._lblTaskInfo);
            this.Controls.Add(this._btnDismiss);
            this.Controls.Add(this._btnIgnoreForever);
            this.Controls.Add(this._btnSnooze1h);
            this.Controls.Add(this._btnSnooze4h);
            this.Controls.Add(this._btnSnooze1d);
            this.Controls.Add(this.pnlTitleBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AlertDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task Reminder";
            this.TopMost = true;
            this.pnlTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
