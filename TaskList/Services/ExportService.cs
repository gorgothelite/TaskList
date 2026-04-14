using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Test
{
    /// <summary>
    /// Task export logic: filtering, sorting, header/row/CSV building, and recipient persistence.
    /// No WinForms dependency — safe to reuse in any host application.
    /// </summary>
    public class ExportService
    {
        private readonly string _recipientsFile;

        public ExportService(string baseDir)
        {
            _recipientsFile = Path.Combine(baseDir, "email_recipients.json");
        }

        // ── Recipients persistence ────────────────────────────────────────────

        public List<EmailRecipient> LoadRecipients()
        {
            try
            {
                if (!File.Exists(_recipientsFile)) return new List<EmailRecipient>();
                return JsonConvert.DeserializeObject<List<EmailRecipient>>(File.ReadAllText(_recipientsFile))
                       ?? new List<EmailRecipient>();
            }
            catch { return new List<EmailRecipient>(); }
        }

        public void SaveRecipients(List<EmailRecipient> recipients)
        {
            try { File.WriteAllText(_recipientsFile, JsonConvert.SerializeObject(recipients, Formatting.Indented)); }
            catch { }
        }

        // ── Filtering / sorting ───────────────────────────────────────────────

        /// <summary>
        /// Applies status and priority filters, then returns tasks ordered by priority/due-date
        /// with subtasks interleaved beneath their parents.
        /// </summary>
        public static IEnumerable<TaskItem> ApplyFilter(IEnumerable<TaskItem> tasks, ExportFilter filter)
        {
            var q = tasks.AsEnumerable();

            if (filter.ActiveOnly)     q = q.Where(t => !t.IsDone);
            else if (filter.DoneOnly)  q = q.Where(t =>  t.IsDone);

            var allowed = new List<TaskPriority>();
            if (filter.IncludeLow)      allowed.Add(TaskPriority.Low);
            if (filter.IncludeMedium)   allowed.Add(TaskPriority.Medium);
            if (filter.IncludeHigh)     allowed.Add(TaskPriority.High);
            if (filter.IncludeCritical) allowed.Add(TaskPriority.Critical);
            q = q.Where(t => allowed.Contains(t.Priority));

            var filtered = q.ToList();
            var topLevel = filtered.Where(t => t.ParentId == null)
                                   .OrderByDescending(t => (int)t.Priority).ThenBy(t => t.DueDate)
                                   .ToList();
            var subtasks = filtered.Where(t => t.ParentId != null)
                                   .OrderByDescending(t => (int)t.Priority).ThenBy(t => t.DueDate)
                                   .ToList();

            var result   = new List<TaskItem>();
            var addedIds = new HashSet<string>();
            foreach (var parent in topLevel)
            {
                result.Add(parent);
                addedIds.Add(parent.Id);
                foreach (var child in subtasks.Where(s => s.ParentId == parent.Id))
                {
                    result.Add(child);
                    addedIds.Add(child.Id);
                }
            }
            foreach (var child in subtasks.Where(s => !addedIds.Contains(s.Id)))
                result.Add(child);

            return result;
        }

        // ── Headers / rows / CSV ──────────────────────────────────────────────

        public static List<string> BuildHeaders(ColumnSelection cols)
        {
            var list = new List<string>();
            if (cols.Name)     list.Add("Name");
            if (cols.Priority) list.Add("Priority");
            if (cols.Due)      list.Add("Due Date");
            if (cols.Status)   list.Add("Status");
            if (cols.Notes)    list.Add("Notes");
            list.Add("Story Points");
            list.Add("Project");
            list.Add("Feature");
            list.Add("Assignee");
            list.Add("Reporter");
            return list;
        }

        public static List<List<string>> BuildRows(IEnumerable<TaskItem> filteredTasks, ColumnSelection cols)
        {
            var result = new List<List<string>>();
            foreach (var t in filteredTasks)
            {
                var row = new List<string>();
                if (cols.Name)     row.Add(t.ParentId != null ? "  \u21b3 " + t.Name : t.Name);
                if (cols.Priority) row.Add(t.Priority.ToString());
                if (cols.Due)      row.Add(t.DueDate.ToString("yyyy-MM-dd HH:mm"));
                if (cols.Status)   row.Add(t.IsDone ? "Done" : (t.DueDate < DateTime.Now ? "Overdue" : "Active"));
                if (cols.Notes)    row.Add(t.Notes);
                if (t.JiraImportable)
                {
                    row.Add(t.JiraStoryPoints?.ToString() ?? "");
                    row.Add(t.JiraProject  ?? "");
                    row.Add(t.JiraFeature  ?? "");
                    row.Add(t.JiraAssignee ?? "");
                    row.Add(t.JiraReporter ?? "");
                }
                result.Add(row);
            }
            return result;
        }

        public static string BuildCsv(IEnumerable<TaskItem> filteredTasks, ColumnSelection cols)
        {
            var tasks = filteredTasks.ToList();
            var sb    = new StringBuilder();
            sb.AppendLine(string.Join(",", BuildHeaders(cols).Select(CsvEscape)));
            foreach (var row in BuildRows(tasks, cols))
                sb.AppendLine(string.Join(",", row.Select(CsvEscape)));
            return sb.ToString();
        }

        private static string CsvEscape(string s)
        {
            if (s == null) return "\"\"";
            return $"\"{s.Replace("\"", "\"\"")}\"";
        }

        // ── Filter / column option types ──────────────────────────────────────

        public class ExportFilter
        {
            public bool ActiveOnly      { get; set; }
            public bool DoneOnly        { get; set; }
            public bool IncludeLow      { get; set; } = true;
            public bool IncludeMedium   { get; set; } = true;
            public bool IncludeHigh     { get; set; } = true;
            public bool IncludeCritical { get; set; } = true;
        }

        public class ColumnSelection
        {
            public bool Name     { get; set; } = true;
            public bool Priority { get; set; } = true;
            public bool Due      { get; set; } = true;
            public bool Status   { get; set; } = true;
            public bool Notes    { get; set; } = true;
        }
    }
}
