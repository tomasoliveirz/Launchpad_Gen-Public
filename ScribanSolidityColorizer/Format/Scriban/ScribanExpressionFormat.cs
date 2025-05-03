using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scriban.expression")]
    [Name("scriban-solidity.scriban.expression")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanExpressionFormat : ClassificationFormatDefinition
    {
        public ScribanExpressionFormat()
        {
            DisplayName = "Scriban Expression";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#686adb");
        }
    }
}
