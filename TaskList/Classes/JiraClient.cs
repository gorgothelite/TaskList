// File: JiraClient.cs
// Target: .NET Framework 4.8, C# 7.3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JiraPortable
{
    /// <summary>
    /// Minimal portable Jira REST client for creating issues (stories).
    /// Supports Jira Cloud (email + API token) and Jira Server/DC (username + password).
    /// </summary>
    public sealed class JiraClient : IDisposable
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;
        private readonly bool _isCloud; // Controls whether we use accountId (Cloud) vs name (Server/DC).
        private bool _disposed;

        /// <summary>
        /// Creates a new JiraClient.
        /// For Jira Cloud, use email + API token. For Jira Server/DC, use username + password.
        /// </summary>
        /// <param name="baseUrl">E.g., https://your-domain.atlassian.net OR https://jira.yourcompany.local</param>
        /// <param name="usernameOrEmail">Cloud: email address; Server/DC: username</param>
        /// <param name="apiTokenOrPassword">Cloud: API token; Server/DC: password</param>
        /// <param name="isCloud">True for Jira Cloud; False for Jira Server/Data Center</param>
        public JiraClient(string baseUrl, string usernameOrEmail, string apiTokenOrPassword, bool isCloud)
        {
            if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentNullException(nameof(baseUrl));
            if (string.IsNullOrWhiteSpace(usernameOrEmail)) throw new ArgumentNullException(nameof(usernameOrEmail));
            if (string.IsNullOrWhiteSpace(apiTokenOrPassword)) throw new ArgumentNullException(nameof(apiTokenOrPassword));

            // Ensure base URL has no trailing slash; REST paths add their own.
            _baseUrl = baseUrl.TrimEnd('/');
            _isCloud = isCloud;

            // Ensure TLS 1.2 (required for Jira Cloud and most secure Server/DC setups).
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            _http = new HttpClient();
            var basic = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{usernameOrEmail}:{apiTokenOrPassword}"));
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", basic);
            _http.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        /// <summary>
        /// Creates a Story issue in Jira.
        /// Returns Issue ID + Key on success (e.g., KEY-123).
        /// </summary>
        public async Task<JiraIssueCreateResult> CreateStoryAsync(JiraStoryCreateRequest request, CancellationToken ct = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.ProjectKey)) throw new ArgumentException("ProjectKey is required.", nameof(request));
            if (string.IsNullOrWhiteSpace(request.Summary)) throw new ArgumentException("Summary is required.", nameof(request));

            var issueType = request.IssueTypeName ?? "Story";
            // Map display names to keys allowed on Create
            //var createFields = await GetCreateFieldMapAsync(request.ProjectKey, issueType, ct).ConfigureAwait(false);

            // Assemble the fields object according to Jira REST v2
            // Cloud requires accountId for assignee; Server/DC uses name.
            var fields = new Dictionary<string, object>
            {
                ["project"]   = new { key = request.ProjectKey },
                ["summary"]   = request.Summary,
                ["issuetype"] = new { name = request.IssueTypeName ?? "Story" },
                ["customfield_22503"] = request.IssueStoryPoints,
            };

            if (!string.IsNullOrWhiteSpace(request.Description))
                fields["description"] = request.Description;

            if (!string.IsNullOrWhiteSpace(request.Feature))
                fields["customfield_10006"] = request.Feature;

            if (!string.IsNullOrWhiteSpace(request.PriorityName))
            {
                fields["priority"] = new { name = request.PriorityName };
            }

            if (request.Labels != null && request.Labels.Count > 0)
            {
                fields["labels"] = request.Labels;
            }

            //if (request.Components != null && request.Components.Count > 0)
            //{
            //    fields["components"] = request.Components.Select(c => new { name = c }).ToArray();
            //}

            if (!string.IsNullOrWhiteSpace(request.Assignee))
            {
                fields["assignee"] = _isCloud ? (object)new { accountId = request.Assignee }
                                              : new { name = request.Assignee };
            }

            if (!string.IsNullOrWhiteSpace(request.Reporter))
            {
                fields["reporter"] = _isCloud ? (object)new { accountId = request.Reporter }
                                              : new { name = request.Reporter };
            }

            // Epic Link is a custom field and varies per instance.
            // Common default for classic projects on Cloud is customfield_10014, but DO NOT hard-code blindly.
            // If provided here, we pass it straight through.
            if (!string.IsNullOrWhiteSpace(request.EpicLinkCustomFieldId) && !string.IsNullOrWhiteSpace(request.EpicIssueKey))
            {
                fields[request.EpicLinkCustomFieldId] = request.EpicIssueKey;
            }

            // Arbitrary additional fields (e.g., customfield_12345, fixVersions, story points, etc.)
            if (request.AdditionalFields != null)
            {
                foreach (var kvp in request.AdditionalFields)
                {
                    if (!fields.ContainsKey(kvp.Key))
                    {
                        fields[kvp.Key] = kvp.Value;
                    }
                }
            }

            var payload = new { fields };
            var json = JsonConvert.SerializeObject(payload);

            var url = $"{_baseUrl}/rest/api/2/issue";
            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            using (var resp = await _http.PostAsync(url, content, ct).ConfigureAwait(false))
            {
                var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!resp.IsSuccessStatusCode)
                {
                    throw new JiraApiException(
                        $"Jira returned {(int)resp.StatusCode} {resp.ReasonPhrase}",
                        (int)resp.StatusCode,
                        ParseErrorDetails(body)
                    );
                }

                var created = JsonConvert.DeserializeObject<JiraIssueCreateResult>(body);
                return created ?? new JiraIssueCreateResult(); // Defensive; should not be null if success.
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

                // shape: projects[0].issuetypes[...].fields (dictionary fieldKey -> fieldDef)
                var meta = JsonConvert.DeserializeObject<dynamic>(body);
                var fieldsMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase); // displayName -> key

                if (meta.projects != null && meta.projects.Count > 0)
                {
                    foreach (var it in meta.projects[0].issuetypes)
                    {
                        string itName = (string)it.name;
                        if (!string.Equals(itName, issueTypeName, StringComparison.OrdinalIgnoreCase)) continue;

                        var fieldsObj = it.fields;
                        // fieldsObj is a dictionary: key -> { name, schema, ... }
                        foreach (var field in fieldsObj)
                        {
                            string key = field.Name;                 // e.g., "customfield_10456" or "priority"
                            string name = (string)field.Value.name;  // e.g., "Feature Link"
                            if (!fieldsMap.ContainsKey(name))
                                fieldsMap[name] = key;
                        }
                        break;
                    }
                }
                return fieldsMap;
            }
        }


        /// <summary>
        /// Helper: parse Jira error response to a readable string (handles errorMessages + errors).
        /// </summary>
        private static string ParseErrorDetails(string responseBody)
        {
            if (string.IsNullOrWhiteSpace(responseBody)) return "No error body";
            try
            {
                var obj = JsonConvert.DeserializeObject<JiraErrorResponse>(responseBody);
                if (obj == null) return responseBody;

                var lines = new List<string>();
                if (obj.errorMessages != null && obj.errorMessages.Count > 0)
                {
                    lines.AddRange(obj.errorMessages);
                }
                if (obj.errors != null && obj.errors.Count > 0)
                {
                    lines.AddRange(obj.errors.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
                }
                return lines.Count > 0 ? string.Join("; ", lines) : responseBody;
            }
            catch
            {
                return responseBody; // Not JSON or unexpected shape.
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _http?.Dispose();
        }
    }

    /// <summary>
    /// Request model for story creation.
    /// </summary>
    public sealed class JiraStoryCreateRequest
    {
        /// <summary>Project key (e.g., "APP")</summary>
        public string ProjectKey { get; set; }

        /// <summary>Summary/title</summary>
        public string Summary { get; set; }

        /// <summary>Description (supports Jira markdown)</summary>
        public string Description { get; set; }

        /// <summary>Optional: "Story" by default; could be "Bug", "Task", etc.</summary>
        public string IssueTypeName { get; set; } = "Story";

        /// <summary>customfield_10006 value (Feature / Epic link key, e.g. "PROJ-1")</summary>
        public string Feature { get; set; }

        /// <summary>
        /// Assignee identifier:
        /// - Jira Cloud: accountId (string GUID-like)
        /// - Jira Server/DC: username
        /// </summary>
        public string Assignee { get; set; }

        /// <summary>
        /// Reporter identifier:
        /// - Jira Cloud: accountId (string GUID-like)
        /// - Jira Server/DC: username
        /// </summary>
        public string Reporter { get; set; }

        /// <summary>Optional: Priority name ("Highest","High","Medium","Low")</summary>
        public string PriorityName { get; set; }

        /// <summary>Optional: Labels (simple strings)</summary>
        public List<string> Labels { get; set; } = new List<string>();

        /// <summary>Optional: Components by name</summary>
        public List<string> Components { get; set; } = new List<string>();

        //customfield_22503

        /// <summary>Optional: "Story" by default; could be "Bug", "Task", etc.</summary>
        public int IssueStoryPoints { get; set; } = 0;

        /// <summary>
        /// If linking to an Epic: the custom field id (e.g., "customfield_10014") and the Epic issue key (e.g., "APP-1").
        /// Field id varies per Jira instance; do not assume defaults.
        /// </summary>
        public string EpicLinkCustomFieldId { get; set; }
        public string EpicIssueKey { get; set; }

        /// <summary>
        /// Arbitrary additional fields to send in "fields" object, e.g.:
        /// { "customfield_12345": 8, "fixVersions": [ { "name": "1.2.3" } ] }
        /// </summary>
        public Dictionary<string, object> AdditionalFields { get; set; } = new Dictionary<string, object>();
    }

    /// <summary>
    /// Response model for successful issue creation.
    /// </summary>
    public sealed class JiraIssueCreateResult
    {
        // In Jira REST v2, successful POST /issue returns id + key + self
        public string id { get; set; }
        public string key { get; set; }
        public string self { get; set; }
    }

    /// <summary>
    /// Error model (subset) used to parse Jira error details.
    /// </summary>
    internal sealed class JiraErrorResponse
    {
        public List<string> errorMessages { get; set; } = new List<string>();
        public Dictionary<string, string> errors { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// Exception type for Jira REST failures.
    /// </summary>
    public sealed class JiraApiException : Exception
    {
        public int StatusCode { get; }
        public string Details { get; }

        public JiraApiException(string message, int statusCode, string details) : base(message)
        {
            StatusCode = statusCode;
            Details = details;
        }

        public override string ToString() => $"{Message} (HTTP {StatusCode}) - {Details}";
    }
}