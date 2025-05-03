using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanScribanColorizer.Format
{

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scriban.instance")]
    [Name("scriban-solidity.scriban.instance")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanInstanceFormat : ClassificationFormatDefinition
    {
        public ScribanInstanceFormat()
        {
            DisplayName = "Scriban Instance";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#bfff00");
        }
    }
}

