using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class SpendAllowanceFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();

            #region Identifiers
            var ownerAddress = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "owner" 
            };
            var spenderAddress = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "spender" 
            };
            var valueExpr = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "value" 
            };
            var currentAllowanceExpr = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "currentAllowance" 
            };
            var typeUint256Max = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Literal,
                LiteralValue = "type(uint256).max" 
            };
            #endregion

            #region Variable Declaration
            var allowanceCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition 
                { 
                    Kind = ExpressionKind.Identifier,
                    Identifier = "allowance" 
                },
                Arguments = new List<ExpressionDefinition> { ownerAddress, spenderAddress }
            };

            var currentAllowanceDeclaration = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.LocalDeclaration,
                LocalParameter = new ParameterDefinition
                {
                    Name = "currentAllowance",
                    Type = DataTypeReference.Uint256
                }
            };

            var currentAllowanceAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = currentAllowanceExpr,
                    Right = allowanceCall
                }
            };
            #endregion

            #region Inner If Statement
            var insufficientAllowanceCondition = new ExpressionDefinition
            {
                Kind = ExpressionKind.Binary,
                Left = currentAllowanceExpr,
                Operator = BinaryOperator.LessThan,
                Right = valueExpr
            };

            var revertStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Trigger,
                Trigger = new TriggerDefinition
                {
                    Kind = TriggerKind.Error,
                    Name = "ERC20InsufficientAllowance",
                    Parameters = new List<ParameterDefinition>
                    {
                        new() { Name = "spender", Type = DataTypeReference.Address },
                        new() { Name = "allowance", Type = DataTypeReference.Uint256 },
                        new() { Name = "needed", Type = DataTypeReference.Uint256 }
                    }
                },
                TriggerArguments = new List<ExpressionDefinition>
                {
                    spenderAddress,
                    currentAllowanceExpr,
                    valueExpr
                }
            };

            var innerIfStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = insufficientAllowanceCondition,
                        Body = new List<FunctionStatementDefinition>
                        {
                            revertStatement
                        }
                    }
                }
            };
            #endregion

            #region Outer If Statement
            var notMaxCondition = new ExpressionDefinition
            {
                Kind = ExpressionKind.Binary,
                Left = currentAllowanceExpr,
                Operator = BinaryOperator.LessThan,
                Right = typeUint256Max
            };

            var outerIfStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = notMaxCondition,
                        Body = new List<FunctionStatementDefinition>
                        {
                            innerIfStatement
                        }
                    }
                }
            };
            #endregion

            #region Function Definition
            var res = new FunctionDefinition
            {
                Name = "_spendAllowance",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Internal,
                Parameters = parameters,
                Body = new List<FunctionStatementDefinition>
                {
                    currentAllowanceDeclaration,
                    currentAllowanceAssignment,
                    outerIfStatement
                }
            };
            #endregion

            return res;
        }

        private List<ParameterDefinition> BuildParameters()
        {
            var owner = new ParameterDefinition
            {
                Name = "owner",
                Type = DataTypeReference.Address
            };
            var spender = new ParameterDefinition
            {
                Name = "spender",
                Type = DataTypeReference.Address
            };
            var value = new ParameterDefinition
            {
                Name = "value",
                Type = DataTypeReference.Uint256
            };

            var parameters = new List<ParameterDefinition>
            {
                owner,
                spender,
                value
            };
            return parameters;
        }
    }
}