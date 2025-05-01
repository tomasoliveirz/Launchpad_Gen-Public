using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Media;
using Microsoft.VisualStudio.OLE.Interop;
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

        private bool IsEnteringScriban(bool inScriban, char curr, char next, char peek2) =>
                        !inScriban && curr == '{' && ((next == '{' && (peek2 == '-' || peek2 == '~'))  || 
                        (next == '{')) || (curr == '{' && next == '%' && (peek2 == '-' || peek2 == '~') || 
                        next == '%');                                    

        private bool IsLeavingScriban(bool inScriban, bool isControl, char curr, char next, char peek2) =>
                        inScriban && ((curr == '-' || curr == '~') && next == (isControl ? '%' : '}') && 
                        peek2 == (isControl ? '}' : '}')) || (curr == (isControl ? '%' : '}') && next == (isControl ? '}' : '}'));

        public IEnumerable<ITagSpan<ScribanSolidityTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (SnapshotSpan span in spans)
            {
                var text = span.GetText();
                var snapshot = span.Snapshot;

                bool inScriban = false;
                bool isControl = false;

                int tokenStart = 0;

                var buffer = new StringBuilder();

                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    char next = i + 1 < text.Length ? text[i + 1] : '\0';
                    char peek2 = i+2 < text.Length ? text[i+2] : '\0';

                    if (IsEnteringScriban(inScriban, c, next, peek2))
                    {
                        var flush = Flush(buffer, span.Start.Position + tokenStart, snapshot);
                        if(flush != null) yield return flush;
                        var kind = next == '%'
                            ? ScribanSolidityTokenTypes.ScribanControl
                            : ScribanSolidityTokenTypes.ScribanExpression;

                        var length = 2;
                        var tokenText = $"{c}{next}";
                        if (peek2 == '-')
                        {
                            length++;
                            tokenText += peek2;
                        }
                        yield return MakeTag(kind, span.Start.Position + i, length, tokenText, snapshot);

                        inScriban = true;
                        isControl = next == '%';
                        i++;
                        tokenStart = i + 1; 
                        if(peek2 == '-')
                        {
                            i++;
                            tokenStart++;
                        }
                        buffer.Clear();
                    }
                    else if (IsLeavingScriban(inScriban, isControl, c, next, peek2))
                    {
                        var kind = isControl
                   ? ScribanSolidityTokenTypes.ScribanControl
                   : ScribanSolidityTokenTypes.ScribanExpression;
                        var length = 2;
                        var tokenText = $"{c}{next}";
                        if (c == '-') {
                            length++;
                            tokenText += peek2;
                        }
                        yield return MakeTag(kind, span.Start.Position + i, length, tokenText, snapshot);

                        inScriban = false;
                        if (c == '-')
                        {
                            i++;
                            tokenStart++;
                        }
                        buffer.Clear();
                    }
                    else
                    {
                        buffer.Append(c);
                    }
                }
                if (buffer.Length > 0)
                {
                    if (inScriban)
                    {
                        var kind = isControl
                            ? ScribanSolidityTokenTypes.ScribanControl
                            : ScribanSolidityTokenTypes.ScribanExpression;
                        yield return MakeTag(kind,
                            span.Start.Position + tokenStart,
                            buffer.Length,
                            buffer.ToString(),
                            snapshot);
                    }
                    else
                    {
                        // flush as solidity
                        var flush = Flush(buffer, span.Start.Position + tokenStart, snapshot);
                        if (flush != null) yield return flush;
                    }
                }
            }
        }

        private TagSpan<ScribanSolidityTag> Flush(StringBuilder buffer, int absoluteStart,ITextSnapshot snapshot)
        {
            if (buffer.Length == 0) return null;
            var bufferLength = buffer.Length;
            var bufferString = buffer.ToString();
            buffer.Clear();
            return MakeTag(
                ScribanSolidityTokenTypes.SolidityValue,   // or whatever enum value you use for solidity text
                absoluteStart,
                bufferLength,
                bufferString,
                snapshot);
        }

        private TagSpan<ScribanSolidityTag> MakeTag(ScribanSolidityTokenTypes kind, int absoluteStart, int length, string text,ITextSnapshot snapshot)
        {
            var span = new SnapshotSpan(snapshot, new Span(absoluteStart, length));
            return new TagSpan<ScribanSolidityTag>(span, new ScribanSolidityTag(kind));
        }
    }
}
