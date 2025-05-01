using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scribans.operator")]
    [Name("scriban-solidity.scribans.operator")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanOperatorFormat : ClassificationFormatDefinition
    {
        public ScribanOperatorFormat()
        {
            DisplayName = "Scriban Operator";
            ForegroundColor = Colors.OrangeRed;
        }
    }
}
