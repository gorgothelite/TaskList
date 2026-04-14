using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test
{
    /// <summary>
    /// Persists UI-agnostic application preferences to the "settings" section of app_settings.json.
    /// No WinForms dependency — safe to reuse in any host application.
    /// </summary>
    public class SettingsService
    {
        private readonly string _file;

        public SettingsService(string baseDir)
        {
            _file = System.IO.Path.Combine(baseDir, "app_settings.json");
        }

        public AppSettings Load()
        {
            try
            {
                if (!File.Exists(_file)) return new AppSettings();
                var obj = JObject.Parse(File.ReadAllText(_file));
                var section = obj["settings"];
                if (section == null) return new AppSettings();
                return section.ToObject<AppSettings>() ?? new AppSettings();
            }
            catch { return new AppSettings(); }
        }

        public void Save(AppSettings settings)
        {
            try
            {
                var obj = File.Exists(_file)
                    ? JObject.Parse(File.ReadAllText(_file))
                    : new JObject();
                obj["settings"] = JObject.FromObject(settings);
                File.WriteAllText(_file, obj.ToString(Formatting.Indented));
            }
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
