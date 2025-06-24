using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Engine.Services
{
    public class SlitherAnalyzerService
    {
        private readonly HttpClient _httpClient;

        public SlitherAnalyzerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AnalyzeAsync(string solidityCode)
        {
            var request = new { sourceCode = solidityCode };
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5000/api/slither/analyze", content);

            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    using var errorDoc = JsonDocument.Parse(responseText);
                    if (errorDoc.RootElement.TryGetProperty("error", out var errorMessage))
                    {
                        var errorText = errorMessage.GetString();
                        return ExtractErrorsBeforeTraceback(errorText);
                    }
                }
                catch (JsonException)
                {
                }

                return ExtractErrorsBeforeTraceback(responseText);
            }

            try
            {
                using var doc = JsonDocument.Parse(responseText);
                var root = doc.RootElement;

                if (!root.TryGetProperty("issues", out var issuesArray))
                {
                    return "Solidity code is OK.";
                }

                if (issuesArray.ValueKind != JsonValueKind.Array || issuesArray.GetArrayLength() == 0)
                {
                    return "Solidity code is OK.";
                }

                var sb = new StringBuilder();
                foreach (var issue in issuesArray.EnumerateArray())
                {
                    var description = issue.GetProperty("description").GetString();
                    var impact = issue.GetProperty("impact").GetString();
                    var confidence = issue.GetProperty("confidence").GetString();
                    var check = issue.GetProperty("check").GetString();
                    var location = issue.GetProperty("first_markdown_element").GetString();

                    sb.AppendLine($"• {description?.Trim()}");
                    sb.AppendLine($"  - Location: {location}");
                    sb.AppendLine($"  - Impact: {impact}, Confidence: {confidence}");
                    sb.AppendLine($"  - Check: {check}");
                    sb.AppendLine();
                }

                return sb.ToString().Trim();
            }
            catch (JsonException)
            {
                return ExtractErrorsBeforeTraceback(responseText);
            }
        }

        private string ExtractErrorsBeforeTraceback(string responseText)
        {
            var tracebackIndex = responseText.IndexOf("Traceback (most recent call last):");

            string textToProcess = tracebackIndex == -1 ? responseText : responseText.Substring(0, tracebackIndex);

            var errorStartIndex = textToProcess.IndexOf("Error:");

            if (errorStartIndex == -1)
            {
                return "No compilation errors found.";
            }

            return textToProcess.Substring(errorStartIndex).Trim();
        }
    }
}