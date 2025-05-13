using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ScribanSolidityColorizer.Format.QuickInfo
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "scriban-solidity-description")]
    [Name("scriban-solidity-description")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class ScribanSolidityDescriptionFormat : ClassificationFormatDefinition
    {
        public ScribanSolidityDescriptionFormat()
        {
            DisplayName = "Scriban Solidity Description";
            ForegroundColor = (Color)ColorConverter.ConvertFromString("#e3dcd1");
        }
    }
}
