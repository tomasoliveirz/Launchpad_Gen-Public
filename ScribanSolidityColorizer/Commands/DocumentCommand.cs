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
using ScribanSolidityColorizer.Pages;

namespace ScribanSolidityColorizer.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class DocumentCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("e9961db3-8eea-4d32-b73f-30d171ff1cef");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        private DocumentCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static DocumentCommand Instance { get; private set; }

        private IServiceProvider ServiceProvider => this.package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new DocumentCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        private async void Execute(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var dte = await package.GetServiceAsync(typeof(DTE)) as DTE2;
            var doc = dte?.ActiveDocument;
            if (doc == null)
            {
                VsShellUtilities.ShowMessageBox(ServiceProvider, "No active document.", "Document Command",
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
                VsShellUtilities.ShowMessageBox(ServiceProvider, $"Error calling ChatGPT: {ex.Message}", "Document Command",
                    OLEMSGICON.OLEMSGICON_CRITICAL, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }
            responseText = CleanResponse(responseText);
            startPoint.Delete(textDoc.EndPoint);
            startPoint.Insert(responseText);
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

        private string CleanResponse(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
                return response;

            // Remove ```solidity or ``` and ```
            response = response.Trim();

            if (response.StartsWith("```"))
            {
                int firstLineEnd = response.IndexOf('\n');
                if (firstLineEnd >= 0)
                    response = response.Substring(firstLineEnd + 1);
            }

            if (response.EndsWith("```"))
            {
                int lastFence = response.LastIndexOf("```");
                if (lastFence >= 0)
                    response = response.Substring(0, lastFence).TrimEnd();
            }

            return response;
        }

        private async Task<string> CallChatGptApiAsync(string fileContent, string apiKey)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                new { role = "system", content = "Please generate clear inline documentation for the functions on following code:" },
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
    }
}
