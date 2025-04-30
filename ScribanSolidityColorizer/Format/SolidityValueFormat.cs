using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;
namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.value")]
    [Name("scriban-solidity.solidity.value")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class SolidityValueFormat : ClassificationFormatDefinition
    {
        public SolidityValueFormat()
        {
            DisplayName = "Scriban Solidity Value";
            ForegroundColor = Colors.Teal;
        }
    }
}
