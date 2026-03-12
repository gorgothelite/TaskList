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
        private System.Windows.Forms.Button _btnExport;
        private System.Windows.Forms.Button _btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader          = new System.Windows.Forms.Panel();
            this._lblTitle          = new System.Windows.Forms.Label();
            this.grpSource          = new System.Windows.Forms.GroupBox();
            this._radResults        = new System.Windows.Forms.RadioButton();
            this._radMaster         = new System.Windows.Forms.RadioButton();
            this.grpInclude         = new System.Windows.Forms.GroupBox();
            this._chkWorklogs       = new System.Windows.Forms.CheckBox();
            this._chkComments       = new System.Windows.Forms.CheckBox();
            this.grpAiSummary       = new System.Windows.Forms.GroupBox();
            this._chkSummarize      = new System.Windows.Forms.CheckBox();
            this._lblProvider       = new System.Windows.Forms.Label();
            this._cmbProvider       = new System.Windows.Forms.ComboBox();
            this._lblAiKey          = new System.Windows.Forms.Label();
            this._txtAiKey          = new System.Windows.Forms.TextBox();
            this._lblAzureEndpoint  = new System.Windows.Forms.Label();
            this._txtAzureEndpoint  = new System.Windows.Forms.TextBox();
            this.grpColumns         = new System.Windows.Forms.GroupBox();
            this._chkColKey         = new System.Windows.Forms.CheckBox();
            this._chkColSummary     = new System.Windows.Forms.CheckBox();
            this._chkColType        = new System.Windows.Forms.CheckBox();
            this._chkColStatus      = new System.Windows.Forms.CheckBox();
            this._chkColPriority    = new System.Windows.Forms.CheckBox();
            this._chkColProject     = new System.Windows.Forms.CheckBox();
            this._chkColAssignee    = new System.Windows.Forms.CheckBox();
            this._chkColDueDate     = new System.Windows.Forms.CheckBox();
            this._chkColRecordType  = new System.Windows.Forms.CheckBox();
            this._chkColDate        = new System.Windows.Forms.CheckBox();
            this._chkColAuthor      = new System.Windows.Forms.CheckBox();
            this._chkColHours       = new System.Windows.Forms.CheckBox();
            this._chkColText        = new System.Windows.Forms.CheckBox();
            this.grpPreview         = new System.Windows.Forms.GroupBox();
            this._btnLoadPreview    = new System.Windows.Forms.Button();
            this._lblPreviewInfo    = new System.Windows.Forms.Label();
            this._dgvPreview        = new System.Windows.Forms.DataGridView();
            this._lblStatus         = new System.Windows.Forms.Label();
            this._btnExport         = new System.Windows.Forms.Button();
            this._btnCancel         = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.grpSource.SuspendLayout();
            this.grpInclude.SuspendLayout();
            this.grpAiSummary.SuspendLayout();
            this.grpColumns.SuspendLayout();
            this.grpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvPreview)).BeginInit();
            this.SuspendLayout();

            // ── pnlHeader ────────────────────────────────────────────────────
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(44, 44, 46);
            this.pnlHeader.Controls.Add(this._lblTitle);
            this.pnlHeader.Dock     = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name     = "pnlHeader";
            this.pnlHeader.Size     = new System.Drawing.Size(720, 42);
            this.pnlHeader.TabIndex = 0;

            this._lblTitle.AutoSize  = true;
            this._lblTitle.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.White;
            this._lblTitle.Location  = new System.Drawing.Point(12, 10);
            this._lblTitle.Name      = "_lblTitle";
            this._lblTitle.TabIndex  = 0;
            this._lblTitle.Text      = "Export Jira Data to Excel";

            // ── grpSource ────────────────────────────────────────────────────
            this.grpSource.Controls.Add(this._radResults);
            this.grpSource.Controls.Add(this._radMaster);
            this.grpSource.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this.grpSource.Location  = new System.Drawing.Point(14, 52);
            this.grpSource.Name      = "grpSource";
            this.grpSource.Size      = new System.Drawing.Size(200, 80);
            this.grpSource.TabIndex  = 1;
            this.grpSource.Text      = "Data Source";

            this._radResults.AutoSize  = true;
            this._radResults.Checked   = true;
            this._radResults.ForeColor = System.Drawing.Color.White;
            this._radResults.Location  = new System.Drawing.Point(14, 22);
            this._radResults.Name      = "_radResults";
            this._radResults.TabIndex  = 0;
            this._radResults.Text      = "Search Results";
            this._radResults.CheckedChanged += new System.EventHandler(this.RadSource_Changed);

            this._radMaster.AutoSize  = true;
            this._radMaster.ForeColor = System.Drawing.Color.White;
            this._radMaster.Location  = new System.Drawing.Point(14, 48);
            this._radMaster.Name      = "_radMaster";
            this._radMaster.TabIndex  = 1;
            this._radMaster.Text      = "Master List";
            this._radMaster.CheckedChanged += new System.EventHandler(this.RadSource_Changed);

            // ── grpInclude ───────────────────────────────────────────────────
            this.grpInclude.Controls.Add(this._chkWorklogs);
            this.grpInclude.Controls.Add(this._chkComments);
            this.grpInclude.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this.grpInclude.Location  = new System.Drawing.Point(226, 52);
            this.grpInclude.Name      = "grpInclude";
            this.grpInclude.Size      = new System.Drawing.Size(140, 80);
            this.grpInclude.TabIndex  = 2;
            this.grpInclude.Text      = "Include";

            this._chkWorklogs.AutoSize  = true;
            this._chkWorklogs.Checked   = true;
            this._chkWorklogs.ForeColor = System.Drawing.Color.White;
            this._chkWorklogs.Location  = new System.Drawing.Point(14, 22);
            this._chkWorklogs.Name      = "_chkWorklogs";
            this._chkWorklogs.TabIndex  = 0;
            this._chkWorklogs.Text      = "Worklogs";

            this._chkComments.AutoSize  = true;
            this._chkComments.Checked   = true;
            this._chkComments.ForeColor = System.Drawing.Color.White;
            this._chkComments.Location  = new System.Drawing.Point(14, 48);
            this._chkComments.Name      = "_chkComments";
            this._chkComments.TabIndex  = 1;
            this._chkComments.Text      = "Comments";

            // ── grpAiSummary ─────────────────────────────────────────────────
            this.grpAiSummary.Controls.Add(this._chkSummarize);
            this.grpAiSummary.Controls.Add(this._lblProvider);
            this.grpAiSummary.Controls.Add(this._cmbProvider);
            this.grpAiSummary.Controls.Add(this._lblAiKey);
            this.grpAiSummary.Controls.Add(this._txtAiKey);
            this.grpAiSummary.Controls.Add(this._lblAzureEndpoint);
            this.grpAiSummary.Controls.Add(this._txtAzureEndpoint);
            this.grpAiSummary.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this.grpAiSummary.Location  = new System.Drawing.Point(14, 144);
            this.grpAiSummary.Name      = "grpAiSummary";
            this.grpAiSummary.Size      = new System.Drawing.Size(692, 148);
            this.grpAiSummary.TabIndex  = 3;
            this.grpAiSummary.Text      = "AI Summary";

            this._chkSummarize.AutoSize  = true;
            this._chkSummarize.ForeColor = System.Drawing.Color.White;
            this._chkSummarize.Location  = new System.Drawing.Point(14, 22);
            this._chkSummarize.Name      = "_chkSummarize";
            this._chkSummarize.TabIndex  = 0;
            this._chkSummarize.Text      = "Summarize with AI after export";

            this._lblProvider.AutoSize  = true;
            this._lblProvider.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblProvider.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this._lblProvider.Location  = new System.Drawing.Point(14, 52);
            this._lblProvider.Name      = "_lblProvider";
            this._lblProvider.TabIndex  = 1;
            this._lblProvider.Text      = "PROVIDER";

            this._cmbProvider.BackColor     = System.Drawing.Color.FromArgb(55, 55, 60);
            this._cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbProvider.FlatStyle     = System.Windows.Forms.FlatStyle.Flat;
            this._cmbProvider.ForeColor     = System.Drawing.Color.White;
            this._cmbProvider.Items.AddRange(new object[] {
                "Claude (Anthropic)",
                "ChatGPT (OpenAI)",
                "Azure OpenAI (Copilot)" });
            this._cmbProvider.Location  = new System.Drawing.Point(14, 66);
            this._cmbProvider.Name      = "_cmbProvider";
            this._cmbProvider.Size      = new System.Drawing.Size(200, 24);
            this._cmbProvider.TabIndex  = 2;
            this._cmbProvider.SelectedIndex = 0;
            this._cmbProvider.SelectedIndexChanged += new System.EventHandler(this.CmbProvider_Changed);

            this._lblAiKey.AutoSize  = true;
            this._lblAiKey.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblAiKey.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this._lblAiKey.Location  = new System.Drawing.Point(228, 52);
            this._lblAiKey.Name      = "_lblAiKey";
            this._lblAiKey.TabIndex  = 3;
            this._lblAiKey.Text      = "ANTHROPIC API KEY";

            this._txtAiKey.BackColor    = System.Drawing.Color.FromArgb(55, 55, 60);
            this._txtAiKey.BorderStyle  = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtAiKey.ForeColor    = System.Drawing.Color.White;
            this._txtAiKey.Location     = new System.Drawing.Point(228, 66);
            this._txtAiKey.Name         = "_txtAiKey";
            this._txtAiKey.PasswordChar = '\u25cf';
            this._txtAiKey.Size         = new System.Drawing.Size(452, 24);
            this._txtAiKey.TabIndex     = 4;

            this._lblAzureEndpoint.AutoSize  = true;
            this._lblAzureEndpoint.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._lblAzureEndpoint.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this._lblAzureEndpoint.Location  = new System.Drawing.Point(14, 104);
            this._lblAzureEndpoint.Name      = "_lblAzureEndpoint";
            this._lblAzureEndpoint.TabIndex  = 5;
            this._lblAzureEndpoint.Text      = "AZURE ENDPOINT URL  (full URL incl. deployment & api-version)";
            this._lblAzureEndpoint.Visible   = false;

            this._txtAzureEndpoint.BackColor    = System.Drawing.Color.FromArgb(55, 55, 60);
            this._txtAzureEndpoint.BorderStyle  = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtAzureEndpoint.ForeColor    = System.Drawing.Color.White;
            this._txtAzureEndpoint.Location     = new System.Drawing.Point(14, 118);
            this._txtAzureEndpoint.Name         = "_txtAzureEndpoint";
            this._txtAzureEndpoint.Size         = new System.Drawing.Size(666, 24);
            this._txtAzureEndpoint.TabIndex     = 6;
            this._txtAzureEndpoint.Visible      = false;

            // ── grpColumns ───────────────────────────────────────────────────
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
            this.grpColumns.Anchor    = System.Windows.Forms.AnchorStyles.Top |
                                        System.Windows.Forms.AnchorStyles.Left |
                                        System.Windows.Forms.AnchorStyles.Right;
            this.grpColumns.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this.grpColumns.Location  = new System.Drawing.Point(14, 306);
            this.grpColumns.Name      = "grpColumns";
            this.grpColumns.Size      = new System.Drawing.Size(692, 130);
            this.grpColumns.TabIndex  = 4;
            this.grpColumns.Text      = "Columns to export";

            // Row 1 (y=24): Key, Summary, Type, Status, Priority
            this._chkColKey.AutoSize  = true;
            this._chkColKey.Checked   = true;
            this._chkColKey.ForeColor = System.Drawing.Color.White;
            this._chkColKey.Location  = new System.Drawing.Point(14, 24);
            this._chkColKey.Name      = "_chkColKey";
            this._chkColKey.TabIndex  = 0;
            this._chkColKey.Text      = "Key";

            this._chkColSummary.AutoSize  = true;
            this._chkColSummary.Checked   = true;
            this._chkColSummary.ForeColor = System.Drawing.Color.White;
            this._chkColSummary.Location  = new System.Drawing.Point(80, 24);
            this._chkColSummary.Name      = "_chkColSummary";
            this._chkColSummary.TabIndex  = 1;
            this._chkColSummary.Text      = "Summary";

            this._chkColType.AutoSize  = true;
            this._chkColType.Checked   = true;
            this._chkColType.ForeColor = System.Drawing.Color.White;
            this._chkColType.Location  = new System.Drawing.Point(220, 24);
            this._chkColType.Name      = "_chkColType";
            this._chkColType.TabIndex  = 2;
            this._chkColType.Text      = "Type";

            this._chkColStatus.AutoSize  = true;
            this._chkColStatus.Checked   = true;
            this._chkColStatus.ForeColor = System.Drawing.Color.White;
            this._chkColStatus.Location  = new System.Drawing.Point(310, 24);
            this._chkColStatus.Name      = "_chkColStatus";
            this._chkColStatus.TabIndex  = 3;
            this._chkColStatus.Text      = "Status";

            this._chkColPriority.AutoSize  = true;
            this._chkColPriority.Checked   = true;
            this._chkColPriority.ForeColor = System.Drawing.Color.White;
            this._chkColPriority.Location  = new System.Drawing.Point(410, 24);
            this._chkColPriority.Name      = "_chkColPriority";
            this._chkColPriority.TabIndex  = 4;
            this._chkColPriority.Text      = "Priority";

            // Row 2 (y=52): Project, Assignee, Due Date, Record Type, Date
            this._chkColProject.AutoSize  = true;
            this._chkColProject.Checked   = true;
            this._chkColProject.ForeColor = System.Drawing.Color.White;
            this._chkColProject.Location  = new System.Drawing.Point(14, 52);
            this._chkColProject.Name      = "_chkColProject";
            this._chkColProject.TabIndex  = 5;
            this._chkColProject.Text      = "Project";

            this._chkColAssignee.AutoSize  = true;
            this._chkColAssignee.Checked   = true;
            this._chkColAssignee.ForeColor = System.Drawing.Color.White;
            this._chkColAssignee.Location  = new System.Drawing.Point(110, 52);
            this._chkColAssignee.Name      = "_chkColAssignee";
            this._chkColAssignee.TabIndex  = 6;
            this._chkColAssignee.Text      = "Assignee";

            this._chkColDueDate.AutoSize  = true;
            this._chkColDueDate.Checked   = true;
            this._chkColDueDate.ForeColor = System.Drawing.Color.White;
            this._chkColDueDate.Location  = new System.Drawing.Point(220, 52);
            this._chkColDueDate.Name      = "_chkColDueDate";
            this._chkColDueDate.TabIndex  = 7;
            this._chkColDueDate.Text      = "Due Date";

            this._chkColRecordType.AutoSize  = true;
            this._chkColRecordType.Checked   = true;
            this._chkColRecordType.ForeColor = System.Drawing.Color.White;
            this._chkColRecordType.Location  = new System.Drawing.Point(330, 52);
            this._chkColRecordType.Name      = "_chkColRecordType";
            this._chkColRecordType.TabIndex  = 8;
            this._chkColRecordType.Text      = "Record Type";

            this._chkColDate.AutoSize  = true;
            this._chkColDate.Checked   = true;
            this._chkColDate.ForeColor = System.Drawing.Color.White;
            this._chkColDate.Location  = new System.Drawing.Point(460, 52);
            this._chkColDate.Name      = "_chkColDate";
            this._chkColDate.TabIndex  = 9;
            this._chkColDate.Text      = "Date";

            // Row 3 (y=80): Author, Hours, Text
            this._chkColAuthor.AutoSize  = true;
            this._chkColAuthor.Checked   = true;
            this._chkColAuthor.ForeColor = System.Drawing.Color.White;
            this._chkColAuthor.Location  = new System.Drawing.Point(14, 80);
            this._chkColAuthor.Name      = "_chkColAuthor";
            this._chkColAuthor.TabIndex  = 10;
            this._chkColAuthor.Text      = "Author";

            this._chkColHours.AutoSize  = true;
            this._chkColHours.Checked   = true;
            this._chkColHours.ForeColor = System.Drawing.Color.White;
            this._chkColHours.Location  = new System.Drawing.Point(110, 80);
            this._chkColHours.Name      = "_chkColHours";
            this._chkColHours.TabIndex  = 11;
            this._chkColHours.Text      = "Hours";

            this._chkColText.AutoSize  = true;
            this._chkColText.Checked   = true;
            this._chkColText.ForeColor = System.Drawing.Color.White;
            this._chkColText.Location  = new System.Drawing.Point(210, 80);
            this._chkColText.Name      = "_chkColText";
            this._chkColText.TabIndex  = 12;
            this._chkColText.Text      = "Text";

            // ── grpPreview ───────────────────────────────────────────────────
            this.grpPreview.Controls.Add(this._btnLoadPreview);
            this.grpPreview.Controls.Add(this._lblPreviewInfo);
            this.grpPreview.Controls.Add(this._dgvPreview);
            this.grpPreview.Anchor    = System.Windows.Forms.AnchorStyles.Top    |
                                        System.Windows.Forms.AnchorStyles.Bottom |
                                        System.Windows.Forms.AnchorStyles.Left   |
                                        System.Windows.Forms.AnchorStyles.Right;
            this.grpPreview.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this.grpPreview.Location  = new System.Drawing.Point(14, 450);
            this.grpPreview.Name      = "grpPreview";
            this.grpPreview.Size      = new System.Drawing.Size(692, 248);
            this.grpPreview.TabIndex  = 5;
            this.grpPreview.Text      = "Preview";

            this._btnLoadPreview.BackColor = System.Drawing.Color.FromArgb(60, 60, 68);
            this._btnLoadPreview.FlatAppearance.BorderSize = 0;
            this._btnLoadPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnLoadPreview.ForeColor = System.Drawing.Color.White;
            this._btnLoadPreview.Location  = new System.Drawing.Point(14, 22);
            this._btnLoadPreview.Name      = "_btnLoadPreview";
            this._btnLoadPreview.Size      = new System.Drawing.Size(130, 26);
            this._btnLoadPreview.TabIndex  = 0;
            this._btnLoadPreview.Text      = "Load Preview";
            this._btnLoadPreview.UseVisualStyleBackColor = false;
            this._btnLoadPreview.Click    += new System.EventHandler(this.BtnLoadPreview_Click);

            this._lblPreviewInfo.AutoSize  = true;
            this._lblPreviewInfo.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this._lblPreviewInfo.Location  = new System.Drawing.Point(154, 26);
            this._lblPreviewInfo.Name      = "_lblPreviewInfo";
            this._lblPreviewInfo.TabIndex  = 1;
            this._lblPreviewInfo.Text      = "Click 'Load Preview' to see a sample of the export data (first 5 issues).";

            // DataGridView
            this._dgvPreview.Anchor =
                System.Windows.Forms.AnchorStyles.Top    |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left   |
                System.Windows.Forms.AnchorStyles.Right;
            this._dgvPreview.AllowUserToAddRows    = false;
            this._dgvPreview.AllowUserToDeleteRows = false;
            this._dgvPreview.AllowUserToResizeRows = false;
            this._dgvPreview.BackgroundColor       = System.Drawing.Color.FromArgb(30, 30, 32);
            this._dgvPreview.BorderStyle           = System.Windows.Forms.BorderStyle.None;
            this._dgvPreview.CellBorderStyle       = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this._dgvPreview.GridColor             = System.Drawing.Color.FromArgb(55, 55, 60);
            this._dgvPreview.EnableHeadersVisualStyles = false;
            this._dgvPreview.Location              = new System.Drawing.Point(14, 56);
            this._dgvPreview.Name                  = "_dgvPreview";
            this._dgvPreview.ReadOnly              = true;
            this._dgvPreview.RowHeadersVisible     = false;
            this._dgvPreview.SelectionMode         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvPreview.Size                  = new System.Drawing.Size(664, 178);
            this._dgvPreview.TabIndex              = 2;
            this._dgvPreview.ScrollBars            = System.Windows.Forms.ScrollBars.Both;

            // Cell style
            var cellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            cellStyle.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            cellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 100, 180);
            cellStyle.SelectionForeColor = System.Drawing.Color.White;
            cellStyle.WrapMode           = System.Windows.Forms.DataGridViewTriState.False;
            this._dgvPreview.DefaultCellStyle = cellStyle;

            var altCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            altCellStyle.BackColor = System.Drawing.Color.FromArgb(42, 42, 45);
            altCellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            altCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 100, 180);
            altCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this._dgvPreview.AlternatingRowsDefaultCellStyle = altCellStyle;

            var headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            headerStyle.BackColor = System.Drawing.Color.FromArgb(50, 50, 55);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            headerStyle.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            headerStyle.Padding   = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._dgvPreview.ColumnHeadersDefaultCellStyle = headerStyle;
            this._dgvPreview.ColumnHeadersHeight           = 26;
            this._dgvPreview.ColumnHeadersHeightSizeMode   =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // ── _lblStatus ───────────────────────────────────────────────────
            this._lblStatus.Anchor    = System.Windows.Forms.AnchorStyles.Bottom |
                                        System.Windows.Forms.AnchorStyles.Left;
            this._lblStatus.AutoSize  = true;
            this._lblStatus.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this._lblStatus.Location  = new System.Drawing.Point(14, 714);
            this._lblStatus.Name      = "_lblStatus";
            this._lblStatus.TabIndex  = 6;
            this._lblStatus.Text      = "Ready.";

            // ── _btnExport ───────────────────────────────────────────────────
            this._btnExport.Anchor    = System.Windows.Forms.AnchorStyles.Bottom |
                                        System.Windows.Forms.AnchorStyles.Right;
            this._btnExport.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this._btnExport.FlatAppearance.BorderSize = 0;
            this._btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnExport.ForeColor = System.Drawing.Color.White;
            this._btnExport.Location  = new System.Drawing.Point(490, 708);
            this._btnExport.Name      = "_btnExport";
            this._btnExport.Size      = new System.Drawing.Size(152, 28);
            this._btnExport.TabIndex  = 7;
            this._btnExport.Text      = "Export to Excel\u2026";
            this._btnExport.UseVisualStyleBackColor = false;
            this._btnExport.Click    += new System.EventHandler(this.BtnExport_Click);

            // ── _btnCancel ───────────────────────────────────────────────────
            this._btnCancel.Anchor       = System.Windows.Forms.AnchorStyles.Bottom |
                                           System.Windows.Forms.AnchorStyles.Right;
            this._btnCancel.BackColor    = System.Drawing.Color.FromArgb(70, 70, 78);
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.FlatAppearance.BorderSize = 0;
            this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.ForeColor = System.Drawing.Color.White;
            this._btnCancel.Location  = new System.Drawing.Point(652, 708);
            this._btnCancel.Name      = "_btnCancel";
            this._btnCancel.Size      = new System.Drawing.Size(54, 28);
            this._btnCancel.TabIndex  = 8;
            this._btnCancel.Text      = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = false;

            // ── JiraExportDialog ─────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(37, 37, 38);
            this.CancelButton        = this._btnCancel;
            this.ClientSize          = new System.Drawing.Size(720, 748);
            this.Controls.Add(this.grpSource);
            this.Controls.Add(this.grpInclude);
            this.Controls.Add(this.grpAiSummary);
            this.Controls.Add(this.grpColumns);
            this.Controls.Add(this.grpPreview);
            this.Controls.Add(this._lblStatus);
            this.Controls.Add(this._btnExport);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this.pnlHeader);
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor       = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox     = false;
            this.MinimumSize     = new System.Drawing.Size(720, 650);
            this.Name            = "JiraExportDialog";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Export Jira Data to Excel";

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
    }
}
