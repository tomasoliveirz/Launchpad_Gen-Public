using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace ScribanSolidityColorizer.Pages
{
    public class OptionsPage : DialogPage
    {
        [Category("API Keys")]
        [DisplayName("OpenAI Key (General)")]
        [Description("The general OpenAI API key.")]
        public string OpenAIKey { get; set; } = "";

        [Category("API Keys")]
        [DisplayName("OpenAI GPT-3.5 Key")]
        [Description("The API key specifically for GPT-3.5.")]
        public string GPT35Key { get; set; } = "";

        [Category("API Keys")]
        [DisplayName("OpenAI GPT-4 Key")]
        [Description("The API key specifically for GPT-4.")]
        public string GPT4Key { get; set; } = "";

        [Category("API Keys")]
        [DisplayName("Anthropic Claude Key")]
        [Description("The API key for Claude.")]
        public string ClaudeKey { get; set; } = "";

        [Category("API Keys")]
        [DisplayName("Google Gemini Key")]
        [Description("The API key for Google Gemini.")]
        public string GeminiKey { get; set; } = "";
    }
}
