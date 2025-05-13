using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace ScribanSolidityColorizer.QuickInfo
{
    [Export(typeof(IQuickInfoSourceProvider))]
    [Name("ToolTip QuickInfo Source")]
    [Order(Before = "Default Quick Info Presenter")]
    [ContentType("scribans-solidity")]
    [Obsolete]
    internal class ScribanSolidityQuickInfoSourceProvider : IQuickInfoSourceProvider
    {

        [Export]
        [FileExtension(".solidity.scriban")]
        [ContentType("scribans-solidity")]
        internal static FileExtensionToContentTypeDefinition ScribanSolidityFileType = null;

        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        [Import]
        internal ITextBufferFactoryService TextBufferFactoryService { get; set; }

        public IQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
        {
            return new ScribanSolidityQuickInfoSource(this, textBuffer);
        }
    }
}
