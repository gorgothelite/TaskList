using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Test
{
    /// <summary>
    /// Handles all file I/O for tasks and revision history.
    /// No WinForms dependency — safe to reuse in any host application.
    /// </summary>
    public class TaskRepository
    {
        private readonly string _dataFile;
        private readonly string _histFile;
        private readonly string _backupDir;
        private const int MaxBackups = 20;

        public string BackupDir => _backupDir;

        public TaskRepository(string baseDir)
        {
            _dataFile  = Path.Combine(baseDir, "tasks.json");
            _histFile  = Path.Combine(baseDir, "tasks_history.json");
            _backupDir = Path.Combine(baseDir, "backups");
        }

        public List<TaskItem> LoadTasks()
        {
            if (!File.Exists(_dataFile)) return new List<TaskItem>();
            try
            {
                return JsonConvert.DeserializeObject<List<TaskItem>>(File.ReadAllText(_dataFile))
                       ?? new List<TaskItem>();
            }
            catch { return new List<TaskItem>(); }
        }

        /// <summary>
        /// Writes tasks.json and a timestamped backup.
        /// Returns the backup filename on success. Throws on I/O failure.
        /// </summary>
        public string SaveTasks(List<TaskItem> tasks)
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(_dataFile, json);
            Directory.CreateDirectory(_backupDir);
            string name = $"tasks_{DateTime.Now:yyyyMMdd_HHmmss_fff}.json";
            File.WriteAllText(Path.Combine(_backupDir, name), json);
            PruneBackups();
            return name;
        }

        public List<RevisionEntry> LoadHistory()
        {
            if (!File.Exists(_histFile)) return new List<RevisionEntry>();
            try
            {
                return JsonConvert.DeserializeObject<List<RevisionEntry>>(File.ReadAllText(_histFile))
                       ?? new List<RevisionEntry>();
            }
            catch { return new List<RevisionEntry>(); }
        }

        public void SaveHistory(List<RevisionEntry> history)
        {
            try { File.WriteAllText(_histFile, JsonConvert.SerializeObject(history, Formatting.Indented)); }
            catch { }
        }

        /// <summary>
        /// Overwrites tasks.json with the given backup file.
        /// Throws on I/O failure so the caller can handle UI feedback.
        /// </summary>
        public void RestoreBackup(string sourcePath)
        {
            File.Copy(sourcePath, _dataFile, overwrite: true);
        }

        private void PruneBackups()
        {
            try
            {
                foreach (var f in Directory.GetFiles(_backupDir, "tasks_*.json")
                                           .OrderByDescending(f => f)
                                           .Skip(MaxBackups))
                    File.Delete(f);
            }
            catch { }
        }
    }
}
