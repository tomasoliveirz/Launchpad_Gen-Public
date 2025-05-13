using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.number")]
    [Name("scriban-solidity.solidity.number")]
    [UserVisible(true)]
    [Order(Before = Priority.Low)]
    internal sealed class ScribanSolidityNumberLiteralFormat : ClassificationFormatDefinition
    {
        public ScribanSolidityNumberLiteralFormat()
        {
            DisplayName = "Scriban Solidity Number Literal";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#f08ff7");

        }
    }
}
