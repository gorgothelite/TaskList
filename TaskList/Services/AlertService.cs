using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    /// <summary>
    /// Alert query logic: determines which task (if any) should fire an alert right now.
    /// No WinForms dependency — safe to reuse in any host application.
    /// </summary>
    public static class AlertService
    {
        /// <summary>
        /// Returns the highest-priority task that is currently due for an alert,
        /// or null if no task qualifies.
        /// </summary>
        public static TaskItem GetNextAlert(IEnumerable<TaskItem> tasks)
        {
            return tasks
                .Where(t => !t.IsDone
                         && !t.IsOnHold
                         && !t.AlertIgnored
                         && t.AlertLeadMinutes >= 0
                         && (!t.HasSnooze || DateTime.Now >= t.SnoozedUntil)
                         && t.DueDate <= DateTime.Now.AddMinutes(t.AlertLeadMinutes))
                .OrderByDescending(t => (int)t.Priority)
                .ThenBy(t => t.DueDate)
                .FirstOrDefault();
        }
    }
}
