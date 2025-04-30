using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.number-literal")]
    [Name("scriban-solidity.solidity.number-literal")]
    [UserVisible(true)]
    [Order(Before = Priority.Low)]
    internal sealed class ScribanSolidityNumberLiteralFormat : ClassificationFormatDefinition
    {
        public ScribanSolidityNumberLiteralFormat()
        {
            DisplayName = "Scriban Solidity Number Literal";
            ForegroundColor = Colors.OliveDrab;
        }
    }
}
