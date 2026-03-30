namespace Test
{
    partial class HistoryDialog
    {
        private System.ComponentModel.IContainer components = null;

        // ── Toolbar ───────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel  pnlToolbar;
        private System.Windows.Forms.Label  _lblTitle;
        private System.Windows.Forms.Label  _lblCount;
        private System.Windows.Forms.Button _btnRestoreBackup;
        private System.Windows.Forms.Button _btnClearHistory;

        // ── Legend (children built in code) ──────────────────────────────────
        private System.Windows.Forms.Panel pnlLegend;

        // ── Right detail panel ────────────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlRight;
        private System.Windows.Forms.Label    _lblSummaryCaption;
        private System.Windows.Forms.Label    _lblSummary;
        private System.Windows.Forms.Label    _lblChangesCaption;
        private System.Windows.Forms.ListView _lvChanges;

        // ── Left list panel ───────────────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlLeft;
        private System.Windows.Forms.ListView _lvHistory;

        // ── Timer ─────────────────────────────────────────────────────────────
        private System.Windows.Forms.Timer _refreshTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this._lblTitle = new System.Windows.Forms.Label();
            this._lblCount = new System.Windows.Forms.Label();
            this._btnRestoreBackup = new System.Windows.Forms.Button();
            this._btnClearHistory = new System.Windows.Forms.Button();
            this.pnlLegend = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this._lblSummaryCaption = new System.Windows.Forms.Label();
            this._lblSummary = new System.Windows.Forms.Label();
            this._lblChangesCaption = new System.Windows.Forms.Label();
            this._lvChanges = new System.Windows.Forms.ListView();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this._lvHistory = new System.Windows.Forms.ListView();
            this._refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlToolbar.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(46)))));
            this.pnlToolbar.Controls.Add(this._lblTitle);
            this.pnlToolbar.Controls.Add(this._lblCount);
            this.pnlToolbar.Controls.Add(this._btnRestoreBackup);
            this.pnlToolbar.Controls.Add(this._btnClearHistory);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(980, 46);
            this.pnlToolbar.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.BackColor = System.Drawing.Color.Transparent;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.White;
            this._lblTitle.Location = new System.Drawing.Point(12, 12);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(124, 20);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "Revision History";
            // 
            // _lblCount
            // 
            this._lblCount.AutoSize = true;
            this._lblCount.BackColor = System.Drawing.Color.Transparent;
            this._lblCount.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblCount.Location = new System.Drawing.Point(178, 16);
            this._lblCount.Name = "_lblCount";
            this._lblCount.Size = new System.Drawing.Size(59, 15);
            this._lblCount.TabIndex = 1;
            this._lblCount.Text = "(0 entries)";
            // 
            // _btnRestoreBackup
            // 
            this._btnRestoreBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnRestoreBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this._btnRestoreBackup.FlatAppearance.BorderSize = 0;
            this._btnRestoreBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnRestoreBackup.ForeColor = System.Drawing.Color.White;
            this._btnRestoreBackup.Location = new System.Drawing.Point(550, 9);
            this._btnRestoreBackup.Name = "_btnRestoreBackup";
            this._btnRestoreBackup.Size = new System.Drawing.Size(140, 28);
            this._btnRestoreBackup.TabIndex = 2;
            this._btnRestoreBackup.Text = "Restore Backup…";
            this._btnRestoreBackup.UseVisualStyleBackColor = false;
            this._btnRestoreBackup.Click += new System.EventHandler(this.BtnRestore_Click);
            // 
            // _btnClearHistory
            // 
            this._btnClearHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnClearHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this._btnClearHistory.FlatAppearance.BorderSize = 0;
            this._btnClearHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnClearHistory.ForeColor = System.Drawing.Color.White;
            this._btnClearHistory.Location = new System.Drawing.Point(700, 9);
            this._btnClearHistory.Name = "_btnClearHistory";
            this._btnClearHistory.Size = new System.Drawing.Size(110, 28);
            this._btnClearHistory.TabIndex = 3;
            this._btnClearHistory.Text = "Clear History";
            this._btnClearHistory.UseVisualStyleBackColor = false;
            this._btnClearHistory.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // pnlLegend
            // 
            this.pnlLegend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.pnlLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLegend.Location = new System.Drawing.Point(0, 590);
            this.pnlLegend.Name = "pnlLegend";
            this.pnlLegend.Size = new System.Drawing.Size(980, 30);
            this.pnlLegend.TabIndex = 3;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.pnlRight.Controls.Add(this._lblSummaryCaption);
            this.pnlRight.Controls.Add(this._lblSummary);
            this.pnlRight.Controls.Add(this._lblChangesCaption);
            this.pnlRight.Controls.Add(this._lvChanges);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(640, 46);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(340, 544);
            this.pnlRight.TabIndex = 2;
            // 
            // _lblSummaryCaption
            // 
            this._lblSummaryCaption.AutoSize = true;
            this._lblSummaryCaption.BackColor = System.Drawing.Color.Transparent;
            this._lblSummaryCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblSummaryCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblSummaryCaption.Location = new System.Drawing.Point(14, 14);
            this._lblSummaryCaption.Name = "_lblSummaryCaption";
            this._lblSummaryCaption.Size = new System.Drawing.Size(58, 12);
            this._lblSummaryCaption.TabIndex = 0;
            this._lblSummaryCaption.Text = "SUMMARY";
            // 
            // _lblSummary
            // 
            this._lblSummary.BackColor = System.Drawing.Color.Transparent;
            this._lblSummary.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._lblSummary.ForeColor = System.Drawing.Color.White;
            this._lblSummary.Location = new System.Drawing.Point(14, 32);
            this._lblSummary.Name = "_lblSummary";
            this._lblSummary.Size = new System.Drawing.Size(308, 52);
            this._lblSummary.TabIndex = 1;
            // 
            // _lblChangesCaption
            // 
            this._lblChangesCaption.AutoSize = true;
            this._lblChangesCaption.BackColor = System.Drawing.Color.Transparent;
            this._lblChangesCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblChangesCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblChangesCaption.Location = new System.Drawing.Point(14, 94);
            this._lblChangesCaption.Name = "_lblChangesCaption";
            this._lblChangesCaption.Size = new System.Drawing.Size(80, 12);
            this._lblChangesCaption.TabIndex = 2;
            this._lblChangesCaption.Text = "FIELD CHANGES";
            // 
            // _lvChanges
            // 
            this._lvChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lvChanges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._lvChanges.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lvChanges.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lvChanges.ForeColor = System.Drawing.Color.White;
            this._lvChanges.FullRowSelect = true;
            this._lvChanges.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._lvChanges.HideSelection = false;
            this._lvChanges.Location = new System.Drawing.Point(14, 112);
            this._lvChanges.Name = "_lvChanges";
            this._lvChanges.Size = new System.Drawing.Size(308, 420);
            this._lvChanges.TabIndex = 3;
            this._lvChanges.UseCompatibleStateImageBehavior = false;
            this._lvChanges.View = System.Windows.Forms.View.Details;
            this._lvChanges.Columns.Add("Field",     100);
            this._lvChanges.Columns.Add("Old Value", 100);
            this._lvChanges.Columns.Add("New Value", 100);
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.pnlLeft.Controls.Add(this._lvHistory);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 46);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(640, 544);
            this.pnlLeft.TabIndex = 1;
            // 
            // _lvHistory
            // 
            this._lvHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this._lvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lvHistory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lvHistory.ForeColor = System.Drawing.Color.White;
            this._lvHistory.FullRowSelect = true;
            this._lvHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._lvHistory.HideSelection = false;
            this._lvHistory.Location = new System.Drawing.Point(0, 0);
            this._lvHistory.MultiSelect = false;
            this._lvHistory.Name = "_lvHistory";
            this._lvHistory.Size = new System.Drawing.Size(640, 544);
            this._lvHistory.TabIndex = 0;
            this._lvHistory.UseCompatibleStateImageBehavior = false;
            this._lvHistory.View = System.Windows.Forms.View.Details;
            this._lvHistory.Columns.Add("Timestamp", 160);
            this._lvHistory.Columns.Add("Action",    80);
            this._lvHistory.Columns.Add("Task",      320);
            this._lvHistory.Columns.Add("Backup",    60);
            this._lvHistory.SelectedIndexChanged += new System.EventHandler(this.OnHistorySelect);
            // 
            // HistoryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(980, 620);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLegend);
            this.Controls.Add(this.pnlToolbar);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(760, 480);
            this.Name = "HistoryDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Revision History & Backups";
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
