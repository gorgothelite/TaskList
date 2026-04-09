using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public enum TaskPriority   { Low = 0, Medium = 1, High = 2, Critical = 3 }
    public enum RevisionAction { TaskAdded, TaskEdited, TaskDeleted, StatusChanged, AlertSnoozed, AlertIgnored, DataLoaded }
    public enum SnoozeChoice   { Dismiss, IgnoreAlways, Snooze1Hour, Snooze4Hours, Snooze1Day }

    public class WorkSession
    {
        public DateTime  Start { get; set; }
        public DateTime? End   { get; set; }
    }

    public class TaskItem
    {
        public string       Id           { get; set; } = Guid.NewGuid().ToString();
        public string       Name         { get; set; } = "";
        public string       Notes        { get; set; } = "";
        public TaskPriority Priority     { get; set; } = TaskPriority.Medium;
        public DateTime     DueDate      { get; set; } = DateTime.Now.AddDays(1);
        public DateTime     StartDate    { get; set; } = DateTime.Now;
        public DateTime     EndDate      { get; set; } = DateTime.Now.AddDays(1);
        public bool         DueDateEnabled { get; set; } = true;
        public double       TotalTime    { get; set; } = 0;   // legacy — no longer mutated
        public List<WorkSession> WorkLog { get; set; } = new List<WorkSession>();
        public bool         IsDone       { get; set; } = false;
        public bool         IsOnHold     { get; set; } = false;
        public bool         AlertIgnored    { get; set; } = false;
        public bool         HasSnooze       { get; set; } = false;
        public DateTime     SnoozedUntil    { get; set; } = DateTime.MinValue;
        public int          AlertLeadMinutes { get; set; } = 1440;
        public string       ParentId         { get; set; } = null;
        public List<string> ImagePaths       { get; set; } = new List<string>();

        /// <summary>
        /// Sum of all completed work sessions plus any currently-open session.
        /// Hold and Done periods are excluded because sessions are closed at those transitions.
        /// </summary>
        public double ComputeTotalActiveHours()
        {
            double seconds = 0;
            foreach (var s in WorkLog)
                seconds += ((s.End ?? DateTime.Now) - s.Start).TotalSeconds;
            return seconds / 3600.0;
        }

        public TaskItem Clone()
        {
            var copy = (TaskItem)MemberwiseClone();
            copy.WorkLog = new List<WorkSession>(WorkLog.Select(s => new WorkSession { Start = s.Start, End = s.End }));
            return copy;
        }
    }

    public class FieldChange
    {
        public string Field    { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }

    public class RevisionEntry
    {
        public string            RevisionId { get; set; } = Guid.NewGuid().ToString();
        public DateTime          Timestamp  { get; set; } = DateTime.Now;
        public RevisionAction    Action     { get; set; }
        public string            TaskId     { get; set; }
        public string            TaskName   { get; set; }
        public string            Summary    { get; set; }
        public List<FieldChange> Changes    { get; set; } = new List<FieldChange>();
        public string            BackupFile { get; set; }
    }

    public class EmailRecipient
    {
        public string Name      { get; set; } = "";
        public string Email     { get; set; } = "";
        public string Position  { get; set; } = "";
        public bool   IsDefault { get; set; } = false;

        public override string ToString() =>
            string.IsNullOrWhiteSpace(Position)
                ? $"{Name} <{Email}>"
                : $"{Name} ({Position}) <{Email}>";
    }
}
