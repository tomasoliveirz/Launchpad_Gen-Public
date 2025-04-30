using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using ScribanSolidityColorizer.Tag;

namespace ScribanSolidityColorizer.Classifier
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("scribans-solidity")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class ScribanSolidityClassifierProvider : ITaggerProvider
    {
        [Export]
        [Name("scribans-solidity")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition ScribanSolidityContentType = null;

        [Export]
        [FileExtension(".solidity.scriban")]
        [ContentType("scribans-solidity")]
        internal static FileExtensionToContentTypeDefinition ScribanSolidityFileType = null;

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IBufferTagAggregatorFactoryService aggregatorFactory = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {

            ITagAggregator<ScribanSolidityTag> scribansSolidityTagAggregator =
                                            aggregatorFactory.CreateTagAggregator<ScribanSolidityTag>(buffer);

            return new ScribanSolidityClassifier(buffer, scribansSolidityTagAggregator, ClassificationTypeRegistry) as ITagger<T>;
        }
    }
}
