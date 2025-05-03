using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using ScribanSolidityColorizer.Enums;
using ScribanSolidityColorizer.Expressions;
using ScribanSolidityColorizer.Helpers;
using ScribanSoliditySyntaxHighlighter.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace ScribanSolidityColorizer.Tag
{
    internal class ScribanSolidityTokenTagger : ITagger<ScribanSolidityTag>
    {

        ITextBuffer _buffer;
        IDictionary<string, ScribanSolidityTokenTypes> _tokenTypes;
        Dictionary<ScribanSolidityTokenTypes, string[]> _scribanExpressions;
        Dictionary<ScribanSolidityTokenTypes, string[]> _solidityExpressions;

        internal ScribanSolidityTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            var solidityExpressions = ExpressionsHelper.GroupedExpressions(typeof(SolidityExpressions)); 
            _scribanExpressions = ExpressionsHelper.GroupedExpressions(typeof(ScribanExpressions));
            _solidityExpressions = ExpressionsHelper.GroupedExpressions(typeof(SolidityExpressions));
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }
        private bool IsEnteringScriban(char c, char next) => (c == '{' && next == '{');
        private bool StartHasSpaceRemoval(char peek) => (peek == '-' || peek == '~');
        private bool IsLeavingScriban(char c, char next) => (c == '}' && next == '}');
        private bool IsLeavingScribanAndRemovingSpaces(char c, char next, char peek) => ((c == '-' || c=='~') && peek == '}' && next == '}');

        public IEnumerable<ITagSpan<ScribanSolidityTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var span in spans)
            {
                string text = span.GetText();
                var snapshot = span.Snapshot;

                bool inScriban = false;
                int tokenStart = 0;
                var buffer = new StringBuilder();

                for (int i = 0; i < text.Length;)
                {
                    char curr = text[i];
                    char next = i + 1 < text.Length ? text[i + 1] : '\0';
                    char peek = i + 2 < text.Length ? text[i + 2] : '\0';

                    if (!inScriban && IsEnteringScriban(curr, next))
                    {
                        var solidityChunk = buffer.ToString();
                        foreach (var tag in ParseSolidityBuffer(
                                     solidityChunk,
                                     span.Start.Position + tokenStart,
                                     snapshot))
                            yield return tag;

                        bool hasMarker = peek == '-' || peek == '~';
                        int length = 2 + (hasMarker ? 1 : 0);
                        string tok = text.Substring(i, length);
                        yield return MakeTag(
                            ScribanSolidityTokenTypes.ScribanWrapper,
                            span.Start.Position + i,
                            length, tok, snapshot);

                        i += length;
                        tokenStart = i;
                        inScriban = true;
                        buffer.Clear();
                        continue;
                    }

                    if (inScriban && IsLeavingScriban(curr, next))
                    {
                        var scribanChunk = buffer.ToString();
                        foreach (var tag in ParseScribanBuffer(
                                     scribanChunk,
                                     span.Start.Position + tokenStart,
                                     snapshot))
                            yield return tag;

                        int length = 2;
                        string tok = text.Substring(i, length);
                        yield return MakeTag(
                            ScribanSolidityTokenTypes.ScribanWrapper,
                            span.Start.Position + i,
                            length, tok, snapshot);

                        i += length;
                        tokenStart = i;
                        inScriban = false;
                        buffer.Clear();
                        continue;
                    }

                    if (inScriban && IsLeavingScribanAndRemovingSpaces(curr, next, peek))
                    {
                        var scribanChunk = buffer.ToString();
                        foreach (var tag in ParseScribanBuffer(
                                     scribanChunk,
                                     span.Start.Position + tokenStart,
                                     snapshot))
                            yield return tag;

                        int length = 3;
                        string tok = text.Substring(i, length);
                        yield return MakeTag(
                            ScribanSolidityTokenTypes.ScribanWrapper,
                            span.Start.Position + i,
                            length, tok, snapshot);

                        i += length;
                        tokenStart = i;
                        inScriban = false;
                        buffer.Clear();
                        continue;
                    }

                    buffer.Append(curr);
                    i++;
                }

                var tail = buffer.ToString();
                if (tail.Length > 0)
                {
                    var flushTags = inScriban
                        ? ParseScribanBuffer(tail, span.Start.Position + tokenStart, snapshot)
                        : ParseSolidityBuffer(tail, span.Start.Position + tokenStart, snapshot);

                    foreach (var t in flushTags)
                        yield return t;
                }
            }
        }

        private IEnumerable<ITagSpan<ScribanSolidityTag>> ParseEntry(string text, StringBuilder buffer, int cursor, int peekModifer, int tokenLength, int tokenStart, ITextSnapshot snapshot)
        {
            var txt = buffer.ToString();
            foreach (var tag in ParseSolidityBuffer(txt, tokenStart, snapshot)) yield return tag;
            buffer.Clear();
            string token = text.Substring(cursor, tokenLength);
            yield return MakeTag(ScribanSolidityTokenTypes.ScribanWrapper, cursor, tokenLength, token, snapshot);
        }

        private IEnumerable<ITagSpan<ScribanSolidityTag>> ParseExit(string text, StringBuilder buffer, int cursor, int peekModifer, int tokenLength, int tokenStart, ITextSnapshot snapshot)
        {
            var txt = buffer.ToString();
            foreach (var tag in ParseScribanBuffer(txt, tokenStart, snapshot)) yield return tag;
            buffer.Clear();
            string token = text.Substring(cursor, tokenLength+peekModifer);
            yield return MakeTag(ScribanSolidityTokenTypes.ScribanWrapper, cursor, tokenLength, token, snapshot);
        }


        private IEnumerable<ITagSpan<ScribanSolidityTag>> ParseScribanBuffer(
    string text,
    int absoluteStart,
    ITextSnapshot snapshot)
        {
            foreach (var (s, len) in StringLiteralFinder.Find(text)) yield return MakeTag(ScribanSolidityTokenTypes.ScribanString, absoluteStart + s, len, text.Substring(s, len), snapshot);
            foreach (var (s, len) in NumberLiteralFinder.Find(text)) yield return MakeTag(ScribanSolidityTokenTypes.ScribanNumber, absoluteStart + s, len, text.Substring(s, len), snapshot);
            foreach(var expression in _scribanExpressions)
            {
                foreach(var word in expression.Value)
                {
                    foreach (var pos in WholeWordFinder.Find(text, word))
                    {
                        yield return MakeTag(expression.Key, absoluteStart + pos, word.Length, word, snapshot);
                    }
                }
            }

          

            //// 3) scriban keywords (for, in, if, end)
            //foreach (var kw in _scribanExpressions)
            //{
            //    foreach(var word in kw.Value)
            //    {
            //        foreach (var idx in WholeWordFinder.Find(text, word))
            //            yield return MakeTag(
            //                ScribanSolidityTokenTypes.ScribanControl,
            //                absoluteStart + idx,
            //                word.Length,
            //                word,
            //                snapshot);
            //    }
            //}

            //// 4) everything else
            //if (text.Length > 0)
            //    yield return MakeTag(
            //        ScribanSolidityTokenTypes.ScribanExpression,
            //        absoluteStart,
            //        text.Length,
            //        text,
            //        snapshot);
        }

        private IEnumerable<ITagSpan<ScribanSolidityTag>> ParseSolidityBuffer(
    string text,
    int absoluteStart,
    ITextSnapshot snapshot)
        {
            foreach (var (s, len) in StringLiteralFinder.Find(text)) yield return MakeTag(ScribanSolidityTokenTypes.SolidityString, absoluteStart + s, len, text.Substring(s, len), snapshot);
            foreach (var (s, len) in NumberLiteralFinder.Find(text)) yield return MakeTag(ScribanSolidityTokenTypes.SolidityNumber, absoluteStart + s, len, text.Substring(s, len), snapshot);
            foreach (var expression in _solidityExpressions)
            {
                foreach (var word in expression.Value)
                {
                    foreach (var pos in WholeWordFinder.Find(text, word))
                    {
                        yield return MakeTag(expression.Key, absoluteStart + pos, word.Length, word, snapshot);
                    }
                }
            }


            

            //// 4) everything else  
            ////    any region not yet covered you can treat as a single Value span:
            //if (text.Length > 0)
            //    yield return MakeTag(
            //        ScribanSolidityTokenTypes.SolidityValue,
            //        absoluteStart,
            //        text.Length,
            //        text,
            //        snapshot);
        }

        private TagSpan<ScribanSolidityTag> MakeTag(ScribanSolidityTokenTypes kind, int absoluteStart, int length, string text,ITextSnapshot snapshot)
        {
            var span = new SnapshotSpan(snapshot, new Span(absoluteStart, length));
            return new TagSpan<ScribanSolidityTag>(span, new ScribanSolidityTag(kind));
        }
    }
}
