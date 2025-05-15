using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class ConditionStatementModel : StatementModel
    {
        public List<ConditionalStatementBlock> ConditionalBlocks { get; } = new List<ConditionalStatementBlock>();
        
        protected override string TemplateBaseName => "ConditionStatement";
        public ConditionStatementModel() { }

        // simple if statement 
        public ConditionStatementModel(ExpressionModel condition)
        {
            AddIfBlock(condition);
        }
        public ConditionStatementModel(string condition)
        {
            AddIfBlock(new LiteralExpressionModel(condition));
        }
        
        

        public ConditionStatementModel AddIfBlock(ExpressionModel condition, params StatementModel[] statements)
        {
            if (ConditionalBlocks.Count > 0 && !ConditionalBlocks[0].IsElseIfBlock && !ConditionalBlocks[0].IsElseBlock)
            {
                throw new InvalidOperationException("Cannot add a second 'if' block. Use AddElseIfBlock instead.");
            }
            
            var block = new ConditionalStatementBlock
            {
                Condition = condition ?? throw new ArgumentNullException(nameof(condition)),
                IsIfBlock = true
            };
            
            if (statements != null)
            {
                block.Statements.AddRange(statements);
            }
            
            ConditionalBlocks.Insert(0, block); // always if block at the beginning
            return this;
        }
        

        // adds an 'else if' block with a condition and optional statements
        public ConditionStatementModel AddElseIfBlock(ExpressionModel condition, params StatementModel[] statements)
        {
            if (ConditionalBlocks.Count == 0)
            {
                throw new InvalidOperationException("Cannot add 'else if' block without an 'if' block first.");
            }
            
            // check if the last block is an 'else' block
            if (ConditionalBlocks.Any(b => b.IsElseBlock))
            {
                throw new InvalidOperationException("Cannot add 'else if' block after an 'else' block.");
            }
            
            var block = new ConditionalStatementBlock
            {
                Condition = condition ?? throw new ArgumentNullException(nameof(condition)),
                IsElseIfBlock = true
            };
            
            if (statements != null)
            {
                block.Statements.AddRange(statements);
            }
            
            // add else-if block after the if block or the last else-if block
            int insertIndex = 0;
            for (int i = 0; i < ConditionalBlocks.Count; i++)
            {
                if (ConditionalBlocks[i].IsIfBlock || ConditionalBlocks[i].IsElseIfBlock)
                {
                    insertIndex = i + 1;
                }
            }
            
            ConditionalBlocks.Insert(insertIndex, block);
            return this;
        }
        

        // adds an else block with optional statements
        public ConditionStatementModel AddElseBlock(params StatementModel[] statements)
        {
            if (ConditionalBlocks.Count == 0)
            {
                throw new InvalidOperationException("Cannot add 'else' block without an 'if' block first.");
            }
            
            // if there's already an else block
            if (ConditionalBlocks.Any(b => b.IsElseBlock))
            {
                throw new InvalidOperationException("Cannot add multiple 'else' blocks.");
            }
            
            var block = new ConditionalStatementBlock
            {
                IsElseBlock = true,
                //  a placeholder condition - not used for else blocks
                Condition = ExpressionModel.Equal("true", "true") 
            };
            
            if (statements != null)
            {
                block.Statements.AddRange(statements);
            }
            
            // else block at the end
            ConditionalBlocks.Add(block);
            return this;
        }
        
        
        // adds a statement to the most recently added block
        public ConditionStatementModel AddStatement(StatementModel statement)
        {
            if (ConditionalBlocks.Count == 0)
            {
                throw new InvalidOperationException("Cannot add statement without a conditional block first.");
            }
            
            ConditionalBlocks[ConditionalBlocks.Count - 1].AddStatement(statement);
            return this;
        }
        
        // some helper factory methods for common patterns if we need them 
        

        // creates an if-then statement
        public static ConditionStatementModel If(ExpressionModel condition, params StatementModel[] thenStatements)
        {
            var model = new ConditionStatementModel();
            model.AddIfBlock(condition, thenStatements);
            return model;
        }
        

        // creates an if-then statement with a string condition
        public static ConditionStatementModel If(string condition, params StatementModel[] thenStatements)
        {
            return If(new LiteralExpressionModel(condition), thenStatements);
        }
        
        // creates an if-then-else statement
        public static ConditionStatementModel IfElse(
            ExpressionModel condition,
            StatementModel[] thenStatements,
            StatementModel[] elseStatements)
        {
            var model = new ConditionStatementModel();
            model.AddIfBlock(condition, thenStatements);
            model.AddElseBlock(elseStatements);
            return model;
        }
        
        // same with a string condition
        public static ConditionStatementModel IfElse(
            string condition,
            StatementModel[] thenStatements,
            StatementModel[] elseStatements)
        {
            return IfElse(new LiteralExpressionModel(condition), thenStatements, elseStatements);
        }
        
        // if-elseif-else statement
        public static ConditionStatementModel IfElseIfElse(
            ExpressionModel ifCondition,
            StatementModel[] ifStatements,
            ExpressionModel elseIfCondition,
            StatementModel[] elseIfStatements,
            StatementModel[] elseStatements)
        {
            var model = new ConditionStatementModel();
            model.AddIfBlock(ifCondition, ifStatements);
            model.AddElseIfBlock(elseIfCondition, elseIfStatements);
            model.AddElseBlock(elseStatements);
            return model;
        }
        // same with string conditions
        public static ConditionStatementModel IfElseIfElse(
            string ifCondition,
            StatementModel[] ifStatements,
            string elseIfCondition,
            StatementModel[] elseIfStatements,
            StatementModel[] elseStatements)
        {
            return IfElseIfElse(
                new LiteralExpressionModel(ifCondition),
                ifStatements,
                new LiteralExpressionModel(elseIfCondition),
                elseIfStatements,
                elseStatements);
        }
    }

    /// <summary>
    /// Represents a block within a conditional statement (if/else if/else)
    /// </summary>
    public class ConditionalStatementBlock
    {
        public required ExpressionModel Condition { get; set; }
        public List<StatementModel> Statements { get; } = new List<StatementModel>();
        
        // flags for block type
        public bool IsIfBlock { get; set; }
        public bool IsElseIfBlock { get; set; }
        public bool IsElseBlock { get; set; }
        
        // add a statement to a block
        public ConditionalStatementBlock AddStatement(StatementModel statement)
        {
            Statements.Add(statement ?? throw new ArgumentNullException(nameof(statement)));
            return this;
        }
        
        // add multiple statements to this block
        public ConditionalStatementBlock AddStatements(params StatementModel[] statements)
        {
            if (statements != null)
            {
                Statements.AddRange(statements);
            }
            return this;
        }

        // add a nested conditional statement to this block
        public ConditionalStatementBlock AddNestedCondition(ConditionStatementModel condition)
        {
            Statements.Add(condition);
            return this;
        }
        
        // simple nested if statement
        public ConditionalStatementBlock AddNestedIf(ExpressionModel condition, params StatementModel[] statements)
        {
            var nestedIf = ConditionStatementModel.If(condition, statements);
            Statements.Add(nestedIf);
            return this;
        }
        
        // convenience method to add a nested if-else statement
        public ConditionalStatementBlock AddNestedIfElse(
            ExpressionModel condition, 
            StatementModel[] thenStatements, 
            StatementModel[] elseStatements)
        {
            var nestedIfElse = ConditionStatementModel.IfElse(condition, thenStatements, elseStatements);
            Statements.Add(nestedIfElse);
            return this;
        }
    }
}