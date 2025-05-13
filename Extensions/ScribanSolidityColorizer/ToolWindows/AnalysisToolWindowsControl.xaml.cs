using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ScribanSolidityColorizer.ToolWindows
{
    /// <summary>
    /// Interaction logic for AnalysisToolWindowsControl.
    /// </summary>
    public partial class AnalysisToolWindowsControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisToolWindowsControl"/> class.
        /// </summary>
        public AnalysisToolWindowsControl()
        {
            this.InitializeComponent();
            ObservableCollection = new ObservableCollection<string>();
            this.DataContext = this; 
        }

        public ObservableCollection<string> ObservableCollection { get; set; }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "Code Analysis");
        }
        public void SetAnalysisText(string chatGptJson)
        {
            ObservableCollection.Clear();
            MainAnalysis.Text = "";
            AnalysisTitle.Text = "";
            if (string.IsNullOrWhiteSpace(chatGptJson))
                return;

            try
            {
                var root = JObject.Parse(chatGptJson);

                string typeOfCode = root["typeofcode"]?.ToString();
                string language = root["language"]?.ToString();
                AnalysisTitle.Text = $"{typeOfCode} ({language})";
                var features = root["features"]?.Select(f => f.ToString()).ToList() ?? new List<string>();
                var insightsToken = root["insights"];

                var insights = new List<string>();

                if (insightsToken != null)
                {
                    if (insightsToken.Type == JTokenType.Array)
                    {
                        insights = insightsToken.Select(i => i.ToString()).ToList();
                    }
                    else if (insightsToken.Type == JTokenType.String)
                    {
                        var raw = insightsToken.ToString();
                        insights = raw.Split(new[] { '\n', ';' }, System.StringSplitOptions.RemoveEmptyEntries)
                                      .Select(s => s.Trim())
                                      .ToList();
                    }
                }

                var summary = "";
                foreach(var feature in features.Where(x => !string.IsNullOrEmpty(x)))
                {
                    summary += "- " + feature + (features.Last()==feature ? "\n":"\n\n");
                }

                MainAnalysis.Text = summary;

                var nonEmptyInsights = insights.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

                if (nonEmptyInsights.Any())
                {
                    foreach (var insight in nonEmptyInsights)
                    {
                        ObservableCollection.Add("• " + insight+"\n");
                    }
                }
            }
            catch (JsonException ex)
            {
                MainAnalysis.Text = "Failed to parse analysis response.\n\n" + ex.Message;
            }
        }

        public void ShowLoading()
        {
            AnalysisTitle.Text = "Loading analysis, please wait...";
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            var text = new List<string>() { MainAnalysis.Text };
            text.AddRange(ObservableCollection);

            Clipboard.SetText(string.Join(";", text));
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MainAnalysis.Text = "";
        }

    }
}