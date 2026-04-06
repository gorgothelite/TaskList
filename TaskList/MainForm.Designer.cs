namespace Test
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // ── Containers ───────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlDetail;

        // ── Toolbar controls ─────────────────────────────────────────────────
        private System.Windows.Forms.Button   btnAdd;
        private System.Windows.Forms.Label    lblStatusFilter;
        private DarkComboBox _cmbStatusF;
        private System.Windows.Forms.Label    lblPriorityFilter;
        private DarkComboBox _cmbPriorityF;
        private System.Windows.Forms.Button   btnHistory;
        private System.Windows.Forms.Button   btnExport;
        private System.Windows.Forms.Button   btnJira;

        // ── List view ────────────────────────────────────────────────────────
        private System.Windows.Forms.ListView _lv;

        // ── Detail panel ─────────────────────────────────────────────────────
        private System.Windows.Forms.Label   lblDetailHeader;
        private System.Windows.Forms.Label   lblNameCaption;
        private System.Windows.Forms.Label   _lblName;
        private System.Windows.Forms.Label   lblPriorityCaption;
        private System.Windows.Forms.Label   _lblPriority;
        private System.Windows.Forms.Label   lblDueCaption;
        private System.Windows.Forms.Label   _lblDue;
        private System.Windows.Forms.Label   lblStatusCaption;
        private System.Windows.Forms.Label   _lblStatus;
        private System.Windows.Forms.Label   lblNotesCaption;
        private System.Windows.Forms.TextBox _txtNotes;
        private System.Windows.Forms.Panel   pnlDivider;
        private System.Windows.Forms.Button  _btnEdit;
        private System.Windows.Forms.Button  _btnDone;
        private System.Windows.Forms.Button  _btnDelete;
        private System.Windows.Forms.Button  _btnHold;
        private System.Windows.Forms.Panel   pnlDivider2;
        private System.Windows.Forms.Label   lblSubCaption;
        private System.Windows.Forms.Label   _lblSubInfo;
        private System.Windows.Forms.Button  _btnAddSubtask;
        private System.Windows.Forms.Panel   pnlDivider3;
        private System.Windows.Forms.Label   lblImagesCaption;
        private System.Windows.Forms.Button  _btnAddImage;
        private System.Windows.Forms.Panel   _pnlImagesThumbs;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this._cmbStatusF = new Test.DarkComboBox();
            this.lblPriorityFilter = new System.Windows.Forms.Label();
            this._cmbPriorityF = new Test.DarkComboBox();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnJira = new System.Windows.Forms.Button();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this._lv = new System.Windows.Forms.ListView();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.lblDetailHeader = new System.Windows.Forms.Label();
            this.lblNameCaption = new System.Windows.Forms.Label();
            this._lblName = new System.Windows.Forms.Label();
            this.lblPriorityCaption = new System.Windows.Forms.Label();
            this._lblPriority = new System.Windows.Forms.Label();
            this.lblDueCaption = new System.Windows.Forms.Label();
            this._lblDue = new System.Windows.Forms.Label();
            this.lblStatusCaption = new System.Windows.Forms.Label();
            this._lblStatus = new System.Windows.Forms.Label();
            this.lblNotesCaption = new System.Windows.Forms.Label();
            this._txtNotes = new System.Windows.Forms.TextBox();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this._btnEdit = new System.Windows.Forms.Button();
            this._btnDone = new System.Windows.Forms.Button();
            this._btnDelete = new System.Windows.Forms.Button();
            this._btnHold = new System.Windows.Forms.Button();
            this.pnlDivider2 = new System.Windows.Forms.Panel();
            this.lblSubCaption = new System.Windows.Forms.Label();
            this._lblSubInfo = new System.Windows.Forms.Label();
            this._btnAddSubtask = new System.Windows.Forms.Button();
            this.pnlDivider3 = new System.Windows.Forms.Panel();
            this.lblImagesCaption = new System.Windows.Forms.Label();
            this._btnAddImage = new System.Windows.Forms.Button();
            this._pnlImagesThumbs = new System.Windows.Forms.Panel();
            this.lblTotalTimeSpent = new System.Windows.Forms.Label();
            this.lblTotalTimeSpentDisplay = new System.Windows.Forms.Label();
            this.pnlToolbar.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(46)))));
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(1080, 32);
            this.pnlTitleBar.TabIndex = 10;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(46)))));
            this.pnlToolbar.Controls.Add(this.btnAdd);
            this.pnlToolbar.Controls.Add(this.lblStatusFilter);
            this.pnlToolbar.Controls.Add(this._cmbStatusF);
            this.pnlToolbar.Controls.Add(this.lblPriorityFilter);
            this.pnlToolbar.Controls.Add(this._cmbPriorityF);
            this.pnlToolbar.Controls.Add(this.btnHistory);
            this.pnlToolbar.Controls.Add(this.btnExport);
            this.pnlToolbar.Controls.Add(this.btnJira);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 32);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1080, 50);
            this.pnlToolbar.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(10, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(116, 28);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+ Add Task";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // lblStatusFilter
            // 
            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.ForeColor = System.Drawing.Color.Silver;
            this.lblStatusFilter.Location = new System.Drawing.Point(140, 17);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Size = new System.Drawing.Size(46, 17);
            this.lblStatusFilter.TabIndex = 1;
            this.lblStatusFilter.Text = "Status:";
            // 
            // _cmbStatusF
            // 
            this._cmbStatusF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._cmbStatusF.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._cmbStatusF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbStatusF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cmbStatusF.ForeColor = System.Drawing.Color.White;
            this._cmbStatusF.Items.AddRange(new object[] {
            "All",
            "Active",
            "Done",
            "On Hold"});
            this._cmbStatusF.Location = new System.Drawing.Point(192, 13);
            this._cmbStatusF.Name = "_cmbStatusF";
            this._cmbStatusF.Size = new System.Drawing.Size(94, 25);
            this._cmbStatusF.TabIndex = 2;
            this._cmbStatusF.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblPriorityFilter
            // 
            this.lblPriorityFilter.AutoSize = true;
            this.lblPriorityFilter.ForeColor = System.Drawing.Color.Silver;
            this.lblPriorityFilter.Location = new System.Drawing.Point(300, 17);
            this.lblPriorityFilter.Name = "lblPriorityFilter";
            this.lblPriorityFilter.Size = new System.Drawing.Size(52, 17);
            this.lblPriorityFilter.TabIndex = 3;
            this.lblPriorityFilter.Text = "Priority:";
            // 
            // _cmbPriorityF
            // 
            this._cmbPriorityF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._cmbPriorityF.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._cmbPriorityF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPriorityF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cmbPriorityF.ForeColor = System.Drawing.Color.White;
            this._cmbPriorityF.Items.AddRange(new object[] {
            "All",
            "Low",
            "Medium",
            "High",
            "Critical"});
            this._cmbPriorityF.Location = new System.Drawing.Point(362, 13);
            this._cmbPriorityF.Name = "_cmbPriorityF";
            this._cmbPriorityF.Size = new System.Drawing.Size(106, 25);
            this._cmbPriorityF.TabIndex = 4;
            this._cmbPriorityF.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.ForeColor = System.Drawing.Color.White;
            this.btnHistory.Location = new System.Drawing.Point(952, 11);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(118, 28);
            this.btnHistory.TabIndex = 5;
            this.btnHistory.Text = "History";
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(60)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(824, 11);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(118, 28);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export CSV…";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // btnJira
            // 
            this.btnJira.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJira.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(110)))));
            this.btnJira.FlatAppearance.BorderSize = 0;
            this.btnJira.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJira.ForeColor = System.Drawing.Color.White;
            this.btnJira.Location = new System.Drawing.Point(692, 11);
            this.btnJira.Name = "btnJira";
            this.btnJira.Size = new System.Drawing.Size(122, 28);
            this.btnJira.TabIndex = 7;
            this.btnJira.Text = "Jira";
            this.btnJira.UseVisualStyleBackColor = false;
            this.btnJira.Click += new System.EventHandler(this.BtnJira_Click);
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.pnlLeft.Controls.Add(this._lv);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 82);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(760, 714);
            this.pnlLeft.TabIndex = 1;
            // 
            // _lv
            // 
            this._lv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this._lv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lv.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._lv.ForeColor = System.Drawing.Color.White;
            this._lv.FullRowSelect = true;
            this._lv.HideSelection = false;
            this._lv.Location = new System.Drawing.Point(0, 0);
            this._lv.MultiSelect = false;
            this._lv.Name = "_lv";
            this._lv.OwnerDraw = true;
            this._lv.Size = new System.Drawing.Size(760, 714);
            this._lv.TabIndex = 0;
            this._lv.UseCompatibleStateImageBehavior = false;
            this._lv.View = System.Windows.Forms.View.Details;
            // 
            // pnlDetail
            // 
            this.pnlDetail.AllowDrop = true;
            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.pnlDetail.Controls.Add(this.lblTotalTimeSpent);
            this.pnlDetail.Controls.Add(this.lblTotalTimeSpentDisplay);
            this.pnlDetail.Controls.Add(this.lblDetailHeader);
            this.pnlDetail.Controls.Add(this.lblNameCaption);
            this.pnlDetail.Controls.Add(this._lblName);
            this.pnlDetail.Controls.Add(this.lblPriorityCaption);
            this.pnlDetail.Controls.Add(this._lblPriority);
            this.pnlDetail.Controls.Add(this.lblDueCaption);
            this.pnlDetail.Controls.Add(this._lblDue);
            this.pnlDetail.Controls.Add(this.lblStatusCaption);
            this.pnlDetail.Controls.Add(this._lblStatus);
            this.pnlDetail.Controls.Add(this.lblNotesCaption);
            this.pnlDetail.Controls.Add(this._txtNotes);
            this.pnlDetail.Controls.Add(this.pnlDivider);
            this.pnlDetail.Controls.Add(this._btnEdit);
            this.pnlDetail.Controls.Add(this._btnDone);
            this.pnlDetail.Controls.Add(this._btnDelete);
            this.pnlDetail.Controls.Add(this._btnHold);
            this.pnlDetail.Controls.Add(this.pnlDivider2);
            this.pnlDetail.Controls.Add(this.lblSubCaption);
            this.pnlDetail.Controls.Add(this._lblSubInfo);
            this.pnlDetail.Controls.Add(this._btnAddSubtask);
            this.pnlDetail.Controls.Add(this.pnlDivider3);
            this.pnlDetail.Controls.Add(this.lblImagesCaption);
            this.pnlDetail.Controls.Add(this._btnAddImage);
            this.pnlDetail.Controls.Add(this._pnlImagesThumbs);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDetail.Location = new System.Drawing.Point(760, 82);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(320, 714);
            this.pnlDetail.TabIndex = 2;
            this.pnlDetail.DragDrop += new System.Windows.Forms.DragEventHandler(this.PnlDetail_DragDrop);
            this.pnlDetail.DragEnter += new System.Windows.Forms.DragEventHandler(this.PnlDetail_DragEnter);
            // 
            // lblDetailHeader
            // 
            this.lblDetailHeader.AutoSize = true;
            this.lblDetailHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblDetailHeader.ForeColor = System.Drawing.Color.White;
            this.lblDetailHeader.Location = new System.Drawing.Point(18, 18);
            this.lblDetailHeader.Name = "lblDetailHeader";
            this.lblDetailHeader.Size = new System.Drawing.Size(112, 25);
            this.lblDetailHeader.TabIndex = 0;
            this.lblDetailHeader.Text = "Task Details";
            // 
            // lblNameCaption
            // 
            this.lblNameCaption.AutoSize = true;
            this.lblNameCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblNameCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblNameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this.lblNameCaption.Location = new System.Drawing.Point(18, 62);
            this.lblNameCaption.Name = "lblNameCaption";
            this.lblNameCaption.Size = new System.Drawing.Size(35, 12);
            this.lblNameCaption.TabIndex = 1;
            this.lblNameCaption.Text = "NAME";
            // 
            // _lblName
            // 
            this._lblName.BackColor = System.Drawing.Color.Transparent;
            this._lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblName.ForeColor = System.Drawing.Color.White;
            this._lblName.Location = new System.Drawing.Point(18, 78);
            this._lblName.Name = "_lblName";
            this._lblName.Size = new System.Drawing.Size(275, 22);
            this._lblName.TabIndex = 2;
            // 
            // lblPriorityCaption
            // 
            this.lblPriorityCaption.AutoSize = true;
            this.lblPriorityCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPriorityCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblPriorityCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this.lblPriorityCaption.Location = new System.Drawing.Point(18, 108);
            this.lblPriorityCaption.Name = "lblPriorityCaption";
            this.lblPriorityCaption.Size = new System.Drawing.Size(51, 12);
            this.lblPriorityCaption.TabIndex = 3;
            this.lblPriorityCaption.Text = "PRIORITY";
            // 
            // _lblPriority
            // 
            this._lblPriority.BackColor = System.Drawing.Color.Transparent;
            this._lblPriority.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblPriority.ForeColor = System.Drawing.Color.White;
            this._lblPriority.Location = new System.Drawing.Point(18, 124);
            this._lblPriority.Name = "_lblPriority";
            this._lblPriority.Size = new System.Drawing.Size(275, 22);
            this._lblPriority.TabIndex = 4;
            // 
            // lblDueCaption
            // 
            this.lblDueCaption.AutoSize = true;
            this.lblDueCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDueCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblDueCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this.lblDueCaption.Location = new System.Drawing.Point(18, 154);
            this.lblDueCaption.Name = "lblDueCaption";
            this.lblDueCaption.Size = new System.Drawing.Size(51, 12);
            this.lblDueCaption.TabIndex = 5;
            this.lblDueCaption.Text = "DUE DATE";
            // 
            // _lblDue
            // 
            this._lblDue.BackColor = System.Drawing.Color.Transparent;
            this._lblDue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblDue.ForeColor = System.Drawing.Color.White;
            this._lblDue.Location = new System.Drawing.Point(18, 170);
            this._lblDue.Name = "_lblDue";
            this._lblDue.Size = new System.Drawing.Size(275, 40);
            this._lblDue.TabIndex = 6;
            // 
            // lblStatusCaption
            // 
            this.lblStatusCaption.AutoSize = true;
            this.lblStatusCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this.lblStatusCaption.Location = new System.Drawing.Point(18, 218);
            this.lblStatusCaption.Name = "lblStatusCaption";
            this.lblStatusCaption.Size = new System.Drawing.Size(41, 12);
            this.lblStatusCaption.TabIndex = 7;
            this.lblStatusCaption.Text = "STATUS";
            // 
            // _lblStatus
            // 
            this._lblStatus.BackColor = System.Drawing.Color.Transparent;
            this._lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblStatus.ForeColor = System.Drawing.Color.White;
            this._lblStatus.Location = new System.Drawing.Point(18, 234);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(275, 22);
            this._lblStatus.TabIndex = 8;
            // 
            // lblNotesCaption
            // 
            this.lblNotesCaption.AutoSize = true;
            this.lblNotesCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblNotesCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblNotesCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this.lblNotesCaption.Location = new System.Drawing.Point(18, 266);
            this.lblNotesCaption.Name = "lblNotesCaption";
            this.lblNotesCaption.Size = new System.Drawing.Size(38, 12);
            this.lblNotesCaption.TabIndex = 9;
            this.lblNotesCaption.Text = "NOTES";
            // 
            // _txtNotes
            // 
            this._txtNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this._txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtNotes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._txtNotes.ForeColor = System.Drawing.Color.LightGray;
            this._txtNotes.Location = new System.Drawing.Point(18, 282);
            this._txtNotes.Multiline = true;
            this._txtNotes.Name = "_txtNotes";
            this._txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtNotes.Size = new System.Drawing.Size(275, 90);
            this._txtNotes.TabIndex = 10;
            this._txtNotes.TextChanged += new System.EventHandler(this.TxtNotes_TextChanged);
            // 
            // pnlDivider
            // 
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(68)))));
            this.pnlDivider.Location = new System.Drawing.Point(18, 426);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(275, 1);
            this.pnlDivider.TabIndex = 11;
            // 
            // _btnEdit
            // 
            this._btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._btnEdit.Enabled = false;
            this._btnEdit.FlatAppearance.BorderSize = 0;
            this._btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnEdit.ForeColor = System.Drawing.Color.White;
            this._btnEdit.Location = new System.Drawing.Point(18, 436);
            this._btnEdit.Name = "_btnEdit";
            this._btnEdit.Size = new System.Drawing.Size(76, 28);
            this._btnEdit.TabIndex = 12;
            this._btnEdit.Text = "Edit";
            this._btnEdit.UseVisualStyleBackColor = false;
            this._btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // _btnDone
            // 
            this._btnDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this._btnDone.Enabled = false;
            this._btnDone.FlatAppearance.BorderSize = 0;
            this._btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDone.ForeColor = System.Drawing.Color.White;
            this._btnDone.Location = new System.Drawing.Point(104, 436);
            this._btnDone.Name = "_btnDone";
            this._btnDone.Size = new System.Drawing.Size(100, 28);
            this._btnDone.TabIndex = 13;
            this._btnDone.Text = "Mark Done";
            this._btnDone.UseVisualStyleBackColor = false;
            this._btnDone.Click += new System.EventHandler(this.BtnDone_Click);
            // 
            // _btnDelete
            // 
            this._btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._btnDelete.Enabled = false;
            this._btnDelete.FlatAppearance.BorderSize = 0;
            this._btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDelete.ForeColor = System.Drawing.Color.White;
            this._btnDelete.Location = new System.Drawing.Point(214, 436);
            this._btnDelete.Name = "_btnDelete";
            this._btnDelete.Size = new System.Drawing.Size(76, 28);
            this._btnDelete.TabIndex = 14;
            this._btnDelete.Text = "Delete";
            this._btnDelete.UseVisualStyleBackColor = false;
            this._btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // _btnHold
            // 
            this._btnHold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this._btnHold.Enabled = false;
            this._btnHold.FlatAppearance.BorderSize = 0;
            this._btnHold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnHold.ForeColor = System.Drawing.Color.White;
            this._btnHold.Location = new System.Drawing.Point(18, 474);
            this._btnHold.Name = "_btnHold";
            this._btnHold.Size = new System.Drawing.Size(275, 28);
            this._btnHold.TabIndex = 15;
            this._btnHold.Text = "Put On Hold";
            this._btnHold.UseVisualStyleBackColor = false;
            this._btnHold.Click += new System.EventHandler(this.BtnHold_Click);
            // 
            // pnlDivider2
            // 
            this.pnlDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(68)))));
            this.pnlDivider2.Location = new System.Drawing.Point(18, 512);
            this.pnlDivider2.Name = "pnlDivider2";
            this.pnlDivider2.Size = new System.Drawing.Size(275, 1);
            this.pnlDivider2.TabIndex = 16;
            this.pnlDivider2.Visible = false;
            // 
            // lblSubCaption
            // 
            this.lblSubCaption.AutoSize = true;
            this.lblSubCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblSubCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSubCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this.lblSubCaption.Location = new System.Drawing.Point(18, 524);
            this.lblSubCaption.Name = "lblSubCaption";
            this.lblSubCaption.Size = new System.Drawing.Size(54, 12);
            this.lblSubCaption.TabIndex = 17;
            this.lblSubCaption.Text = "SUBTASKS";
            this.lblSubCaption.Visible = false;
            // 
            // _lblSubInfo
            // 
            this._lblSubInfo.BackColor = System.Drawing.Color.Transparent;
            this._lblSubInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._lblSubInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(190)))));
            this._lblSubInfo.Location = new System.Drawing.Point(18, 540);
            this._lblSubInfo.Name = "_lblSubInfo";
            this._lblSubInfo.Size = new System.Drawing.Size(275, 20);
            this._lblSubInfo.TabIndex = 18;
            this._lblSubInfo.Visible = false;
            // 
            // _btnAddSubtask
            // 
            this._btnAddSubtask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this._btnAddSubtask.Enabled = false;
            this._btnAddSubtask.FlatAppearance.BorderSize = 0;
            this._btnAddSubtask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddSubtask.ForeColor = System.Drawing.Color.White;
            this._btnAddSubtask.Location = new System.Drawing.Point(18, 568);
            this._btnAddSubtask.Name = "_btnAddSubtask";
            this._btnAddSubtask.Size = new System.Drawing.Size(275, 28);
            this._btnAddSubtask.TabIndex = 19;
            this._btnAddSubtask.Text = "+ Add Subtask";
            this._btnAddSubtask.UseVisualStyleBackColor = false;
            this._btnAddSubtask.Visible = false;
            this._btnAddSubtask.Click += new System.EventHandler(this.BtnAddSubtask_Click);
            // 
            // pnlDivider3
            // 
            this.pnlDivider3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(68)))));
            this.pnlDivider3.Location = new System.Drawing.Point(18, 606);
            this.pnlDivider3.Name = "pnlDivider3";
            this.pnlDivider3.Size = new System.Drawing.Size(275, 1);
            this.pnlDivider3.TabIndex = 20;
            // 
            // lblImagesCaption
            // 
            this.lblImagesCaption.AutoSize = true;
            this.lblImagesCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblImagesCaption.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblImagesCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this.lblImagesCaption.Location = new System.Drawing.Point(18, 619);
            this.lblImagesCaption.Name = "lblImagesCaption";
            this.lblImagesCaption.Size = new System.Drawing.Size(43, 12);
            this.lblImagesCaption.TabIndex = 21;
            this.lblImagesCaption.Text = "IMAGES";
            // 
            // _btnAddImage
            // 
            this._btnAddImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnAddImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this._btnAddImage.Enabled = false;
            this._btnAddImage.FlatAppearance.BorderSize = 0;
            this._btnAddImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddImage.ForeColor = System.Drawing.Color.White;
            this._btnAddImage.Location = new System.Drawing.Point(226, 612);
            this._btnAddImage.Name = "_btnAddImage";
            this._btnAddImage.Size = new System.Drawing.Size(67, 22);
            this._btnAddImage.TabIndex = 22;
            this._btnAddImage.Text = "+ Add";
            this._btnAddImage.UseVisualStyleBackColor = false;
            this._btnAddImage.Click += new System.EventHandler(this.BtnAddImage_Click);
            // 
            // _pnlImagesThumbs
            // 
            this._pnlImagesThumbs.AllowDrop = true;
            this._pnlImagesThumbs.AutoScroll = true;
            this._pnlImagesThumbs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(32)))));
            this._pnlImagesThumbs.Location = new System.Drawing.Point(18, 640);
            this._pnlImagesThumbs.Name = "_pnlImagesThumbs";
            this._pnlImagesThumbs.Size = new System.Drawing.Size(275, 68);
            this._pnlImagesThumbs.TabIndex = 23;
            this._pnlImagesThumbs.DragDrop += new System.Windows.Forms.DragEventHandler(this.PnlDetail_DragDrop);
            this._pnlImagesThumbs.DragEnter += new System.Windows.Forms.DragEventHandler(this.PnlDetail_DragEnter);
            // 
            // lblTotalTimeSpent
            // 
            this.lblTotalTimeSpent.AutoSize = true;
            this.lblTotalTimeSpent.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalTimeSpent.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalTimeSpent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this.lblTotalTimeSpent.Location = new System.Drawing.Point(19, 375);
            this.lblTotalTimeSpent.Name = "lblTotalTimeSpent";
            this.lblTotalTimeSpent.Size = new System.Drawing.Size(84, 12);
            this.lblTotalTimeSpent.TabIndex = 24;
            this.lblTotalTimeSpent.Text = "Total Time Spent";
            // 
            // lblTotalTimeSpentDisplay
            // 
            this.lblTotalTimeSpentDisplay.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalTimeSpentDisplay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalTimeSpentDisplay.ForeColor = System.Drawing.Color.White;
            this.lblTotalTimeSpentDisplay.Location = new System.Drawing.Point(19, 391);
            this.lblTotalTimeSpentDisplay.Name = "lblTotalTimeSpentDisplay";
            this.lblTotalTimeSpentDisplay.Size = new System.Drawing.Size(275, 40);
            this.lblTotalTimeSpentDisplay.TabIndex = 25;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1080, 796);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlDetail);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlTitleBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(860, 632);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task List";
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTotalTimeSpent;
        private System.Windows.Forms.Label lblTotalTimeSpentDisplay;
    }
}
