using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format.Solidity
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.operator")]
    [Name("scriban-solidity.solidity.operator")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class SolidityOperatorFormat : ClassificationFormatDefinition
    {
        public SolidityOperatorFormat()
        {
            DisplayName = "Scriban Solidity Operator";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#2d8e99");
        }
    }
}
