using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using ScribanSolidityColorizer.Enums;
using ScribanSolidityColorizer.Expressions;
using ScribanSolidityColorizer.Helpers;
using ScribanSoliditySyntaxHighlighter.Helpers;

namespace ScribanSolidityColorizer.Tag
{
    internal class ScribanSolidityTokenTagger : ITagger<ScribanSolidityTag>
    {

        ITextBuffer _buffer;
        IDictionary<string, ScribanSolidityTokenTypes> _tokenTypes;
        private readonly Dictionary<ScribanSolidityTokenTypes, string[]> GroupedExpressions;

        internal ScribanSolidityTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            var solidityExpressions = ExpressionsHelper.GroupedExpressions(typeof(SolidityExpressions)); 
            var scribanExpressions = ExpressionsHelper.GroupedExpressions(typeof(ScribanExpressions));
            GroupedExpressions = new Dictionary<ScribanSolidityTokenTypes, string[]>();
            foreach(var solExpression in solidityExpressions) GroupedExpressions.Add(solExpression.Key, solExpression.Value);
            foreach(var scrExpression in scribanExpressions) GroupedExpressions.Add(scrExpression.Key, scrExpression.Value);

        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<ScribanSolidityTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (SnapshotSpan span in spans)
            {
                var text = span.GetText();

                

                #region Solidity string literals
                foreach (var (start, length) in StringLiteralFinder.Find(text))
                {
                    var stringSpan = new SnapshotSpan(span.Snapshot, new Span(span.Start.Position + start, length));
                    yield return new TagSpan<ScribanSolidityTag>(stringSpan, new ScribanSolidityTag(ScribanSolidityTokenTypes.SolidityStringLiteral));
                }
                #endregion

                #region Solidity number literals
                foreach (var (start, length) in NumberLiteralFinder.Find(text))
                {
                    var numberSpan = new SnapshotSpan(span.Snapshot, new Span(span.Start.Position + start, length));
                    yield return new TagSpan<ScribanSolidityTag>(numberSpan, new ScribanSolidityTag(ScribanSolidityTokenTypes.SolidityNumberLiteral));
                }
                #endregion

                #region Solidity comment blocks
                foreach (var (start, length) in CommentFinder.Find(text))
                {
                    var commentSpan = new SnapshotSpan(span.Snapshot, new Span(span.Start.Position + start, length));
                    yield return new TagSpan<ScribanSolidityTag>(commentSpan, new ScribanSolidityTag(ScribanSolidityTokenTypes.SolidityNumberLiteral));
                }

                foreach (var kvp in GroupedExpressions)
                {
                    var expressionType = kvp.Key;
                    var keywords = kvp.Value;

                    foreach (var keyword in keywords)
                    {
                        foreach (var match in WholeWordFinder.Find(text, keyword))
                        {
                            var keywordSpan = new SnapshotSpan(span.Snapshot, new Span(span.Start.Position + match, keyword.Length));
                            yield return new TagSpan<ScribanSolidityTag>(keywordSpan, new ScribanSolidityTag(expressionType));
                        }
                    }
                }
                #endregion
            }
        }
    }
}
