using System.IO;
using Newtonsoft.Json;

namespace Test
{
    /// <summary>
    /// Persists UI-agnostic application preferences to a JSON file.
    /// No WinForms dependency — safe to reuse in any host application.
    /// </summary>
    public class SettingsService
    {
        private readonly string _settingsFile;

        public SettingsService(string baseDir)
        {
            _settingsFile = Path.Combine(baseDir, "tasks_settings.json");
        }

        public AppSettings Load()
        {
            if (!File.Exists(_settingsFile)) return new AppSettings();
            try
            {
                return JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(_settingsFile))
                       ?? new AppSettings();
            }
            catch { return new AppSettings(); }
        }

        public void Save(AppSettings settings)
        {
            try { File.WriteAllText(_settingsFile, JsonConvert.SerializeObject(settings, Formatting.Indented)); }
            catch { }
        }
    }

    public class AppSettings
    {
        public int  SortColumn    { get; set; } = -1;
        public bool SortAscending { get; set; } = true;
        public bool DarkTheme     { get; set; } = true;
    }
}
