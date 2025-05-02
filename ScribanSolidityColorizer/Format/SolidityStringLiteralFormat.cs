using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.string-literal")]
    [Name("scriban-solidity.solidity.string-literal")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class SolidityStringLiteralFormat : ClassificationFormatDefinition
    {
        public SolidityStringLiteralFormat()
        {
            DisplayName = "Scriban Solidity String Literal";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#FFA500");

        }
    }
}
