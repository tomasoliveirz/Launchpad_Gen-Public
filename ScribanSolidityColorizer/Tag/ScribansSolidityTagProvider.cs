using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace ScribanSolidityColorizer.Tag
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("scribans-solidity")]
    [TagType(typeof(ScribanSolidityTag))]
    internal sealed class ScribanSolidityTagProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new ScribanSolidityTokenTagger(buffer) as ITagger<T>;
        }
    }
}
