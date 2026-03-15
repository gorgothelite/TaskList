namespace Test
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // ── Containers ───────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlDetail;

        // ── Toolbar controls ─────────────────────────────────────────────────
        private System.Windows.Forms.Button   btnAdd;
        private System.Windows.Forms.Label    lblStatusFilter;
        private System.Windows.Forms.ComboBox _cmbStatusF;
        private System.Windows.Forms.Label    lblPriorityFilter;
        private System.Windows.Forms.ComboBox _cmbPriorityF;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components         = new System.ComponentModel.Container();
            this.pnlToolbar         = new System.Windows.Forms.Panel();
            this.btnAdd             = new System.Windows.Forms.Button();
            this.lblStatusFilter    = new System.Windows.Forms.Label();
            this._cmbStatusF        = new System.Windows.Forms.ComboBox();
            this.lblPriorityFilter  = new System.Windows.Forms.Label();
            this._cmbPriorityF      = new System.Windows.Forms.ComboBox();
            this.btnHistory         = new System.Windows.Forms.Button();
            this.btnExport          = new System.Windows.Forms.Button();
            this.btnJira            = new System.Windows.Forms.Button();
            this.pnlLeft            = new System.Windows.Forms.Panel();
            this._lv                = new System.Windows.Forms.ListView();
            this.pnlDetail          = new System.Windows.Forms.Panel();
            this.lblDetailHeader    = new System.Windows.Forms.Label();
            this.lblNameCaption     = new System.Windows.Forms.Label();
            this._lblName           = new System.Windows.Forms.Label();
            this.lblPriorityCaption = new System.Windows.Forms.Label();
            this._lblPriority       = new System.Windows.Forms.Label();
            this.lblDueCaption      = new System.Windows.Forms.Label();
            this._lblDue            = new System.Windows.Forms.Label();
            this.lblStatusCaption   = new System.Windows.Forms.Label();
            this._lblStatus         = new System.Windows.Forms.Label();
            this.lblNotesCaption    = new System.Windows.Forms.Label();
            this._txtNotes          = new System.Windows.Forms.TextBox();
            this.pnlDivider         = new System.Windows.Forms.Panel();
            this._btnEdit           = new System.Windows.Forms.Button();
            this._btnDone           = new System.Windows.Forms.Button();
            this._btnDelete         = new System.Windows.Forms.Button();

            this.pnlToolbar.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.SuspendLayout();

            // ── pnlToolbar ───────────────────────────────────────────────────
            this.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(44, 44, 46);
            this.pnlToolbar.Controls.Add(this.btnAdd);
            this.pnlToolbar.Controls.Add(this.lblStatusFilter);
            this.pnlToolbar.Controls.Add(this._cmbStatusF);
            this.pnlToolbar.Controls.Add(this.lblPriorityFilter);
            this.pnlToolbar.Controls.Add(this._cmbPriorityF);
            this.pnlToolbar.Controls.Add(this.btnHistory);
            this.pnlToolbar.Controls.Add(this.btnExport);
            this.pnlToolbar.Controls.Add(this.btnJira);
            this.pnlToolbar.Dock     = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name     = "pnlToolbar";
            this.pnlToolbar.Size     = new System.Drawing.Size(1080, 50);
            this.pnlToolbar.TabIndex = 0;

            // btnAdd
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location  = new System.Drawing.Point(10, 11);
            this.btnAdd.Name      = "btnAdd";
            this.btnAdd.Size      = new System.Drawing.Size(116, 28);
            this.btnAdd.TabIndex  = 0;
            this.btnAdd.Text      = "+ Add Task";
            this.btnAdd.Click    += new System.EventHandler(this.BtnAdd_Click);

            // lblStatusFilter
            this.lblStatusFilter.AutoSize  = true;
            this.lblStatusFilter.ForeColor = System.Drawing.Color.Silver;
            this.lblStatusFilter.Location  = new System.Drawing.Point(140, 17);
            this.lblStatusFilter.Name      = "lblStatusFilter";
            this.lblStatusFilter.TabIndex  = 1;
            this.lblStatusFilter.Text      = "Status:";

            // _cmbStatusF
            this._cmbStatusF.BackColor     = System.Drawing.Color.FromArgb(55, 55, 60);
            this._cmbStatusF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbStatusF.FlatStyle     = System.Windows.Forms.FlatStyle.Flat;
            this._cmbStatusF.ForeColor     = System.Drawing.Color.White;
            this._cmbStatusF.Items.AddRange(new object[] { "All", "Active", "Done" });
            this._cmbStatusF.Location      = new System.Drawing.Point(192, 13);
            this._cmbStatusF.Name          = "_cmbStatusF";
            this._cmbStatusF.Size          = new System.Drawing.Size(94, 23);
            this._cmbStatusF.TabIndex      = 2;
            this._cmbStatusF.SelectedIndex = 0;
            this._cmbStatusF.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);

            // lblPriorityFilter
            this.lblPriorityFilter.AutoSize  = true;
            this.lblPriorityFilter.ForeColor = System.Drawing.Color.Silver;
            this.lblPriorityFilter.Location  = new System.Drawing.Point(300, 17);
            this.lblPriorityFilter.Name      = "lblPriorityFilter";
            this.lblPriorityFilter.TabIndex  = 3;
            this.lblPriorityFilter.Text      = "Priority:";

            // _cmbPriorityF
            this._cmbPriorityF.BackColor     = System.Drawing.Color.FromArgb(55, 55, 60);
            this._cmbPriorityF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPriorityF.FlatStyle     = System.Windows.Forms.FlatStyle.Flat;
            this._cmbPriorityF.ForeColor     = System.Drawing.Color.White;
            this._cmbPriorityF.Items.AddRange(new object[] { "All", "Low", "Medium", "High", "Critical" });
            this._cmbPriorityF.Location      = new System.Drawing.Point(362, 13);
            this._cmbPriorityF.Name          = "_cmbPriorityF";
            this._cmbPriorityF.Size          = new System.Drawing.Size(106, 23);
            this._cmbPriorityF.TabIndex      = 4;
            this._cmbPriorityF.SelectedIndex = 0;
            this._cmbPriorityF.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);

            // btnHistory
            this.btnHistory.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnHistory.BackColor = System.Drawing.Color.FromArgb(60, 60, 90);
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.ForeColor = System.Drawing.Color.White;
            this.btnHistory.Location  = new System.Drawing.Point(952, 11);
            this.btnHistory.Name      = "btnHistory";
            this.btnHistory.Size      = new System.Drawing.Size(118, 28);
            this.btnHistory.TabIndex  = 5;
            this.btnHistory.Text      = "History";
            this.btnHistory.Click    += new System.EventHandler(this.BtnHistory_Click);

            // btnExport
            this.btnExport.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(30, 100, 60);
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location  = new System.Drawing.Point(824, 11);
            this.btnExport.Name      = "btnExport";
            this.btnExport.Size      = new System.Drawing.Size(118, 28);
            this.btnExport.TabIndex  = 6;
            this.btnExport.Text      = "Export CSV…";
            this.btnExport.Click    += new System.EventHandler(this.BtnExport_Click);

            // btnJira
            this.btnJira.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnJira.BackColor = System.Drawing.Color.FromArgb(50, 50, 110);
            this.btnJira.FlatAppearance.BorderSize = 0;
            this.btnJira.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJira.ForeColor = System.Drawing.Color.White;
            this.btnJira.Location  = new System.Drawing.Point(692, 11);
            this.btnJira.Name      = "btnJira";
            this.btnJira.Size      = new System.Drawing.Size(122, 28);
            this.btnJira.TabIndex  = 7;
            this.btnJira.Text      = "Jira";
            this.btnJira.Click    += new System.EventHandler(this.BtnJira_Click);

            // ── pnlLeft ──────────────────────────────────────────────────────
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(28, 28, 30);
            this.pnlLeft.Controls.Add(this._lv);
            this.pnlLeft.Dock     = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 50);
            this.pnlLeft.Name     = "pnlLeft";
            this.pnlLeft.Size     = new System.Drawing.Size(760, 590);
            this.pnlLeft.TabIndex = 1;

            // _lv
            this._lv.BackColor     = System.Drawing.Color.FromArgb(28, 28, 30);
            this._lv.BorderStyle   = System.Windows.Forms.BorderStyle.None;
            this._lv.Dock          = System.Windows.Forms.DockStyle.Fill;
            this._lv.Font          = new System.Drawing.Font("Segoe UI", 9.5F);
            this._lv.ForeColor     = System.Drawing.Color.White;
            this._lv.FullRowSelect = true;
            this._lv.GridLines     = false;
            this._lv.HeaderStyle   = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._lv.HideSelection = false;
            this._lv.Location      = new System.Drawing.Point(0, 0);
            this._lv.MultiSelect   = false;
            this._lv.Name          = "_lv";
            this._lv.OwnerDraw     = true;
            this._lv.Size          = new System.Drawing.Size(760, 590);
            this._lv.TabIndex      = 0;
            this._lv.View          = System.Windows.Forms.View.Details;
            this._lv.Columns.Add("",          22);
            this._lv.Columns.Add("Name",     255);
            this._lv.Columns.Add("Priority",  90);
            this._lv.Columns.Add("Due",      170);
            this._lv.Columns.Add("Status",    84);

            // ── pnlDetail ────────────────────────────────────────────────────
            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(36, 36, 40);
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
            this.pnlDetail.Dock     = System.Windows.Forms.DockStyle.Right;
            this.pnlDetail.Location = new System.Drawing.Point(760, 50);
            this.pnlDetail.Name     = "pnlDetail";
            this.pnlDetail.Size     = new System.Drawing.Size(320, 590);
            this.pnlDetail.TabIndex = 2;

            // lblDetailHeader
            this.lblDetailHeader.AutoSize  = true;
            this.lblDetailHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailHeader.Font      = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblDetailHeader.ForeColor = System.Drawing.Color.White;
            this.lblDetailHeader.Location  = new System.Drawing.Point(18, 18);
            this.lblDetailHeader.Name      = "lblDetailHeader";
            this.lblDetailHeader.TabIndex  = 0;
            this.lblDetailHeader.Text      = "Task Details";

            // lblNameCaption
            this.lblNameCaption.AutoSize  = true;
            this.lblNameCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblNameCaption.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblNameCaption.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this.lblNameCaption.Location  = new System.Drawing.Point(18, 62);
            this.lblNameCaption.Name      = "lblNameCaption";
            this.lblNameCaption.TabIndex  = 1;
            this.lblNameCaption.Text      = "NAME";

            // _lblName
            this._lblName.AutoSize  = false;
            this._lblName.BackColor = System.Drawing.Color.Transparent;
            this._lblName.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this._lblName.ForeColor = System.Drawing.Color.White;
            this._lblName.Location  = new System.Drawing.Point(18, 78);
            this._lblName.Name      = "_lblName";
            this._lblName.Size      = new System.Drawing.Size(275, 22);
            this._lblName.TabIndex  = 2;
            this._lblName.Text      = "";

            // lblPriorityCaption
            this.lblPriorityCaption.AutoSize  = true;
            this.lblPriorityCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPriorityCaption.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblPriorityCaption.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this.lblPriorityCaption.Location  = new System.Drawing.Point(18, 108);
            this.lblPriorityCaption.Name      = "lblPriorityCaption";
            this.lblPriorityCaption.TabIndex  = 3;
            this.lblPriorityCaption.Text      = "PRIORITY";

            // _lblPriority
            this._lblPriority.AutoSize  = false;
            this._lblPriority.BackColor = System.Drawing.Color.Transparent;
            this._lblPriority.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this._lblPriority.ForeColor = System.Drawing.Color.White;
            this._lblPriority.Location  = new System.Drawing.Point(18, 124);
            this._lblPriority.Name      = "_lblPriority";
            this._lblPriority.Size      = new System.Drawing.Size(275, 22);
            this._lblPriority.TabIndex  = 4;
            this._lblPriority.Text      = "";

            // lblDueCaption
            this.lblDueCaption.AutoSize  = true;
            this.lblDueCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDueCaption.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblDueCaption.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this.lblDueCaption.Location  = new System.Drawing.Point(18, 154);
            this.lblDueCaption.Name      = "lblDueCaption";
            this.lblDueCaption.TabIndex  = 5;
            this.lblDueCaption.Text      = "DUE DATE";

            // _lblDue
            this._lblDue.AutoSize  = false;
            this._lblDue.BackColor = System.Drawing.Color.Transparent;
            this._lblDue.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this._lblDue.ForeColor = System.Drawing.Color.White;
            this._lblDue.Location  = new System.Drawing.Point(18, 170);
            this._lblDue.Name      = "_lblDue";
            this._lblDue.Size      = new System.Drawing.Size(275, 40);
            this._lblDue.TabIndex  = 6;
            this._lblDue.Text      = "";

            // lblStatusCaption
            this.lblStatusCaption.AutoSize  = true;
            this.lblStatusCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusCaption.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusCaption.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this.lblStatusCaption.Location  = new System.Drawing.Point(18, 218);
            this.lblStatusCaption.Name      = "lblStatusCaption";
            this.lblStatusCaption.TabIndex  = 7;
            this.lblStatusCaption.Text      = "STATUS";

            // _lblStatus
            this._lblStatus.AutoSize  = false;
            this._lblStatus.BackColor = System.Drawing.Color.Transparent;
            this._lblStatus.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this._lblStatus.ForeColor = System.Drawing.Color.White;
            this._lblStatus.Location  = new System.Drawing.Point(18, 234);
            this._lblStatus.Name      = "_lblStatus";
            this._lblStatus.Size      = new System.Drawing.Size(275, 22);
            this._lblStatus.TabIndex  = 8;
            this._lblStatus.Text      = "";

            // lblNotesCaption
            this.lblNotesCaption.AutoSize  = true;
            this.lblNotesCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblNotesCaption.Font      = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblNotesCaption.ForeColor = System.Drawing.Color.FromArgb(130, 130, 140);
            this.lblNotesCaption.Location  = new System.Drawing.Point(18, 266);
            this.lblNotesCaption.Name      = "lblNotesCaption";
            this.lblNotesCaption.TabIndex  = 9;
            this.lblNotesCaption.Text      = "NOTES";

            // _txtNotes
            this._txtNotes.BackColor   = System.Drawing.Color.FromArgb(50, 50, 56);
            this._txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtNotes.Font        = new System.Drawing.Font("Segoe UI", 9.5F);
            this._txtNotes.ForeColor   = System.Drawing.Color.LightGray;
            this._txtNotes.Location    = new System.Drawing.Point(18, 282);
            this._txtNotes.Multiline   = true;
            this._txtNotes.Name        = "_txtNotes";
            this._txtNotes.ScrollBars  = System.Windows.Forms.ScrollBars.Vertical;
            this._txtNotes.Size        = new System.Drawing.Size(275, 90);
            this._txtNotes.TabIndex    = 10;
            this._txtNotes.TextChanged += new System.EventHandler(this.TxtNotes_TextChanged);

            // pnlDivider
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(60, 60, 68);
            this.pnlDivider.Location  = new System.Drawing.Point(18, 382);
            this.pnlDivider.Name      = "pnlDivider";
            this.pnlDivider.Size      = new System.Drawing.Size(275, 1);
            this.pnlDivider.TabIndex  = 11;

            // _btnEdit
            this._btnEdit.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this._btnEdit.Enabled   = false;
            this._btnEdit.FlatAppearance.BorderSize = 0;
            this._btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnEdit.ForeColor = System.Drawing.Color.White;
            this._btnEdit.Location  = new System.Drawing.Point(18, 392);
            this._btnEdit.Name      = "_btnEdit";
            this._btnEdit.Size      = new System.Drawing.Size(76, 28);
            this._btnEdit.TabIndex  = 12;
            this._btnEdit.Text      = "Edit";
            this._btnEdit.Click    += new System.EventHandler(this.BtnEdit_Click);

            // _btnDone
            this._btnDone.BackColor = System.Drawing.Color.FromArgb(16, 124, 16);
            this._btnDone.Enabled   = false;
            this._btnDone.FlatAppearance.BorderSize = 0;
            this._btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDone.ForeColor = System.Drawing.Color.White;
            this._btnDone.Location  = new System.Drawing.Point(104, 392);
            this._btnDone.Name      = "_btnDone";
            this._btnDone.Size      = new System.Drawing.Size(100, 28);
            this._btnDone.TabIndex  = 13;
            this._btnDone.Text      = "Mark Done";
            this._btnDone.Click    += new System.EventHandler(this.BtnDone_Click);

            // _btnDelete
            this._btnDelete.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);
            this._btnDelete.Enabled   = false;
            this._btnDelete.FlatAppearance.BorderSize = 0;
            this._btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDelete.ForeColor = System.Drawing.Color.White;
            this._btnDelete.Location  = new System.Drawing.Point(214, 392);
            this._btnDelete.Name      = "_btnDelete";
            this._btnDelete.Size      = new System.Drawing.Size(76, 28);
            this._btnDelete.TabIndex  = 14;
            this._btnDelete.Text      = "Delete";
            this._btnDelete.Click    += new System.EventHandler(this.BtnDelete_Click);

            // ── MainForm ─────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(28, 28, 30);
            this.ClientSize          = new System.Drawing.Size(1080, 640);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlDetail);
            this.Controls.Add(this.pnlToolbar);
            this.Font                = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize         = new System.Drawing.Size(860, 540);
            this.Name                = "MainForm";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task List";

            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
