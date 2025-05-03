using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scriban.wrapper")]
    [Name("scriban-solidity.scriban.wrapper")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanWrapperFormat : ClassificationFormatDefinition
    {
        public ScribanWrapperFormat()
        {
            DisplayName = "Scriban Control Block";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#FCF5C7");
            IsBold = true;
        }
    }
}