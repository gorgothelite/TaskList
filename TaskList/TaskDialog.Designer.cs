namespace Test
{
    partial class TaskDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel           pnlTitleBar;
        private System.Windows.Forms.Label           _lblNameCaption;
        private System.Windows.Forms.TextBox         _txtName;
        private System.Windows.Forms.Label           _lblPriorityCaption;
        private System.Windows.Forms.ComboBox        _cmbPriority;
        private System.Windows.Forms.Label           _lblDueDateCaption;
        private System.Windows.Forms.DateTimePicker  _dtpDate;
        private System.Windows.Forms.DateTimePicker  _dtpTime;
        private System.Windows.Forms.Label           _lblAlertCaption;
        private System.Windows.Forms.ComboBox        _cmbAlert;
        private System.Windows.Forms.Label           _lblNotesCaption;
        private System.Windows.Forms.RichTextBox     _rtbNotes;
        private System.Windows.Forms.Button          _btnSave;
        private System.Windows.Forms.Button          _btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components          = new System.ComponentModel.Container();
            this.pnlTitleBar         = new System.Windows.Forms.Panel();
            this._lblNameCaption     = new System.Windows.Forms.Label();
            this._txtName            = new System.Windows.Forms.TextBox();
            this._lblPriorityCaption = new System.Windows.Forms.Label();
            this._cmbPriority        = new System.Windows.Forms.ComboBox();
            this._lblDueDateCaption  = new System.Windows.Forms.Label();
            this._dtpDate            = new System.Windows.Forms.DateTimePicker();
            this._dtpTime            = new System.Windows.Forms.DateTimePicker();
            this._lblAlertCaption    = new System.Windows.Forms.Label();
            this._cmbAlert           = new System.Windows.Forms.ComboBox();
            this._lblNotesCaption    = new System.Windows.Forms.Label();
            this._rtbNotes           = new System.Windows.Forms.RichTextBox();
            this._btnSave            = new System.Windows.Forms.Button();
            this._btnCancel          = new System.Windows.Forms.Button();

            this.pnlTitleBar.SuspendLayout();
            this.SuspendLayout();

            // pnlTitleBar
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(44, 44, 46);
            this.pnlTitleBar.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location  = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name      = "pnlTitleBar";
            this.pnlTitleBar.Size      = new System.Drawing.Size(424, 32);
            this.pnlTitleBar.TabIndex  = 20;

            // _lblNameCaption
            this._lblNameCaption.AutoSize  = true;
            this._lblNameCaption.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblNameCaption.Location  = new System.Drawing.Point(15, 53);
            this._lblNameCaption.Name      = "_lblNameCaption";
            this._lblNameCaption.TabIndex  = 0;
            this._lblNameCaption.Text      = "Name *";

            // _txtName
            this._txtName.BackColor   = System.Drawing.Color.FromArgb(55, 55, 60);
            this._txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtName.ForeColor   = System.Drawing.Color.White;
            this._txtName.Location    = new System.Drawing.Point(112, 47);
            this._txtName.Name        = "_txtName";
            this._txtName.Size        = new System.Drawing.Size(286, 23);
            this._txtName.TabIndex    = 1;

            // _lblPriorityCaption
            this._lblPriorityCaption.AutoSize  = true;
            this._lblPriorityCaption.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblPriorityCaption.Location  = new System.Drawing.Point(15, 90);
            this._lblPriorityCaption.Name      = "_lblPriorityCaption";
            this._lblPriorityCaption.TabIndex  = 2;
            this._lblPriorityCaption.Text      = "Priority";

            // _cmbPriority
            this._cmbPriority.BackColor     = System.Drawing.Color.FromArgb(55, 55, 60);
            this._cmbPriority.DropDownStyle  = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPriority.FlatStyle      = System.Windows.Forms.FlatStyle.Flat;
            this._cmbPriority.ForeColor      = System.Drawing.Color.White;
            this._cmbPriority.Items.AddRange(new object[] { "Low", "Medium", "High", "Critical" });
            this._cmbPriority.Location       = new System.Drawing.Point(112, 84);
            this._cmbPriority.Name           = "_cmbPriority";
            this._cmbPriority.Size           = new System.Drawing.Size(286, 23);
            this._cmbPriority.TabIndex       = 3;
            this._cmbPriority.SelectedIndex  = 1;

            // _lblDueDateCaption
            this._lblDueDateCaption.AutoSize  = true;
            this._lblDueDateCaption.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblDueDateCaption.Location  = new System.Drawing.Point(15, 127);
            this._lblDueDateCaption.Name      = "_lblDueDateCaption";
            this._lblDueDateCaption.TabIndex  = 4;
            this._lblDueDateCaption.Text      = "Due Date";

            // _dtpDate
            this._dtpDate.Format   = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpDate.Location = new System.Drawing.Point(112, 121);
            this._dtpDate.Name     = "_dtpDate";
            this._dtpDate.Size     = new System.Drawing.Size(135, 23);
            this._dtpDate.TabIndex = 5;

            // _dtpTime
            this._dtpTime.Format     = System.Windows.Forms.DateTimePickerFormat.Time;
            this._dtpTime.Location   = new System.Drawing.Point(252, 121);
            this._dtpTime.Name       = "_dtpTime";
            this._dtpTime.ShowUpDown = true;
            this._dtpTime.Size       = new System.Drawing.Size(146, 23);
            this._dtpTime.TabIndex   = 6;

            // _lblAlertCaption
            this._lblAlertCaption.AutoSize  = true;
            this._lblAlertCaption.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblAlertCaption.Location  = new System.Drawing.Point(15, 164);
            this._lblAlertCaption.Name      = "_lblAlertCaption";
            this._lblAlertCaption.TabIndex  = 7;
            this._lblAlertCaption.Text      = "Alert";

            // _cmbAlert
            this._cmbAlert.BackColor     = System.Drawing.Color.FromArgb(55, 55, 60);
            this._cmbAlert.DropDownStyle  = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbAlert.FlatStyle      = System.Windows.Forms.FlatStyle.Flat;
            this._cmbAlert.ForeColor      = System.Drawing.Color.White;
            this._cmbAlert.Location       = new System.Drawing.Point(112, 158);
            this._cmbAlert.Name           = "_cmbAlert";
            this._cmbAlert.Size           = new System.Drawing.Size(286, 23);
            this._cmbAlert.TabIndex       = 8;

            // _lblNotesCaption
            this._lblNotesCaption.AutoSize  = true;
            this._lblNotesCaption.ForeColor = System.Drawing.Color.FromArgb(175, 175, 185);
            this._lblNotesCaption.Location  = new System.Drawing.Point(15, 201);
            this._lblNotesCaption.Name      = "_lblNotesCaption";
            this._lblNotesCaption.TabIndex  = 9;
            this._lblNotesCaption.Text      = "Notes";

            // _rtbNotes
            this._rtbNotes.BackColor   = System.Drawing.Color.FromArgb(55, 55, 60);
            this._rtbNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._rtbNotes.Font        = new System.Drawing.Font("Segoe UI", 9.5F);
            this._rtbNotes.ForeColor   = System.Drawing.Color.White;
            this._rtbNotes.Location    = new System.Drawing.Point(112, 195);
            this._rtbNotes.Name        = "_rtbNotes";
            this._rtbNotes.ScrollBars  = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._rtbNotes.Size        = new System.Drawing.Size(286, 120);
            this._rtbNotes.TabIndex    = 10;

            // _btnSave
            this._btnSave.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this._btnSave.FlatAppearance.BorderSize = 0;
            this._btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSave.ForeColor = System.Drawing.Color.White;
            this._btnSave.Location  = new System.Drawing.Point(112, 331);
            this._btnSave.Name      = "_btnSave";
            this._btnSave.Size      = new System.Drawing.Size(84, 28);
            this._btnSave.TabIndex  = 11;
            this._btnSave.Text      = "Save";
            this._btnSave.Click    += new System.EventHandler(this.BtnSave_Click);

            // _btnCancel
            this._btnCancel.BackColor    = System.Drawing.Color.FromArgb(70, 70, 78);
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.FlatAppearance.BorderSize = 0;
            this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.ForeColor = System.Drawing.Color.White;
            this._btnCancel.Location  = new System.Drawing.Point(206, 331);
            this._btnCancel.Name      = "_btnCancel";
            this._btnCancel.Size      = new System.Drawing.Size(84, 28);
            this._btnCancel.TabIndex  = 12;
            this._btnCancel.Text      = "Cancel";

            // TaskDialog
            this.AcceptButton        = this._btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(37, 37, 38);
            this.CancelButton        = this._btnCancel;
            this.ClientSize          = new System.Drawing.Size(424, 379);
            this.Controls.Add(this._lblNameCaption);
            this.Controls.Add(this._txtName);
            this.Controls.Add(this._lblPriorityCaption);
            this.Controls.Add(this._cmbPriority);
            this.Controls.Add(this._lblDueDateCaption);
            this.Controls.Add(this._dtpDate);
            this.Controls.Add(this._dtpTime);
            this.Controls.Add(this._lblAlertCaption);
            this.Controls.Add(this._cmbAlert);
            this.Controls.Add(this._lblNotesCaption);
            this.Controls.Add(this._rtbNotes);
            this.Controls.Add(this._btnSave);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this.pnlTitleBar);
            this.Font          = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor     = System.Drawing.Color.White;
            this.Name          = "TaskDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text          = "Add Task";

            this.pnlTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
