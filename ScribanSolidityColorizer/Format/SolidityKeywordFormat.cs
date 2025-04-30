using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.solidity.keyword")]
    [Name("scriban-solidity.solidity.keyword")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanSolidityKeywordFormat : ClassificationFormatDefinition
    {
        public ScribanSolidityKeywordFormat()
        {
            DisplayName = "Scriban Solidity Keyword";
            ForegroundColor = Colors.DodgerBlue;
            IsBold = true;
        }
    }
}
