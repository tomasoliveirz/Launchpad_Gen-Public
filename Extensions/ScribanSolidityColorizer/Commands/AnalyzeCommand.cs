using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;
using ScribanSolidityColorizer.ToolWindows;
using ScribanSolidityColorizer.Pages;

namespace ScribanSolidityColorizer.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AnalyzeCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4129;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("e9961db3-8eea-4d32-b73f-30d171ff1cef");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        private AnalyzeCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static AnalyzeCommand Instance { get; private set; }

        private IServiceProvider ServiceProvider => this.package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new AnalyzeCommand(package, commandService);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = "<Pending>")]
        private async void Execute(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var dte = await package.GetServiceAsync(typeof(DTE)) as DTE2;
            var doc = dte?.ActiveDocument;
            if (doc == null)
            {
                VsShellUtilities.ShowMessageBox(ServiceProvider, "No active document.", "Analyze Command",
                    OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            var textDoc = (TextDocument)doc.Object("TextDocument");
            var startPoint = textDoc.StartPoint.CreateEditPoint();
            string fileContent = startPoint.GetText(textDoc.EndPoint);

            string apiKey = GetApiKey();
            string responseText;
            try
            {
                responseText = await CallChatGptApiAsync(fileContent, apiKey);
            }
            catch (Exception ex)
            {
                VsShellUtilities.ShowMessageBox(ServiceProvider, $"Error calling ChatGPT: {ex.Message}", "Analyze Command",
                    OLEMSGICON.OLEMSGICON_CRITICAL, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            ShowAnalysisWindow(responseText);
        }

        private string GetApiKey()
        {
            var optionsPage = (OptionsPage)package.GetDialogPage(typeof(OptionsPage));
            string apiKey = optionsPage.GPT4Key;

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                VsShellUtilities.ShowMessageBox(ServiceProvider, "Please set your API key in Tools → Options → ChatGPT Extension → General.",
                    "API Key Missing",
                    OLEMSGICON.OLEMSGICON_WARNING,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return "";
            }
            return apiKey;
        }

        private async Task<string> CallChatGptApiAsync(string fileContent, string apiKey)
        {
            var window = await package.ShowToolWindowAsync(typeof(AnalysisToolWindows), 0, true, package.DisposalToken);
            var control = (AnalysisToolWindowsControl)((ToolWindowPane)window).Content;
            control.ShowLoading();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                new { role = "system", content = "Please analyze the following code and provide insights, improvements, and potential issues.\r\n\r\nReturn your response strictly in the following JSON format:\r\n\r\n{\r\n  \"typeofcode\": \"<brief description of the type of code (e.g., smart contract, API controller, machine learning model)>\",\r\n  \"language\": \"<programming language>\",\r\n  \"features\": [\"<list of key features or components in the code>\"],\r\n  \"insights\": [\"<list of analysis points, improvements, or potential issues>\"]\r\n}\r\n\r\nDo not include any additional explanation or text outside of the JSON." },
                new { role = "user", content = fileContent }
            }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            response.EnsureSuccessStatusCode();

            var resultJson = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(resultJson);
            string chatResponse = result.choices[0].message.content;

            return chatResponse;
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = "<Pending>")]
        private async void ShowAnalysisWindow(string analysisText)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var window = await package.ShowToolWindowAsync(typeof(AnalysisToolWindows), 0, true, package.DisposalToken);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create Analysis tool window.");
            }


            window.Caption = "Code Analysis with GPT-4";
            var control = (AnalysisToolWindowsControl)((ToolWindowPane)window).Content;
            control.SetAnalysisText(analysisText);
        }
    }
}
