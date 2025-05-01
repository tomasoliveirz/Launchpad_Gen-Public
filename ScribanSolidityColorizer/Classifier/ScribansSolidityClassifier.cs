using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                [ScribanSolidityTokenTypes.SolidityKeyword] = typeService.GetClassificationType("scriban-solidity.solidity.keyword"),
                [ScribanSolidityTokenTypes.SolidityDataType] = typeService.GetClassificationType("scriban-solidity.solidity.data-type"),
                [ScribanSolidityTokenTypes.SolidityComment] = typeService.GetClassificationType("scriban-solidity.solidity.comment"),
                [ScribanSolidityTokenTypes.SolidityStringLiteral] = typeService.GetClassificationType("scriban-solidity.solidity.string-literal"),
                [ScribanSolidityTokenTypes.SolidityNumberLiteral] = typeService.GetClassificationType("scriban-solidity.solidity.number-literal"),
                [ScribanSolidityTokenTypes.SolidityValue] = typeService.GetClassificationType("scriban-solidity.solidity.value"),
                [ScribanSolidityTokenTypes.SolidityVisibility] = typeService.GetClassificationType("scriban-solidity.solidity.visibility"),
                [ScribanSolidityTokenTypes.ScribanExpression] = typeService.GetClassificationType("scriban-solidity.scribans.expression"),
                [ScribanSolidityTokenTypes.ScribanControl] = typeService.GetClassificationType("scriban-solidity.scribans.control"),
                [ScribanSolidityTokenTypes.ScribanOperator] = typeService.GetClassificationType("scriban-solidity.scribans.operator"),
                [ScribanSolidityTokenTypes.ScribanComment] = typeService.GetClassificationType("scriban-solidity.scribans.comment"),
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
