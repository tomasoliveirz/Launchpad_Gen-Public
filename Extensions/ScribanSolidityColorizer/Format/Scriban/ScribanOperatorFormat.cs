using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scriban.operator")]
    [Name("scriban-solidity.scriban.operator")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanOperatorFormat : ClassificationFormatDefinition
    {
        public ScribanOperatorFormat()
        {
            DisplayName = "Scriban Operator";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#68aedb");
        }
    }
}
