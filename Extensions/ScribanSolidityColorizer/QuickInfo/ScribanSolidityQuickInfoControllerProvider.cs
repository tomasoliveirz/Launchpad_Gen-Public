using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace ScribanSolidityColorizer.QuickInfo
{
    [Export(typeof(IIntellisenseControllerProvider))]
    [Name("ToolTip QuickInfo Controller")]
    [ContentType("scribans-solidity")]
    internal class ScribanSolidityQuickInfoControllerProvider : IIntellisenseControllerProvider
    {

        [Import]
        [System.Obsolete]
        internal IQuickInfoBroker QuickInfoBroker { get; set; }

        [System.Obsolete]
        public IIntellisenseController TryCreateIntellisenseController(ITextView textView, IList<ITextBuffer> subjectBuffers)
        {
            return new ScribanSolidityQuickInfoController(textView, subjectBuffers, this);
        }

    }
}
