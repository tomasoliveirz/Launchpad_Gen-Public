using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scribans.control")]
    [Name("scriban-solidity.scribans.control")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanControlFormat : ClassificationFormatDefinition
    {
        public ScribanControlFormat()
        {
            DisplayName = "Scriban Control Block";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#FCF5C7");
            IsBold = true;
        }
    }
}