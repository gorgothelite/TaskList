using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Test
{
    public partial class MainForm : DarkForm
    {
        // ── State ────────────────────────────────────────────────────────────
        private List<TaskItem>             _tasks   = new List<TaskItem>();
        private List<RevisionEntry>        _history = new List<RevisionEntry>();
        private TaskItem                   _sel;
        private System.Windows.Forms.Timer _alertTimer;
        private bool                       _alertActive;
        private HistoryDialog              _historyWindow;
        private JiraForm                   _jiraWindow;
        private System.Windows.Forms.Timer _notesSaveTimer;
        private System.Windows.Forms.Timer _TimeSpentTimer;
        private bool                       _loadingDetails;
        private bool                       _refreshingList;
        private NotifyIcon                 _notifyIcon;
        private ContextMenuStrip           _trayMenu;
        private ToolStripMenuItem          _trayMuteItem;
        private bool                       _mutedNotifications;
        private bool                       _forceClose;
        private Icon                       _trayIconNormal;
        private Icon                       _trayIconMuted;
        private readonly HashSet<string>   _collapsed = new HashSet<string>();
        private HashSet<string>            _parentIds = new HashSet<string>();

        // ── Sort state ───────────────────────────────────────────────────────
        private int  _sortColumn    = -1;   // -1 = default (priority desc, due asc)
        private bool _sortAscending = true;

        // ── File paths ───────────────────────────────────────────────────────
        private static readonly string BaseDir      = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string DataFile     = Path.Combine(BaseDir, "tasks.json");
        private static readonly string HistFile     = Path.Combine(BaseDir, "tasks_history.json");
        private static readonly string SettingsFile = Path.Combine(BaseDir, "tasks_settings.json");
        private static readonly string BackupDir    = Path.Combine(BaseDir, "backups");
        private static readonly string ImagesDir    = Path.Combine(BaseDir, "task_images");
        private const int MaxBackups = 20;

        private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".tif", ".webp" };

        // ── Visual maps ──────────────────────────────────────────────────────
        private static readonly Color[]  PriCol  = { Color.FromArgb(88,196,88), Color.FromArgb(214,188,50), Color.FromArgb(232,116,40), Color.FromArgb(222,52,52) };
        private static readonly string[] PriName = { "Low", "Medium", "High", "Critical" };
        private Icon _iconFromPng; // to keep the handle aliv
        // ── Constructor ──────────────────────────────────────────────────────
        public MainForm()
        {
            _resizable = true;
            InitializeComponent();

            //using (var bmp = (Bitmap)Image.FromFile("Resources\\automation.png"))
            //{
            //    _iconFromPng = Icon.FromHandle(bmp.GetHicon());
            //    this.Icon = _iconFromPng;
            //}

            _cmbStatusF.SelectedIndex   = 0;
            _cmbPriorityF.SelectedIndex = 0;
            _lv.Columns.Add("",          22);
            _lv.Columns.Add("Name",     255);
            _lv.Columns.Add("Priority",  90);
            _lv.Columns.Add("Due",      170);
            _lv.Columns.Add("Status",   230);
            RegisterTitleBar(pnlTitleBar, showMin: true, showMax: true);
            LoadSettings();
            WireListView();
            LoadHistory();
            LoadTasks();
            RefreshList();

            _alertTimer = new System.Windows.Forms.Timer { Interval = 60_000 };
            _alertTimer.Tick += CheckAlerts;
            _alertTimer.Start();

            // Debounce timer: saves notes 800 ms after the user stops typing
            _notesSaveTimer = new System.Windows.Forms.Timer { Interval = 800 };
            _notesSaveTimer.Tick += NotesSaveTimer_Tick;

            _TimeSpentTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _TimeSpentTimer.Tick += TimeSpentTimer_Tick;
            _TimeSpentTimer.Start();
            Shown += (s, e) => CheckAlerts(null, null);
            InitTray();
        }

        // ── System tray ───────────────────────────────────────────────────────
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr handle);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, bool wParam, int lParam);
        private const int WM_SETREDRAW = 11;

        private static Icon BuildTrayIcon(bool muted)
        {
            using (var bmp = new Bitmap(16, 16))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(Color.Transparent);

                    // Base circle: blue when active, grey when muted
                    Color baseColor = muted ? Color.FromArgb(110, 110, 115) : Color.FromArgb(0, 112, 200);
                    using (var br = new SolidBrush(baseColor))
                        g.FillEllipse(br, 1, 1, 13, 13);

                    if (muted)
                    {
                        // Red X
                        using (var pen = new Pen(Color.FromArgb(210, 45, 45), 2.5f) { StartCap = System.Drawing.Drawing2D.LineCap.Round, EndCap = System.Drawing.Drawing2D.LineCap.Round })
                        {
                            g.DrawLine(pen, 4, 4, 11, 11);
                            g.DrawLine(pen, 11, 4, 4, 11);
                        }
                    }
                    else
                    {
                        // White checkmark
                        using (var pen = new Pen(Color.White, 2f) { StartCap = System.Drawing.Drawing2D.LineCap.Round, EndCap = System.Drawing.Drawing2D.LineCap.Round })
                        {
                            g.DrawLine(pen, 3, 8, 6, 11);
                            g.DrawLine(pen, 6, 11, 12, 4);
                        }
                    }
                }

                IntPtr hIcon = bmp.GetHicon();
                try   { return (Icon)Icon.FromHandle(hIcon).Clone(); }
                finally { DestroyIcon(hIcon); }
            }
        }

        private void InitTray()
        {
            _trayIconNormal = BuildTrayIcon(false);
            _trayIconMuted  = BuildTrayIcon(true);

            _trayMenu = new ContextMenuStrip();
            var openItem  = new ToolStripMenuItem("Open", null, (s, e) => RestoreFromTray());
            _trayMuteItem = new ToolStripMenuItem("Mute Notifications", null, TrayMute_Click) { CheckOnClick = true };
            var closeItem = new ToolStripMenuItem("Close Application", null, (s, e) => { _forceClose = true; Close(); });
            _trayMenu.Items.AddRange(new ToolStripItem[] { openItem, _trayMuteItem, new ToolStripSeparator(), closeItem });

            _notifyIcon = new NotifyIcon
            {
                Icon             = _trayIconNormal,
                Text             = "Task Manager",
                ContextMenuStrip = _trayMenu,
                Visible          = false
            };
            _notifyIcon.DoubleClick += (s, e) => RestoreFromTray();
        }

        private void RestoreFromTray()
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
            _notifyIcon.Visible = false;
        }

        private void TrayMute_Click(object sender, EventArgs e)
        {
            _mutedNotifications  = _trayMuteItem.Checked;
            _notifyIcon.Icon     = _mutedNotifications ? _trayIconMuted  : _trayIconNormal;
            _notifyIcon.Text     = _mutedNotifications ? "Task Manager (Muted)" : "Task Manager";
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //if (WindowState == FormWindowState.Minimized)
            //{
                //Hide();
                //_notifyIcon.Visible = true;
            //}
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_forceClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                _notifyIcon.Visible = true;
                return;
            }
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
            _trayIconNormal?.Dispose();
            _trayIconMuted?.Dispose();
            base.OnFormClosing(e);
        }

        private void WireListView()
        {
            _lv.DrawColumnHeader     += DrawHeader;
            _lv.DrawSubItem          += DrawCell;
            _lv.DrawItem             += (s, e) => { };
            _lv.SelectedIndexChanged += OnSelection;
            _lv.MouseClick           += Lv_MouseClick;
            _lv.ColumnClick          += Lv_ColumnClick;
        }

        private void Lv_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _sortColumn)
                _sortAscending = !_sortAscending;
            else
            {
                _sortColumn    = e.Column;
                _sortAscending = e.Column != 2 && e.Column != 0; // priority cols default desc
            }
            SaveSettings();
            RefreshList(updateDetails: false);
            _lv.Invalidate(); // repaint headers to show arrow
        }

        // ── Details ──────────────────────────────────────────────────────────
        private void ShowDetails(TaskItem t)
        {
            SendMessage(pnlDetail.Handle, WM_SETREDRAW, false, 0);
            try { ShowDetailsCore(t); }
            finally
            {
                SendMessage(pnlDetail.Handle, WM_SETREDRAW, true, 0);
                pnlDetail.Invalidate(true);
            }
        }

        private void ShowDetailsCore(TaskItem t)
        {
            _loadingDetails = true;
            bool has = t != null;
            _btnEdit.Enabled = _btnDone.Enabled = _btnDelete.Enabled = _btnHold.Enabled = _btnAddImage.Enabled = has;

            if (!has)
            {
                _lblName.Text = _lblPriority.Text = _lblDue.Text = _lblStatus.Text = "";
                _txtNotes.Text = "";
                pnlDivider2.Visible = lblSubCaption.Visible = _lblSubInfo.Visible = _btnAddSubtask.Visible = false;
                RefreshImageThumbs(null);
                _loadingDetails = false;
                return;
            }

            bool overdue = !t.IsDone && t.DueDate < DateTime.Now;

            _lblName.Text = t.Name; _lblName.ForeColor = Color.White;
            _lblPriority.Text = PriName[(int)t.Priority]; _lblPriority.ForeColor = PriCol[(int)t.Priority];
            _lblDue.Text = t.DueDate.ToString("f") + "\n" + AlertLeadLabel(t.AlertLeadMinutes);
            _lblDue.ForeColor = overdue ? Color.FromArgb(255,100,100) : Color.White;
            lblTotalTimeSpentDisplay.Text = _sel.ComputeTotalActiveHours().ToString("F2");
            if      (t.IsDone)    { _lblStatus.Text = "✓  Completed"; _lblStatus.ForeColor = Color.FromArgb(88,196,88); }
            else if (t.IsOnHold)  { _lblStatus.Text = "⏸  On Hold";   _lblStatus.ForeColor = Color.FromArgb(220,160,0); }
            else if (overdue)     { _lblStatus.Text = "⚠  Overdue";   _lblStatus.ForeColor = Color.FromArgb(255,100,100); }
            else                  { _lblStatus.Text = "●  Active";     _lblStatus.ForeColor = Color.FromArgb(90,190,255); }

            _txtNotes.Text     = t.Notes;
            _btnDone.Text      = t.IsDone ? "Mark Active" : "Mark Done";
            _btnDone.BackColor = t.IsDone ? Color.FromArgb(100,80,0) : Color.FromArgb(16,124,16);
            _btnHold.Text      = t.IsOnHold ? "Remove Hold" : "Put On Hold";
            _btnHold.BackColor = t.IsOnHold ? Color.FromArgb(50,80,40) : Color.FromArgb(120,80,0);

            // Subtask section
            pnlDivider2.Visible = lblSubCaption.Visible = _lblSubInfo.Visible = true;
            bool isSubtask = t.ParentId != null;
            if (isSubtask)
            {
                lblSubCaption.Text = "PARENT TASK";
                var parent = _tasks.FirstOrDefault(x => x.Id == t.ParentId);
                _lblSubInfo.Text = parent != null ? parent.Name : "(deleted)";
                _btnAddSubtask.Visible = false;
            }
            else
            {
                lblSubCaption.Text = "SUBTASKS";
                int total = _tasks.Count(x => x.ParentId == t.Id);
                int done  = _tasks.Count(x => x.ParentId == t.Id && x.IsDone);
                _lblSubInfo.Text = total == 0 ? "None" : $"{done}/{total} done";
                _btnAddSubtask.Visible  = true;
                _btnAddSubtask.Enabled  = true;
            }

            RefreshImageThumbs(t);
            _loadingDetails = false;
        }

        // ── List ─────────────────────────────────────────────────────────────
        private void RefreshList(bool updateDetails = true)
        {
            _parentIds = new HashSet<string>(_tasks
                .Where(t => t.ParentId != null)
                .Select(t => t.ParentId));

            var filtered = GetFiltered().ToList();
            if (_sel != null && !filtered.Any(t => t.Id == _sel.Id))
                _sel = null;

            string selId = _sel?.Id;
            _refreshingList = true;
            _lv.BeginUpdate();
            _lv.Items.Clear();
            foreach (var t in filtered)
            {
                bool overdue = !t.IsDone && t.DueDate < DateTime.Now;
                var li = new ListViewItem("") { Name = t.Id, Tag = t };
                li.SubItems.Add(t.ParentId != null ? "  ↳ " + t.Name : t.Name);
                li.SubItems.Add(PriName[(int)t.Priority]);
                li.SubItems.Add(t.DueDate.ToString("g"));
                li.SubItems.Add(t.IsDone ? "Done" : t.IsOnHold ? "On Hold" : overdue ? "Overdue" : "Active");
                if (t.Id == selId) li.Selected = true;
                _lv.Items.Add(li);
            }
            _lv.EndUpdate();
            _refreshingList = false;
            if (updateDetails) ShowDetails(_sel);
        }

        private IEnumerable<TaskItem> GetFiltered()
        {
            var q = _tasks.AsEnumerable();
            int si = _cmbStatusF.SelectedIndex, pi = _cmbPriorityF.SelectedIndex;
            if (si == 1) q = q.Where(t => !t.IsDone && !t.IsOnHold);
            else if (si == 2) q = q.Where(t =>  t.IsDone);
            else if (si == 3) q = q.Where(t =>  t.IsOnHold);
            if (pi > 0)  q = q.Where(t => (int)t.Priority == pi - 1);

            var filtered  = q.ToList();
            var topLevel  = ApplySort(filtered.Where(t => t.ParentId == null)).ToList();
            var subtasks  = ApplySort(filtered.Where(t => t.ParentId != null)).ToList();

            var result   = new List<TaskItem>();
            var addedIds = new HashSet<string>();
            foreach (var parent in topLevel)
            {
                result.Add(parent);
                addedIds.Add(parent.Id);
                var children = subtasks.Where(s => s.ParentId == parent.Id).ToList();
                if (!_collapsed.Contains(parent.Id))
                    foreach (var child in children) { result.Add(child); addedIds.Add(child.Id); }
                else
                    foreach (var child in children) addedIds.Add(child.Id); // hidden but accounted for
            }
            // Include any subtasks whose parent was filtered out
            foreach (var child in subtasks.Where(s => !addedIds.Contains(s.Id)))
                result.Add(child);
            return result;
        }

        private IEnumerable<TaskItem> ApplySort(IEnumerable<TaskItem> src)
        {
            // Status sort order: Active=0, Overdue=1, On Hold=2, Done=3
            int StatusRank(TaskItem t)
            {
                if (t.IsDone)    return 3;
                if (t.IsOnHold)  return 2;
                if (t.DueDate < DateTime.Now) return 1;
                return 0;
            }

            IOrderedEnumerable<TaskItem> ordered;
            switch (_sortColumn)
            {
                case 0: // priority dot
                case 2: // priority text
                    ordered = _sortAscending
                        ? src.OrderBy(t => (int)t.Priority).ThenBy(t => t.DueDate)
                        : src.OrderByDescending(t => (int)t.Priority).ThenBy(t => t.DueDate);
                    break;
                case 1: // name
                    ordered = _sortAscending
                        ? src.OrderBy(t => t.Name, StringComparer.OrdinalIgnoreCase)
                        : src.OrderByDescending(t => t.Name, StringComparer.OrdinalIgnoreCase);
                    break;
                case 3: // due date
                    ordered = _sortAscending
                        ? src.OrderBy(t => t.DueDate)
                        : src.OrderByDescending(t => t.DueDate);
                    break;
                case 4: // status
                    ordered = _sortAscending
                        ? src.OrderBy(StatusRank).ThenBy(t => t.DueDate)
                        : src.OrderByDescending(StatusRank).ThenBy(t => t.DueDate);
                    break;
                default: // default: active first, priority desc, due asc
                    ordered = src.OrderBy(t => t.IsDone)
                                 .ThenByDescending(t => (int)t.Priority)
                                 .ThenBy(t => t.DueDate);
                    break;
            }
            return ordered;
        }

        private void OnSelection(object sender, EventArgs e)
        {
            if (_refreshingList) return;
            _sel = _lv.SelectedItems.Count > 0 ? (TaskItem)_lv.SelectedItems[0].Tag : null;
            ShowDetails(_sel);
        }

        private void Lv_MouseClick(object sender, MouseEventArgs e)
        {
            var hit = _lv.HitTest(e.X, e.Y);
            if (hit.Item == null) return;
            var task = hit.Item.Tag as TaskItem;
            if (task == null || !_parentIds.Contains(task.Id)) return;

            // Only toggle if click lands in the triangle zone (first 20px of the name column)
            int col0End = _lv.Columns[0].Width;
            if (e.X < col0End || e.X > col0End + 20) return;

            var prevSel = _sel;
            if (_collapsed.Contains(task.Id)) _collapsed.Remove(task.Id);
            else                              _collapsed.Add(task.Id);

            // Skip the detail panel repaint unless the selected item was hidden by collapsing
            RefreshList(updateDetails: false);
            if (_sel != prevSel) ShowDetails(_sel);
        }

        // ── CRUD ─────────────────────────────────────────────────────────────
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new TaskDialog())
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                _tasks.Add(dlg.Result);
                BeginActiveSession(dlg.Result);
                _sel = dlg.Result;
                AddRevision(new RevisionEntry { Action = RevisionAction.TaskAdded, TaskId = dlg.Result.Id, TaskName = dlg.Result.Name, Summary = $"Task \"{dlg.Result.Name}\" added (Priority: {dlg.Result.Priority}, Due: {dlg.Result.DueDate:g})" });
                SaveAll(); RefreshList();
            }
        }

        private void EditTask()
        {
            if (_sel == null) return;
            var before = _sel.Clone();
            using (var dlg = new TaskDialog(_sel))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                var changes = DiffTask(before, _sel);
                AddRevision(new RevisionEntry { Action = RevisionAction.TaskEdited, TaskId = _sel.Id, TaskName = _sel.Name, Summary = $"Task \"{_sel.Name}\" edited ({changes.Count} field(s) changed)", Changes = changes });
                SaveAll(); RefreshList();
            }
        }

        private void ToggleDone()
        {
            if (_sel == null) return;
            bool was = _sel.IsDone;
            _sel.IsDone = !was;
            if (_sel.IsDone) EndActiveSession(_sel);
            else             BeginActiveSession(_sel);
            AddRevision(new RevisionEntry { Action = RevisionAction.StatusChanged, TaskId = _sel.Id, TaskName = _sel.Name, Summary = $"Task \"{_sel.Name}\" marked {(_sel.IsDone ? "Done" : "Active")}", Changes = new List<FieldChange> { new FieldChange { Field = "IsDone", OldValue = was.ToString(), NewValue = _sel.IsDone.ToString() } } });
            SaveAll(); RefreshList();
        }

        private void DeleteTask()
        {
            if (_sel == null) return;
            var children = _tasks.Where(t => t.ParentId == _sel.Id).ToList();
            string msg = children.Count > 0
                ? $"Delete \"{_sel.Name}\" and its {children.Count} subtask(s)?"
                : $"Delete \"{_sel.Name}\"?";
            if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            AddRevision(new RevisionEntry { Action = RevisionAction.TaskDeleted, TaskId = _sel.Id, TaskName = _sel.Name, Summary = $"Task \"{_sel.Name}\" deleted" });
            foreach (var child in children)
            {
                AddRevision(new RevisionEntry { Action = RevisionAction.TaskDeleted, TaskId = child.Id, TaskName = child.Name, Summary = $"Subtask \"{child.Name}\" deleted (parent deleted)" });
                _tasks.Remove(child);
            }
            _tasks.Remove(_sel); _sel = null;
            SaveAll(); RefreshList();
        }

        private void BtnAddSubtask_Click(object sender, EventArgs e)
        {
            if (_sel == null || _sel.ParentId != null) return;
            var parentTask = _sel;
            using (var dlg = new TaskDialog())
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                dlg.Result.ParentId = parentTask.Id;
                _tasks.Add(dlg.Result);
                BeginActiveSession(dlg.Result);
                AddRevision(new RevisionEntry { Action = RevisionAction.TaskAdded, TaskId = dlg.Result.Id, TaskName = dlg.Result.Name, Summary = $"Subtask \"{dlg.Result.Name}\" added to \"{parentTask.Name}\" (Priority: {dlg.Result.Priority}, Due: {dlg.Result.DueDate:g})" });
                SaveAll(); RefreshList();
            }
        }

        private void ToggleHold()
        {
            if (_sel == null) return;
            bool wasOnHold = _sel.IsOnHold;
            _sel.IsOnHold = !wasOnHold;
            if (_sel.IsOnHold) EndActiveSession(_sel);
            else               BeginActiveSession(_sel);
            AddRevision(new RevisionEntry { Action = RevisionAction.StatusChanged, TaskId = _sel.Id, TaskName = _sel.Name, Summary = $"Task \"{_sel.Name}\" {(_sel.IsOnHold ? "put on hold" : "removed from hold")}", Changes = new List<FieldChange> { new FieldChange { Field = "IsOnHold", OldValue = wasOnHold.ToString(), NewValue = _sel.IsOnHold.ToString() } } });
            SaveAll(); RefreshList();
        }

        // ── Button event handlers (wired in Designer) ─────────────────────────
        private void BtnEdit_Click(object sender,       EventArgs e) => EditTask();
        private void BtnDone_Click(object sender,       EventArgs e) => ToggleDone();
        private void BtnDelete_Click(object sender,     EventArgs e) => DeleteTask();
        private void BtnHold_Click(object sender,       EventArgs e) => ToggleHold();
        private void Filter_Changed(object sender,   EventArgs e) => RefreshList();

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (var dlg = new ExportDialog(_tasks))
                dlg.ShowDialog(this);
        }

        private void BtnJira_Click(object sender, EventArgs e)
        {
            if (_jiraWindow != null && !_jiraWindow.IsDisposed)
            {
                _jiraWindow.BringToFront();
                return;
            }
            _jiraWindow = new JiraForm();
            _jiraWindow.TasksImported += OnJiraTasksImported;
            _jiraWindow.FormClosed += (s2, ev) => _jiraWindow = null;
            _jiraWindow.Show(this);
        }

        private void OnJiraTasksImported(System.Collections.Generic.List<TaskItem> incoming)
        {
            int added = 0;
            foreach (var t in incoming)
            {
                // Skip if a task with the same name already exists
                if (_tasks.Exists(x => x.Name == t.Name)) continue;
                _tasks.Add(t);
                BeginActiveSession(t);
                AddRevision(new RevisionEntry
                {
                    Action   = RevisionAction.TaskAdded,
                    TaskId   = t.Id,
                    TaskName = t.Name,
                    Summary  = $"Imported from Jira: \"{t.Name}\""
                });
                added++;
            }
            if (added > 0)
            {
                SaveAll();
                RefreshList();
                MessageBox.Show($"{added} task(s) imported from Jira.", "Import Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("All selected items already exist in your task list.",
                    "Nothing Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TxtNotes_TextChanged(object sender, EventArgs e)
        {
            // Ignore changes triggered by ShowDetails loading a new selection
            if (_loadingDetails || _sel == null) return;

            // Reset the debounce timer on every keystroke
            _notesSaveTimer.Stop();
            _notesSaveTimer.Start();
        }

        // ── Work-session helpers ─────────────────────────────────────────────
        private static void BeginActiveSession(TaskItem t)
        {
            if (t.IsDone || t.IsOnHold) return;
            if (t.WorkLog.Any(s => s.End == null)) return;   // already open
            t.WorkLog.Add(new WorkSession { Start = DateTime.Now });
        }

        private static void EndActiveSession(TaskItem t)
        {
            var open = t.WorkLog.LastOrDefault(s => s.End == null);
            if (open != null) open.End = DateTime.Now;
        }

        private void TimeSpentTimer_Tick(object sender, EventArgs e)
        {
            // No longer mutates data — just keeps the detail panel display current
            // for the selected task while it is active.
            if (_sel != null && !_sel.IsDone && !_sel.IsOnHold)
                lblTotalTimeSpentDisplay.Text = _sel.ComputeTotalActiveHours().ToString("F2");
        }

        private void NotesSaveTimer_Tick(object sender, EventArgs e)
        {
            _notesSaveTimer.Stop();
            if (_sel == null) return;

            string newNotes = _txtNotes.Text;
            if (newNotes == _sel.Notes) return;   // nothing actually changed

            string oldNotes = _sel.Notes;
            _sel.Notes = newNotes;
            AddRevision(new RevisionEntry
            {
                Action   = RevisionAction.TaskEdited,
                TaskId   = _sel.Id,
                TaskName = _sel.Name,
                Summary  = $"Notes updated for \"{_sel.Name}\"",
                Changes  = new System.Collections.Generic.List<FieldChange>
                {
                    new FieldChange { Field = "Notes", OldValue = oldNotes, NewValue = newNotes }
                }
            });
            SaveAll();
        }
        private void BtnHistory_Click(object sender, EventArgs e)
        {
            if (_historyWindow != null && !_historyWindow.IsDisposed)
            {
                _historyWindow.BringToFront();
                return;
            }
            _historyWindow = new HistoryDialog(_history, BackupDir);
            _historyWindow.HistoryCleared += () => SaveHistory();
            _historyWindow.FormClosed += (s2, ev) => _historyWindow = null;
            _historyWindow.Show(this);
        }

        // ── Custom drawing ────────────────────────────────────────────────────
        private void DrawHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var bg = new SolidBrush(Color.FromArgb(44,44,46))) e.Graphics.FillRectangle(bg, e.Bounds);
            using (var pen = new Pen(Color.FromArgb(60,60,65))) e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom-1, e.Bounds.Right, e.Bounds.Bottom-1);

            bool isSorted = e.ColumnIndex == _sortColumn;
            string arrow  = isSorted ? (_sortAscending ? " ▲" : " ▼") : "";
            Color  fg     = isSorted ? Color.FromArgb(220, 220, 230) : Color.FromArgb(160, 160, 170);
            TextRenderer.DrawText(e.Graphics, e.Header.Text + arrow, new Font("Segoe UI",8.5f,FontStyle.Bold), new Rectangle(e.Bounds.X+5, e.Bounds.Y, e.Bounds.Width-5, e.Bounds.Height), fg, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        private void DrawCell(object sender, DrawListViewSubItemEventArgs e)
        {
            var task = e.Item.Tag as TaskItem;
            if (task == null) { e.DrawDefault = true; return; }

            bool sel = e.Item.Selected, isDone = task.IsDone, overdue = !isDone && task.DueDate < DateTime.Now;
            Color bg = sel ? Color.FromArgb(0,84,158) : (e.ItemIndex%2==0 ? Color.FromArgb(28,28,30) : Color.FromArgb(33,33,37));
            using (var br = new SolidBrush(bg)) e.Graphics.FillRectangle(br, e.Bounds);

            if (e.ColumnIndex == 0)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int d=9, px=e.Bounds.X+(e.Bounds.Width-d)/2, py=e.Bounds.Y+(e.Bounds.Height-d)/2;
                using (var dotBr = new SolidBrush(PriCol[(int)task.Priority])) e.Graphics.FillEllipse(dotBr, px, py, d, d);
                return;
            }

            Color fg;
            if      (sel)                                                     fg = Color.White;
            else if (isDone)                                                  fg = Color.FromArgb(100,100,108);
            else if (e.ColumnIndex == 2)                                      fg = PriCol[(int)task.Priority];
            else if (e.ColumnIndex==4 && task.IsOnHold)                       fg = Color.FromArgb(220,160,0);
            else if ((e.ColumnIndex==3 || e.ColumnIndex==4) && overdue)      fg = Color.FromArgb(255,108,108);
            else                                                              fg = Color.FromArgb(218,218,225);

            // Collapse/expand triangle for parent tasks in column 1
            bool isParent = e.ColumnIndex == 1 && _parentIds.Contains(task.Id);
            if (isParent)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                bool collapsed = _collapsed.Contains(task.Id);
                Color arrowColor = sel ? Color.White : Color.FromArgb(150, 150, 165);
                int cx = e.Bounds.X + 7, cy = e.Bounds.Y + e.Bounds.Height / 2;
                Point[] tri = collapsed
                    ? new[] { new Point(cx, cy-5), new Point(cx+8, cy), new Point(cx, cy+5) }           // ▶
                    : new[] { new Point(cx, cy-3), new Point(cx+8, cy-3), new Point(cx+4, cy+5) };      // ▼
                using (var arrowBr = new SolidBrush(arrowColor))
                    e.Graphics.FillPolygon(arrowBr, tri);
            }

            float textLeft  = isParent ? e.Bounds.X + 20 : e.Bounds.X + 5;
            float textWidth = e.Bounds.Width - (isParent ? 22 : 7);
            var sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
            using (var fgBr = new SolidBrush(fg)) e.Graphics.DrawString(e.SubItem.Text, _lv.Font, fgBr, new RectangleF(textLeft, e.Bounds.Y, textWidth, e.Bounds.Height), sf);

            if (isDone && e.ColumnIndex == 1)
            {
                float tw = Math.Min(e.Graphics.MeasureString(e.SubItem.Text, _lv.Font).Width, textWidth);
                float mid = e.Bounds.Y + e.Bounds.Height/2f;
                using (var pen = new Pen(fg)) e.Graphics.DrawLine(pen, textLeft, mid, textLeft + tw, mid);
            }
        }

        // ── Alerts ────────────────────────────────────────────────────────────
        private void CheckAlerts(object sender, EventArgs e)
        {
            if (_alertActive || _mutedNotifications) return;
            var due = _tasks.Where(t => !t.IsDone && !t.IsOnHold && !t.AlertIgnored && t.AlertLeadMinutes >= 0 && (!t.HasSnooze || DateTime.Now >= t.SnoozedUntil) && t.DueDate <= DateTime.Now.AddMinutes(t.AlertLeadMinutes))
                            .OrderByDescending(t => (int)t.Priority).ThenBy(t => t.DueDate).FirstOrDefault();
            if (due == null) return;
            _alertActive = true;
            using (var dlg = new AlertDialog(due))
            {
                dlg.ShowDialog(this);
                switch (dlg.Choice)
                {
                    case SnoozeChoice.Dismiss:      due.HasSnooze=true; due.SnoozedUntil=DateTime.Now.AddHours(1);  AddRevision(new RevisionEntry { Action=RevisionAction.AlertSnoozed, TaskId=due.Id, TaskName=due.Name, Summary=$"Alert for \"{due.Name}\" dismissed" }); break;
                    case SnoozeChoice.IgnoreAlways: due.AlertIgnored=true;                                          AddRevision(new RevisionEntry { Action=RevisionAction.AlertIgnored,  TaskId=due.Id, TaskName=due.Name, Summary=$"Alerts for \"{due.Name}\" disabled" }); break;
                    case SnoozeChoice.Snooze1Hour:  due.HasSnooze=true; due.SnoozedUntil=DateTime.Now.AddHours(1);  AddRevision(new RevisionEntry { Action=RevisionAction.AlertSnoozed, TaskId=due.Id, TaskName=due.Name, Summary=$"Alert for \"{due.Name}\" snoozed 1 h" }); break;
                    case SnoozeChoice.Snooze4Hours: due.HasSnooze=true; due.SnoozedUntil=DateTime.Now.AddHours(4);  AddRevision(new RevisionEntry { Action=RevisionAction.AlertSnoozed, TaskId=due.Id, TaskName=due.Name, Summary=$"Alert for \"{due.Name}\" snoozed 4 h" }); break;
                    case SnoozeChoice.Snooze1Day:   due.HasSnooze=true; due.SnoozedUntil=DateTime.Now.AddDays(1);   AddRevision(new RevisionEntry { Action=RevisionAction.AlertSnoozed, TaskId=due.Id, TaskName=due.Name, Summary=$"Alert for \"{due.Name}\" snoozed 1 day" }); break;
                }
                SaveAll();
            }
            _alertActive = false;
        }

        // ── Revision helpers ──────────────────────────────────────────────────
        private void AddRevision(RevisionEntry rev)
        {
            _history.Add(rev);
            // Push to the history window immediately if it's open
            if (_historyWindow != null && !_historyWindow.IsDisposed)
                _historyWindow.Refresh();
        }

        private static string AlertLeadLabel(int minutes)
        {
            foreach (var opt in TaskDialog.AlertOptions)
                if (opt.Minutes == minutes) return opt.Label;
            if (minutes < 0) return "Never";
            if (minutes < 60) return $"{minutes} min before";
            if (minutes < 1440) return $"{minutes / 60} hr before";
            return $"{minutes / 1440} day(s) before";
        }

        private static List<FieldChange> DiffTask(TaskItem before, TaskItem after)
        {
            var list = new List<FieldChange>();
            void Chk(string f, string o, string n) { if (o != n) list.Add(new FieldChange { Field=f, OldValue=o, NewValue=n }); }
            Chk("Name",     before.Name,                        after.Name);
            Chk("Priority", before.Priority.ToString(),         after.Priority.ToString());
            Chk("DueDate",  before.DueDate.ToString("g"),       after.DueDate.ToString("g"));
            Chk("Notes",    before.Notes,                       after.Notes);
            Chk("Alert",    AlertLeadLabel(before.AlertLeadMinutes), AlertLeadLabel(after.AlertLeadMinutes));
            return list;
        }

        // ── Images ────────────────────────────────────────────────────────────
        private static string GetTaskImageDir(TaskItem t) =>
            Path.Combine(ImagesDir, t.Id);

        private void RefreshImageThumbs(TaskItem t)
        {
            foreach (Control c in _pnlImagesThumbs.Controls)
                if (c is PictureBox pb && pb.Image != null) { pb.Image.Dispose(); pb.Image = null; }
            _pnlImagesThumbs.Controls.Clear();

            if (t == null || t.ImagePaths.Count == 0)
            {
                lblImagesCaption.Text = "IMAGES";
                return;
            }

            lblImagesCaption.Text = $"IMAGES ({t.ImagePaths.Count})";
            string taskDir = GetTaskImageDir(t);
            int x = 4;
            foreach (string filename in t.ImagePaths.ToList())
            {
                string fullPath = Path.Combine(taskDir, filename);
                Image img = null;
                if (File.Exists(fullPath))
                {
                    try
                    {
                        using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                            img = new Bitmap(fs);
                    }
                    catch { }
                }

                var pb = new PictureBox
                {
                    Size      = new Size(48, 48),
                    Location  = new Point(x, 2),
                    SizeMode  = PictureBoxSizeMode.Zoom,
                    BackColor = img != null ? Color.FromArgb(40, 40, 44) : Color.FromArgb(60, 35, 35),
                    Cursor    = Cursors.Hand,
                    Image     = img,
                    Tag       = filename
                };

                string capturedPath     = fullPath;
                string capturedFilename = filename;
                pb.MouseDown += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left  && File.Exists(capturedPath)) System.Diagnostics.Process.Start(capturedPath);
                    if (e.Button == MouseButtons.Right) ShowImageContextMenu(pb, capturedFilename);
                };

                _pnlImagesThumbs.Controls.Add(pb);
                x += 52;
            }
        }

        private void ShowImageContextMenu(PictureBox pb, string filename)
        {
            if (_sel == null) return;
            var menu       = new ContextMenuStrip();
            var viewItem   = new ToolStripMenuItem("View");
            var removeItem = new ToolStripMenuItem("Remove");
            viewItem.Click += (s, e) =>
            {
                string path = Path.Combine(GetTaskImageDir(_sel), filename);
                if (File.Exists(path)) System.Diagnostics.Process.Start(path);
            };
            removeItem.Click += (s, e) =>
            {
                if (MessageBox.Show($"Remove \"{filename}\" from this task?\nThe file will be deleted.",
                        "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                string path = Path.Combine(GetTaskImageDir(_sel), filename);
                _sel.ImagePaths.Remove(filename);
                try { if (File.Exists(path)) File.Delete(path); } catch { }
                SaveAll();
                RefreshImageThumbs(_sel);
            };
            menu.Items.Add(viewItem);
            menu.Items.Add(removeItem);
            menu.Show(pb, new Point(0, pb.Height));
        }

        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            if (_sel == null) return;
            using (var ofd = new OpenFileDialog
            {
                Title       = "Select Image(s)",
                Filter      = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff;*.tif;*.webp|All Files|*.*",
                Multiselect = true
            })
            {
                if (ofd.ShowDialog(this) != DialogResult.OK) return;
                AddImages(ofd.FileNames);
            }
        }

        private void AddImages(string[] paths)
        {
            if (_sel == null || paths.Length == 0) return;
            string taskDir = GetTaskImageDir(_sel);
            Directory.CreateDirectory(taskDir);
            bool any = false;
            foreach (string src in paths)
            {
                if (!ImageExtensions.Contains(Path.GetExtension(src).ToLowerInvariant())) continue;
                string name = Path.GetFileName(src);
                string dest = Path.Combine(taskDir, name);
                if (File.Exists(dest))
                {
                    string stem = Path.GetFileNameWithoutExtension(name);
                    string ext  = Path.GetExtension(name);
                    int    n    = 1;
                    do { dest = Path.Combine(taskDir, $"{stem}_{n++}{ext}"); } while (File.Exists(dest));
                    name = Path.GetFileName(dest);
                }
                try { File.Copy(src, dest); } catch { continue; }
                _sel.ImagePaths.Add(name);
                any = true;
            }
            if (!any) return;
            SaveAll();
            RefreshImageThumbs(_sel);
        }

        private void PasteImages()
        {
            if (_sel == null) return;
            if (Clipboard.ContainsImage())
            {
                var img = Clipboard.GetImage();
                if (img == null) return;
                string taskDir  = GetTaskImageDir(_sel);
                Directory.CreateDirectory(taskDir);
                string filename = $"paste_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png";
                string dest     = Path.Combine(taskDir, filename);
                try { img.Save(dest, System.Drawing.Imaging.ImageFormat.Png); } catch { img.Dispose(); return; }
                img.Dispose();
                _sel.ImagePaths.Add(filename);
                SaveAll();
                RefreshImageThumbs(_sel);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                var files = Clipboard.GetFileDropList().Cast<string>()
                    .Where(f => ImageExtensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                    .ToArray();
                AddImages(files);
            }
        }

        private void PnlDetail_DragEnter(object sender, DragEventArgs e)
        {
            if (_sel == null || !e.Data.GetDataPresent(DataFormats.FileDrop)) { e.Effect = DragDropEffects.None; return; }
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            e.Effect = files.Any(f => ImageExtensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void PnlDetail_DragDrop(object sender, DragEventArgs e)
        {
            if (_sel == null || !e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var files = ((string[])e.Data.GetData(DataFormats.FileDrop))
                .Where(f => ImageExtensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                .ToArray();
            AddImages(files);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.V) && _sel != null && ActiveControl != _txtNotes)
                PasteImages();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ── Persistence ───────────────────────────────────────────────────────
        private void SaveAll()
        {
            string name = WriteTasks();
            if (name != null && _history.Count > 0) _history[_history.Count-1].BackupFile = name;
            SaveHistory();
        }

        private string WriteTasks()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
                File.WriteAllText(DataFile, json);
                Directory.CreateDirectory(BackupDir);
                string name = $"tasks_{DateTime.Now:yyyyMMdd_HHmmss_fff}.json";
                File.WriteAllText(Path.Combine(BackupDir, name), json);
                PruneBackups();
                return name;
            }
            catch (Exception ex) { MessageBox.Show($"Could not save tasks:\n{ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return null; }
        }

        private void PruneBackups()
        {
            try { foreach (var f in Directory.GetFiles(BackupDir,"tasks_*.json").OrderByDescending(f=>f).Skip(MaxBackups)) File.Delete(f); } catch { }
        }

        private void SaveHistory()
        {
            try { File.WriteAllText(HistFile, JsonConvert.SerializeObject(_history, Formatting.Indented)); } catch { }
        }

        private void LoadTasks()
        {
            if (!File.Exists(DataFile)) return;
            try
            {
                _tasks = JsonConvert.DeserializeObject<List<TaskItem>>(File.ReadAllText(DataFile)) ?? new List<TaskItem>();
                // For active tasks that have no open session (new tasks or migration from old data),
                // begin one now so time accumulates from this session forward.
                foreach (var t in _tasks.Where(t => !t.IsDone && !t.IsOnHold))
                    BeginActiveSession(t);
                AddRevision(new RevisionEntry { Action=RevisionAction.DataLoaded, TaskName="(startup)", Summary=$"{_tasks.Count} task(s) loaded at {DateTime.Now:g}" });
            }
            catch { _tasks = new List<TaskItem>(); }
        }

        private void LoadHistory()
        {
            if (!File.Exists(HistFile)) return;
            try { _history = JsonConvert.DeserializeObject<List<RevisionEntry>>(File.ReadAllText(HistFile)) ?? new List<RevisionEntry>(); } catch { _history = new List<RevisionEntry>(); }
        }

        private void LoadSettings()
        {
            if (!File.Exists(SettingsFile)) return;
            try
            {
                var obj = Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(SettingsFile));
                _sortColumn    = obj.Value<int?>("SortColumn")    ?? -1;
                _sortAscending = obj.Value<bool?>("SortAscending") ?? true;
            }
            catch { }
        }

        private void SaveSettings()
        {
            try
            {
                File.WriteAllText(SettingsFile, JsonConvert.SerializeObject(
                    new { SortColumn = _sortColumn, SortAscending = _sortAscending },
                    Formatting.Indented));
            }
            catch { }
        }

        private void _lv_DoubleClick(object sender, EventArgs e)
        {
            EditTask();
        }
    }
}
