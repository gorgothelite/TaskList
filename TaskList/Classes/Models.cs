using System;
using System.Collections.Generic;

namespace Test
{
    public enum TaskPriority   { Low = 0, Medium = 1, High = 2, Critical = 3 }
    public enum RevisionAction { TaskAdded, TaskEdited, TaskDeleted, StatusChanged, AlertSnoozed, AlertIgnored, DataLoaded }
    public enum SnoozeChoice   { Dismiss, IgnoreAlways, Snooze1Hour, Snooze4Hours, Snooze1Day }

    public class TaskItem
    {
        public string       Id           { get; set; } = Guid.NewGuid().ToString();
        public string       Name         { get; set; } = "";
        public string       Notes        { get; set; } = "";
        public TaskPriority Priority     { get; set; } = TaskPriority.Medium;
        public DateTime     DueDate      { get; set; } = DateTime.Now.AddDays(1);
        public bool         IsDone       { get; set; } = false;
        public bool         IsOnHold     { get; set; } = false;
        public bool         AlertIgnored    { get; set; } = false;
        public bool         HasSnooze       { get; set; } = false;
        public DateTime     SnoozedUntil    { get; set; } = DateTime.MinValue;
        public int          AlertLeadMinutes { get; set; } = 1440;

        public TaskItem Clone() => (TaskItem)MemberwiseClone();
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
