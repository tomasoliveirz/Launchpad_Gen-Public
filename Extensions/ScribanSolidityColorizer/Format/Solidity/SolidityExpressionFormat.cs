using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.expression")]
    [Name("scriban-solidity.solidity.expression")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanSolidityExpressionFormat : ClassificationFormatDefinition
    {
        public ScribanSolidityExpressionFormat()
        {
            DisplayName = "Scriban Solidity Keyword";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#3580BB");
        }
    }
}
