using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;


namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Processors
{
    public static class SolidityStatementProcessor
    {
        public static string Render(StatementModel statement)
        {
            if (statement == null)
                throw new ArgumentNullException(nameof(statement));

            try
            {
                var properties = ExtractProperties(statement);

                statement.ProcessProperties(properties);

                ProcessNestedStatements(properties);

                var processor = new BaseSolidityTemplateProcessor<object>(statement.TemplateName);
                string rendered = processor.Render(properties);

                return CleanupFormatting(rendered);
            }
            catch (Exception ex)
            {
                return $"// Error rendering {statement.GetType().Name}: {ex.Message}";
            }
        }

        private static string CleanupFormatting(string content)
        {
            var lines = content.Split('\n')
                .Select(line => line.TrimEnd()) 
                .ToList();

            for (int i = lines.Count - 1; i > 0; i--)
            {
                if (string.IsNullOrWhiteSpace(lines[i]) && string.IsNullOrWhiteSpace(lines[i - 1]))
                {
                    lines.RemoveAt(i);
                }
            }

            return string.Join("\n", lines);
        }

        private static Dictionary<string, object> ExtractProperties(StatementModel statement)
        {
            return statement.GetType().GetProperties()
                .Where(p => p.CanRead &&
                       p.Name != "TemplateName" &&
                       p.Name != "TemplateBaseName")
                .ToDictionary(p => p.Name, p => p.GetValue(statement) ?? string.Empty);
        }

        private static void ProcessNestedStatements(Dictionary<string, object> properties)
        {
            foreach (var key in properties.Keys.ToList())
            {
                if (properties[key] is IEnumerable<StatementModel> statementCollection)
                {
                    properties[key] = statementCollection.Select(Render).ToList();
                }
                else if (properties[key] is StatementModel nestedStatement)
                {
                    properties[key] = Render(nestedStatement);
                }
                else if (properties[key] is ExpressionModel exprModel)
                {
                    properties[key] = exprModel.ToString();
                }
                else if (properties[key] is IEnumerable<ConditionalStatementBlock> blocks)
                {
                    var processedBlocks = new List<Dictionary<string, object>>();

                    foreach (var block in blocks)
                    {
                        var blockProps = new Dictionary<string, object>
                        {
                            ["Condition"] = block.Condition?.ToString() ?? string.Empty,
                            ["IsElseBlock"] = block.IsElseBlock,
                            ["Statements"] = block.Statements.Select(Render).ToList()
                        };

                        processedBlocks.Add(blockProps);
                    }

                    properties[key] = processedBlocks;
                }
            }
        }
    }
}