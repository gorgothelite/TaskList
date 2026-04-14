using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    /// <summary>
    /// All task business logic: CRUD, state transitions, work sessions,
    /// revision history, and alert snooze rules.
    /// No WinForms dependency — safe to reuse in any host application.
    /// </summary>
    public class TaskService
    {
        private readonly TaskRepository _repo;

        public List<TaskItem>      Tasks   { get; private set; } = new List<TaskItem>();
        public List<RevisionEntry> History { get; private set; } = new List<RevisionEntry>();

        /// <summary>Raised after every revision entry is appended to History.</summary>
        public event Action<RevisionEntry> RevisionAdded;

        /// <summary>Raised when a save operation fails. Argument is the caught exception.</summary>
        public event Action<Exception> SaveFailed;

        public TaskService(TaskRepository repo)
        {
            _repo = repo;
        }

        // ── Load ─────────────────────────────────────────────────────────────

        public void Load()
        {
            History = _repo.LoadHistory();
            Tasks   = _repo.LoadTasks();

            foreach (var t in Tasks.Where(t => !t.IsDone && !t.IsOnHold))
                BeginActiveSession(t);

            AddRevision(new RevisionEntry
            {
                Action   = RevisionAction.DataLoaded,
                TaskName = "(startup)",
                Summary  = $"{Tasks.Count} task(s) loaded at {DateTime.Now:g}"
            });
        }

        // ── Persistence ──────────────────────────────────────────────────────

        public void SaveAll()
        {
            try
            {
                string backupName = _repo.SaveTasks(Tasks);
                if (backupName != null && History.Count > 0)
                    History[History.Count - 1].BackupFile = backupName;
                _repo.SaveHistory(History);
            }
            catch (Exception ex)
            {
                SaveFailed?.Invoke(ex);
            }
        }

        public void SaveHistory() => _repo.SaveHistory(History);

        // ── CRUD ─────────────────────────────────────────────────────────────

        public void AddTask(TaskItem task)
        {
            Tasks.Add(task);
            BeginActiveSession(task);
            AddRevision(new RevisionEntry
            {
                Action   = RevisionAction.TaskAdded,
                TaskId   = task.Id,
                TaskName = task.Name,
                Summary  = $"Task \"{task.Name}\" added (Priority: {task.Priority}, Due: {task.DueDate:g})"
            });
            SaveAll();
        }

        public void AddSubtask(TaskItem task, TaskItem parent)
        {
            task.ParentId = parent.Id;
            Tasks.Add(task);
            BeginActiveSession(task);
            AddRevision(new RevisionEntry
            {
                Action   = RevisionAction.TaskAdded,
                TaskId   = task.Id,
                TaskName = task.Name,
                Summary  = $"Subtask \"{task.Name}\" added to \"{parent.Name}\" (Priority: {task.Priority}, Due: {task.DueDate:g})"
            });
            SaveAll();
        }

        /// <summary>
        /// Records a diff and saves after an edit dialog has already mutated the task.
        /// Pass the pre-edit clone as <paramref name="before"/> and the live task as <paramref name="after"/>.
        /// </summary>
        public void CommitEdit(TaskItem before, TaskItem after)
        {
            var changes = DiffTask(before, after);
            AddRevision(new RevisionEntry
            {
                Action   = RevisionAction.TaskEdited,
                TaskId   = after.Id,
                TaskName = after.Name,
                Summary  = $"Task \"{after.Name}\" edited ({changes.Count} field(s) changed)",
                Changes  = changes
            });
            SaveAll();
        }

        /// <summary>Deletes the task and all its subtasks, records revisions, and saves.</summary>
        public void DeleteTask(TaskItem task)
        {
            var children = Tasks.Where(t => t.ParentId == task.Id).ToList();
            AddRevision(new RevisionEntry
            {
                Action   = RevisionAction.TaskDeleted,
                TaskId   = task.Id,
                TaskName = task.Name,
                Summary  = $"Task \"{task.Name}\" deleted"
            });
            foreach (var child in children)
            {
                AddRevision(new RevisionEntry
                {
                    Action   = RevisionAction.TaskDeleted,
                    TaskId   = child.Id,
                    TaskName = child.Name,
                    Summary  = $"Subtask \"{child.Name}\" deleted (parent deleted)"
                });
                Tasks.Remove(child);
            }
            Tasks.Remove(task);
            SaveAll();
        }

        public void SetDone(TaskItem task, bool done)
        {
            bool was = task.IsDone;
            task.IsDone = done;
            if (done) EndActiveSession(task);
            else      BeginActiveSession(task);
            AddRevision(new RevisionEntry
            {
                Action   = RevisionAction.StatusChanged,
                TaskId   = task.Id,
                TaskName = task.Name,
                Summary  = $"Task \"{task.Name}\" marked {(done ? "Done" : "Active")}",
                Changes  = new List<FieldChange> { new FieldChange { Field = "IsDone", OldValue = was.ToString(), NewValue = done.ToString() } }
            });
            SaveAll();
        }

        public void SetHold(TaskItem task, bool onHold)
        {
            bool was = task.IsOnHold;
            task.IsOnHold = onHold;
            if (onHold) EndActiveSession(task);
            else        BeginActiveSession(task);
            AddRevision(new RevisionEntry
            {
                Action   = RevisionAction.StatusChanged,
                TaskId   = task.Id,
                TaskName = task.Name,
                Summary  = $"Task \"{task.Name}\" {(onHold ? "put on hold" : "removed from hold")}",
                Changes  = new List<FieldChange> { new FieldChange { Field = "IsOnHold", OldValue = was.ToString(), NewValue = onHold.ToString() } }
            });
            SaveAll();
        }

        public void UpdateNotes(TaskItem task, string newNotes)
        {
            string oldNotes = task.Notes;
            task.Notes = newNotes;
            AddRevision(new RevisionEntry
            {
                Action   = RevisionAction.TaskEdited,
                TaskId   = task.Id,
                TaskName = task.Name,
                Summary  = $"Notes updated for \"{task.Name}\"",
                Changes  = new List<FieldChange> { new FieldChange { Field = "Notes", OldValue = oldNotes, NewValue = newNotes } }
            });
            SaveAll();
        }

        /// <summary>
        /// Imports tasks from Jira, skipping any whose name already exists.
        /// Returns the number of tasks actually added.
        /// </summary>
        public int ImportFromJira(List<TaskItem> incoming)
        {
            int added = 0;
            foreach (var t in incoming)
            {
                if (Tasks.Exists(x => x.Name == t.Name)) continue;
                Tasks.Add(t);
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
            if (added > 0) SaveAll();
            return added;
        }

        // ── Alert snooze ─────────────────────────────────────────────────────

        public void ApplySnooze(TaskItem task, SnoozeChoice choice)
        {
            switch (choice)
            {
                case SnoozeChoice.Dismiss:
                    task.HasSnooze    = true;
                    task.SnoozedUntil = DateTime.Now.AddHours(1);
                    AddRevision(new RevisionEntry { Action = RevisionAction.AlertSnoozed, TaskId = task.Id, TaskName = task.Name, Summary = $"Alert for \"{task.Name}\" dismissed" });
                    break;
                case SnoozeChoice.IgnoreAlways:
                    task.AlertIgnored = true;
                    AddRevision(new RevisionEntry { Action = RevisionAction.AlertIgnored, TaskId = task.Id, TaskName = task.Name, Summary = $"Alerts for \"{task.Name}\" disabled" });
                    break;
                case SnoozeChoice.Snooze1Hour:
                    task.HasSnooze    = true;
                    task.SnoozedUntil = DateTime.Now.AddHours(1);
                    AddRevision(new RevisionEntry { Action = RevisionAction.AlertSnoozed, TaskId = task.Id, TaskName = task.Name, Summary = $"Alert for \"{task.Name}\" snoozed 1 h" });
                    break;
                case SnoozeChoice.Snooze4Hours:
                    task.HasSnooze    = true;
                    task.SnoozedUntil = DateTime.Now.AddHours(4);
                    AddRevision(new RevisionEntry { Action = RevisionAction.AlertSnoozed, TaskId = task.Id, TaskName = task.Name, Summary = $"Alert for \"{task.Name}\" snoozed 4 h" });
                    break;
                case SnoozeChoice.Snooze1Day:
                    task.HasSnooze    = true;
                    task.SnoozedUntil = DateTime.Now.AddDays(1);
                    AddRevision(new RevisionEntry { Action = RevisionAction.AlertSnoozed, TaskId = task.Id, TaskName = task.Name, Summary = $"Alert for \"{task.Name}\" snoozed 1 day" });
                    break;
            }
            SaveAll();
        }

        // ── Work sessions ────────────────────────────────────────────────────

        public static void BeginActiveSession(TaskItem t)
        {
            if (t.IsDone || t.IsOnHold) return;
            if (t.WorkLog.Any(s => s.End == null)) return;
            t.WorkLog.Add(new WorkSession { Start = DateTime.Now });
        }

        public static void EndActiveSession(TaskItem t)
        {
            var open = t.WorkLog.LastOrDefault(s => s.End == null);
            if (open != null) open.End = DateTime.Now;
        }

        // ── Revision helpers ─────────────────────────────────────────────────

        private void AddRevision(RevisionEntry rev)
        {
            History.Add(rev);
            RevisionAdded?.Invoke(rev);
        }

        private static List<FieldChange> DiffTask(TaskItem before, TaskItem after)
        {
            var list = new List<FieldChange>();
            void Chk(string f, string o, string n) { if (o != n) list.Add(new FieldChange { Field = f, OldValue = o, NewValue = n }); }
            Chk("Name",     before.Name,                               after.Name);
            Chk("Priority", before.Priority.ToString(),                after.Priority.ToString());
            Chk("DueDate",  before.DueDate.ToString("g"),              after.DueDate.ToString("g"));
            Chk("Notes",    before.Notes,                              after.Notes);
            Chk("Alert",    AlertPresets.GetLabel(before.AlertLeadMinutes), AlertPresets.GetLabel(after.AlertLeadMinutes));
            return list;
        }
    }
}
