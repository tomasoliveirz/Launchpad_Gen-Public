using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;
namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.visibility")]
    [Name("scriban-solidity.solidity.visibility")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class SolidityVisibilityFormat : ClassificationFormatDefinition
    {
        public SolidityVisibilityFormat()
        {
            DisplayName = "Scriban Solidity Visibility Modifier";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#03520f");
        }
    }
}
