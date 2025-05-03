using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.instance")]
    [Name("scriban-solidity.solidity.instance")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class SolidityInstanceFormat : ClassificationFormatDefinition
    {
        public SolidityInstanceFormat()
        {
            DisplayName = "Solidity Instance";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#2B247C");
        }
    }
}

