namespace Test
{
    partial class JiraImportDialog
    {
        private System.ComponentModel.IContainer components = null;

        // ── Title bar ─────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlTitleBar;

        // ── Settings group ────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpConfig;
        private System.Windows.Forms.Label _lblUrlCaption;
        private System.Windows.Forms.Label _lblJiraUrl;
        private System.Windows.Forms.Label _lblProject;
        private System.Windows.Forms.TextBox _txtProject;
        private System.Windows.Forms.Label _lblSpField;
        private System.Windows.Forms.TextBox _txtSpField;
        private System.Windows.Forms.Label _lblFlField;
        private System.Windows.Forms.TextBox _txtFlField;
        private System.Windows.Forms.Label _lblFeatField;
        private System.Windows.Forms.TextBox _txtFeatField;
        private System.Windows.Forms.Label _lblFeatLink;
        private System.Windows.Forms.TextBox _txtDefaultFeatureLink;
        private System.Windows.Forms.Label _lblReporter;
        private System.Windows.Forms.TextBox _txtDefaultReporter;
        private System.Windows.Forms.Label _lblAssignee;
        private System.Windows.Forms.TextBox _txtDefaultAssignee;
        private System.Windows.Forms.Label _lblDefSp;
        private System.Windows.Forms.NumericUpDown _numDefaultStoryPts;
        private System.Windows.Forms.Button _btnApplyDefaults;

        // ── Grid ──────────────────────────────────────────────────────────────
        private System.Windows.Forms.Label _lblTasksHeader;
        private System.Windows.Forms.DataGridView _dgvTasks;

        // ── Bottom bar ────────────────────────────────────────────────────────
        private System.Windows.Forms.Label _lblStatus;
        private System.Windows.Forms.Button _btnSelectAll;
        private System.Windows.Forms.Button _btnSelectNone;
        private System.Windows.Forms.Button _btnImport;
        private System.Windows.Forms.Button _btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTitleBar            = new System.Windows.Forms.Panel();
            this.grpConfig              = new System.Windows.Forms.GroupBox();
            this._lblUrlCaption         = new System.Windows.Forms.Label();
            this._lblJiraUrl            = new System.Windows.Forms.Label();
            this._lblProject            = new System.Windows.Forms.Label();
            this._txtProject            = new System.Windows.Forms.TextBox();
            this._lblSpField            = new System.Windows.Forms.Label();
            this._txtSpField            = new System.Windows.Forms.TextBox();
            this._lblFlField            = new System.Windows.Forms.Label();
            this._txtFlField            = new System.Windows.Forms.TextBox();
            this._lblFeatField          = new System.Windows.Forms.Label();
            this._txtFeatField          = new System.Windows.Forms.TextBox();
            this._lblFeatLink           = new System.Windows.Forms.Label();
            this._txtDefaultFeatureLink = new System.Windows.Forms.TextBox();
            this._lblReporter           = new System.Windows.Forms.Label();
            this._txtDefaultReporter    = new System.Windows.Forms.TextBox();
            this._lblAssignee           = new System.Windows.Forms.Label();
            this._txtDefaultAssignee    = new System.Windows.Forms.TextBox();
            this._lblDefSp              = new System.Windows.Forms.Label();
            this._numDefaultStoryPts    = new System.Windows.Forms.NumericUpDown();
            this._btnApplyDefaults      = new System.Windows.Forms.Button();
            this._lblTasksHeader        = new System.Windows.Forms.Label();
            this._dgvTasks              = new System.Windows.Forms.DataGridView();
            this._lblStatus             = new System.Windows.Forms.Label();
            this._btnSelectAll          = new System.Windows.Forms.Button();
            this._btnSelectNone         = new System.Windows.Forms.Button();
            this._btnImport             = new System.Windows.Forms.Button();
            this._btnCancel             = new System.Windows.Forms.Button();

            this.grpConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numDefaultStoryPts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTasks)).BeginInit();
            this.pnlTitleBar.SuspendLayout();
            this.SuspendLayout();

            // ── pnlTitleBar ───────────────────────────────────────────────────
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(44, 44, 46);
            this.pnlTitleBar.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location  = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name      = "pnlTitleBar";
            this.pnlTitleBar.Size      = new System.Drawing.Size(1000, 32);
            this.pnlTitleBar.TabIndex  = 0;

            // ── grpConfig ─────────────────────────────────────────────────────
            this.grpConfig.Controls.Add(this._lblUrlCaption);
            this.grpConfig.Controls.Add(this._lblJiraUrl);
            this.grpConfig.Controls.Add(this._lblProject);
            this.grpConfig.Controls.Add(this._txtProject);
            this.grpConfig.Controls.Add(this._lblSpField);
            this.grpConfig.Controls.Add(this._txtSpField);
            this.grpConfig.Controls.Add(this._lblFlField);
            this.grpConfig.Controls.Add(this._txtFlField);
            this.grpConfig.Controls.Add(this._lblFeatField);
            this.grpConfig.Controls.Add(this._txtFeatField);
            this.grpConfig.Controls.Add(this._lblFeatLink);
            this.grpConfig.Controls.Add(this._txtDefaultFeatureLink);
            this.grpConfig.Controls.Add(this._lblReporter);
            this.grpConfig.Controls.Add(this._txtDefaultReporter);
            this.grpConfig.Controls.Add(this._lblAssignee);
            this.grpConfig.Controls.Add(this._txtDefaultAssignee);
            this.grpConfig.Controls.Add(this._lblDefSp);
            this.grpConfig.Controls.Add(this._numDefaultStoryPts);
            this.grpConfig.Controls.Add(this._btnApplyDefaults);
            this.grpConfig.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this.grpConfig.Location  = new System.Drawing.Point(14, 46);
            this.grpConfig.Name      = "grpConfig";
            this.grpConfig.Size      = new System.Drawing.Size(972, 90);
            this.grpConfig.TabIndex  = 1;
            this.grpConfig.TabStop   = false;
            this.grpConfig.Text      = "Settings  (field IDs are Jira custom field names, e.g. customfield_10016)";

            // ── Row 1: URL | Project | SP Field | FL Field | Default SP ──────────

            this._lblUrlCaption.AutoSize  = true;
            this._lblUrlCaption.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblUrlCaption.Location  = new System.Drawing.Point(14, 24);
            this._lblUrlCaption.Text      = "Jira:";

            this._lblJiraUrl.AutoSize  = true;
            this._lblJiraUrl.ForeColor = System.Drawing.Color.FromArgb(100, 160, 240);
            this._lblJiraUrl.Location  = new System.Drawing.Point(50, 24);
            this._lblJiraUrl.Size      = new System.Drawing.Size(140, 17);
            this._lblJiraUrl.Text      = "(loading…)";

            this._lblProject.AutoSize  = true;
            this._lblProject.ForeColor = System.Drawing.Color.White;
            this._lblProject.Location  = new System.Drawing.Point(208, 24);
            this._lblProject.Text      = "Project *:";

            this._txtProject.BackColor   = System.Drawing.Color.FromArgb(50, 50, 56);
            this._txtProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtProject.ForeColor   = System.Drawing.Color.White;
            this._txtProject.Location    = new System.Drawing.Point(272, 21);
            this._txtProject.Name        = "_txtProject";
            this._txtProject.Size        = new System.Drawing.Size(80, 23);
            this._txtProject.TabIndex    = 0;

            this._lblSpField.AutoSize  = true;
            this._lblSpField.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblSpField.Location  = new System.Drawing.Point(368, 24);
            this._lblSpField.Text      = "SP Field:";

            this._txtSpField.BackColor   = System.Drawing.Color.FromArgb(50, 50, 56);
            this._txtSpField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSpField.ForeColor   = System.Drawing.Color.White;
            this._txtSpField.Location    = new System.Drawing.Point(424, 21);
            this._txtSpField.Name        = "_txtSpField";
            this._txtSpField.Size        = new System.Drawing.Size(140, 23);
            this._txtSpField.TabIndex    = 1;

            this._lblFlField.AutoSize  = true;
            this._lblFlField.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblFlField.Location  = new System.Drawing.Point(580, 24);
            this._lblFlField.Text      = "FL Field:";

            this._txtFlField.BackColor   = System.Drawing.Color.FromArgb(50, 50, 56);
            this._txtFlField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtFlField.ForeColor   = System.Drawing.Color.White;
            this._txtFlField.Location    = new System.Drawing.Point(630, 21);
            this._txtFlField.Name        = "_txtFlField";
            this._txtFlField.Size        = new System.Drawing.Size(140, 23);
            this._txtFlField.TabIndex    = 2;

            this._lblDefSp.AutoSize  = true;
            this._lblDefSp.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblDefSp.Location  = new System.Drawing.Point(786, 24);
            this._lblDefSp.Text      = "Def SP:";

            this._numDefaultStoryPts.BackColor    = System.Drawing.Color.FromArgb(50, 50, 56);
            this._numDefaultStoryPts.ForeColor    = System.Drawing.Color.White;
            this._numDefaultStoryPts.Location     = new System.Drawing.Point(832, 21);
            this._numDefaultStoryPts.Maximum      = new decimal(new int[] { 999, 0, 0, 0 });
            this._numDefaultStoryPts.Name         = "_numDefaultStoryPts";
            this._numDefaultStoryPts.Size         = new System.Drawing.Size(58, 23);
            this._numDefaultStoryPts.TabIndex     = 3;

            // ── Row 2: Feat Field | Feature Link | Reporter | Assignee | Apply ─

            this._lblFeatField.AutoSize  = true;
            this._lblFeatField.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblFeatField.Location  = new System.Drawing.Point(14, 56);
            this._lblFeatField.Text      = "Feat Field:";

            this._txtFeatField.BackColor   = System.Drawing.Color.FromArgb(50, 50, 56);
            this._txtFeatField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtFeatField.ForeColor   = System.Drawing.Color.White;
            this._txtFeatField.Location    = new System.Drawing.Point(80, 53);
            this._txtFeatField.Name        = "_txtFeatField";
            this._txtFeatField.Size        = new System.Drawing.Size(130, 23);
            this._txtFeatField.TabIndex    = 4;

            this._lblFeatLink.AutoSize  = true;
            this._lblFeatLink.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblFeatLink.Location  = new System.Drawing.Point(226, 56);
            this._lblFeatLink.Text      = "Feat Link:";

            this._txtDefaultFeatureLink.BackColor   = System.Drawing.Color.FromArgb(50, 50, 56);
            this._txtDefaultFeatureLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtDefaultFeatureLink.ForeColor   = System.Drawing.Color.White;
            this._txtDefaultFeatureLink.Location    = new System.Drawing.Point(292, 53);
            this._txtDefaultFeatureLink.Name        = "_txtDefaultFeatureLink";
            this._txtDefaultFeatureLink.Size        = new System.Drawing.Size(120, 23);
            this._txtDefaultFeatureLink.TabIndex    = 5;

            this._lblReporter.AutoSize  = true;
            this._lblReporter.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblReporter.Location  = new System.Drawing.Point(428, 56);
            this._lblReporter.Text      = "Reporter:";

            this._txtDefaultReporter.BackColor   = System.Drawing.Color.FromArgb(50, 50, 56);
            this._txtDefaultReporter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtDefaultReporter.ForeColor   = System.Drawing.Color.White;
            this._txtDefaultReporter.Location    = new System.Drawing.Point(486, 53);
            this._txtDefaultReporter.Name        = "_txtDefaultReporter";
            this._txtDefaultReporter.Size        = new System.Drawing.Size(130, 23);
            this._txtDefaultReporter.TabIndex    = 6;

            this._lblAssignee.AutoSize  = true;
            this._lblAssignee.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblAssignee.Location  = new System.Drawing.Point(632, 56);
            this._lblAssignee.Text      = "Assignee:";

            this._txtDefaultAssignee.BackColor   = System.Drawing.Color.FromArgb(50, 50, 56);
            this._txtDefaultAssignee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtDefaultAssignee.ForeColor   = System.Drawing.Color.White;
            this._txtDefaultAssignee.Location    = new System.Drawing.Point(690, 53);
            this._txtDefaultAssignee.Name        = "_txtDefaultAssignee";
            this._txtDefaultAssignee.Size        = new System.Drawing.Size(120, 23);
            this._txtDefaultAssignee.TabIndex    = 7;

            this._btnApplyDefaults.BackColor             = System.Drawing.Color.FromArgb(60, 60, 70);
            this._btnApplyDefaults.FlatAppearance.BorderSize = 0;
            this._btnApplyDefaults.FlatStyle             = System.Windows.Forms.FlatStyle.Flat;
            this._btnApplyDefaults.ForeColor             = System.Drawing.Color.White;
            this._btnApplyDefaults.Location              = new System.Drawing.Point(826, 53);
            this._btnApplyDefaults.Name                  = "_btnApplyDefaults";
            this._btnApplyDefaults.Size                  = new System.Drawing.Size(132, 23);
            this._btnApplyDefaults.TabIndex              = 8;
            this._btnApplyDefaults.Text                  = "Apply Defaults to Grid";
            this._btnApplyDefaults.UseVisualStyleBackColor = false;
            this._btnApplyDefaults.Click                += new System.EventHandler(this.BtnApplyDefaults_Click);

            // ── _lblTasksHeader ───────────────────────────────────────────────
            this._lblTasksHeader.AutoSize  = true;
            this._lblTasksHeader.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this._lblTasksHeader.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this._lblTasksHeader.Location  = new System.Drawing.Point(14, 146);
            this._lblTasksHeader.Name      = "_lblTasksHeader";
            this._lblTasksHeader.Text      = "Tasks — edit any cell to override defaults before importing.  " +
                                             "Feature Link and Parent Key columns are mutually exclusive by row type.";

            // ── _dgvTasks ─────────────────────────────────────────────────────
            this._dgvTasks.Anchor = System.Windows.Forms.AnchorStyles.Top
                                  | System.Windows.Forms.AnchorStyles.Bottom
                                  | System.Windows.Forms.AnchorStyles.Left
                                  | System.Windows.Forms.AnchorStyles.Right;
            this._dgvTasks.BackgroundColor            = System.Drawing.Color.FromArgb(28, 28, 30);
            this._dgvTasks.BorderStyle                = System.Windows.Forms.BorderStyle.None;
            this._dgvTasks.CellBorderStyle            = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this._dgvTasks.GridColor                  = System.Drawing.Color.FromArgb(50, 50, 56);
            this._dgvTasks.EnableHeadersVisualStyles  = false;
            this._dgvTasks.RowHeadersVisible          = false;
            this._dgvTasks.SelectionMode              = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvTasks.AllowUserToAddRows         = false;
            this._dgvTasks.AllowUserToDeleteRows      = false;
            this._dgvTasks.Location                   = new System.Drawing.Point(14, 166);
            this._dgvTasks.Name                       = "_dgvTasks";
            this._dgvTasks.Size                       = new System.Drawing.Size(972, 432);
            this._dgvTasks.TabIndex                   = 10;

            // Default cell style
            var cellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor          = System.Drawing.Color.FromArgb(36, 36, 40),
                ForeColor          = System.Drawing.Color.White,
                SelectionBackColor = System.Drawing.Color.FromArgb(0, 84, 158),
                SelectionForeColor = System.Drawing.Color.White,
                Font               = new System.Drawing.Font("Segoe UI", 9F)
            };
            this._dgvTasks.DefaultCellStyle = cellStyle;

            var altStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor          = System.Drawing.Color.FromArgb(32, 32, 36),
                ForeColor          = System.Drawing.Color.White,
                SelectionBackColor = System.Drawing.Color.FromArgb(0, 84, 158),
                SelectionForeColor = System.Drawing.Color.White,
            };
            this._dgvTasks.AlternatingRowsDefaultCellStyle = altStyle;

            var hdrStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor          = System.Drawing.Color.FromArgb(44, 44, 50),
                ForeColor          = System.Drawing.Color.FromArgb(175, 175, 185),
                SelectionBackColor = System.Drawing.Color.FromArgb(44, 44, 50),
                SelectionForeColor = System.Drawing.Color.FromArgb(175, 175, 185),
                Font               = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                Alignment          = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,
            };
            this._dgvTasks.ColumnHeadersDefaultCellStyle = hdrStyle;
            this._dgvTasks.ColumnHeadersHeight           = 26;
            this._dgvTasks.RowTemplate.Height            = 22;

            this._dgvTasks.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvTasks_CellFormatting);
            this._dgvTasks.CellBeginEdit  += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvTasks_CellBeginEdit);

            // ── Bottom row ────────────────────────────────────────────────────

            this._lblStatus.Anchor    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this._lblStatus.AutoSize  = true;
            this._lblStatus.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this._lblStatus.Location  = new System.Drawing.Point(14, 616);
            this._lblStatus.Name      = "_lblStatus";
            this._lblStatus.Text      = "";

            this._btnSelectAll.Anchor                        = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this._btnSelectAll.BackColor                     = System.Drawing.Color.FromArgb(60, 60, 70);
            this._btnSelectAll.FlatAppearance.BorderSize     = 0;
            this._btnSelectAll.FlatStyle                     = System.Windows.Forms.FlatStyle.Flat;
            this._btnSelectAll.ForeColor                     = System.Drawing.Color.White;
            this._btnSelectAll.Location                      = new System.Drawing.Point(552, 608);
            this._btnSelectAll.Name                          = "_btnSelectAll";
            this._btnSelectAll.Size                          = new System.Drawing.Size(80, 28);
            this._btnSelectAll.TabIndex                      = 20;
            this._btnSelectAll.Text                          = "Select All";
            this._btnSelectAll.UseVisualStyleBackColor       = false;
            this._btnSelectAll.Click                        += new System.EventHandler(this.BtnSelectAll_Click);

            this._btnSelectNone.Anchor                       = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this._btnSelectNone.BackColor                    = System.Drawing.Color.FromArgb(60, 60, 70);
            this._btnSelectNone.FlatAppearance.BorderSize    = 0;
            this._btnSelectNone.FlatStyle                    = System.Windows.Forms.FlatStyle.Flat;
            this._btnSelectNone.ForeColor                    = System.Drawing.Color.White;
            this._btnSelectNone.Location                     = new System.Drawing.Point(640, 608);
            this._btnSelectNone.Name                         = "_btnSelectNone";
            this._btnSelectNone.Size                         = new System.Drawing.Size(90, 28);
            this._btnSelectNone.TabIndex                     = 21;
            this._btnSelectNone.Text                         = "Select None";
            this._btnSelectNone.UseVisualStyleBackColor      = false;
            this._btnSelectNone.Click                       += new System.EventHandler(this.BtnSelectNone_Click);

            this._btnImport.Anchor                       = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this._btnImport.BackColor                    = System.Drawing.Color.FromArgb(0, 120, 215);
            this._btnImport.FlatAppearance.BorderSize    = 0;
            this._btnImport.FlatStyle                    = System.Windows.Forms.FlatStyle.Flat;
            this._btnImport.ForeColor                    = System.Drawing.Color.White;
            this._btnImport.Location                     = new System.Drawing.Point(738, 608);
            this._btnImport.Name                         = "_btnImport";
            this._btnImport.Size                         = new System.Drawing.Size(120, 28);
            this._btnImport.TabIndex                     = 22;
            this._btnImport.Text                         = "Import to Jira…";
            this._btnImport.UseVisualStyleBackColor      = false;
            this._btnImport.Click                       += new System.EventHandler(this.BtnImport_Click);

            this._btnCancel.Anchor                   = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this._btnCancel.BackColor                = System.Drawing.Color.FromArgb(70, 70, 78);
            this._btnCancel.DialogResult             = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.FlatAppearance.BorderSize = 0;
            this._btnCancel.FlatStyle                = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.ForeColor                = System.Drawing.Color.White;
            this._btnCancel.Location                 = new System.Drawing.Point(866, 608);
            this._btnCancel.Name                     = "_btnCancel";
            this._btnCancel.Size                     = new System.Drawing.Size(120, 28);
            this._btnCancel.TabIndex                 = 23;
            this._btnCancel.Text                     = "Close";
            this._btnCancel.UseVisualStyleBackColor  = false;

            // ── Form ──────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(37, 37, 38);
            this.CancelButton        = this._btnCancel;
            this.ClientSize          = new System.Drawing.Size(1000, 650);
            this.Font                = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor           = System.Drawing.Color.White;
            this.MinimumSize         = new System.Drawing.Size(800, 500);
            this.Name                = "JiraImportDialog";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text                = "Import Tasks to Jira";

            this.Controls.Add(this.grpConfig);
            this.Controls.Add(this._lblTasksHeader);
            this.Controls.Add(this._dgvTasks);
            this.Controls.Add(this._lblStatus);
            this.Controls.Add(this._btnSelectAll);
            this.Controls.Add(this._btnSelectNone);
            this.Controls.Add(this._btnImport);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this.pnlTitleBar);

            this.grpConfig.ResumeLayout(false);
            this.grpConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numDefaultStoryPts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTasks)).EndInit();
            this.pnlTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
