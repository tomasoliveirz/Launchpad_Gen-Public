using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format.Scriban
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity.scriban.number")]
    [Name("scriban-solidity.scriban.number")]
    [UserVisible(true)]
    [Order(Before = Priority.Low)]
    internal sealed class ScribanNumberLiteralFormat : ClassificationFormatDefinition
    {
        public ScribanNumberLiteralFormat()
        {
            DisplayName = "Scriban  Number Literal";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#c408d1");

        }
    }
}
