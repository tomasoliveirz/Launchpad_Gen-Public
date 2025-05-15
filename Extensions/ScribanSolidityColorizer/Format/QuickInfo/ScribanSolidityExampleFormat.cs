using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format.QuickInfo
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity-example")]
    [Name("scriban-solidity-example")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanSolidityExampleFormat : ClassificationFormatDefinition
    {
        public ScribanSolidityExampleFormat()
        {
            DisplayName = "Scriban Solidity Example";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#e3dcd1");
            BackgroundColor = (Color)ColorConverter.ConvertFromString("#333333");
            IsItalic = true;
        }
    }
}
