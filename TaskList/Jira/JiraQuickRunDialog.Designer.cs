namespace Test
{
    partial class JiraQuickRunDialog
    {
        private System.ComponentModel.IContainer components = null;

        // ── Title bar ─────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlTitleBar;

        // ── Configuration group ───────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpConfiguration;
        private System.Windows.Forms.Label    _lblPresetCap;
        private Test.DarkComboBox             _cmbPreset;
        private System.Windows.Forms.Button   _btnReloadPresets;
        private System.Windows.Forms.Label    _lblTemplateCap;
        private Test.DarkComboBox             _cmbTemplate;
        private System.Windows.Forms.Button   _btnEditTemplates;
        private System.Windows.Forms.Label    _lblFolderCap;
        private System.Windows.Forms.TextBox  _txtFolder;
        private System.Windows.Forms.Button   _btnBrowse;
        private System.Windows.Forms.Label    _lblPatternCap;
        private System.Windows.Forms.TextBox  _txtPattern;

        // ── Data options group ────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpDataOptions;
        private System.Windows.Forms.CheckBox _chkWorklogs;
        private System.Windows.Forms.CheckBox _chkComments;
        private System.Windows.Forms.CheckBox _chkAiSummary;

        // ── Columns group ─────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpColumns;
        private System.Windows.Forms.CheckBox _chkKey;
        private System.Windows.Forms.CheckBox _chkSummary;
        private System.Windows.Forms.CheckBox _chkType;
        private System.Windows.Forms.CheckBox _chkStatus;
        private System.Windows.Forms.CheckBox _chkPriority;
        private System.Windows.Forms.CheckBox _chkProject;
        private System.Windows.Forms.CheckBox _chkAssignee;
        private System.Windows.Forms.CheckBox _chkDueDate;
        private System.Windows.Forms.CheckBox _chkRecordType;
        private System.Windows.Forms.CheckBox _chkDate;
        private System.Windows.Forms.CheckBox _chkAuthor;
        private System.Windows.Forms.CheckBox _chkHours;
        private System.Windows.Forms.CheckBox _chkText;

        // ── Bottom bar ────────────────────────────────────────────────────────
        private System.Windows.Forms.Button _btnSave;
        private System.Windows.Forms.Button _btnRun;
        private System.Windows.Forms.Label  _lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTitleBar       = new System.Windows.Forms.Panel();
            this.grpConfiguration  = new System.Windows.Forms.GroupBox();
            this._lblPresetCap     = new System.Windows.Forms.Label();
            this._cmbPreset        = new Test.DarkComboBox();
            this._btnReloadPresets = new System.Windows.Forms.Button();
            this._lblTemplateCap   = new System.Windows.Forms.Label();
            this._cmbTemplate      = new Test.DarkComboBox();
            this._btnEditTemplates = new System.Windows.Forms.Button();
            this._lblFolderCap     = new System.Windows.Forms.Label();
            this._txtFolder        = new System.Windows.Forms.TextBox();
            this._btnBrowse        = new System.Windows.Forms.Button();
            this._lblPatternCap    = new System.Windows.Forms.Label();
            this._txtPattern       = new System.Windows.Forms.TextBox();
            this.grpDataOptions    = new System.Windows.Forms.GroupBox();
            this._chkWorklogs      = new System.Windows.Forms.CheckBox();
            this._chkComments      = new System.Windows.Forms.CheckBox();
            this._chkAiSummary     = new System.Windows.Forms.CheckBox();
            this.grpColumns        = new System.Windows.Forms.GroupBox();
            this._chkKey           = new System.Windows.Forms.CheckBox();
            this._chkSummary       = new System.Windows.Forms.CheckBox();
            this._chkType          = new System.Windows.Forms.CheckBox();
            this._chkStatus        = new System.Windows.Forms.CheckBox();
            this._chkPriority      = new System.Windows.Forms.CheckBox();
            this._chkProject       = new System.Windows.Forms.CheckBox();
            this._chkAssignee      = new System.Windows.Forms.CheckBox();
            this._chkDueDate       = new System.Windows.Forms.CheckBox();
            this._chkRecordType    = new System.Windows.Forms.CheckBox();
            this._chkDate          = new System.Windows.Forms.CheckBox();
            this._chkAuthor        = new System.Windows.Forms.CheckBox();
            this._chkHours         = new System.Windows.Forms.CheckBox();
            this._chkText          = new System.Windows.Forms.CheckBox();
            this._btnSave          = new System.Windows.Forms.Button();
            this._btnRun           = new System.Windows.Forms.Button();
            this._lblStatus        = new System.Windows.Forms.Label();
            this.grpConfiguration.SuspendLayout();
            this.grpDataOptions.SuspendLayout();
            this.grpColumns.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTitleBar
            //
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(46)))));
            this.pnlTitleBar.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location  = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name      = "pnlTitleBar";
            this.pnlTitleBar.Size      = new System.Drawing.Size(600, 32);
            this.pnlTitleBar.TabIndex  = 0;
            //
            // grpConfiguration
            //
            this.grpConfiguration.Controls.Add(this._lblPresetCap);
            this.grpConfiguration.Controls.Add(this._cmbPreset);
            this.grpConfiguration.Controls.Add(this._btnReloadPresets);
            this.grpConfiguration.Controls.Add(this._lblTemplateCap);
            this.grpConfiguration.Controls.Add(this._cmbTemplate);
            this.grpConfiguration.Controls.Add(this._btnEditTemplates);
            this.grpConfiguration.Controls.Add(this._lblFolderCap);
            this.grpConfiguration.Controls.Add(this._txtFolder);
            this.grpConfiguration.Controls.Add(this._btnBrowse);
            this.grpConfiguration.Controls.Add(this._lblPatternCap);
            this.grpConfiguration.Controls.Add(this._txtPattern);
            this.grpConfiguration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.grpConfiguration.Location  = new System.Drawing.Point(12, 42);
            this.grpConfiguration.Name      = "grpConfiguration";
            this.grpConfiguration.Size      = new System.Drawing.Size(576, 238);
            this.grpConfiguration.TabIndex  = 1;
            this.grpConfiguration.TabStop   = false;
            this.grpConfiguration.Text      = "Configuration";
            //
            // _lblPresetCap
            //
            this._lblPresetCap.AutoSize  = true;
            this._lblPresetCap.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblPresetCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblPresetCap.Location  = new System.Drawing.Point(10, 24);
            this._lblPresetCap.Name      = "_lblPresetCap";
            this._lblPresetCap.TabIndex  = 0;
            this._lblPresetCap.Text      = "JQL PRESET";
            //
            // _cmbPreset
            //
            this._cmbPreset.BackColor     = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._cmbPreset.DrawMode      = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._cmbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPreset.FlatStyle     = System.Windows.Forms.FlatStyle.Flat;
            this._cmbPreset.ForeColor     = System.Drawing.Color.White;
            this._cmbPreset.Location      = new System.Drawing.Point(10, 40);
            this._cmbPreset.Name          = "_cmbPreset";
            this._cmbPreset.Size          = new System.Drawing.Size(390, 25);
            this._cmbPreset.TabIndex      = 1;
            //
            // _btnReloadPresets
            //
            this._btnReloadPresets.BackColor                 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnReloadPresets.FlatAppearance.BorderSize = 0;
            this._btnReloadPresets.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this._btnReloadPresets.ForeColor                 = System.Drawing.Color.White;
            this._btnReloadPresets.Location                  = new System.Drawing.Point(406, 40);
            this._btnReloadPresets.Name                      = "_btnReloadPresets";
            this._btnReloadPresets.Size                      = new System.Drawing.Size(80, 25);
            this._btnReloadPresets.TabIndex                  = 2;
            this._btnReloadPresets.Text                      = "Reload";
            this._btnReloadPresets.UseVisualStyleBackColor   = false;
            this._btnReloadPresets.Click += new System.EventHandler(this.BtnReloadPresets_Click);
            //
            // _lblTemplateCap
            //
            this._lblTemplateCap.AutoSize  = true;
            this._lblTemplateCap.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblTemplateCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblTemplateCap.Location  = new System.Drawing.Point(10, 76);
            this._lblTemplateCap.Name      = "_lblTemplateCap";
            this._lblTemplateCap.TabIndex  = 3;
            this._lblTemplateCap.Text      = "AI PROMPT TEMPLATE";
            //
            // _cmbTemplate
            //
            this._cmbTemplate.BackColor     = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._cmbTemplate.DrawMode      = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbTemplate.FlatStyle     = System.Windows.Forms.FlatStyle.Flat;
            this._cmbTemplate.ForeColor     = System.Drawing.Color.White;
            this._cmbTemplate.Location      = new System.Drawing.Point(10, 92);
            this._cmbTemplate.Name          = "_cmbTemplate";
            this._cmbTemplate.Size          = new System.Drawing.Size(390, 25);
            this._cmbTemplate.TabIndex      = 4;
            //
            // _btnEditTemplates
            //
            this._btnEditTemplates.BackColor                 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnEditTemplates.FlatAppearance.BorderSize = 0;
            this._btnEditTemplates.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this._btnEditTemplates.ForeColor                 = System.Drawing.Color.White;
            this._btnEditTemplates.Location                  = new System.Drawing.Point(406, 92);
            this._btnEditTemplates.Name                      = "_btnEditTemplates";
            this._btnEditTemplates.Size                      = new System.Drawing.Size(80, 25);
            this._btnEditTemplates.TabIndex                  = 5;
            this._btnEditTemplates.Text                      = "Edit\u2026";
            this._btnEditTemplates.UseVisualStyleBackColor   = false;
            this._btnEditTemplates.Click += new System.EventHandler(this.BtnEditTemplates_Click);
            //
            // _lblFolderCap
            //
            this._lblFolderCap.AutoSize  = true;
            this._lblFolderCap.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblFolderCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblFolderCap.Location  = new System.Drawing.Point(10, 128);
            this._lblFolderCap.Name      = "_lblFolderCap";
            this._lblFolderCap.TabIndex  = 6;
            this._lblFolderCap.Text      = "SAVE FOLDER";
            //
            // _txtFolder
            //
            this._txtFolder.BackColor   = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtFolder.ForeColor   = System.Drawing.Color.White;
            this._txtFolder.Location    = new System.Drawing.Point(10, 144);
            this._txtFolder.Name        = "_txtFolder";
            this._txtFolder.Size        = new System.Drawing.Size(350, 24);
            this._txtFolder.TabIndex    = 7;
            //
            // _btnBrowse
            //
            this._btnBrowse.BackColor                 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnBrowse.FlatAppearance.BorderSize = 0;
            this._btnBrowse.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this._btnBrowse.ForeColor                 = System.Drawing.Color.White;
            this._btnBrowse.Location                  = new System.Drawing.Point(366, 143);
            this._btnBrowse.Name                      = "_btnBrowse";
            this._btnBrowse.Size                      = new System.Drawing.Size(120, 26);
            this._btnBrowse.TabIndex                  = 8;
            this._btnBrowse.Text                      = "Browse\u2026";
            this._btnBrowse.UseVisualStyleBackColor   = false;
            this._btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            //
            // _lblPatternCap
            //
            this._lblPatternCap.AutoSize  = true;
            this._lblPatternCap.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblPatternCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblPatternCap.Location  = new System.Drawing.Point(10, 180);
            this._lblPatternCap.Name      = "_lblPatternCap";
            this._lblPatternCap.TabIndex  = 9;
            this._lblPatternCap.Text      = "FILE NAME PATTERN  (use {date} for timestamp)";
            //
            // _txtPattern
            //
            this._txtPattern.BackColor   = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtPattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtPattern.ForeColor   = System.Drawing.Color.White;
            this._txtPattern.Location    = new System.Drawing.Point(10, 196);
            this._txtPattern.Name        = "_txtPattern";
            this._txtPattern.Size        = new System.Drawing.Size(460, 24);
            this._txtPattern.TabIndex    = 10;
            this._txtPattern.Text        = "jira_export_{date}.xlsx";
            //
            // grpDataOptions
            //
            this.grpDataOptions.Controls.Add(this._chkWorklogs);
            this.grpDataOptions.Controls.Add(this._chkComments);
            this.grpDataOptions.Controls.Add(this._chkAiSummary);
            this.grpDataOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.grpDataOptions.Location  = new System.Drawing.Point(12, 292);
            this.grpDataOptions.Name      = "grpDataOptions";
            this.grpDataOptions.Size      = new System.Drawing.Size(576, 68);
            this.grpDataOptions.TabIndex  = 2;
            this.grpDataOptions.TabStop   = false;
            this.grpDataOptions.Text      = "Data Options";
            //
            // _chkWorklogs
            //
            this._chkWorklogs.AutoSize   = true;
            this._chkWorklogs.Checked    = true;
            this._chkWorklogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkWorklogs.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkWorklogs.Location   = new System.Drawing.Point(12, 30);
            this._chkWorklogs.Name       = "_chkWorklogs";
            this._chkWorklogs.Size       = new System.Drawing.Size(130, 20);
            this._chkWorklogs.TabIndex   = 0;
            this._chkWorklogs.Text       = "Include Worklogs";
            //
            // _chkComments
            //
            this._chkComments.AutoSize   = true;
            this._chkComments.Checked    = true;
            this._chkComments.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkComments.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkComments.Location   = new System.Drawing.Point(190, 30);
            this._chkComments.Name       = "_chkComments";
            this._chkComments.Size       = new System.Drawing.Size(130, 20);
            this._chkComments.TabIndex   = 1;
            this._chkComments.Text       = "Include Comments";
            //
            // _chkAiSummary
            //
            this._chkAiSummary.AutoSize   = true;
            this._chkAiSummary.Checked    = true;
            this._chkAiSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkAiSummary.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkAiSummary.Location   = new System.Drawing.Point(368, 30);
            this._chkAiSummary.Name       = "_chkAiSummary";
            this._chkAiSummary.Size       = new System.Drawing.Size(150, 20);
            this._chkAiSummary.TabIndex   = 2;
            this._chkAiSummary.Text       = "Include AI Summary";
            //
            // grpColumns
            //
            this.grpColumns.Controls.Add(this._chkKey);
            this.grpColumns.Controls.Add(this._chkSummary);
            this.grpColumns.Controls.Add(this._chkType);
            this.grpColumns.Controls.Add(this._chkStatus);
            this.grpColumns.Controls.Add(this._chkPriority);
            this.grpColumns.Controls.Add(this._chkProject);
            this.grpColumns.Controls.Add(this._chkAssignee);
            this.grpColumns.Controls.Add(this._chkDueDate);
            this.grpColumns.Controls.Add(this._chkRecordType);
            this.grpColumns.Controls.Add(this._chkDate);
            this.grpColumns.Controls.Add(this._chkAuthor);
            this.grpColumns.Controls.Add(this._chkHours);
            this.grpColumns.Controls.Add(this._chkText);
            this.grpColumns.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.grpColumns.Location  = new System.Drawing.Point(12, 372);
            this.grpColumns.Name      = "grpColumns";
            this.grpColumns.Size      = new System.Drawing.Size(576, 134);
            this.grpColumns.TabIndex  = 3;
            this.grpColumns.TabStop   = false;
            this.grpColumns.Text      = "Export Columns";
            // Row 1: Key, Summary, Type, Status   (y=22)
            // Row 2: Priority, Project, Assignee, Due Date   (y=46)
            // Row 3: Record Type, Date, Author, Hours   (y=70)
            // Row 4: Text   (y=94)
            //
            // _chkKey
            //
            this._chkKey.AutoSize   = true;
            this._chkKey.Checked    = true;
            this._chkKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkKey.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkKey.Location   = new System.Drawing.Point(10, 22);
            this._chkKey.Name       = "_chkKey";
            this._chkKey.Size       = new System.Drawing.Size(80, 20);
            this._chkKey.TabIndex   = 0;
            this._chkKey.Text       = "Key";
            //
            // _chkSummary
            //
            this._chkSummary.AutoSize   = true;
            this._chkSummary.Checked    = true;
            this._chkSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkSummary.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkSummary.Location   = new System.Drawing.Point(144, 22);
            this._chkSummary.Name       = "_chkSummary";
            this._chkSummary.Size       = new System.Drawing.Size(80, 20);
            this._chkSummary.TabIndex   = 1;
            this._chkSummary.Text       = "Summary";
            //
            // _chkType
            //
            this._chkType.AutoSize   = true;
            this._chkType.Checked    = true;
            this._chkType.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkType.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkType.Location   = new System.Drawing.Point(278, 22);
            this._chkType.Name       = "_chkType";
            this._chkType.Size       = new System.Drawing.Size(80, 20);
            this._chkType.TabIndex   = 2;
            this._chkType.Text       = "Type";
            //
            // _chkStatus
            //
            this._chkStatus.AutoSize   = true;
            this._chkStatus.Checked    = true;
            this._chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkStatus.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkStatus.Location   = new System.Drawing.Point(412, 22);
            this._chkStatus.Name       = "_chkStatus";
            this._chkStatus.Size       = new System.Drawing.Size(80, 20);
            this._chkStatus.TabIndex   = 3;
            this._chkStatus.Text       = "Status";
            //
            // _chkPriority
            //
            this._chkPriority.AutoSize   = true;
            this._chkPriority.Checked    = true;
            this._chkPriority.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkPriority.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkPriority.Location   = new System.Drawing.Point(10, 46);
            this._chkPriority.Name       = "_chkPriority";
            this._chkPriority.Size       = new System.Drawing.Size(80, 20);
            this._chkPriority.TabIndex   = 4;
            this._chkPriority.Text       = "Priority";
            //
            // _chkProject
            //
            this._chkProject.AutoSize   = true;
            this._chkProject.Checked    = true;
            this._chkProject.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkProject.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkProject.Location   = new System.Drawing.Point(144, 46);
            this._chkProject.Name       = "_chkProject";
            this._chkProject.Size       = new System.Drawing.Size(80, 20);
            this._chkProject.TabIndex   = 5;
            this._chkProject.Text       = "Project";
            //
            // _chkAssignee
            //
            this._chkAssignee.AutoSize   = true;
            this._chkAssignee.Checked    = true;
            this._chkAssignee.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkAssignee.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkAssignee.Location   = new System.Drawing.Point(278, 46);
            this._chkAssignee.Name       = "_chkAssignee";
            this._chkAssignee.Size       = new System.Drawing.Size(80, 20);
            this._chkAssignee.TabIndex   = 6;
            this._chkAssignee.Text       = "Assignee";
            //
            // _chkDueDate
            //
            this._chkDueDate.AutoSize   = true;
            this._chkDueDate.Checked    = true;
            this._chkDueDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkDueDate.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkDueDate.Location   = new System.Drawing.Point(412, 46);
            this._chkDueDate.Name       = "_chkDueDate";
            this._chkDueDate.Size       = new System.Drawing.Size(80, 20);
            this._chkDueDate.TabIndex   = 7;
            this._chkDueDate.Text       = "Due Date";
            //
            // _chkRecordType
            //
            this._chkRecordType.AutoSize   = true;
            this._chkRecordType.Checked    = true;
            this._chkRecordType.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkRecordType.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkRecordType.Location   = new System.Drawing.Point(10, 70);
            this._chkRecordType.Name       = "_chkRecordType";
            this._chkRecordType.Size       = new System.Drawing.Size(100, 20);
            this._chkRecordType.TabIndex   = 8;
            this._chkRecordType.Text       = "Record Type";
            //
            // _chkDate
            //
            this._chkDate.AutoSize   = true;
            this._chkDate.Checked    = true;
            this._chkDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkDate.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkDate.Location   = new System.Drawing.Point(144, 70);
            this._chkDate.Name       = "_chkDate";
            this._chkDate.Size       = new System.Drawing.Size(80, 20);
            this._chkDate.TabIndex   = 9;
            this._chkDate.Text       = "Date";
            //
            // _chkAuthor
            //
            this._chkAuthor.AutoSize   = true;
            this._chkAuthor.Checked    = true;
            this._chkAuthor.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkAuthor.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkAuthor.Location   = new System.Drawing.Point(278, 70);
            this._chkAuthor.Name       = "_chkAuthor";
            this._chkAuthor.Size       = new System.Drawing.Size(80, 20);
            this._chkAuthor.TabIndex   = 10;
            this._chkAuthor.Text       = "Author";
            //
            // _chkHours
            //
            this._chkHours.AutoSize   = true;
            this._chkHours.Checked    = true;
            this._chkHours.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkHours.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkHours.Location   = new System.Drawing.Point(412, 70);
            this._chkHours.Name       = "_chkHours";
            this._chkHours.Size       = new System.Drawing.Size(80, 20);
            this._chkHours.TabIndex   = 11;
            this._chkHours.Text       = "Hours";
            //
            // _chkText
            //
            this._chkText.AutoSize   = true;
            this._chkText.Checked    = true;
            this._chkText.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkText.ForeColor  = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this._chkText.Location   = new System.Drawing.Point(10, 94);
            this._chkText.Name       = "_chkText";
            this._chkText.Size       = new System.Drawing.Size(80, 20);
            this._chkText.TabIndex   = 12;
            this._chkText.Text       = "Text";
            //
            // _btnSave
            //
            this._btnSave.BackColor                 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnSave.FlatAppearance.BorderSize = 0;
            this._btnSave.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this._btnSave.ForeColor                 = System.Drawing.Color.White;
            this._btnSave.Location                  = new System.Drawing.Point(12, 520);
            this._btnSave.Name                      = "_btnSave";
            this._btnSave.Size                      = new System.Drawing.Size(140, 32);
            this._btnSave.TabIndex                  = 4;
            this._btnSave.Text                      = "Save Settings";
            this._btnSave.UseVisualStyleBackColor   = false;
            this._btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            //
            // _btnRun
            //
            this._btnRun.BackColor                 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this._btnRun.FlatAppearance.BorderSize = 0;
            this._btnRun.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this._btnRun.Font                      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnRun.ForeColor                 = System.Drawing.Color.White;
            this._btnRun.Location                  = new System.Drawing.Point(416, 518);
            this._btnRun.Name                      = "_btnRun";
            this._btnRun.Size                      = new System.Drawing.Size(172, 36);
            this._btnRun.TabIndex                  = 5;
            this._btnRun.Text                      = "\u25b6  Run Now";
            this._btnRun.UseVisualStyleBackColor   = false;
            this._btnRun.Click += new System.EventHandler(this.BtnRun_Click);
            //
            // _lblStatus
            //
            this._lblStatus.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this._lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblStatus.Location  = new System.Drawing.Point(12, 562);
            this._lblStatus.Name      = "_lblStatus";
            this._lblStatus.Size      = new System.Drawing.Size(576, 22);
            this._lblStatus.TabIndex  = 6;
            //
            // JiraQuickRunDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.ClientSize          = new System.Drawing.Size(600, 596);
            this.Controls.Add(this.grpConfiguration);
            this.Controls.Add(this.grpDataOptions);
            this.Controls.Add(this.grpColumns);
            this.Controls.Add(this._btnSave);
            this.Controls.Add(this._btnRun);
            this.Controls.Add(this._lblStatus);
            this.Controls.Add(this.pnlTitleBar);
            this.Font          = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor     = System.Drawing.Color.White;
            this.MaximizeBox   = false;
            this.MinimizeBox   = false;
            this.MinimumSize   = new System.Drawing.Size(600, 604);
            this.Name          = "JiraQuickRunDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text          = "Quick Run";
            this.grpConfiguration.ResumeLayout(false);
            this.grpConfiguration.PerformLayout();
            this.grpDataOptions.ResumeLayout(false);
            this.grpDataOptions.PerformLayout();
            this.grpColumns.ResumeLayout(false);
            this.grpColumns.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
