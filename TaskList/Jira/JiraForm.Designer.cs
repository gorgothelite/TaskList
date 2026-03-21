namespace Test
{
    partial class JiraForm
    {
        private System.ComponentModel.IContainer components = null;

        // ── Toolbar ───────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel  pnlToolbar;
        private System.Windows.Forms.Label  _lblTitle;

        // ── Left settings sidebar ─────────────────────────────────────────────
        private System.Windows.Forms.Panel      pnlSettings;
        private System.Windows.Forms.GroupBox   grpConnection;
        private System.Windows.Forms.Label      _lblUrlCaption;
        private System.Windows.Forms.TextBox    _txtJiraUrl;
        private System.Windows.Forms.Label      _lblUserNameCaption;
        private System.Windows.Forms.TextBox    _txtUserName;
        private System.Windows.Forms.Label      _lblPasswordCaption;
        private System.Windows.Forms.TextBox    _txtPassword;
        private System.Windows.Forms.Button     _btnSaveSettings;
        private System.Windows.Forms.Button     _btnTestConnection;
        private System.Windows.Forms.GroupBox   grpQuery;
        private System.Windows.Forms.Label      _lblPresetsCaption;
        private System.Windows.Forms.ComboBox   _cmbPresets;
        private System.Windows.Forms.Button     _btnManagePresets;
        private System.Windows.Forms.Label      _lblJqlCaption;
        private System.Windows.Forms.TextBox    _txtJql;
        private System.Windows.Forms.Button     _btnSearch;
        private System.Windows.Forms.Button     _btnClearSearch;

        // ── Right area (fills remaining space) ────────────────────────────────
        private System.Windows.Forms.Panel      pnlRight;

        // Search results (top of right)
        private System.Windows.Forms.Label      _lblResultsCaption;
        private System.Windows.Forms.ListView   _lvResults;

        // Action bar (middle strip)
        private System.Windows.Forms.Panel      pnlActionBar;
        private System.Windows.Forms.Button     _btnAddIssue;
        private System.Windows.Forms.Button     _btnAddParent;
        private System.Windows.Forms.Button     _btnExportExcel;

        // Master list (bottom of right)
        private System.Windows.Forms.Panel      pnlMasterList;
        private System.Windows.Forms.Label      _lblMasterCount;
        private System.Windows.Forms.ListView   _lvMaster;
        private System.Windows.Forms.Button     _btnRemoveMaster;
        private System.Windows.Forms.Button     _btnClearMaster;
        private System.Windows.Forms.Button     _btnImportTasks;

        // Status bar
        private System.Windows.Forms.Label      _lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this._lblTitle = new System.Windows.Forms.Label();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this._lblUrlCaption = new System.Windows.Forms.Label();
            this._txtJiraUrl = new System.Windows.Forms.TextBox();
            this._lblUserNameCaption = new System.Windows.Forms.Label();
            this._txtUserName = new System.Windows.Forms.TextBox();
            this._lblPasswordCaption = new System.Windows.Forms.Label();
            this._txtPassword = new System.Windows.Forms.TextBox();
            this._btnSaveSettings = new System.Windows.Forms.Button();
            this._btnTestConnection = new System.Windows.Forms.Button();
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this._lblPresetsCaption = new System.Windows.Forms.Label();
            this._cmbPresets = new System.Windows.Forms.ComboBox();
            this._btnManagePresets = new System.Windows.Forms.Button();
            this._lblJqlCaption = new System.Windows.Forms.Label();
            this._txtJql = new System.Windows.Forms.TextBox();
            this._btnSearch = new System.Windows.Forms.Button();
            this._btnClearSearch = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this._lvResults = new System.Windows.Forms.ListView();
            this._lblResultsCaption = new System.Windows.Forms.Label();
            this.pnlActionBar = new System.Windows.Forms.Panel();
            this._btnAddIssue = new System.Windows.Forms.Button();
            this._btnAddParent = new System.Windows.Forms.Button();
            this._btnExportExcel = new System.Windows.Forms.Button();
            this.pnlMasterList = new System.Windows.Forms.Panel();
            this._lblMasterCount = new System.Windows.Forms.Label();
            this._lvMaster = new System.Windows.Forms.ListView();
            this._btnRemoveMaster = new System.Windows.Forms.Button();
            this._btnClearMaster = new System.Windows.Forms.Button();
            this._btnImportTasks = new System.Windows.Forms.Button();
            this._lblStatus = new System.Windows.Forms.Label();
            this.pnlToolbar.SuspendLayout();
            this.pnlSettings.SuspendLayout();
            this.grpConnection.SuspendLayout();
            this.grpQuery.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlActionBar.SuspendLayout();
            this.pnlMasterList.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(46)))));
            this.pnlToolbar.Controls.Add(this._lblTitle);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1200, 46);
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
            this._lblTitle.Size = new System.Drawing.Size(117, 20);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "Jira Integration";
            // 
            // pnlSettings
            // 
            this.pnlSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.pnlSettings.Controls.Add(this.grpConnection);
            this.pnlSettings.Controls.Add(this.grpQuery);
            this.pnlSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSettings.Location = new System.Drawing.Point(0, 46);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Padding = new System.Windows.Forms.Padding(10);
            this.pnlSettings.Size = new System.Drawing.Size(290, 714);
            this.pnlSettings.TabIndex = 1;
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this._lblUrlCaption);
            this.grpConnection.Controls.Add(this._txtJiraUrl);
            this.grpConnection.Controls.Add(this._lblUserNameCaption);
            this.grpConnection.Controls.Add(this._txtUserName);
            this.grpConnection.Controls.Add(this._lblPasswordCaption);
            this.grpConnection.Controls.Add(this._txtPassword);
            this.grpConnection.Controls.Add(this._btnSaveSettings);
            this.grpConnection.Controls.Add(this._btnTestConnection);
            this.grpConnection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.grpConnection.Location = new System.Drawing.Point(10, 10);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(268, 210);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection";
            // 
            // _lblUrlCaption
            // 
            this._lblUrlCaption.AutoSize = true;
            this._lblUrlCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblUrlCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblUrlCaption.Location = new System.Drawing.Point(10, 24);
            this._lblUrlCaption.Name = "_lblUrlCaption";
            this._lblUrlCaption.Size = new System.Drawing.Size(48, 12);
            this._lblUrlCaption.TabIndex = 0;
            this._lblUrlCaption.Text = "JIRA URL";
            // 
            // _txtJiraUrl
            // 
            this._txtJiraUrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtJiraUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtJiraUrl.ForeColor = System.Drawing.Color.White;
            this._txtJiraUrl.Location = new System.Drawing.Point(10, 40);
            this._txtJiraUrl.Name = "_txtJiraUrl";
            this._txtJiraUrl.Size = new System.Drawing.Size(245, 24);
            this._txtJiraUrl.TabIndex = 1;
            this._txtJiraUrl.Text = "https://jira.caemilusa.com/";
            // 
            // _lblUserNameCaption
            // 
            this._lblUserNameCaption.AutoSize = true;
            this._lblUserNameCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblUserNameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblUserNameCaption.Location = new System.Drawing.Point(10, 72);
            this._lblUserNameCaption.Name = "_lblUserNameCaption";
            this._lblUserNameCaption.Size = new System.Drawing.Size(55, 12);
            this._lblUserNameCaption.TabIndex = 2;
            this._lblUserNameCaption.Text = "User Name";
            // 
            // _txtUserName
            // 
            this._txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtUserName.ForeColor = System.Drawing.Color.White;
            this._txtUserName.Location = new System.Drawing.Point(10, 88);
            this._txtUserName.Name = "_txtUserName";
            this._txtUserName.Size = new System.Drawing.Size(245, 24);
            this._txtUserName.TabIndex = 3;
            this._txtUserName.Text = "you";
            // 
            // _lblPasswordCaption
            // 
            this._lblPasswordCaption.AutoSize = true;
            this._lblPasswordCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblPasswordCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblPasswordCaption.Location = new System.Drawing.Point(10, 120);
            this._lblPasswordCaption.Name = "_lblPasswordCaption";
            this._lblPasswordCaption.Size = new System.Drawing.Size(48, 12);
            this._lblPasswordCaption.TabIndex = 4;
            this._lblPasswordCaption.Text = "Password";
            // 
            // _txtPassword
            // 
            this._txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtPassword.ForeColor = System.Drawing.Color.White;
            this._txtPassword.Location = new System.Drawing.Point(10, 136);
            this._txtPassword.Name = "_txtPassword";
            this._txtPassword.PasswordChar = '●';
            this._txtPassword.Size = new System.Drawing.Size(245, 24);
            this._txtPassword.TabIndex = 5;
            this._txtPassword.Text = "Jira Password";
            // 
            // _btnSaveSettings
            // 
            this._btnSaveSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnSaveSettings.FlatAppearance.BorderSize = 0;
            this._btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSaveSettings.ForeColor = System.Drawing.Color.White;
            this._btnSaveSettings.Location = new System.Drawing.Point(10, 172);
            this._btnSaveSettings.Name = "_btnSaveSettings";
            this._btnSaveSettings.Size = new System.Drawing.Size(116, 28);
            this._btnSaveSettings.TabIndex = 6;
            this._btnSaveSettings.Text = "Save Settings";
            this._btnSaveSettings.UseVisualStyleBackColor = false;
            this._btnSaveSettings.Click += new System.EventHandler(this.BtnSaveSettings_Click);
            // 
            // _btnTestConnection
            // 
            this._btnTestConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(175)))));
            this._btnTestConnection.FlatAppearance.BorderSize = 0;
            this._btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnTestConnection.ForeColor = System.Drawing.Color.White;
            this._btnTestConnection.Location = new System.Drawing.Point(138, 172);
            this._btnTestConnection.Name = "_btnTestConnection";
            this._btnTestConnection.Size = new System.Drawing.Size(117, 28);
            this._btnTestConnection.TabIndex = 7;
            this._btnTestConnection.Text = "Test Connection";
            this._btnTestConnection.UseVisualStyleBackColor = false;
            this._btnTestConnection.Click += new System.EventHandler(this.BtnTestConnection_Click);
            // 
            // grpQuery
            // 
            this.grpQuery.Controls.Add(this._lblPresetsCaption);
            this.grpQuery.Controls.Add(this._cmbPresets);
            this.grpQuery.Controls.Add(this._btnManagePresets);
            this.grpQuery.Controls.Add(this._lblJqlCaption);
            this.grpQuery.Controls.Add(this._txtJql);
            this.grpQuery.Controls.Add(this._btnSearch);
            this.grpQuery.Controls.Add(this._btnClearSearch);
            this.grpQuery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.grpQuery.Location = new System.Drawing.Point(10, 230);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(268, 250);
            this.grpQuery.TabIndex = 1;
            this.grpQuery.TabStop = false;
            this.grpQuery.Text = "Query";
            // 
            // _lblPresetsCaption
            // 
            this._lblPresetsCaption.AutoSize = true;
            this._lblPresetsCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblPresetsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblPresetsCaption.Location = new System.Drawing.Point(10, 24);
            this._lblPresetsCaption.Name = "_lblPresetsCaption";
            this._lblPresetsCaption.Size = new System.Drawing.Size(79, 12);
            this._lblPresetsCaption.TabIndex = 0;
            this._lblPresetsCaption.Text = "QUICK PRESETS";
            // 
            // _cmbPresets
            // 
            this._cmbPresets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPresets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cmbPresets.ForeColor = System.Drawing.Color.White;
            this._cmbPresets.Items.AddRange(new object[] {
            "— select a preset —"});
            this._cmbPresets.Location = new System.Drawing.Point(10, 40);
            this._cmbPresets.Name = "_cmbPresets";
            this._cmbPresets.Size = new System.Drawing.Size(180, 25);
            this._cmbPresets.TabIndex = 1;
            this._cmbPresets.SelectedIndexChanged += new System.EventHandler(this.CmbPresets_SelectedIndexChanged);
            // 
            // _btnManagePresets
            // 
            this._btnManagePresets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnManagePresets.FlatAppearance.BorderSize = 0;
            this._btnManagePresets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnManagePresets.ForeColor = System.Drawing.Color.White;
            this._btnManagePresets.Location = new System.Drawing.Point(194, 39);
            this._btnManagePresets.Name = "_btnManagePresets";
            this._btnManagePresets.Size = new System.Drawing.Size(61, 27);
            this._btnManagePresets.TabIndex = 2;
            this._btnManagePresets.Text = "Edit...";
            this._btnManagePresets.UseVisualStyleBackColor = false;
            this._btnManagePresets.Click += new System.EventHandler(this.BtnManagePresets_Click);
            // 
            // _lblJqlCaption
            // 
            this._lblJqlCaption.AutoSize = true;
            this._lblJqlCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblJqlCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblJqlCaption.Location = new System.Drawing.Point(10, 74);
            this._lblJqlCaption.Name = "_lblJqlCaption";
            this._lblJqlCaption.Size = new System.Drawing.Size(58, 12);
            this._lblJqlCaption.TabIndex = 2;
            this._lblJqlCaption.Text = "JQL QUERY";
            // 
            // _txtJql
            // 
            this._txtJql.AcceptsReturn = true;
            this._txtJql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtJql.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtJql.ForeColor = System.Drawing.Color.White;
            this._txtJql.Location = new System.Drawing.Point(10, 90);
            this._txtJql.Multiline = true;
            this._txtJql.Name = "_txtJql";
            this._txtJql.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtJql.Size = new System.Drawing.Size(245, 100);
            this._txtJql.TabIndex = 3;
            this._txtJql.Text = "e.g. project = MYPROJ AND status != Done";
            // 
            // _btnSearch
            // 
            this._btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._btnSearch.FlatAppearance.BorderSize = 0;
            this._btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSearch.ForeColor = System.Drawing.Color.White;
            this._btnSearch.Location = new System.Drawing.Point(10, 204);
            this._btnSearch.Name = "_btnSearch";
            this._btnSearch.Size = new System.Drawing.Size(116, 30);
            this._btnSearch.TabIndex = 4;
            this._btnSearch.Text = "Search";
            this._btnSearch.UseVisualStyleBackColor = false;
            this._btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // _btnClearSearch
            // 
            this._btnClearSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(78)))));
            this._btnClearSearch.FlatAppearance.BorderSize = 0;
            this._btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnClearSearch.ForeColor = System.Drawing.Color.White;
            this._btnClearSearch.Location = new System.Drawing.Point(138, 204);
            this._btnClearSearch.Name = "_btnClearSearch";
            this._btnClearSearch.Size = new System.Drawing.Size(117, 30);
            this._btnClearSearch.TabIndex = 5;
            this._btnClearSearch.Text = "Clear";
            this._btnClearSearch.UseVisualStyleBackColor = false;
            this._btnClearSearch.Click += new System.EventHandler(this.BtnClearSearch_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.pnlRight.Controls.Add(this._lvResults);
            this.pnlRight.Controls.Add(this._lblResultsCaption);
            this.pnlRight.Controls.Add(this.pnlActionBar);
            this.pnlRight.Controls.Add(this.pnlMasterList);
            this.pnlRight.Controls.Add(this._lblStatus);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(290, 46);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 6, 10, 0);
            this.pnlRight.Size = new System.Drawing.Size(910, 714);
            this.pnlRight.TabIndex = 2;
            // 
            // _lvResults
            // 
            this._lvResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this._lvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lvResults.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lvResults.ForeColor = System.Drawing.Color.White;
            this._lvResults.FullRowSelect = true;
            this._lvResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._lvResults.HideSelection = false;
            this._lvResults.Location = new System.Drawing.Point(10, 18);
            this._lvResults.Name = "_lvResults";
            this._lvResults.Size = new System.Drawing.Size(890, 398);
            this._lvResults.TabIndex = 1;
            this._lvResults.UseCompatibleStateImageBehavior = false;
            this._lvResults.View = System.Windows.Forms.View.Details;
            this._lvResults.DoubleClick += new System.EventHandler(this.LvResults_DoubleClick);
            // 
            // _lblResultsCaption
            // 
            this._lblResultsCaption.AutoSize = true;
            this._lblResultsCaption.BackColor = System.Drawing.Color.Transparent;
            this._lblResultsCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblResultsCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblResultsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblResultsCaption.Location = new System.Drawing.Point(10, 6);
            this._lblResultsCaption.Name = "_lblResultsCaption";
            this._lblResultsCaption.Size = new System.Drawing.Size(365, 12);
            this._lblResultsCaption.TabIndex = 0;
            this._lblResultsCaption.Text = "SEARCH RESULTS  –  select rows then use buttons below to add to master list";
            // 
            // pnlActionBar
            // 
            this.pnlActionBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(46)))));
            this.pnlActionBar.Controls.Add(this._btnAddIssue);
            this.pnlActionBar.Controls.Add(this._btnAddParent);
            this.pnlActionBar.Controls.Add(this._btnExportExcel);
            this.pnlActionBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionBar.Location = new System.Drawing.Point(10, 416);
            this.pnlActionBar.Name = "pnlActionBar";
            this.pnlActionBar.Size = new System.Drawing.Size(890, 44);
            this.pnlActionBar.TabIndex = 2;
            // 
            // _btnAddIssue
            // 
            this._btnAddIssue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(60)))));
            this._btnAddIssue.FlatAppearance.BorderSize = 0;
            this._btnAddIssue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddIssue.ForeColor = System.Drawing.Color.White;
            this._btnAddIssue.Location = new System.Drawing.Point(8, 8);
            this._btnAddIssue.Name = "_btnAddIssue";
            this._btnAddIssue.Size = new System.Drawing.Size(160, 28);
            this._btnAddIssue.TabIndex = 0;
            this._btnAddIssue.Text = "↓  Add to Master List";
            this._btnAddIssue.UseVisualStyleBackColor = false;
            this._btnAddIssue.Click += new System.EventHandler(this.BtnAddIssue_Click);
            // 
            // _btnAddParent
            // 
            this._btnAddParent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(100)))));
            this._btnAddParent.FlatAppearance.BorderSize = 0;
            this._btnAddParent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddParent.ForeColor = System.Drawing.Color.White;
            this._btnAddParent.Location = new System.Drawing.Point(178, 8);
            this._btnAddParent.Name = "_btnAddParent";
            this._btnAddParent.Size = new System.Drawing.Size(200, 28);
            this._btnAddParent.TabIndex = 1;
            this._btnAddParent.Text = "↓  Add Parent / Epic";
            this._btnAddParent.UseVisualStyleBackColor = false;
            this._btnAddParent.Click += new System.EventHandler(this.BtnAddParent_Click);
            // 
            // _btnExportExcel
            // 
            this._btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(45)))), ((int)(((byte)(100)))));
            this._btnExportExcel.FlatAppearance.BorderSize = 0;
            this._btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnExportExcel.ForeColor = System.Drawing.Color.White;
            this._btnExportExcel.Location = new System.Drawing.Point(388, 8);
            this._btnExportExcel.Name = "_btnExportExcel";
            this._btnExportExcel.Size = new System.Drawing.Size(180, 28);
            this._btnExportExcel.TabIndex = 2;
            this._btnExportExcel.Text = "⬇  Export to Excel…";
            this._btnExportExcel.UseVisualStyleBackColor = false;
            this._btnExportExcel.Click += new System.EventHandler(this.BtnExportExcel_Click);
            // 
            // pnlMasterList
            // 
            this.pnlMasterList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.pnlMasterList.Controls.Add(this._lblMasterCount);
            this.pnlMasterList.Controls.Add(this._lvMaster);
            this.pnlMasterList.Controls.Add(this._btnRemoveMaster);
            this.pnlMasterList.Controls.Add(this._btnClearMaster);
            this.pnlMasterList.Controls.Add(this._btnImportTasks);
            this.pnlMasterList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMasterList.Location = new System.Drawing.Point(10, 460);
            this.pnlMasterList.Name = "pnlMasterList";
            this.pnlMasterList.Padding = new System.Windows.Forms.Padding(6, 4, 6, 6);
            this.pnlMasterList.Size = new System.Drawing.Size(890, 232);
            this.pnlMasterList.TabIndex = 3;
            // 
            // _lblMasterCount
            // 
            this._lblMasterCount.AutoSize = true;
            this._lblMasterCount.BackColor = System.Drawing.Color.Transparent;
            this._lblMasterCount.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblMasterCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblMasterCount.Location = new System.Drawing.Point(6, 6);
            this._lblMasterCount.Name = "_lblMasterCount";
            this._lblMasterCount.Size = new System.Drawing.Size(120, 15);
            this._lblMasterCount.TabIndex = 0;
            this._lblMasterCount.Text = "Master list: 0 item(s)";
            // 
            // _lvMaster
            // 
            this._lvMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lvMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this._lvMaster.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lvMaster.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lvMaster.ForeColor = System.Drawing.Color.White;
            this._lvMaster.FullRowSelect = true;
            this._lvMaster.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._lvMaster.HideSelection = false;
            this._lvMaster.Location = new System.Drawing.Point(6, 26);
            this._lvMaster.Name = "_lvMaster";
            this._lvMaster.Size = new System.Drawing.Size(878, 158);
            this._lvMaster.TabIndex = 1;
            this._lvMaster.UseCompatibleStateImageBehavior = false;
            this._lvMaster.View = System.Windows.Forms.View.Details;
            // 
            // _btnRemoveMaster
            // 
            this._btnRemoveMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnRemoveMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this._btnRemoveMaster.FlatAppearance.BorderSize = 0;
            this._btnRemoveMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnRemoveMaster.ForeColor = System.Drawing.Color.White;
            this._btnRemoveMaster.Location = new System.Drawing.Point(6, 194);
            this._btnRemoveMaster.Name = "_btnRemoveMaster";
            this._btnRemoveMaster.Size = new System.Drawing.Size(120, 28);
            this._btnRemoveMaster.TabIndex = 2;
            this._btnRemoveMaster.Text = "Remove Selected";
            this._btnRemoveMaster.UseVisualStyleBackColor = false;
            this._btnRemoveMaster.Click += new System.EventHandler(this.BtnRemoveMaster_Click);
            // 
            // _btnClearMaster
            // 
            this._btnClearMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnClearMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(78)))));
            this._btnClearMaster.FlatAppearance.BorderSize = 0;
            this._btnClearMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnClearMaster.ForeColor = System.Drawing.Color.White;
            this._btnClearMaster.Location = new System.Drawing.Point(136, 194);
            this._btnClearMaster.Name = "_btnClearMaster";
            this._btnClearMaster.Size = new System.Drawing.Size(90, 28);
            this._btnClearMaster.TabIndex = 3;
            this._btnClearMaster.Text = "Clear All";
            this._btnClearMaster.UseVisualStyleBackColor = false;
            this._btnClearMaster.Click += new System.EventHandler(this.BtnClearMaster_Click);
            // 
            // _btnImportTasks
            // 
            this._btnImportTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnImportTasks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._btnImportTasks.FlatAppearance.BorderSize = 0;
            this._btnImportTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnImportTasks.ForeColor = System.Drawing.Color.White;
            this._btnImportTasks.Location = new System.Drawing.Point(756, 194);
            this._btnImportTasks.Name = "_btnImportTasks";
            this._btnImportTasks.Size = new System.Drawing.Size(128, 28);
            this._btnImportTasks.TabIndex = 4;
            this._btnImportTasks.Text = "⬆  Import to Tasks";
            this._btnImportTasks.UseVisualStyleBackColor = false;
            this._btnImportTasks.Click += new System.EventHandler(this.BtnImportTasks_Click);
            // 
            // _lblStatus
            // 
            this._lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblStatus.Location = new System.Drawing.Point(10, 692);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(890, 22);
            this._lblStatus.TabIndex = 4;
            // 
            // JiraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.pnlToolbar);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "JiraForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jira Query";
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlSettings.ResumeLayout(false);
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpQuery.ResumeLayout(false);
            this.grpQuery.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlActionBar.ResumeLayout(false);
            this.pnlMasterList.ResumeLayout(false);
            this.pnlMasterList.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
