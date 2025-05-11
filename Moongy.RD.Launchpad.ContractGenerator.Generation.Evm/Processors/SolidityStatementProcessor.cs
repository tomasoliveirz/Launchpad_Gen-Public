using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public static class SolidityStatementProcessor
    {
        public static string Render(StatementModel statement)
        {
            if (statement == null)
                throw new ArgumentNullException(nameof(statement));
                
            try
            {
                // wxtract properties from the statement
                var properties = ExtractProperties(statement);
                
                // process statement properties
                statement.ProcessProperties(properties);
                
                // process nested statements
                ProcessNestedStatements(properties);
                
                // render using template
                var processor = new BaseSolidityTemplateProcessor<object>(statement.TemplateName);
                string rendered = processor.Render(properties);
                
                // then clean formatting
                return CleanupFormatting(rendered);
            }
            catch (Exception ex)
            {
                return $"// Error rendering {statement.GetType().Name}: {ex.Message}";
            }
        }
        
        // function to process the cleanup of formatting
        private static string CleanupFormatting(string content)
        {
            // remove extra empty lines and normalize indentation
            var lines = content.Split('\n')
                .Select(line => line.TrimEnd())  // trim
                .ToList();
                
            // limit consecutive empty lines to one line
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
                    // render each statement
                    properties[key] = statementCollection.Select(Render).ToList();
                }
                else if (properties[key] is StatementModel nestedStatement)
                {
                    // render nested statement
                    properties[key] = Render(nestedStatement);
                }
                else if (properties[key] is ExpressionModel exprModel)
                {
                    // convert ExpressionModel to string
                    properties[key] = exprModel.ToString();
                }
                else if (properties[key] is IEnumerable<ConditionalStatementBlock> blocks)
                {
                    // process each conditional block
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