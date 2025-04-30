using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.comment")]
    [Name("scriban-solidity.comment")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class SolidityCommentFormat : ClassificationFormatDefinition
    {
        public SolidityCommentFormat()
        {
            DisplayName = "Scriban Solidity Comment";
            ForegroundColor = Colors.ForestGreen;
            IsItalic = true;
        }
    }
}