using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scriban.string")]
    [Name("scriban-solidity.solidity.string")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanStringLiteralFormat : ClassificationFormatDefinition
    {
        public ScribanStringLiteralFormat()
        {
            DisplayName = "Scriban Solidity String Literal";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#FFA500");

        }
    }
}
