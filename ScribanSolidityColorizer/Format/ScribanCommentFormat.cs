using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scribans.comment")]
    [Name("scriban-solidity.scribans.comment")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanCommentFormat : ClassificationFormatDefinition
    {
        public ScribanCommentFormat()
        {
            DisplayName = "Scriban Comment";
            ForegroundColor = Colors.OrangeRed;
        }
    }
}

