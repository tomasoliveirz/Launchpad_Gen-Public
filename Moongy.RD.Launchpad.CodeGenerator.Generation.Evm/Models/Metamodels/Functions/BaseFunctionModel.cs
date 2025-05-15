using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Functions
{
    /// <summary>
    /// Base abstract class for all Solidity function models.
    /// </summary>
    public abstract class BaseFunctionModel : SolidityModel
    {
        public abstract string TemplateName { get; }
        public List<FunctionParameterModel> Parameters { get; set; } = new();
        public List<ReturnParameterModel> ReturnParameters { get; set; } = new();
        public SolidityVisibilityEnum Visibility { get; set; } = SolidityVisibilityEnum.Public;
        public SolidityFunctionMutabilityEnum Mutability { get; set; } = SolidityFunctionMutabilityEnum.None;
        public List<ModifierModel> Modifiers { get; set; } = new();
        public List<StatementModel> Statements { get; set; } = new();
        public bool IsVirtual { get; set; }
        public bool IsOverride { get; set; }
        public bool IsInterfaceDeclaration { get; set; }
        public string? CustomError { get; set; }
        public List<string> OverrideSpecifiers { get; set; } = new();
        public abstract void Validate();
        
        public virtual T AddStatement<T>(T statement) where T : StatementModel
        {
            Statements.Add(statement);
            return statement;
        }
        
        // require statements using an expression
        public virtual RequireStatement AddRequire(ExpressionModel condition, string? message = null)
        {
            var statement = new RequireStatement { 
                Condition = condition, 
                Message = message 
            };

            return AddStatement(statement);
        }
        // require statements using a string condition
        public virtual RequireStatement AddRequire(string conditionExpression, string? message = null)
        {
            var condition = new LiteralExpressionModel(conditionExpression);
            return AddRequire(condition, message);
        }


        public virtual ConditionStatementModel AddIf(ExpressionModel condition)
        {
            var statement = ConditionStatementModel.If(condition);
            Statements.Add(statement);
            return statement;
        }
        public virtual ConditionStatementModel AddIf(string conditionExpression)
        {
            return AddIf(new LiteralExpressionModel(conditionExpression));
        }

        public virtual ConditionStatementModel AddIfElse(
            ExpressionModel condition,
            StatementModel[] thenStatements,
            StatementModel[] elseStatements)
        {
            var statement = ConditionStatementModel.IfElse(condition, thenStatements, elseStatements);
            Statements.Add(statement);
            return statement;
        }
        
        public virtual ConditionStatementModel AddIfElse(
            string conditionExpression,
            StatementModel[] thenStatements,
            StatementModel[] elseStatements)
        {
            return AddIfElse(new LiteralExpressionModel(conditionExpression), thenStatements, elseStatements);
        }
        
        public virtual ReturnStatement AddReturn(params string[] values)
        {
            var statement = new ReturnStatement(values);    
            return AddStatement(statement);
        }
        

        public virtual EmitStatement AddEmit(string eventName)
        {
            var statement = new EmitStatement(eventName);
            return AddStatement(statement);
        }
        
        public virtual VariableDeclarationStatement AddVariable(
            TypeReference type,
            string name,
            string? initialValue = null,
            SolidityMemoryLocation? location = null)
        {
            var statement = new VariableDeclarationStatement(type, name, initialValue, location);
            return AddStatement(statement);
        }
        

        public virtual VariableDeclarationStatement AddVariable(
            TypeReference type,
            string name,
            ExpressionModel initialValue,
            SolidityMemoryLocation? location = null)
        {
            var statement = new VariableDeclarationStatement(type, name, initialValue, location);
            return AddStatement(statement);
        }
        
        public virtual AssignmentStatement AddAssignment(
            string target,
            string value,
            string @operator = "=")
        {
            var statement = new AssignmentStatement { 
                Target = target, 
                Value = value, 
                Operator = @operator 
            };
            
            return AddStatement(statement);
        }
        
        public virtual AssignmentStatement AddAssignment(
            ExpressionModel targetExpression,
            ExpressionModel valueExpression,
            string @operator = "=")
        {
            var statement = new AssignmentStatement(targetExpression, valueExpression, @operator)
            {
                TargetExpression = targetExpression,
                ValueExpression = valueExpression,
                @Operator = @operator,
                Target = targetExpression.ToString(),
                Value = valueExpression.ToString()
            };
            return AddStatement(statement);
        }
        
        public virtual ForStatement AddFor(
            StatementModel initialization,
            ExpressionModel condition,
            StatementModel iterator)
        {
            var statement = new ForStatement(initialization, condition, iterator);
            return AddStatement(statement);
        }

        public virtual WhileStatement AddWhile(ExpressionModel condition)
        {
            var statement = new WhileStatement(condition);
            return AddStatement(statement);
        }
        
        public virtual DoWhileStatement AddDoWhile(ExpressionModel condition)
        {
            var statement = new DoWhileStatement(condition);
            return AddStatement(statement);
        }
        
        public virtual RevertStatement AddRevert(string errorName, params string[] arguments)
        {
            var statement = new RevertStatement(errorName, arguments);
            return AddStatement(statement);
        }
        
        public virtual RevertStatement AddRevert(string? message = null)
        {
            var statement = new RevertStatement(message);
            return AddStatement(statement);
        }
    }
}