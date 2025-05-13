using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;
namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.data-type")]
    [Name("scriban-solidity.solidity.data-type")]
    [UserVisible(true)]
    [Order(Before = Priority.High)]
    internal sealed class SolidityDataTypeFormat : ClassificationFormatDefinition
    {
        public SolidityDataTypeFormat()
        {
            DisplayName = "Scriban Solidity Data Type";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#688bdb");

        }
    }
}
