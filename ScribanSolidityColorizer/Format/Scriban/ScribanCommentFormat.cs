using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scriban.comment")]
    [Name("scriban-solidity.scriban.comment")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanCommentFormat : ClassificationFormatDefinition
    {
        public ScribanCommentFormat()
        {
            DisplayName = "Scriban Comment";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#e3dcd1");
        }
    }
}

