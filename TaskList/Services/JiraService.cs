using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test
{
    /// <summary>
    /// Jira HTTP client, config/preset persistence, issue parsing, and Jira→TaskItem conversion.
    /// No WinForms dependency — safe to reuse in any host application.
    /// </summary>
    public class JiraService
    {
        /// <summary>Shared HTTP client — one per app lifetime. Also used by JiraQuickRunDialog and JiraExportDialog.</summary>
        internal static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };

        private readonly string _jiraFile;
        private readonly string _templatesFile;

        public JiraService(string baseDir)
        {
            _jiraFile      = Path.Combine(baseDir, "jira.json");
            _templatesFile = Path.Combine(baseDir, "ai_prompt_templates.json");
        }

        // ── Shared file helper ────────────────────────────────────────────────

        private JObject ReadJiraFile()
        {
            if (!File.Exists(_jiraFile)) return new JObject();
            try { return JObject.Parse(File.ReadAllText(_jiraFile)); }
            catch { return new JObject(); }
        }

        private void WriteJiraFile(JObject obj)
        {
            File.WriteAllText(_jiraFile, obj.ToString(Formatting.Indented));
        }

        // ── Config ────────────────────────────────────────────────────────────

        public JiraConfig LoadConfig()
        {
            try
            {
                var conn = ReadJiraFile()["connection"] as JObject;
                if (conn == null) return new JiraConfig();
                return new JiraConfig
                {
                    Url   = conn["url"]?.ToString()      ?? "",
                    Email = conn["username"]?.ToString()  ?? "",
                    Token = DpapiCrypto.UnprotectFromBase64(conn["password"]?.ToString() ?? "", DataProtectionScope.CurrentUser)
                };
            }
            catch { return new JiraConfig(); }
        }

        public void SaveConfig(string url, string email, string token)
        {
            try
            {
                var obj = ReadJiraFile();
                obj["connection"] = new JObject
                {
                    ["url"]      = url,
                    ["username"] = email,
                    ["password"] = DpapiCrypto.ProtectToBase64(token, DataProtectionScope.CurrentUser)
                };
                WriteJiraFile(obj);
            }
            catch { }
        }

        /// <summary>
        /// Sets Basic-auth headers on the shared HTTP client.
        /// Returns false if any credential field is empty.
        /// </summary>
        public bool SetupAuth(string url, string email, string token)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                return false;
            string cred = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{email}:{token}"));
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", cred);
            Http.DefaultRequestHeaders.Accept.Clear();
            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return true;
        }

        // ── Presets ───────────────────────────────────────────────────────────

        public List<JiraPreset> LoadPresets()
        {
            try
            {
                var arr = ReadJiraFile()["presets"] as JArray;
                if (arr != null)
                {
                    var list = arr.Select(item => new JiraPreset
                    {
                        Name = item["name"]?.ToString() ?? "",
                        Jql  = item["jql"]?.ToString()  ?? ""
                    }).ToList();
                    if (list.Count > 0) return list;
                }
            }
            catch { }
            return DefaultPresets();
        }

        public void SavePresets(List<JiraPreset> presets)
        {
            try
            {
                var obj = ReadJiraFile();
                obj["presets"] = new JArray(presets.Select(p => new JObject { ["name"] = p.Name, ["jql"] = p.Jql }));
                WriteJiraFile(obj);
            }
            catch { }
        }

        public static List<JiraPreset> DefaultPresets() => new List<JiraPreset>
        {
            new JiraPreset { Name = "My Current Tasks",      Jql = "Sprint in openSprints() AND assignee = currentUser() AND resolution = Unresolved ORDER BY priority DESC" },
            new JiraPreset { Name = "My work last week",     Jql = "assignee = currentUser() AND updated >= -1w ORDER BY updated DESC" },
            new JiraPreset { Name = "Open bugs by priority", Jql = "issuetype = Bug AND status != Done ORDER BY priority DESC" },
            new JiraPreset { Name = "In progress",           Jql = "status = \"In Progress\" ORDER BY updated DESC" },
            new JiraPreset { Name = "Open stories & epics",  Jql = "issuetype in (Story, Epic) AND status != Done ORDER BY priority DESC" },
            new JiraPreset { Name = "Due this week",         Jql = "duedate <= endOfWeek() AND resolution = Unresolved ORDER BY duedate ASC" },
            new JiraPreset { Name = "All epics",             Jql = "project is not EMPTY AND issuetype = Epic ORDER BY created DESC" },
            new JiraPreset { Name = "Search all text\u2026", Jql = "text ~ \"\" ORDER BY updated DESC" },
        };

        // ── Async API calls ───────────────────────────────────────────────────

        /// <summary>Pings /rest/api/2/myself. Returns (success, displayName, errorMessage).</summary>
        public async Task<(bool Success, string DisplayName, string Error)> TestConnectionAsync(string baseUrl)
        {
            try
            {
                var resp = await Http.GetAsync(BuildUrl("/rest/api/2/myself", baseUrl));
                if (resp.IsSuccessStatusCode)
                {
                    var json = JObject.Parse(await resp.Content.ReadAsStringAsync());
                    return (true, json["displayName"]?.ToString() ?? "Unknown", null);
                }
                return (false, null, $"Connection failed: {(int)resp.StatusCode} {resp.ReasonPhrase}");
            }
            catch (Exception ex) { return (false, null, $"Error: {ex.Message}"); }
        }

        public async Task<JiraSearchResult> SearchAsync(string baseUrl, string jql)
        {
            try
            {
                var payload = new JObject
                {
                    ["jql"]        = jql,
                    ["maxResults"] = 100,
                    ["fields"]     = new JArray("summary", "status", "priority", "issuetype",
                                                "assignee", "project", "duedate", "parent",
                                                "customfield_10014")
                };
                var resp = await Http.PostAsync(
                    BuildUrl("/rest/api/2/search", baseUrl),
                    new StringContent(payload.ToString(), Encoding.UTF8, "application/json"));
                var body = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                    return new JiraSearchResult { Error = $"Search failed: {(int)resp.StatusCode} – {ParseError(body)}" };

                var data    = JObject.Parse(body);
                var issArr  = data["issues"] as JArray;
                if (issArr == null)
                    return new JiraSearchResult { Error = "Unexpected response – no 'issues' array found." };

                var issues  = new List<JiraIssue>();
                int skipped = 0;
                foreach (var issue in issArr)
                {
                    try { issues.Add(ParseIssue(issue)); }
                    catch { skipped++; }
                }
                return new JiraSearchResult
                {
                    Success = true,
                    Issues  = issues,
                    Total   = data["total"]?.ToObject<int>() ?? issues.Count,
                    Skipped = skipped
                };
            }
            catch (Exception ex) { return new JiraSearchResult { Error = $"Error: {ex.Message}" }; }
        }

        /// <summary>
        /// Fetches the parent or epic-link issue for <paramref name="forIssue"/>.
        /// Returns (issue, null) on success, (null, "no-link:KEY") if no parent exists,
        /// or (null, "fetch-error") on HTTP failure.
        /// </summary>
        public async Task<(JiraIssue Issue, string ErrorTag)> FetchParentOrEpicAsync(string baseUrl, JiraIssue forIssue)
        {
            try
            {
                var resp = await Http.GetAsync(BuildUrl(
                    $"/rest/api/2/issue/{forIssue.Key}?fields=parent,customfield_10014,summary,status,priority,issuetype,assignee,project,duedate",
                    baseUrl));
                if (!resp.IsSuccessStatusCode) return (null, "fetch-error");

                var data = JObject.Parse(await resp.Content.ReadAsStringAsync());
                var f    = data["fields"];

                // parent field (Jira Cloud next-gen / company-managed)
                var parent = f["parent"];
                if (parent != null && parent.Type != JTokenType.Null)
                {
                    var pf = parent["fields"];
                    return (new JiraIssue
                    {
                        Key      = parent["key"]?.ToString()                                 ?? "",
                        Summary  = pf?["summary"]?.ToString() ?? parent["key"]?.ToString()  ?? "",
                        Type     = (pf?["issuetype"] as JObject)?["name"]?.ToString()        ?? "Parent",
                        Status   = (pf?["status"]    as JObject)?["name"]?.ToString()        ?? "",
                        Priority = (pf?["priority"]  as JObject)?["name"]?.ToString()        ?? "",
                        Project  = forIssue.Project,
                        DueDate  = ""
                    }, null);
                }

                // customfield_10014 = epic link (classic Jira)
                string epicKey = f["customfield_10014"]?.ToString();
                if (!string.IsNullOrEmpty(epicKey))
                {
                    var epicResp = await Http.GetAsync(BuildUrl(
                        $"/rest/api/2/issue/{epicKey}?fields=summary,status,priority,issuetype,assignee,project,duedate",
                        baseUrl));
                    if (!epicResp.IsSuccessStatusCode) return (null, "fetch-error");
                    return (ParseIssue(JObject.Parse(await epicResp.Content.ReadAsStringAsync())), null);
                }

                return (null, $"no-link:{forIssue.Key}");
            }
            catch { return (null, "fetch-error"); }
        }

        // ── Parsing helpers ───────────────────────────────────────────────────

        public static JiraIssue ParseIssue(JToken issue)
        {
            var f = issue["fields"];
            return new JiraIssue
            {
                Key      = issue["key"]?.ToString()                                  ?? "",
                Summary  = f["summary"]?.ToString()                                  ?? "",
                Type     = (f["issuetype"] as JObject)?["name"]?.ToString()          ?? "",
                Status   = (f["status"]    as JObject)?["name"]?.ToString()          ?? "",
                Priority = (f["priority"]  as JObject)?["name"]?.ToString()          ?? "",
                Assignee = (f["assignee"]  as JObject)?["displayName"]?.ToString()   ?? "Unassigned",
                Project  = (f["project"]   as JObject)?["name"]?.ToString()          ?? "",
                DueDate  = FormatDate(f["duedate"]?.ToString()),
            };
        }

        public static string ParseError(string json)
        {
            try
            {
                var obj = JObject.Parse(json);
                return obj["errorMessages"]?.First?.ToString()
                    ?? obj["message"]?.ToString()
                    ?? json.Substring(0, Math.Min(120, json.Length));
            }
            catch { return json.Substring(0, Math.Min(120, json.Length)); }
        }

        public static string FormatDate(string iso)
        {
            if (string.IsNullOrEmpty(iso)) return "";
            return DateTime.TryParse(iso, out var d) ? d.ToString("yyyy-MM-dd") : iso;
        }

        public static string BuildUrl(string path, string baseUrl) => baseUrl.TrimEnd('/') + path;

        // ── Jira → TaskItem conversion ────────────────────────────────────────

        // ── AI settings persistence ───────────────────────────────────────────

        public AiSettings LoadAiSettings()
        {
            try
            {
                var ai = ReadJiraFile()["ai"] as JObject;
                if (ai == null) return new AiSettings();
                return new AiSettings
                {
                    ClaudeKey        = ai["anthropic_key"]?.ToString()        ?? "",
                    OpenAiKey        = ai["openai_key"]?.ToString()            ?? "",
                    AzureKey         = ai["azure_openai_key"]?.ToString()      ?? "",
                    AzureEndpoint    = ai["azure_openai_endpoint"]?.ToString() ?? "",
                    ForgeKey         = ai["forge_key"]?.ToString()             ?? "",
                    ProviderIndex    = ai["ai_provider"]?.ToObject<int>()      ?? 0,
                    SelectedTemplate = ai["selected_ai_template"]?.ToString()  ?? "",
                };
            }
            catch { return new AiSettings(); }
        }

        public void SaveAiSettings(AiSettings s)
        {
            try
            {
                var obj = ReadJiraFile();
                obj["ai"] = new JObject
                {
                    ["ai_provider"]           = s.ProviderIndex,
                    ["anthropic_key"]         = s.ClaudeKey,
                    ["openai_key"]            = s.OpenAiKey,
                    ["azure_openai_key"]      = s.AzureKey,
                    ["azure_openai_endpoint"] = s.AzureEndpoint,
                    ["forge_key"]             = s.ForgeKey,
                    ["selected_ai_template"]  = s.SelectedTemplate,
                };
                WriteJiraFile(obj);
            }
            catch { }
        }

        // ── Prompt template persistence ───────────────────────────────────────

        public List<SummaryTemplate> LoadTemplates()
        {
            if (File.Exists(_templatesFile))
            {
                try
                {
                    var arr = JArray.Parse(File.ReadAllText(_templatesFile));
                    var list = arr.Select(item => new SummaryTemplate
                    {
                        Name     = item["name"]?.ToString()     ?? "",
                        Template = item["template"]?.ToString() ?? ""
                    }).ToList();
                    if (list.Count > 0) return list;
                }
                catch { }
            }
            return new List<SummaryTemplate>();
        }

        public void SaveTemplates(List<SummaryTemplate> templates)
        {
            try
            {
                var arr = new JArray(templates.Select(t => new JObject
                {
                    ["name"]     = t.Name,
                    ["template"] = t.Template
                }));
                File.WriteAllText(_templatesFile, arr.ToString());
            }
            catch { }
        }

        // ── ADF (Atlassian Document Format) → plain text ──────────────────────

        public static string AdfToText(JToken token)
        {
            if (token == null || token.Type == JTokenType.Null) return "";
            if (token.Type == JTokenType.String)               return token.ToString();
            if (token.Type != JTokenType.Object)               return "";
            var obj     = (JObject)token;
            var content = obj["content"] as JArray;
            return content == null ? "" : ExtractAdf(content).Trim();
        }

        private static string ExtractAdf(JArray nodes)
        {
            if (nodes == null) return "";
            var sb = new StringBuilder();
            foreach (var node in nodes)
            {
                if (node?.Type != JTokenType.Object) continue;
                var obj  = (JObject)node;
                var type = obj["type"]?.ToString();
                if (type == "text")
                    sb.Append(obj["text"]?.ToString() ?? "");
                else
                {
                    var children = obj["content"] as JArray;
                    if (children != null) sb.Append(ExtractAdf(children));
                }
                if (type == "paragraph" || type == "heading" ||
                    type == "listItem"  || type == "codeBlock")
                    sb.Append("\n");
            }
            return sb.ToString();
        }

        // ── Jira export column/row building ───────────────────────────────────

        public static List<string> BuildExportHeaders(JiraExportColumns cols)
        {
            var h = new List<string>();
            if (cols.Key)        h.Add("Key");
            if (cols.Summary)    h.Add("Summary");
            if (cols.Type)       h.Add("Type");
            if (cols.Status)     h.Add("Status");
            if (cols.Priority)   h.Add("Priority");
            if (cols.Project)    h.Add("Project");
            if (cols.Assignee)   h.Add("Assignee");
            if (cols.DueDate)    h.Add("Due Date");
            if (cols.RecordType) h.Add("Record Type");
            if (cols.Date)       h.Add("Date");
            if (cols.Author)     h.Add("Author");
            if (cols.Hours)      h.Add("Hours");
            if (cols.Text)       h.Add("Text");
            return h;
        }

        public static List<string> BuildExportRow(JiraIssue issue, string recordType,
            string date, string author, string hours, string text, JiraExportColumns cols)
        {
            var row = new List<string>();
            if (cols.Key)        row.Add(issue.Key      ?? "");
            if (cols.Summary)    row.Add(issue.Summary  ?? "");
            if (cols.Type)       row.Add(issue.Type     ?? "");
            if (cols.Status)     row.Add(issue.Status   ?? "");
            if (cols.Priority)   row.Add(issue.Priority ?? "");
            if (cols.Project)    row.Add(issue.Project  ?? "");
            if (cols.Assignee)   row.Add(issue.Assignee ?? "");
            if (cols.DueDate)    row.Add(issue.DueDate  ?? "");
            if (cols.RecordType) row.Add(recordType     ?? "");
            if (cols.Date)       row.Add(date           ?? "");
            if (cols.Author)     row.Add(author         ?? "");
            if (cols.Hours)      row.Add(hours          ?? "");
            if (cols.Text)       row.Add(text           ?? "");
            return row;
        }

        // ── Quick-run config persistence ──────────────────────────────────────

        public bool IsQuickRunConfigured()
        {
            try
            {
                var qr = ReadJiraFile()["quick_run"] as JObject;
                if (qr == null) return false;
                return !string.IsNullOrWhiteSpace(qr["preset_name"]?.ToString())
                    && !string.IsNullOrWhiteSpace(qr["save_folder"]?.ToString());
            }
            catch { return false; }
        }

        public JiraQuickRunConfig LoadQuickRunConfig()
        {
            try
            {
                var qr = ReadJiraFile()["quick_run"] as JObject;
                if (qr == null) return new JiraQuickRunConfig();
                return new JiraQuickRunConfig
                {
                    PresetName       = qr["preset_name"]?.ToString()               ?? "",
                    TemplateName     = qr["template_name"]?.ToString()             ?? "",
                    SaveFolder       = qr["save_folder"]?.ToString()               ?? "",
                    FileNamePattern  = qr["file_name_pattern"]?.ToString()         ?? "jira_export_{date}.xlsx",
                    IncludeWorklogs  = qr["include_worklogs"]?.ToObject<bool>()   ?? true,
                    IncludeComments  = qr["include_comments"]?.ToObject<bool>()   ?? true,
                    IncludeAiSummary = qr["include_ai_summary"]?.ToObject<bool>() ?? true,
                    Columns = new JiraExportColumns
                    {
                        Key        = qr["col_key"]?.ToObject<bool>()         ?? true,
                        Summary    = qr["col_summary"]?.ToObject<bool>()     ?? true,
                        Type       = qr["col_type"]?.ToObject<bool>()        ?? true,
                        Status     = qr["col_status"]?.ToObject<bool>()      ?? true,
                        Priority   = qr["col_priority"]?.ToObject<bool>()    ?? true,
                        Project    = qr["col_project"]?.ToObject<bool>()     ?? true,
                        Assignee   = qr["col_assignee"]?.ToObject<bool>()    ?? true,
                        DueDate    = qr["col_due_date"]?.ToObject<bool>()    ?? true,
                        RecordType = qr["col_record_type"]?.ToObject<bool>() ?? true,
                        Date       = qr["col_date"]?.ToObject<bool>()        ?? true,
                        Author     = qr["col_author"]?.ToObject<bool>()      ?? true,
                        Hours      = qr["col_hours"]?.ToObject<bool>()       ?? true,
                        Text       = qr["col_text"]?.ToObject<bool>()        ?? true,
                    }
                };
            }
            catch { return new JiraQuickRunConfig(); }
        }

        public void SaveQuickRunConfig(JiraQuickRunConfig c)
        {
            try
            {
                var cols = c.Columns ?? new JiraExportColumns();
                var obj  = ReadJiraFile();
                obj["quick_run"] = new JObject
                {
                    ["preset_name"]        = c.PresetName,
                    ["template_name"]      = c.TemplateName,
                    ["save_folder"]        = c.SaveFolder,
                    ["file_name_pattern"]  = c.FileNamePattern,
                    ["include_worklogs"]   = c.IncludeWorklogs,
                    ["include_comments"]   = c.IncludeComments,
                    ["include_ai_summary"] = c.IncludeAiSummary,
                    ["col_key"]            = cols.Key,
                    ["col_summary"]        = cols.Summary,
                    ["col_type"]           = cols.Type,
                    ["col_status"]         = cols.Status,
                    ["col_priority"]       = cols.Priority,
                    ["col_project"]        = cols.Project,
                    ["col_assignee"]       = cols.Assignee,
                    ["col_due_date"]       = cols.DueDate,
                    ["col_record_type"]    = cols.RecordType,
                    ["col_date"]           = cols.Date,
                    ["col_author"]         = cols.Author,
                    ["col_hours"]          = cols.Hours,
                    ["col_text"]           = cols.Text,
                };
                WriteJiraFile(obj);
            }
            catch { }
        }

        // ── Create / update issues in Jira from a TaskItem ───────────────────

        /// <summary>
        /// Creates a Jira issue from the Jira fields stored on a TaskItem.
        /// Returns the new issue key (e.g. "PROJ-42") on success.
        /// Throws if the config is missing or the API call fails.
        /// </summary>
        public async System.Threading.Tasks.Task<string> CreateIssueForTaskAsync(TaskItem task)
        {
            var cfg = LoadConfig();
            if (string.IsNullOrWhiteSpace(cfg.Url) || string.IsNullOrWhiteSpace(cfg.Email) || string.IsNullOrWhiteSpace(cfg.Token))
                throw new InvalidOperationException("Jira connection is not configured. Open the Jira window and enter your URL, username, and API token.");

            using (var client = new JiraClient(cfg.Url, cfg.Email, cfg.Token, isCloud: false))
            {
                var req = new JiraStoryCreateRequest
                {
                    ProjectKey       = task.JiraProject,
                    Summary          = task.Name,
                    Description      = task.Notes,
                    IssueTypeName    = string.IsNullOrWhiteSpace(task.JiraIssueType) ? "Story" : task.JiraIssueType,
                    IssueStoryPoints = task.JiraStoryPoints ?? 0,
                    Feature          = task.JiraFeature,
                    Assignee         = task.JiraAssignee,
                    Reporter         = task.JiraReporter,
                };
                var result = await client.CreateStoryAsync(req).ConfigureAwait(false);
                return result.key;
            }
        }

        /// <summary>
        /// Updates an existing Jira issue using the Jira fields stored on a TaskItem.
        /// Throws if the config is missing, the task has no JiraKey, or the API call fails.
        /// </summary>
        public async System.Threading.Tasks.Task UpdateIssueForTaskAsync(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.JiraKey))
                throw new InvalidOperationException("This task has no Jira key — push it to Jira first.");

            var cfg = LoadConfig();
            if (string.IsNullOrWhiteSpace(cfg.Url) || string.IsNullOrWhiteSpace(cfg.Email) || string.IsNullOrWhiteSpace(cfg.Token))
                throw new InvalidOperationException("Jira connection is not configured. Open the Jira window and enter your URL, username, and API token.");

            using (var client = new JiraClient(cfg.Url, cfg.Email, cfg.Token, isCloud: false))
            {
                var req = new JiraStoryUpdateRequest
                {
                    Summary          = task.Name,
                    Description      = task.Notes,
                    IssueStoryPoints = task.JiraStoryPoints ?? 0,
                    Feature          = task.JiraFeature,
                    Assignee         = task.JiraAssignee,
                    Reporter         = task.JiraReporter,
                };
                await client.UpdateStoryAsync(task.JiraKey, req).ConfigureAwait(false);
            }
        }

        // ── Jira → TaskItem conversion ────────────────────────────────────────

        public static List<TaskItem> ConvertToTasks(IEnumerable<JiraIssue> issues)
        {
            var result = new List<TaskItem>();
            foreach (var ji in issues)
            {
                DateTime due = DateTime.Now.AddDays(7);
                if (!string.IsNullOrEmpty(ji.DueDate) && DateTime.TryParse(ji.DueDate, out var d)) due = d;

                TaskPriority pri = TaskPriority.Medium;
                switch ((ji.Priority ?? "").ToLowerInvariant())
                {
                    case "highest": case "critical": pri = TaskPriority.Critical; break;
                    case "high":                     pri = TaskPriority.High;     break;
                    case "low": case "lowest":       pri = TaskPriority.Low;      break;
                }

                result.Add(new TaskItem
                {
                    Name     = $"[{ji.Key}] {ji.Summary}",
                    Notes    = $"Jira: {ji.Key}\nProject: {ji.Project}\nType: {ji.Type}\nStatus: {ji.Status}\nAssignee: {ji.Assignee}",
                    Priority = pri,
                    DueDate  = due
                });
            }
            return result;
        }
    }

    // ── Search result ─────────────────────────────────────────────────────────
    public class JiraSearchResult
    {
        public bool            Success { get; set; }
        public List<JiraIssue> Issues  { get; set; } = new List<JiraIssue>();
        public int             Total   { get; set; }
        public int             Skipped { get; set; }
        public string          Error   { get; set; }
    }

    // ── Config DTO ────────────────────────────────────────────────────────────
    public class JiraConfig
    {
        public string Url   { get; set; } = "";
        public string Email { get; set; } = "";
        public string Token { get; set; } = ""; // decrypted
    }

    // ── Lightweight issue DTO ─────────────────────────────────────────────────
    public class JiraIssue
    {
        public string Key      { get; set; }
        public string Summary  { get; set; }
        public string Type     { get; set; }
        public string Status   { get; set; }
        public string Priority { get; set; }
        public string Assignee { get; set; }
        public string Project  { get; set; }
        public string DueDate  { get; set; }
    }

    // ── Named JQL preset ──────────────────────────────────────────────────────
    public class JiraPreset
    {
        public string Name { get; set; }
        public string Jql  { get; set; }
    }

    // ── AI settings DTO ───────────────────────────────────────────────────────
    public class AiSettings
    {
        public string ClaudeKey        { get; set; } = "";
        public string OpenAiKey        { get; set; } = "";
        public string AzureKey         { get; set; } = "";
        public string AzureEndpoint    { get; set; } = "";
        public string ForgeKey         { get; set; } = "";
        public int    ProviderIndex    { get; set; } = 0;
        public string SelectedTemplate { get; set; } = "";
    }

    // ── Prompt template ───────────────────────────────────────────────────────
    public class SummaryTemplate
    {
        public string Name     { get; set; } = "";
        public string Template { get; set; } = "";
    }

    // ── Quick-run config DTO ─────────────────────────────────────────────────
    public class JiraQuickRunConfig
    {
        public string           PresetName       { get; set; } = "";
        public string           TemplateName     { get; set; } = "";
        public string           SaveFolder       { get; set; } = "";
        public string           FileNamePattern  { get; set; } = "jira_export_{date}.xlsx";
        public bool             IncludeWorklogs  { get; set; } = true;
        public bool             IncludeComments  { get; set; } = true;
        public bool             IncludeAiSummary { get; set; } = true;
        public JiraExportColumns Columns         { get; set; } = new JiraExportColumns();
    }

    // ── Jira export column selection ──────────────────────────────────────────
    public class JiraExportColumns
    {
        public bool Key        { get; set; } = true;
        public bool Summary    { get; set; } = true;
        public bool Type       { get; set; } = true;
        public bool Status     { get; set; } = true;
        public bool Priority   { get; set; } = true;
        public bool Project    { get; set; } = true;
        public bool Assignee   { get; set; } = true;
        public bool DueDate    { get; set; } = true;
        public bool RecordType { get; set; } = true;
        public bool Date       { get; set; } = true;
        public bool Author     { get; set; } = true;
        public bool Hours      { get; set; } = true;
        public bool Text       { get; set; } = true;
    }

    // ── Jira REST client ──────────────────────────────────────────────────────

    /// <summary>
    /// Minimal Jira REST client for creating issues (stories).
    /// Supports Jira Cloud (email + API token) and Jira Server/DC (username + password).
    /// </summary>
    public sealed class JiraClient : IDisposable
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;
        private readonly bool _isCloud;
        private bool _disposed;

        public JiraClient(string baseUrl, string usernameOrEmail, string apiTokenOrPassword, bool isCloud)
        {
            if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentNullException(nameof(baseUrl));
            if (string.IsNullOrWhiteSpace(usernameOrEmail)) throw new ArgumentNullException(nameof(usernameOrEmail));
            if (string.IsNullOrWhiteSpace(apiTokenOrPassword)) throw new ArgumentNullException(nameof(apiTokenOrPassword));

            _baseUrl = baseUrl.TrimEnd('/');
            _isCloud = isCloud;

            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            _http = new HttpClient();
            var basic = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{usernameOrEmail}:{apiTokenOrPassword}"));
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);
            _http.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        /// <summary>Creates a Story issue in Jira. Returns Issue ID + Key on success (e.g., KEY-123).</summary>
        public async Task<JiraIssueCreateResult> CreateStoryAsync(JiraStoryCreateRequest request, CancellationToken ct = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.ProjectKey)) throw new ArgumentException("ProjectKey is required.", nameof(request));
            if (string.IsNullOrWhiteSpace(request.Summary)) throw new ArgumentException("Summary is required.", nameof(request));

            var fields = new Dictionary<string, object>
            {
                ["project"]           = new { key = request.ProjectKey },
                ["summary"]           = request.Summary,
                ["issuetype"]         = new { name = request.IssueTypeName ?? "Story" },
                ["customfield_22503"] = request.IssueStoryPoints,
            };

            if (!string.IsNullOrWhiteSpace(request.Description))
                fields["description"] = request.Description;

            if (!string.IsNullOrWhiteSpace(request.Feature))
                fields["customfield_10006"] = request.Feature;

            if (!string.IsNullOrWhiteSpace(request.PriorityName))
                fields["priority"] = new { name = request.PriorityName };

            if (request.Labels != null && request.Labels.Count > 0)
                fields["labels"] = request.Labels;

            if (!string.IsNullOrWhiteSpace(request.Assignee))
                fields["assignee"] = _isCloud ? (object)new { accountId = request.Assignee } : new { name = request.Assignee };

            if (!string.IsNullOrWhiteSpace(request.Reporter))
                fields["reporter"] = _isCloud ? (object)new { accountId = request.Reporter } : new { name = request.Reporter };

            if (!string.IsNullOrWhiteSpace(request.EpicLinkCustomFieldId) && !string.IsNullOrWhiteSpace(request.EpicIssueKey))
                fields[request.EpicLinkCustomFieldId] = request.EpicIssueKey;

            if (request.AdditionalFields != null)
            {
                foreach (var kvp in request.AdditionalFields)
                {
                    if (!fields.ContainsKey(kvp.Key))
                        fields[kvp.Key] = kvp.Value;
                }
            }

            var json = JsonConvert.SerializeObject(new { fields });
            var url  = $"{_baseUrl}/rest/api/2/issue";
            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            using (var resp = await _http.PostAsync(url, content, ct).ConfigureAwait(false))
            {
                var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!resp.IsSuccessStatusCode)
                    throw new JiraApiException(
                        $"Jira returned {(int)resp.StatusCode} {resp.ReasonPhrase}",
                        (int)resp.StatusCode,
                        ParseJiraErrorDetails(body));

                return JsonConvert.DeserializeObject<JiraIssueCreateResult>(body) ?? new JiraIssueCreateResult();
            }
        }

        /// <summary>
        /// Updates an existing issue in Jira via PUT /rest/api/2/issue/{key}.
        /// Only non-null/non-empty fields in <paramref name="request"/> are sent.
        /// </summary>
        public async Task UpdateStoryAsync(string issueKey, JiraStoryUpdateRequest request, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(issueKey)) throw new ArgumentNullException(nameof(issueKey));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var fields = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(request.Summary))
                fields["summary"] = request.Summary;

            if (request.Description != null)
                fields["description"] = request.Description;

            fields["customfield_22503"] = request.IssueStoryPoints;

            if (!string.IsNullOrWhiteSpace(request.Feature))
                fields["customfield_10006"] = request.Feature;

            if (!string.IsNullOrWhiteSpace(request.Assignee))
                fields["assignee"] = _isCloud ? (object)new { accountId = request.Assignee } : new { name = request.Assignee };

            if (!string.IsNullOrWhiteSpace(request.Reporter))
                fields["reporter"] = _isCloud ? (object)new { accountId = request.Reporter } : new { name = request.Reporter };

            var json = JsonConvert.SerializeObject(new { fields });
            var url  = $"{_baseUrl}/rest/api/2/issue/{Uri.EscapeDataString(issueKey)}";
            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            using (var resp = await _http.SendAsync(new HttpRequestMessage(new HttpMethod("PUT"), url) { Content = content }, ct).ConfigureAwait(false))
            {
                if (!resp.IsSuccessStatusCode)
                {
                    var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                    throw new JiraApiException(
                        $"Jira update returned {(int)resp.StatusCode} {resp.ReasonPhrase}",
                        (int)resp.StatusCode,
                        ParseJiraErrorDetails(body));
                }
            }
        }

        public async Task<Dictionary<string, string>> GetCreateFieldMapAsync(string projectKey, string issueTypeName, CancellationToken ct = default)
        {
            var url = $"{_baseUrl}/rest/api/2/issue/createmeta?projectKeys={Uri.EscapeDataString(projectKey)}&issuetypeNames={Uri.EscapeDataString(issueTypeName)}&expand=projects.issuetypes.fields";
            using (var resp = await _http.GetAsync(url, ct).ConfigureAwait(false))
            {
                var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!resp.IsSuccessStatusCode)
                    throw new JiraApiException($"CreateMeta failed {(int)resp.StatusCode} {resp.ReasonPhrase}", (int)resp.StatusCode, body);

                var meta      = JsonConvert.DeserializeObject<dynamic>(body);
                var fieldsMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                if (meta.projects != null && meta.projects.Count > 0)
                {
                    foreach (var it in meta.projects[0].issuetypes)
                    {
                        string itName = (string)it.name;
                        if (!string.Equals(itName, issueTypeName, StringComparison.OrdinalIgnoreCase)) continue;

                        foreach (var field in it.fields)
                        {
                            string key  = field.Name;
                            string name = (string)field.Value.name;
                            if (!fieldsMap.ContainsKey(name))
                                fieldsMap[name] = key;
                        }
                        break;
                    }
                }
                return fieldsMap;
            }
        }

        private static string ParseJiraErrorDetails(string responseBody)
        {
            if (string.IsNullOrWhiteSpace(responseBody)) return "No error body";
            try
            {
                var obj = JsonConvert.DeserializeObject<JiraErrorResponse>(responseBody);
                if (obj == null) return responseBody;
                var lines = new List<string>();
                if (obj.errorMessages != null && obj.errorMessages.Count > 0)
                    lines.AddRange(obj.errorMessages);
                if (obj.errors != null && obj.errors.Count > 0)
                    lines.AddRange(obj.errors.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
                return lines.Count > 0 ? string.Join("; ", lines) : responseBody;
            }
            catch { return responseBody; }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _http?.Dispose();
        }
    }

    // ── Issue creation request / response ─────────────────────────────────────

    public sealed class JiraStoryCreateRequest
    {
        public string ProjectKey    { get; set; }
        public string Summary       { get; set; }
        public string Description   { get; set; }
        public string IssueTypeName { get; set; } = "Story";
        /// <summary>customfield_10006 value (Feature / Epic link key)</summary>
        public string Feature       { get; set; }
        /// <summary>Cloud: accountId; Server/DC: username</summary>
        public string Assignee      { get; set; }
        /// <summary>Cloud: accountId; Server/DC: username</summary>
        public string Reporter      { get; set; }
        public string PriorityName  { get; set; }
        public List<string> Labels      { get; set; } = new List<string>();
        public List<string> Components  { get; set; } = new List<string>();
        public int    IssueStoryPoints  { get; set; } = 0;
        public string EpicLinkCustomFieldId { get; set; }
        public string EpicIssueKey          { get; set; }
        public Dictionary<string, object> AdditionalFields { get; set; } = new Dictionary<string, object>();
    }

    public sealed class JiraStoryUpdateRequest
    {
        public string Summary         { get; set; }
        public string Description     { get; set; }
        public int    IssueStoryPoints { get; set; } = 0;
        /// <summary>customfield_10006 value (Feature / Epic link key)</summary>
        public string Feature         { get; set; }
        /// <summary>Cloud: accountId; Server/DC: username</summary>
        public string Assignee        { get; set; }
        /// <summary>Cloud: accountId; Server/DC: username</summary>
        public string Reporter        { get; set; }
    }

    public sealed class JiraIssueCreateResult
    {
        public string id   { get; set; }
        public string key  { get; set; }
        public string self { get; set; }
    }

    internal sealed class JiraErrorResponse
    {
        public List<string>               errorMessages { get; set; } = new List<string>();
        public Dictionary<string, string> errors        { get; set; } = new Dictionary<string, string>();
    }

    public sealed class JiraApiException : Exception
    {
        public int    StatusCode { get; }
        public string Details    { get; }

        public JiraApiException(string message, int statusCode, string details) : base(message)
        {
            StatusCode = statusCode;
            Details    = details;
        }

        public override string ToString() => $"{Message} (HTTP {StatusCode}) - {Details}";
    }
}
