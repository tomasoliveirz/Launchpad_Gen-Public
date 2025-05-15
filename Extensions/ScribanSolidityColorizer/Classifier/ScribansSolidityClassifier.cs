using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using ScribanSolidityColorizer.Enums;
using ScribanSolidityColorizer.Tag;

namespace ScribanSolidityColorizer.Classifier
{
    internal sealed class ScribanSolidityClassifier : ITagger<ClassificationTag>
    {
        readonly ITextBuffer _buffer;
        readonly ITagAggregator<ScribanSolidityTag> _aggregator;
        readonly IDictionary<ScribanSolidityTokenTypes, IClassificationType> _scribansSolidityTypes;

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        internal ScribanSolidityClassifier(ITextBuffer buffer,
                               ITagAggregator<ScribanSolidityTag> ookTagAggregator,
                               IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = ookTagAggregator;
            _scribansSolidityTypes = new Dictionary<ScribanSolidityTokenTypes, IClassificationType>
            {
                [ScribanSolidityTokenTypes.ScribanComment] = typeService.GetClassificationType("scriban-solidity.scriban.comment"),
                [ScribanSolidityTokenTypes.ScribanExpression] = typeService.GetClassificationType("scriban-solidity.scriban.expression"),
                [ScribanSolidityTokenTypes.ScribanNumber] = typeService.GetClassificationType("scriban-solidity.scriban.number"),
                [ScribanSolidityTokenTypes.ScribanOperator] = typeService.GetClassificationType("scriban-solidity.scriban.operator"),
                [ScribanSolidityTokenTypes.ScribanString] = typeService.GetClassificationType("scriban-solidity.scriban.string"),
                [ScribanSolidityTokenTypes.ScribanWrapper] = typeService.GetClassificationType("scriban-solidity.scriban.wrapper"),

                [ScribanSolidityTokenTypes.SolidityComment] = typeService.GetClassificationType("scriban-solidity.solidity.comment"),
                [ScribanSolidityTokenTypes.SolidityDataType] = typeService.GetClassificationType("scriban-solidity.solidity.data-type"),
                [ScribanSolidityTokenTypes.SolidityExpression] = typeService.GetClassificationType("scriban-solidity.solidity.expression"),
                [ScribanSolidityTokenTypes.SolidityNumber] = typeService.GetClassificationType("scriban-solidity.solidity.number"),
                [ScribanSolidityTokenTypes.SolidityString] = typeService.GetClassificationType("scriban-solidity.solidity.string"),
                [ScribanSolidityTokenTypes.SolidityVisibility] = typeService.GetClassificationType("scriban-solidity.solidity.visibility"),
                [ScribanSolidityTokenTypes.SolidityOperator] = typeService.GetClassificationType("scriban-solidity.solidity.operator"),
            };
        }

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var tagSpan in _aggregator.GetTags(spans))
            {
                var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
                yield return
                    new TagSpan<ClassificationTag>(tagSpans[0],
                                                   new ClassificationTag(_scribansSolidityTypes[tagSpan.Tag.Type]));
            }
        }
    }
}
