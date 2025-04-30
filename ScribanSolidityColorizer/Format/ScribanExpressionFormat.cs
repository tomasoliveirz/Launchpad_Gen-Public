using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scribans.expression")]
    [Name("scriban-solidity.scribans.expression")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanExpressionFormat : ClassificationFormatDefinition
    {
        public ScribanExpressionFormat()
        {
            DisplayName = "Scriban Expression";
            ForegroundColor = Colors.OrangeRed;
        }
    }
}
