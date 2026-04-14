using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Test
{
    /// <summary>
    /// Builds AI summary prompts and calls Claude / OpenAI / Azure / Forge APIs.
    /// No WinForms dependency — safe to reuse in any host application.
    /// </summary>
    public class AiSummaryService
    {
        public const string DefaultTemplateText =
            "You are reviewing Jira work logs and comments for a team status summary.\n\n" +
            "{DATA}\n\n" +
            "Please provide a concise professional summary covering:\n" +
            "1. Key work accomplished\n" +
            "2. Any blockers or issues raised\n" +
            "3. Notable time investments\n\n" +
            "Keep it suitable for a status report.";

        // ── Prompt building ───────────────────────────────────────────────────

        public string BuildSummaryPrompt(List<string> headers, List<List<string>> rows,
            int issueCount, int worklogCount, int commentCount, double totalHours,
            string templateText)
        {
            int iKey     = headers.IndexOf("Key");
            int iProject = headers.IndexOf("Project");
            int iDate    = headers.IndexOf("Date");
            int iDueDate = headers.IndexOf("DueDate");
            int iAuthor  = headers.IndexOf("Author");
            int iRec     = headers.IndexOf("Record Type");
            int iHours   = headers.IndexOf("Hours");
            int iText    = headers.IndexOf("Text");
            int iStatus  = headers.IndexOf("Status");

            var body       = new StringBuilder();
            int charBudget = 80_000;

            foreach (var row in rows)
            {
                string rec     = iRec     >= 0 && iRec     < row.Count ? row[iRec]     : "";
                string project = iProject >= 0 && iProject < row.Count ? row[iProject] : "";
                string key     = iKey     >= 0 && iKey     < row.Count ? row[iKey]     : "";
                string date    = iDate    >= 0 && iDate    < row.Count ? row[iDate]    : "";
                string author  = iAuthor  >= 0 && iAuthor  < row.Count ? row[iAuthor]  : "";
                string hours   = iHours   >= 0 && iHours   < row.Count ? row[iHours]   : "";
                string text    = iText    >= 0 && iText    < row.Count ? row[iText]    : "";
                string status  = iStatus  >= 0 && iStatus  < row.Count ? row[iStatus]  : "";

                if (string.IsNullOrWhiteSpace(text)) continue;

                string line = rec == "Worklog"
                    ? $"[Worklog] Key: {key} | Project: {project} | Date: {date} | Author: {author} | Hours: {hours}h | Summary: {text} | Status: {status}\n"
                    : $"[Comment] Key: {key} | Project: {project} | Date: {date} | Author: {author} | Summary:{text} | Status: {status}\n";

                if (body.Length + line.Length > charBudget)
                {
                    body.Append("... (additional entries truncated due to length)\n");
                    break;
                }
                body.Append(line);
            }

            string data =
                $"Statistics:\n" +
                $"  Issues processed: {issueCount}\n" +
                $"  Worklogs: {worklogCount} ({totalHours:N2} hours total)\n" +
                $"  Comments: {commentCount}\n\n" +
                $"Entries:\n{body}";

            if (string.IsNullOrWhiteSpace(templateText))
                templateText = DefaultTemplateText;

            return templateText.Contains("{DATA}")
                ? templateText.Replace("{DATA}", data)
                : templateText + "\n\n" + data;
        }

        // ── AI dispatch ───────────────────────────────────────────────────────

        public enum AiProvider { Claude, OpenAI, Azure, Forge }

        public Task<string> SummarizeAsync(AiProvider provider, string claudeKey, string openAiKey,
            string azureKey, string azureEndpoint, string forgeKey, string prompt)
        {
            switch (provider)
            {
                case AiProvider.OpenAI: return CallOpenAI(openAiKey, prompt);
                case AiProvider.Azure:  return CallAzureOpenAI(azureKey, azureEndpoint, prompt);
                case AiProvider.Forge:  return CallForgeAI(forgeKey, prompt);
                default:                return CallClaude(claudeKey, prompt);
            }
        }

        // ── Individual provider calls ─────────────────────────────────────────

        public async Task<string> CallClaude(string key, string prompt)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("Enter an Anthropic API key to use Claude summarization.");

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("x-api-key", key);
                http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var body = new JObject
                {
                    ["model"]      = "claude-opus-4-6",
                    ["max_tokens"] = 2048,
                    ["messages"]   = new JArray(new JObject { ["role"] = "user", ["content"] = prompt })
                };

                var resp = await http.PostAsync(
                    "https://api.anthropic.com/v1/messages",
                    new StringContent(body.ToString(), Encoding.UTF8, "application/json"));

                var text = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                    throw new Exception($"Claude API error {(int)resp.StatusCode}: {text.Substring(0, Math.Min(300, text.Length))}");

                return JObject.Parse(text)["content"]?[0]?["text"]?.ToString() ?? "(no response returned)";
            }
        }

        public async Task<string> CallOpenAI(string key, string prompt)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("Enter an OpenAI API key to use ChatGPT summarization.");

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var body = new JObject
                {
                    ["model"]      = "gpt-4o",
                    ["max_tokens"] = 2048,
                    ["messages"]   = new JArray(new JObject { ["role"] = "user", ["content"] = prompt })
                };

                var resp = await http.PostAsync(
                    "https://api.openai.com/v1/chat/completions",
                    new StringContent(body.ToString(), Encoding.UTF8, "application/json"));

                var text = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                    throw new Exception($"OpenAI API error {(int)resp.StatusCode}: {text.Substring(0, Math.Min(300, text.Length))}");

                return JObject.Parse(text)["choices"]?[0]?["message"]?["content"]?.ToString() ?? "(no response returned)";
            }
        }

        public async Task<string> CallForgeAI(string key, string prompt)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("Enter a Forge API key to use Forge summarization.");

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var body = new JObject
                {
                    ["model"]      = "gpt-oss-20b-Q6_K.gguf",
                    ["max_tokens"] = 2048,
                    ["messages"]   = new JArray(new JObject { ["role"] = "user", ["content"] = prompt })
                };

                var resp = await http.PostAsync(
                    "https://forge-dev.vdl.cluster.caemilusa.us/api/chat/completions",
                    new StringContent(body.ToString(), Encoding.UTF8, "application/json"));

                var text = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                    throw new Exception($"Forge API error {(int)resp.StatusCode}: {text.Substring(0, Math.Min(300, text.Length))}");

                return JObject.Parse(text)["choices"]?[0]?["message"]?["content"]?.ToString() ?? "(no response returned)";
            }
        }

        public async Task<string> CallAzureOpenAI(string key, string endpoint, string prompt)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("Enter an Azure OpenAI API key to use Copilot summarization.");
            if (string.IsNullOrEmpty(endpoint))
                throw new Exception("Enter the Azure OpenAI endpoint URL.");

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("api-key", key);
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var body = new JObject
                {
                    ["max_tokens"] = 2048,
                    ["messages"]   = new JArray(new JObject { ["role"] = "user", ["content"] = prompt })
                };

                var resp = await http.PostAsync(
                    endpoint,
                    new StringContent(body.ToString(), Encoding.UTF8, "application/json"));

                var text = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                    throw new Exception($"Azure OpenAI error {(int)resp.StatusCode}: {text.Substring(0, Math.Min(300, text.Length))}");

                return JObject.Parse(text)["choices"]?[0]?["message"]?["content"]?.ToString() ?? "(no response returned)";
            }
        }
    }
}
