using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.string")]
    [Name("scriban-solidity.scroban.string")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanSolidityStringLiteralFormat : ClassificationFormatDefinition
    {
        public ScribanSolidityStringLiteralFormat()
        {
            DisplayName = "Scriban String Literal";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#ffd900");

        }
    }
}
