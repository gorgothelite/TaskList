namespace Test
{
    partial class JiraExportDialog
    {
        private System.ComponentModel.IContainer components = null;

        // ── Header panel ──────────────────────────────────────────────────────
        private System.Windows.Forms.Panel  pnlHeader;
        private System.Windows.Forms.Label  _lblTitle;

        // ── Source group ──────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox    grpSource;
        private System.Windows.Forms.RadioButton _radResults;
        private System.Windows.Forms.RadioButton _radMaster;

        // ── Include group ─────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpInclude;
        private System.Windows.Forms.CheckBox _chkWorklogs;
        private System.Windows.Forms.CheckBox _chkComments;

        // ── AI Summary group ──────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpAiSummary;
        private System.Windows.Forms.CheckBox _chkSummarize;
        private System.Windows.Forms.Label    _lblTemplate;
        private System.Windows.Forms.ComboBox _cmbTemplate;
        private System.Windows.Forms.Button   _btnEditTemplates;
        private System.Windows.Forms.Label    _lblProvider;
        private System.Windows.Forms.ComboBox _cmbProvider;
        private System.Windows.Forms.Label    _lblAiKey;
        private System.Windows.Forms.TextBox  _txtAiKey;
        private System.Windows.Forms.Label    _lblAzureEndpoint;
        private System.Windows.Forms.TextBox  _txtAzureEndpoint;

        // ── Columns group ─────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpColumns;
        private System.Windows.Forms.CheckBox _chkColKey;
        private System.Windows.Forms.CheckBox _chkColSummary;
        private System.Windows.Forms.CheckBox _chkColType;
        private System.Windows.Forms.CheckBox _chkColStatus;
        private System.Windows.Forms.CheckBox _chkColPriority;
        private System.Windows.Forms.CheckBox _chkColProject;
        private System.Windows.Forms.CheckBox _chkColAssignee;
        private System.Windows.Forms.CheckBox _chkColDueDate;
        private System.Windows.Forms.CheckBox _chkColRecordType;
        private System.Windows.Forms.CheckBox _chkColDate;
        private System.Windows.Forms.CheckBox _chkColAuthor;
        private System.Windows.Forms.CheckBox _chkColHours;
        private System.Windows.Forms.CheckBox _chkColText;

        // ── Preview group ─────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox        grpPreview;
        private System.Windows.Forms.Button          _btnLoadPreview;
        private System.Windows.Forms.Label           _lblPreviewInfo;
        private System.Windows.Forms.DataGridView    _dgvPreview;

        // ── Status & buttons ──────────────────────────────────────────────────
        private System.Windows.Forms.Label  _lblStatus;
        private System.Windows.Forms.Button _btnEditRecipients;
        private System.Windows.Forms.Button _btnSendOutlook;
        private System.Windows.Forms.Button _btnExport;
        private System.Windows.Forms.Button _btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this._lblTitle = new System.Windows.Forms.Label();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this._radResults = new System.Windows.Forms.RadioButton();
            this._radMaster = new System.Windows.Forms.RadioButton();
            this.grpInclude = new System.Windows.Forms.GroupBox();
            this._chkWorklogs = new System.Windows.Forms.CheckBox();
            this._chkComments = new System.Windows.Forms.CheckBox();
            this.grpAiSummary = new System.Windows.Forms.GroupBox();
            this._chkSummarize = new System.Windows.Forms.CheckBox();
            this._lblProvider = new System.Windows.Forms.Label();
            this._cmbProvider = new System.Windows.Forms.ComboBox();
            this._lblAiKey = new System.Windows.Forms.Label();
            this._txtAiKey = new System.Windows.Forms.TextBox();
            this._lblAzureEndpoint  = new System.Windows.Forms.Label();
            this._txtAzureEndpoint  = new System.Windows.Forms.TextBox();
            this._lblTemplate       = new System.Windows.Forms.Label();
            this._cmbTemplate       = new System.Windows.Forms.ComboBox();
            this._btnEditTemplates  = new System.Windows.Forms.Button();
            this.grpColumns = new System.Windows.Forms.GroupBox();
            this._chkColKey = new System.Windows.Forms.CheckBox();
            this._chkColSummary = new System.Windows.Forms.CheckBox();
            this._chkColType = new System.Windows.Forms.CheckBox();
            this._chkColStatus = new System.Windows.Forms.CheckBox();
            this._chkColPriority = new System.Windows.Forms.CheckBox();
            this._chkColProject = new System.Windows.Forms.CheckBox();
            this._chkColAssignee = new System.Windows.Forms.CheckBox();
            this._chkColDueDate = new System.Windows.Forms.CheckBox();
            this._chkColRecordType = new System.Windows.Forms.CheckBox();
            this._chkColDate = new System.Windows.Forms.CheckBox();
            this._chkColAuthor = new System.Windows.Forms.CheckBox();
            this._chkColHours = new System.Windows.Forms.CheckBox();
            this._chkColText = new System.Windows.Forms.CheckBox();
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this._btnLoadPreview = new System.Windows.Forms.Button();
            this._lblPreviewInfo = new System.Windows.Forms.Label();
            this._dgvPreview = new System.Windows.Forms.DataGridView();
            this._lblStatus          = new System.Windows.Forms.Label();
            this._btnEditRecipients  = new System.Windows.Forms.Button();
            this._btnSendOutlook     = new System.Windows.Forms.Button();
            this._btnExport          = new System.Windows.Forms.Button();
            this._btnCancel          = new System.Windows.Forms.Button();
            this._btnSaveSettings    = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.grpSource.SuspendLayout();
            this.grpInclude.SuspendLayout();
            this.grpAiSummary.SuspendLayout();
            this.grpColumns.SuspendLayout();
            this.grpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(46)))));
            this.pnlHeader.Controls.Add(this._lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(720, 42);
            this.pnlHeader.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.White;
            this._lblTitle.Location = new System.Drawing.Point(12, 10);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(179, 20);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "Export Jira Data to Excel";
            // 
            // grpSource
            // 
            this.grpSource.Controls.Add(this._radResults);
            this.grpSource.Controls.Add(this._radMaster);
            this.grpSource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.grpSource.Location = new System.Drawing.Point(14, 52);
            this.grpSource.Name = "grpSource";
            this.grpSource.Size = new System.Drawing.Size(200, 80);
            this.grpSource.TabIndex = 1;
            this.grpSource.TabStop = false;
            this.grpSource.Text = "Data Source";
            // 
            // _radResults
            // 
            this._radResults.AutoSize = true;
            this._radResults.Checked = true;
            this._radResults.ForeColor = System.Drawing.Color.White;
            this._radResults.Location = new System.Drawing.Point(14, 22);
            this._radResults.Name = "_radResults";
            this._radResults.Size = new System.Drawing.Size(110, 21);
            this._radResults.TabIndex = 0;
            this._radResults.TabStop = true;
            this._radResults.Text = "Search Results";
            this._radResults.CheckedChanged += new System.EventHandler(this.RadSource_Changed);
            // 
            // _radMaster
            // 
            this._radMaster.AutoSize = true;
            this._radMaster.ForeColor = System.Drawing.Color.White;
            this._radMaster.Location = new System.Drawing.Point(14, 48);
            this._radMaster.Name = "_radMaster";
            this._radMaster.Size = new System.Drawing.Size(90, 21);
            this._radMaster.TabIndex = 1;
            this._radMaster.Text = "Master List";
            this._radMaster.CheckedChanged += new System.EventHandler(this.RadSource_Changed);
            // 
            // grpInclude
            // 
            this.grpInclude.Controls.Add(this._chkWorklogs);
            this.grpInclude.Controls.Add(this._chkComments);
            this.grpInclude.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.grpInclude.Location = new System.Drawing.Point(226, 52);
            this.grpInclude.Name = "grpInclude";
            this.grpInclude.Size = new System.Drawing.Size(140, 80);
            this.grpInclude.TabIndex = 2;
            this.grpInclude.TabStop = false;
            this.grpInclude.Text = "Include";
            // 
            // _chkWorklogs
            // 
            this._chkWorklogs.AutoSize = true;
            this._chkWorklogs.Checked = true;
            this._chkWorklogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkWorklogs.ForeColor = System.Drawing.Color.White;
            this._chkWorklogs.Location = new System.Drawing.Point(14, 22);
            this._chkWorklogs.Name = "_chkWorklogs";
            this._chkWorklogs.Size = new System.Drawing.Size(82, 21);
            this._chkWorklogs.TabIndex = 0;
            this._chkWorklogs.Text = "Worklogs";
            // 
            // _chkComments
            // 
            this._chkComments.AutoSize = true;
            this._chkComments.Checked = true;
            this._chkComments.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkComments.ForeColor = System.Drawing.Color.White;
            this._chkComments.Location = new System.Drawing.Point(14, 48);
            this._chkComments.Name = "_chkComments";
            this._chkComments.Size = new System.Drawing.Size(89, 21);
            this._chkComments.TabIndex = 1;
            this._chkComments.Text = "Comments";
            // 
            // grpAiSummary
            // 
            this.grpAiSummary.Controls.Add(this._btnSaveSettings);
            this.grpAiSummary.Controls.Add(this._chkSummarize);
            this.grpAiSummary.Controls.Add(this._lblTemplate);
            this.grpAiSummary.Controls.Add(this._cmbTemplate);
            this.grpAiSummary.Controls.Add(this._btnEditTemplates);
            this.grpAiSummary.Controls.Add(this._lblProvider);
            this.grpAiSummary.Controls.Add(this._cmbProvider);
            this.grpAiSummary.Controls.Add(this._lblAiKey);
            this.grpAiSummary.Controls.Add(this._txtAiKey);
            this.grpAiSummary.Controls.Add(this._lblAzureEndpoint);
            this.grpAiSummary.Controls.Add(this._txtAzureEndpoint);
            this.grpAiSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.grpAiSummary.Location = new System.Drawing.Point(14, 144);
            this.grpAiSummary.Name = "grpAiSummary";
            this.grpAiSummary.Size = new System.Drawing.Size(692, 192);
            this.grpAiSummary.TabIndex = 3;
            this.grpAiSummary.TabStop = false;
            this.grpAiSummary.Text = "AI Summary";
            // 
            // _chkSummarize
            // 
            this._chkSummarize.AutoSize = true;
            this._chkSummarize.ForeColor = System.Drawing.Color.White;
            this._chkSummarize.Location = new System.Drawing.Point(14, 22);
            this._chkSummarize.Name = "_chkSummarize";
            this._chkSummarize.Size = new System.Drawing.Size(206, 21);
            this._chkSummarize.TabIndex = 0;
            this._chkSummarize.Text = "Summarize with AI after export";
            //
            // _lblTemplate
            //
            this._lblTemplate.AutoSize  = true;
            this._lblTemplate.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblTemplate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblTemplate.Location  = new System.Drawing.Point(14, 52);
            this._lblTemplate.Name      = "_lblTemplate";
            this._lblTemplate.TabIndex  = 8;
            this._lblTemplate.Text      = "PROMPT TEMPLATE";
            //
            // _cmbTemplate
            //
            this._cmbTemplate.BackColor     = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbTemplate.FlatStyle     = System.Windows.Forms.FlatStyle.Flat;
            this._cmbTemplate.ForeColor     = System.Drawing.Color.White;
            this._cmbTemplate.Location      = new System.Drawing.Point(14, 66);
            this._cmbTemplate.Name          = "_cmbTemplate";
            this._cmbTemplate.Size          = new System.Drawing.Size(326, 25);
            this._cmbTemplate.TabIndex      = 9;
            this._cmbTemplate.SelectedIndexChanged += new System.EventHandler(this.CmbTemplate_Changed);
            //
            // _btnEditTemplates
            //
            this._btnEditTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnEditTemplates.FlatAppearance.BorderSize = 0;
            this._btnEditTemplates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnEditTemplates.ForeColor = System.Drawing.Color.White;
            this._btnEditTemplates.Location  = new System.Drawing.Point(348, 65);
            this._btnEditTemplates.Name      = "_btnEditTemplates";
            this._btnEditTemplates.Size      = new System.Drawing.Size(118, 27);
            this._btnEditTemplates.TabIndex  = 10;
            this._btnEditTemplates.Text      = "Edit Templates\u2026";
            this._btnEditTemplates.UseVisualStyleBackColor = false;
            this._btnEditTemplates.Click    += new System.EventHandler(this.BtnEditTemplates_Click);
            //
            // _lblProvider
            //
            this._lblProvider.AutoSize = true;
            this._lblProvider.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblProvider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblProvider.Location = new System.Drawing.Point(14, 96);
            this._lblProvider.Name = "_lblProvider";
            this._lblProvider.Size = new System.Drawing.Size(55, 12);
            this._lblProvider.TabIndex = 1;
            this._lblProvider.Text = "PROVIDER";
            // 
            // _cmbProvider
            // 
            this._cmbProvider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cmbProvider.ForeColor = System.Drawing.Color.White;
            this._cmbProvider.Items.AddRange(new object[] {
            "Claude (Anthropic)",
            "ChatGPT (OpenAI)",
            "Azure OpenAI (Copilot)"});
            this._cmbProvider.Location = new System.Drawing.Point(14, 110);
            this._cmbProvider.Name = "_cmbProvider";
            this._cmbProvider.Size = new System.Drawing.Size(200, 25);
            this._cmbProvider.TabIndex = 2;
            this._cmbProvider.SelectedIndexChanged += new System.EventHandler(this.CmbProvider_Changed);
            // 
            // _lblAiKey
            // 
            this._lblAiKey.AutoSize = true;
            this._lblAiKey.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblAiKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblAiKey.Location = new System.Drawing.Point(228, 96);
            this._lblAiKey.Name = "_lblAiKey";
            this._lblAiKey.Size = new System.Drawing.Size(103, 12);
            this._lblAiKey.TabIndex = 3;
            this._lblAiKey.Text = "ANTHROPIC API KEY";
            // 
            // _txtAiKey
            // 
            this._txtAiKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtAiKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtAiKey.ForeColor = System.Drawing.Color.White;
            this._txtAiKey.Location = new System.Drawing.Point(228, 110);
            this._txtAiKey.Name = "_txtAiKey";
            this._txtAiKey.PasswordChar = '●';
            this._txtAiKey.Size = new System.Drawing.Size(452, 24);
            this._txtAiKey.TabIndex = 4;
            // 
            // _lblAzureEndpoint
            // 
            this._lblAzureEndpoint.AutoSize = true;
            this._lblAzureEndpoint.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblAzureEndpoint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblAzureEndpoint.Location = new System.Drawing.Point(14, 148);
            this._lblAzureEndpoint.Name = "_lblAzureEndpoint";
            this._lblAzureEndpoint.Size = new System.Drawing.Size(303, 12);
            this._lblAzureEndpoint.TabIndex = 5;
            this._lblAzureEndpoint.Text = "AZURE ENDPOINT URL  (full URL incl. deployment & api-version)";
            this._lblAzureEndpoint.Visible = false;
            // 
            // _txtAzureEndpoint
            // 
            this._txtAzureEndpoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtAzureEndpoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtAzureEndpoint.ForeColor = System.Drawing.Color.White;
            this._txtAzureEndpoint.Location = new System.Drawing.Point(14, 162);
            this._txtAzureEndpoint.Name = "_txtAzureEndpoint";
            this._txtAzureEndpoint.Size = new System.Drawing.Size(666, 24);
            this._txtAzureEndpoint.TabIndex = 6;
            this._txtAzureEndpoint.Visible = false;
            // 
            // grpColumns
            // 
            this.grpColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpColumns.Controls.Add(this._chkColKey);
            this.grpColumns.Controls.Add(this._chkColSummary);
            this.grpColumns.Controls.Add(this._chkColType);
            this.grpColumns.Controls.Add(this._chkColStatus);
            this.grpColumns.Controls.Add(this._chkColPriority);
            this.grpColumns.Controls.Add(this._chkColProject);
            this.grpColumns.Controls.Add(this._chkColAssignee);
            this.grpColumns.Controls.Add(this._chkColDueDate);
            this.grpColumns.Controls.Add(this._chkColRecordType);
            this.grpColumns.Controls.Add(this._chkColDate);
            this.grpColumns.Controls.Add(this._chkColAuthor);
            this.grpColumns.Controls.Add(this._chkColHours);
            this.grpColumns.Controls.Add(this._chkColText);
            this.grpColumns.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.grpColumns.Location = new System.Drawing.Point(14, 350);
            this.grpColumns.Name = "grpColumns";
            this.grpColumns.Size = new System.Drawing.Size(692, 130);
            this.grpColumns.TabIndex = 4;
            this.grpColumns.TabStop = false;
            this.grpColumns.Text = "Columns to export";
            // 
            // _chkColKey
            // 
            this._chkColKey.AutoSize = true;
            this._chkColKey.Checked = true;
            this._chkColKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColKey.ForeColor = System.Drawing.Color.White;
            this._chkColKey.Location = new System.Drawing.Point(14, 24);
            this._chkColKey.Name = "_chkColKey";
            this._chkColKey.Size = new System.Drawing.Size(48, 21);
            this._chkColKey.TabIndex = 0;
            this._chkColKey.Text = "Key";
            // 
            // _chkColSummary
            // 
            this._chkColSummary.AutoSize = true;
            this._chkColSummary.Checked = true;
            this._chkColSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColSummary.ForeColor = System.Drawing.Color.White;
            this._chkColSummary.Location = new System.Drawing.Point(80, 24);
            this._chkColSummary.Name = "_chkColSummary";
            this._chkColSummary.Size = new System.Drawing.Size(81, 21);
            this._chkColSummary.TabIndex = 1;
            this._chkColSummary.Text = "Summary";
            // 
            // _chkColType
            // 
            this._chkColType.AutoSize = true;
            this._chkColType.Checked = true;
            this._chkColType.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColType.ForeColor = System.Drawing.Color.White;
            this._chkColType.Location = new System.Drawing.Point(220, 24);
            this._chkColType.Name = "_chkColType";
            this._chkColType.Size = new System.Drawing.Size(54, 21);
            this._chkColType.TabIndex = 2;
            this._chkColType.Text = "Type";
            // 
            // _chkColStatus
            // 
            this._chkColStatus.AutoSize = true;
            this._chkColStatus.Checked = true;
            this._chkColStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColStatus.ForeColor = System.Drawing.Color.White;
            this._chkColStatus.Location = new System.Drawing.Point(310, 24);
            this._chkColStatus.Name = "_chkColStatus";
            this._chkColStatus.Size = new System.Drawing.Size(62, 21);
            this._chkColStatus.TabIndex = 3;
            this._chkColStatus.Text = "Status";
            // 
            // _chkColPriority
            // 
            this._chkColPriority.AutoSize = true;
            this._chkColPriority.Checked = true;
            this._chkColPriority.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColPriority.ForeColor = System.Drawing.Color.White;
            this._chkColPriority.Location = new System.Drawing.Point(410, 24);
            this._chkColPriority.Name = "_chkColPriority";
            this._chkColPriority.Size = new System.Drawing.Size(68, 21);
            this._chkColPriority.TabIndex = 4;
            this._chkColPriority.Text = "Priority";
            // 
            // _chkColProject
            // 
            this._chkColProject.AutoSize = true;
            this._chkColProject.Checked = true;
            this._chkColProject.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColProject.ForeColor = System.Drawing.Color.White;
            this._chkColProject.Location = new System.Drawing.Point(14, 52);
            this._chkColProject.Name = "_chkColProject";
            this._chkColProject.Size = new System.Drawing.Size(67, 21);
            this._chkColProject.TabIndex = 5;
            this._chkColProject.Text = "Project";
            // 
            // _chkColAssignee
            // 
            this._chkColAssignee.AutoSize = true;
            this._chkColAssignee.Checked = true;
            this._chkColAssignee.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColAssignee.ForeColor = System.Drawing.Color.White;
            this._chkColAssignee.Location = new System.Drawing.Point(110, 52);
            this._chkColAssignee.Name = "_chkColAssignee";
            this._chkColAssignee.Size = new System.Drawing.Size(79, 21);
            this._chkColAssignee.TabIndex = 6;
            this._chkColAssignee.Text = "Assignee";
            // 
            // _chkColDueDate
            // 
            this._chkColDueDate.AutoSize = true;
            this._chkColDueDate.Checked = true;
            this._chkColDueDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColDueDate.ForeColor = System.Drawing.Color.White;
            this._chkColDueDate.Location = new System.Drawing.Point(220, 52);
            this._chkColDueDate.Name = "_chkColDueDate";
            this._chkColDueDate.Size = new System.Drawing.Size(81, 21);
            this._chkColDueDate.TabIndex = 7;
            this._chkColDueDate.Text = "Due Date";
            // 
            // _chkColRecordType
            // 
            this._chkColRecordType.AutoSize = true;
            this._chkColRecordType.Checked = true;
            this._chkColRecordType.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColRecordType.ForeColor = System.Drawing.Color.White;
            this._chkColRecordType.Location = new System.Drawing.Point(330, 52);
            this._chkColRecordType.Name = "_chkColRecordType";
            this._chkColRecordType.Size = new System.Drawing.Size(100, 21);
            this._chkColRecordType.TabIndex = 8;
            this._chkColRecordType.Text = "Record Type";
            // 
            // _chkColDate
            // 
            this._chkColDate.AutoSize = true;
            this._chkColDate.Checked = true;
            this._chkColDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColDate.ForeColor = System.Drawing.Color.White;
            this._chkColDate.Location = new System.Drawing.Point(460, 52);
            this._chkColDate.Name = "_chkColDate";
            this._chkColDate.Size = new System.Drawing.Size(54, 21);
            this._chkColDate.TabIndex = 9;
            this._chkColDate.Text = "Date";
            // 
            // _chkColAuthor
            // 
            this._chkColAuthor.AutoSize = true;
            this._chkColAuthor.Checked = true;
            this._chkColAuthor.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColAuthor.ForeColor = System.Drawing.Color.White;
            this._chkColAuthor.Location = new System.Drawing.Point(14, 80);
            this._chkColAuthor.Name = "_chkColAuthor";
            this._chkColAuthor.Size = new System.Drawing.Size(66, 21);
            this._chkColAuthor.TabIndex = 10;
            this._chkColAuthor.Text = "Author";
            // 
            // _chkColHours
            // 
            this._chkColHours.AutoSize = true;
            this._chkColHours.Checked = true;
            this._chkColHours.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColHours.ForeColor = System.Drawing.Color.White;
            this._chkColHours.Location = new System.Drawing.Point(110, 80);
            this._chkColHours.Name = "_chkColHours";
            this._chkColHours.Size = new System.Drawing.Size(62, 21);
            this._chkColHours.TabIndex = 11;
            this._chkColHours.Text = "Hours";
            // 
            // _chkColText
            // 
            this._chkColText.AutoSize = true;
            this._chkColText.Checked = true;
            this._chkColText.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkColText.ForeColor = System.Drawing.Color.White;
            this._chkColText.Location = new System.Drawing.Point(210, 80);
            this._chkColText.Name = "_chkColText";
            this._chkColText.Size = new System.Drawing.Size(50, 21);
            this._chkColText.TabIndex = 12;
            this._chkColText.Text = "Text";
            // 
            // grpPreview
            // 
            this.grpPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPreview.Controls.Add(this._btnLoadPreview);
            this.grpPreview.Controls.Add(this._lblPreviewInfo);
            this.grpPreview.Controls.Add(this._dgvPreview);
            this.grpPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.grpPreview.Location = new System.Drawing.Point(14, 494);
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.Size = new System.Drawing.Size(692, 248);
            this.grpPreview.TabIndex = 5;
            this.grpPreview.TabStop = false;
            this.grpPreview.Text = "Preview";
            // 
            // _btnLoadPreview
            // 
            this._btnLoadPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(68)))));
            this._btnLoadPreview.FlatAppearance.BorderSize = 0;
            this._btnLoadPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnLoadPreview.ForeColor = System.Drawing.Color.White;
            this._btnLoadPreview.Location = new System.Drawing.Point(14, 22);
            this._btnLoadPreview.Name = "_btnLoadPreview";
            this._btnLoadPreview.Size = new System.Drawing.Size(130, 26);
            this._btnLoadPreview.TabIndex = 0;
            this._btnLoadPreview.Text = "Load Preview";
            this._btnLoadPreview.UseVisualStyleBackColor = false;
            this._btnLoadPreview.Click += new System.EventHandler(this.BtnLoadPreview_Click);
            // 
            // _lblPreviewInfo
            // 
            this._lblPreviewInfo.AutoSize = true;
            this._lblPreviewInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblPreviewInfo.Location = new System.Drawing.Point(154, 26);
            this._lblPreviewInfo.Name = "_lblPreviewInfo";
            this._lblPreviewInfo.Size = new System.Drawing.Size(415, 17);
            this._lblPreviewInfo.TabIndex = 1;
            this._lblPreviewInfo.Text = "Click \'Load Preview\' to see a sample of the export data (first 5 issues).";
            // 
            // _dgvPreview
            // 
            this._dgvPreview.AllowUserToAddRows = false;
            this._dgvPreview.AllowUserToDeleteRows = false;
            this._dgvPreview.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this._dgvPreview.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this._dgvPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvPreview.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(32)))));
            this._dgvPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._dgvPreview.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._dgvPreview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this._dgvPreview.ColumnHeadersHeight = 26;
            this._dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgvPreview.DefaultCellStyle = dataGridViewCellStyle6;
            this._dgvPreview.EnableHeadersVisualStyles = false;
            this._dgvPreview.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._dgvPreview.Location = new System.Drawing.Point(14, 56);
            this._dgvPreview.Name = "_dgvPreview";
            this._dgvPreview.ReadOnly = true;
            this._dgvPreview.RowHeadersVisible = false;
            this._dgvPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvPreview.Size = new System.Drawing.Size(664, 178);
            this._dgvPreview.TabIndex = 2;
            // 
            // _lblStatus
            // 
            this._lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblStatus.AutoSize = true;
            this._lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this._lblStatus.Location = new System.Drawing.Point(14, 758);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(47, 17);
            this._lblStatus.TabIndex = 6;
            this._lblStatus.Text = "Ready.";
            //
            // _btnEditRecipients
            //
            this._btnEditRecipients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnEditRecipients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnEditRecipients.FlatAppearance.BorderSize = 0;
            this._btnEditRecipients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnEditRecipients.ForeColor = System.Drawing.Color.White;
            this._btnEditRecipients.Location = new System.Drawing.Point(198, 752);
            this._btnEditRecipients.Name = "_btnEditRecipients";
            this._btnEditRecipients.Size = new System.Drawing.Size(120, 28);
            this._btnEditRecipients.TabIndex = 7;
            this._btnEditRecipients.Text = "Edit Recipients\u2026";
            this._btnEditRecipients.UseVisualStyleBackColor = false;
            this._btnEditRecipients.Click += new System.EventHandler(this.BtnEditRecipients_Click);
            //
            // _btnSendOutlook
            //
            this._btnSendOutlook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSendOutlook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this._btnSendOutlook.FlatAppearance.BorderSize = 0;
            this._btnSendOutlook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSendOutlook.ForeColor = System.Drawing.Color.White;
            this._btnSendOutlook.Location = new System.Drawing.Point(328, 752);
            this._btnSendOutlook.Name = "_btnSendOutlook";
            this._btnSendOutlook.Size = new System.Drawing.Size(152, 28);
            this._btnSendOutlook.TabIndex = 8;
            this._btnSendOutlook.Text = "Send via Outlook\u2026";
            this._btnSendOutlook.UseVisualStyleBackColor = false;
            this._btnSendOutlook.Click += new System.EventHandler(this.BtnSendOutlook_Click);
            //
            // _btnExport
            //
            this._btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._btnExport.FlatAppearance.BorderSize = 0;
            this._btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnExport.ForeColor = System.Drawing.Color.White;
            this._btnExport.Location = new System.Drawing.Point(490, 752);
            this._btnExport.Name = "_btnExport";
            this._btnExport.Size = new System.Drawing.Size(152, 28);
            this._btnExport.TabIndex = 9;
            this._btnExport.Text = "Export to Excel\u2026";
            this._btnExport.UseVisualStyleBackColor = false;
            this._btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(78)))));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.FlatAppearance.BorderSize = 0;
            this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.ForeColor = System.Drawing.Color.White;
            this._btnCancel.Location = new System.Drawing.Point(652, 752);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(54, 28);
            this._btnCancel.TabIndex = 8;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = false;
            // 
            // _btnSaveSettings
            // 
            this._btnSaveSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnSaveSettings.FlatAppearance.BorderSize = 0;
            this._btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSaveSettings.ForeColor = System.Drawing.Color.White;
            this._btnSaveSettings.Location = new System.Drawing.Point(562, 23);
            this._btnSaveSettings.Name = "_btnSaveSettings";
            this._btnSaveSettings.Size = new System.Drawing.Size(116, 28);
            this._btnSaveSettings.TabIndex = 7;
            this._btnSaveSettings.Text = "Save Settings";
            this._btnSaveSettings.UseVisualStyleBackColor = false;
            this._btnSaveSettings.Click += new System.EventHandler(this._btnSaveSettings_Click);
            // 
            // JiraExportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(720, 792);
            this.Controls.Add(this.grpSource);
            this.Controls.Add(this.grpInclude);
            this.Controls.Add(this.grpAiSummary);
            this.Controls.Add(this.grpColumns);
            this.Controls.Add(this.grpPreview);
            this.Controls.Add(this._lblStatus);
            this.Controls.Add(this._btnEditRecipients);
            this.Controls.Add(this._btnSendOutlook);
            this.Controls.Add(this._btnExport);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(720, 694);
            this.Name = "JiraExportDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Jira Data to Excel";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpSource.ResumeLayout(false);
            this.grpSource.PerformLayout();
            this.grpInclude.ResumeLayout(false);
            this.grpInclude.PerformLayout();
            this.grpAiSummary.ResumeLayout(false);
            this.grpAiSummary.PerformLayout();
            this.grpColumns.ResumeLayout(false);
            this.grpColumns.PerformLayout();
            this.grpPreview.ResumeLayout(false);
            this.grpPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button _btnSaveSettings;
    }
}
