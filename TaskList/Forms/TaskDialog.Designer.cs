namespace Test
{
    partial class TaskDialog
    {
        private System.ComponentModel.IContainer components = null;

        // ── Existing controls ─────────────────────────────────────────────────
        private System.Windows.Forms.Panel           pnlTitleBar;
        private System.Windows.Forms.Label           _lblNameCaption;
        private System.Windows.Forms.TextBox         _txtName;
        private System.Windows.Forms.Label           _lblPriorityCaption;
        private DarkComboBox                          _cmbPriority;
        private System.Windows.Forms.Label           _lblDueDateCaption;
        private System.Windows.Forms.DateTimePicker  _dtpDate;
        private System.Windows.Forms.DateTimePicker  _dtpTime;
        private System.Windows.Forms.CheckBox        cbDueDateEnabled;
        private System.Windows.Forms.Label           _lblAlertCaption;
        private DarkComboBox                          _cmbAlert;
        private System.Windows.Forms.Label           _lblNotesCaption;
        private System.Windows.Forms.RichTextBox     _rtbNotes;
        private System.Windows.Forms.Button          _btnSave;
        private System.Windows.Forms.Button          _btnCancel;

        // ── Jira section ──────────────────────────────────────────────────────
        private System.Windows.Forms.CheckBox        cbJiraImportable;
        private System.Windows.Forms.Panel           pnlJira;
        private System.Windows.Forms.Panel           pnlJiraDivider;
        private System.Windows.Forms.Label           _lblStoryPointsCaption;
        private System.Windows.Forms.TextBox         _txtStoryPoints;
        private System.Windows.Forms.Label           _lblProjectCaption;
        private System.Windows.Forms.TextBox         _txtProject;
        private System.Windows.Forms.Label           _lblFeatureCaption;
        private System.Windows.Forms.TextBox         _txtFeature;
        private System.Windows.Forms.Label           _lblAssigneeCaption;
        private System.Windows.Forms.TextBox         _txtAssignee;
        private System.Windows.Forms.Label           _lblReporterCaption;
        private System.Windows.Forms.TextBox         _txtReporter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this._lblNameCaption = new System.Windows.Forms.Label();
            this._txtName = new System.Windows.Forms.TextBox();
            this._lblPriorityCaption = new System.Windows.Forms.Label();
            this._cmbPriority = new Test.DarkComboBox();
            this._lblDueDateCaption = new System.Windows.Forms.Label();
            this._dtpDate = new System.Windows.Forms.DateTimePicker();
            this._dtpTime = new System.Windows.Forms.DateTimePicker();
            this.cbDueDateEnabled = new System.Windows.Forms.CheckBox();
            this._lblAlertCaption = new System.Windows.Forms.Label();
            this._cmbAlert = new Test.DarkComboBox();
            this._lblNotesCaption = new System.Windows.Forms.Label();
            this._rtbNotes = new System.Windows.Forms.RichTextBox();
            this.cbJiraImportable = new System.Windows.Forms.CheckBox();
            this.pnlJira = new System.Windows.Forms.Panel();
            this.pnlJiraDivider = new System.Windows.Forms.Panel();
            this._lblStoryPointsCaption = new System.Windows.Forms.Label();
            this._txtStoryPoints = new System.Windows.Forms.TextBox();
            this._lblProjectCaption = new System.Windows.Forms.Label();
            this._txtProject = new System.Windows.Forms.TextBox();
            this._lblFeatureCaption = new System.Windows.Forms.Label();
            this._txtFeature = new System.Windows.Forms.TextBox();
            this._lblAssigneeCaption = new System.Windows.Forms.Label();
            this._txtAssignee = new System.Windows.Forms.TextBox();
            this._lblReporterCaption = new System.Windows.Forms.Label();
            this._txtReporter = new System.Windows.Forms.TextBox();
            this._btnSave = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this.pnlJira.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(46)))));
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(424, 32);
            this.pnlTitleBar.TabIndex = 20;
            // 
            // _lblNameCaption
            // 
            this._lblNameCaption.AutoSize = true;
            this._lblNameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblNameCaption.Location = new System.Drawing.Point(15, 53);
            this._lblNameCaption.Name = "_lblNameCaption";
            this._lblNameCaption.Size = new System.Drawing.Size(52, 17);
            this._lblNameCaption.TabIndex = 0;
            this._lblNameCaption.Text = "Name *";
            // 
            // _txtName
            // 
            this._txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtName.ForeColor = System.Drawing.Color.White;
            this._txtName.Location = new System.Drawing.Point(112, 47);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(286, 24);
            this._txtName.TabIndex = 1;
            // 
            // _lblPriorityCaption
            // 
            this._lblPriorityCaption.AutoSize = true;
            this._lblPriorityCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblPriorityCaption.Location = new System.Drawing.Point(15, 90);
            this._lblPriorityCaption.Name = "_lblPriorityCaption";
            this._lblPriorityCaption.Size = new System.Drawing.Size(49, 17);
            this._lblPriorityCaption.TabIndex = 2;
            this._lblPriorityCaption.Text = "Priority";
            // 
            // _cmbPriority
            // 
            this._cmbPriority.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._cmbPriority.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cmbPriority.ForeColor = System.Drawing.Color.White;
            this._cmbPriority.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High",
            "Critical"});
            this._cmbPriority.Location = new System.Drawing.Point(112, 84);
            this._cmbPriority.Name = "_cmbPriority";
            this._cmbPriority.Size = new System.Drawing.Size(286, 25);
            this._cmbPriority.TabIndex = 3;
            // 
            // _lblDueDateCaption
            // 
            this._lblDueDateCaption.AutoSize = true;
            this._lblDueDateCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblDueDateCaption.Location = new System.Drawing.Point(15, 127);
            this._lblDueDateCaption.Name = "_lblDueDateCaption";
            this._lblDueDateCaption.Size = new System.Drawing.Size(62, 17);
            this._lblDueDateCaption.TabIndex = 4;
            this._lblDueDateCaption.Text = "Due Date";
            // 
            // _dtpDate
            // 
            this._dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpDate.Location = new System.Drawing.Point(112, 121);
            this._dtpDate.Name = "_dtpDate";
            this._dtpDate.Size = new System.Drawing.Size(135, 24);
            this._dtpDate.TabIndex = 5;
            // 
            // _dtpTime
            // 
            this._dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this._dtpTime.Location = new System.Drawing.Point(252, 121);
            this._dtpTime.Name = "_dtpTime";
            this._dtpTime.ShowUpDown = true;
            this._dtpTime.Size = new System.Drawing.Size(146, 24);
            this._dtpTime.TabIndex = 6;
            // 
            // cbDueDateEnabled
            // 
            this.cbDueDateEnabled.AutoSize = true;
            this.cbDueDateEnabled.Checked = true;
            this.cbDueDateEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDueDateEnabled.Location = new System.Drawing.Point(112, 160);
            this.cbDueDateEnabled.Name = "cbDueDateEnabled";
            this.cbDueDateEnabled.Size = new System.Drawing.Size(132, 21);
            this.cbDueDateEnabled.TabIndex = 21;
            this.cbDueDateEnabled.Text = "Due Date Enabled";
            this.cbDueDateEnabled.UseVisualStyleBackColor = true;
            this.cbDueDateEnabled.CheckedChanged += new System.EventHandler(this.cbDueDateEnabled_CheckedChanged);
            // 
            // _lblAlertCaption
            // 
            this._lblAlertCaption.AutoSize = true;
            this._lblAlertCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblAlertCaption.Location = new System.Drawing.Point(15, 189);
            this._lblAlertCaption.Name = "_lblAlertCaption";
            this._lblAlertCaption.Size = new System.Drawing.Size(35, 17);
            this._lblAlertCaption.TabIndex = 7;
            this._lblAlertCaption.Text = "Alert";
            // 
            // _cmbAlert
            // 
            this._cmbAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._cmbAlert.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._cmbAlert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbAlert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cmbAlert.ForeColor = System.Drawing.Color.White;
            this._cmbAlert.Location = new System.Drawing.Point(112, 183);
            this._cmbAlert.Name = "_cmbAlert";
            this._cmbAlert.Size = new System.Drawing.Size(286, 25);
            this._cmbAlert.TabIndex = 8;
            // 
            // _lblNotesCaption
            // 
            this._lblNotesCaption.AutoSize = true;
            this._lblNotesCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblNotesCaption.Location = new System.Drawing.Point(15, 226);
            this._lblNotesCaption.Name = "_lblNotesCaption";
            this._lblNotesCaption.Size = new System.Drawing.Size(43, 17);
            this._lblNotesCaption.TabIndex = 9;
            this._lblNotesCaption.Text = "Notes";
            // 
            // _rtbNotes
            // 
            this._rtbNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._rtbNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._rtbNotes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._rtbNotes.ForeColor = System.Drawing.Color.White;
            this._rtbNotes.Location = new System.Drawing.Point(112, 220);
            this._rtbNotes.Name = "_rtbNotes";
            this._rtbNotes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._rtbNotes.Size = new System.Drawing.Size(286, 120);
            this._rtbNotes.TabIndex = 10;
            this._rtbNotes.Text = "";
            // 
            // cbJiraImportable
            // 
            this.cbJiraImportable.AutoSize = true;
            this.cbJiraImportable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this.cbJiraImportable.Location = new System.Drawing.Point(15, 351);
            this.cbJiraImportable.Name = "cbJiraImportable";
            this.cbJiraImportable.Size = new System.Drawing.Size(115, 21);
            this.cbJiraImportable.TabIndex = 22;
            this.cbJiraImportable.Text = "Jira Importable";
            this.cbJiraImportable.UseVisualStyleBackColor = true;
            this.cbJiraImportable.CheckedChanged += new System.EventHandler(this.CbJiraImportable_CheckedChanged);
            // 
            // pnlJira
            // 
            this.pnlJira.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.pnlJira.Controls.Add(this.pnlJiraDivider);
            this.pnlJira.Controls.Add(this._lblStoryPointsCaption);
            this.pnlJira.Controls.Add(this._txtStoryPoints);
            this.pnlJira.Controls.Add(this._lblProjectCaption);
            this.pnlJira.Controls.Add(this._txtProject);
            this.pnlJira.Controls.Add(this._lblFeatureCaption);
            this.pnlJira.Controls.Add(this._txtFeature);
            this.pnlJira.Controls.Add(this._lblAssigneeCaption);
            this.pnlJira.Controls.Add(this._txtAssignee);
            this.pnlJira.Controls.Add(this._lblReporterCaption);
            this.pnlJira.Controls.Add(this._txtReporter);
            this.pnlJira.Location = new System.Drawing.Point(0, 378);
            this.pnlJira.Name = "pnlJira";
            this.pnlJira.Size = new System.Drawing.Size(424, 158);
            this.pnlJira.TabIndex = 23;
            this.pnlJira.Visible = false;
            // 
            // pnlJiraDivider
            // 
            this.pnlJiraDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(68)))));
            this.pnlJiraDivider.Location = new System.Drawing.Point(15, 0);
            this.pnlJiraDivider.Name = "pnlJiraDivider";
            this.pnlJiraDivider.Size = new System.Drawing.Size(394, 1);
            this.pnlJiraDivider.TabIndex = 0;
            // 
            // _lblStoryPointsCaption
            // 
            this._lblStoryPointsCaption.AutoSize = true;
            this._lblStoryPointsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblStoryPointsCaption.Location = new System.Drawing.Point(15, 12);
            this._lblStoryPointsCaption.Name = "_lblStoryPointsCaption";
            this._lblStoryPointsCaption.Size = new System.Drawing.Size(77, 17);
            this._lblStoryPointsCaption.TabIndex = 1;
            this._lblStoryPointsCaption.Text = "Story Points";
            // 
            // _txtStoryPoints
            // 
            this._txtStoryPoints.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtStoryPoints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtStoryPoints.ForeColor = System.Drawing.Color.White;
            this._txtStoryPoints.Location = new System.Drawing.Point(112, 8);
            this._txtStoryPoints.Name = "_txtStoryPoints";
            this._txtStoryPoints.Size = new System.Drawing.Size(80, 24);
            this._txtStoryPoints.TabIndex = 2;
            // 
            // _lblProjectCaption
            // 
            this._lblProjectCaption.AutoSize = true;
            this._lblProjectCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblProjectCaption.Location = new System.Drawing.Point(15, 42);
            this._lblProjectCaption.Name = "_lblProjectCaption";
            this._lblProjectCaption.Size = new System.Drawing.Size(48, 17);
            this._lblProjectCaption.TabIndex = 3;
            this._lblProjectCaption.Text = "Project";
            // 
            // _txtProject
            // 
            this._txtProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtProject.ForeColor = System.Drawing.Color.White;
            this._txtProject.Location = new System.Drawing.Point(112, 38);
            this._txtProject.Name = "_txtProject";
            this._txtProject.Size = new System.Drawing.Size(286, 24);
            this._txtProject.TabIndex = 4;
            // 
            // _lblFeatureCaption
            // 
            this._lblFeatureCaption.AutoSize = true;
            this._lblFeatureCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblFeatureCaption.Location = new System.Drawing.Point(15, 72);
            this._lblFeatureCaption.Name = "_lblFeatureCaption";
            this._lblFeatureCaption.Size = new System.Drawing.Size(51, 17);
            this._lblFeatureCaption.TabIndex = 5;
            this._lblFeatureCaption.Text = "Feature";
            // 
            // _txtFeature
            // 
            this._txtFeature.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtFeature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtFeature.ForeColor = System.Drawing.Color.White;
            this._txtFeature.Location = new System.Drawing.Point(112, 68);
            this._txtFeature.Name = "_txtFeature";
            this._txtFeature.Size = new System.Drawing.Size(286, 24);
            this._txtFeature.TabIndex = 6;
            // 
            // _lblAssigneeCaption
            // 
            this._lblAssigneeCaption.AutoSize = true;
            this._lblAssigneeCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblAssigneeCaption.Location = new System.Drawing.Point(15, 102);
            this._lblAssigneeCaption.Name = "_lblAssigneeCaption";
            this._lblAssigneeCaption.Size = new System.Drawing.Size(60, 17);
            this._lblAssigneeCaption.TabIndex = 7;
            this._lblAssigneeCaption.Text = "Assignee";
            // 
            // _txtAssignee
            // 
            this._txtAssignee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtAssignee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtAssignee.ForeColor = System.Drawing.Color.White;
            this._txtAssignee.Location = new System.Drawing.Point(112, 98);
            this._txtAssignee.Name = "_txtAssignee";
            this._txtAssignee.Size = new System.Drawing.Size(286, 24);
            this._txtAssignee.TabIndex = 8;
            // 
            // _lblReporterCaption
            // 
            this._lblReporterCaption.AutoSize = true;
            this._lblReporterCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(175)))), ((int)(((byte)(185)))));
            this._lblReporterCaption.Location = new System.Drawing.Point(15, 132);
            this._lblReporterCaption.Name = "_lblReporterCaption";
            this._lblReporterCaption.Size = new System.Drawing.Size(60, 17);
            this._lblReporterCaption.TabIndex = 9;
            this._lblReporterCaption.Text = "Reporter";
            // 
            // _txtReporter
            // 
            this._txtReporter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this._txtReporter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtReporter.ForeColor = System.Drawing.Color.White;
            this._txtReporter.Location = new System.Drawing.Point(112, 128);
            this._txtReporter.Name = "_txtReporter";
            this._txtReporter.Size = new System.Drawing.Size(286, 24);
            this._txtReporter.TabIndex = 10;
            // 
            // _btnSave
            // 
            this._btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._btnSave.FlatAppearance.BorderSize = 0;
            this._btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSave.ForeColor = System.Drawing.Color.White;
            this._btnSave.Location = new System.Drawing.Point(112, 382);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(84, 28);
            this._btnSave.TabIndex = 11;
            this._btnSave.Text = "Save";
            this._btnSave.UseVisualStyleBackColor = false;
            this._btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(78)))));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.FlatAppearance.BorderSize = 0;
            this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.ForeColor = System.Drawing.Color.White;
            this._btnCancel.Location = new System.Drawing.Point(206, 382);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(84, 28);
            this._btnCancel.TabIndex = 12;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = false;
            // 
            // TaskDialog
            // 
            this.AcceptButton = this._btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(424, 416);
            this.Controls.Add(this.pnlTitleBar);
            this.Controls.Add(this._lblNameCaption);
            this.Controls.Add(this._txtName);
            this.Controls.Add(this._lblPriorityCaption);
            this.Controls.Add(this._cmbPriority);
            this.Controls.Add(this._lblDueDateCaption);
            this.Controls.Add(this._dtpDate);
            this.Controls.Add(this._dtpTime);
            this.Controls.Add(this.cbDueDateEnabled);
            this.Controls.Add(this._lblAlertCaption);
            this.Controls.Add(this._cmbAlert);
            this.Controls.Add(this._lblNotesCaption);
            this.Controls.Add(this._rtbNotes);
            this.Controls.Add(this.cbJiraImportable);
            this.Controls.Add(this.pnlJira);
            this.Controls.Add(this._btnSave);
            this.Controls.Add(this._btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "TaskDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Task";
            this.pnlJira.ResumeLayout(false);
            this.pnlJira.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
