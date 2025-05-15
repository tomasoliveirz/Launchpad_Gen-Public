using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Operations;
using ScribanSolidityColorizer.Attributes;
using ScribanSolidityColorizer.Enums;
using ScribanSolidityColorizer.Expressions;
using ScribanSoliditySyntaxHighlighter.Helpers;

namespace ScribanSolidityColorizer.QuickInfo
{
    [Obsolete]
    internal class ScribanSolidityQuickInfoSource : IQuickInfoSource
    {
        private readonly ScribanSolidityQuickInfoSourceProvider m_provider;
        private readonly ITextBuffer m_subjectBuffer;
        private readonly Dictionary<string, ProgrammingLanguageExpressionAttribute> _solidityDocs;
        private readonly Dictionary<string, ProgrammingLanguageExpressionAttribute> _scribanDocs;
        readonly IDictionary<ScribanSolidityTokenTypes, string> _scribansSolidityTypes;

        public ScribanSolidityQuickInfoSource(ScribanSolidityQuickInfoSourceProvider provider, ITextBuffer subjectBuffer)
        {
            m_provider = provider;
            m_subjectBuffer = subjectBuffer;
            _solidityDocs = ExpressionsHelper.GroupedExpressionDescriptions(typeof(SolidityExpressions));
            _scribanDocs = ExpressionsHelper.GroupedExpressionDescriptions(typeof(ScribanExpressions));
            _scribansSolidityTypes = new Dictionary<ScribanSolidityTokenTypes, string>
            {
                [ScribanSolidityTokenTypes.ScribanComment] = "scriban-solidity.scriban.comment",
                [ScribanSolidityTokenTypes.ScribanExpression] = "scriban-solidity.scriban.expression",
                [ScribanSolidityTokenTypes.ScribanNumber] = "scriban-solidity.scriban.number",
                [ScribanSolidityTokenTypes.ScribanOperator] = "scriban-solidity.scriban.operator",
                [ScribanSolidityTokenTypes.ScribanString] = "scriban-solidity.scriban.string",
                [ScribanSolidityTokenTypes.ScribanWrapper] = "scriban-solidity.scriban.wrapper",

                [ScribanSolidityTokenTypes.SolidityComment] = "scriban-solidity.solidity.comment",
                [ScribanSolidityTokenTypes.SolidityDataType] = "scriban-solidity.solidity.data-type",
                [ScribanSolidityTokenTypes.SolidityExpression] = "scriban-solidity.solidity.expression",
                [ScribanSolidityTokenTypes.SolidityNumber] = "scriban-solidity.solidity.number",
                [ScribanSolidityTokenTypes.SolidityString] = "scriban-solidity.solidity.string",
                [ScribanSolidityTokenTypes.SolidityVisibility] = "scriban-solidity.solidity.visibility",
                [ScribanSolidityTokenTypes.SolidityOperator] = "scriban-solidity.solidity.operator",
            };
        }

        public void AugmentQuickInfoSession(IQuickInfoSession session, IList<object> qiContent, out ITrackingSpan applicableToSpan)
        {
            // Map the trigger point down to our buffer.
            SnapshotPoint? subjectTriggerPoint = session.GetTriggerPoint(m_subjectBuffer.CurrentSnapshot);
            if (!subjectTriggerPoint.HasValue)
            {
                applicableToSpan = null;
                return;
            }

            ITextSnapshot currentSnapshot = subjectTriggerPoint.Value.Snapshot;
            SnapshotSpan querySpan = new SnapshotSpan(subjectTriggerPoint.Value, 0);

            //look for occurrences of our QuickInfo words in the span
            ITextStructureNavigator navigator = m_provider.NavigatorService.GetTextStructureNavigator(m_subjectBuffer);
            TextExtent extent = navigator.GetExtentOfWord(subjectTriggerPoint.Value);
            string searchText = extent.Span.GetText();
            var keys = (new List<string>());
            keys.AddRange(_solidityDocs.Keys);
            keys.AddRange(_scribanDocs.Keys);
            
            foreach (string key in keys.OrderByDescending(x => x.Length))
            {
                int foundIndex = searchText.IndexOf(key, StringComparison.CurrentCultureIgnoreCase);
                if (foundIndex > -1)
                {
                    applicableToSpan = currentSnapshot.CreateTrackingSpan
                        (
                                                extent.Span.Start + foundIndex, key.Length, SpanTrackingMode.EdgeInclusive
                        );

                    var type = "";
                    _solidityDocs.TryGetValue(key, out ProgrammingLanguageExpressionAttribute value);
                    if (value == null)
                    {
                        _scribanDocs.TryGetValue(key, out value);
                        if (value != null) type = "Scriban";
                    }
                    else type = "Solidity";


                    if (value != null)
                    {
                        var headerLine = new ContainerElement(
                            ContainerElementStyle.Wrapped,  // horizontal
                            new ClassifiedTextElement(
                                new ClassifiedTextRun(type=="Scriban" ? "scriban-solidity-foreground": "scriban-scriban-foreground", (type == "Scriban" ? "🧩" : "🛡️"))
                            ),
                            new ClassifiedTextElement(new ClassifiedTextRun(_scribansSolidityTypes[value.Type], GetEmoji(value.Type))),
                            new ClassifiedTextElement(
                                new ClassifiedTextRun(_scribansSolidityTypes[value.Type], key, ClassifiedTextRunStyle.Bold)
                            )
                        );
                        qiContent.Add(new ContainerElement(
                                                ContainerElementStyle.Stacked,
                                                headerLine,
                                                new ContainerElement(ContainerElementStyle.VerticalPadding,
                                                new ClassifiedTextElement(
                                                    new ClassifiedTextRun("scriban-solidity-description", value.Description)
                                                )),
                                                new ClassifiedTextElement(
                                                    new ClassifiedTextRun("scriban-solidity-example", $"Example: {value.UsageExample}")
                                                )
                                            ));
                    }
                    else
                        qiContent.Add("");

                    return;
                }
            }

            applicableToSpan = null;
        }

        public void Dispose()
        {
        }

        private string GetEmoji(ScribanSolidityTokenTypes type)
        {
            switch (type)
            {
                case ScribanSolidityTokenTypes.ScribanComment:
                case ScribanSolidityTokenTypes.SolidityComment:
                    return "💬";
                case ScribanSolidityTokenTypes.ScribanExpression:
                case ScribanSolidityTokenTypes.SolidityExpression:
                    return "🧮";
                case ScribanSolidityTokenTypes.ScribanOperator:
                case ScribanSolidityTokenTypes.SolidityOperator:
                    return "➕";
                case ScribanSolidityTokenTypes.SolidityDataType:
                    return "🏷️";
                case ScribanSolidityTokenTypes.SolidityVisibility:
                    return "🔒";
                default: return "";

            }
        }
    }
}
