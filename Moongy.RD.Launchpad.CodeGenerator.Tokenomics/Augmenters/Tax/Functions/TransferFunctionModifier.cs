using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax.Functions
{
    public class TransferFunctionModifier
    {
        public void ModifyForTax(FunctionDefinition transferFunction)
        {
            if (HasTaxLogic(transferFunction))
            {
                return;
            }

            AddTaxVariableDeclarations(transferFunction);
            ModifyTransferLogicForTax(transferFunction);
            AddTaxDistributionLogic(transferFunction);
        }
        
        private bool HasTaxLogic(FunctionDefinition transferFunction)
        {
            return transferFunction.Body.Any(s => 
                s.Kind == FunctionStatementKind.LocalDeclaration &&
                (s.LocalParameter?.Name == "amountToTax"));
        }

        private void AddTaxVariableDeclarations(FunctionDefinition transferFunction)
        {
            var insertionPoint = GetSafeInsertionPoint(transferFunction);

            var amountToTaxDeclaration = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.LocalDeclaration,
                LocalParameter = new ParameterDefinition
                {
                    Name = "amountToTax",
                    Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 }
                }
            };

            var amountToTaxAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amountToTax" },
                    Right = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.Divide,
                        Left = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.Binary,
                            Operator = BinaryOperator.Multiply,
                            Left = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "value" },
                            Right = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "taxFee" }
                        },
                        Right = new ExpressionDefinition { Kind = ExpressionKind.Literal, LiteralValue = "100" }
                    }
                }
            };

            var netValueDeclaration = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.LocalDeclaration,
                LocalParameter = new ParameterDefinition
                {
                    Name = "netValue",
                    Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 }
                }
            };

            var netValueAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "netValue" },
                    Right = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.Subtract,
                        Left = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "value" },
                        Right = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amountToTax" }
                    }
                }
            };

            transferFunction.Body.Insert(insertionPoint, amountToTaxDeclaration);
            transferFunction.Body.Insert(insertionPoint + 1, amountToTaxAssignment);
            transferFunction.Body.Insert(insertionPoint + 2, netValueDeclaration);
            transferFunction.Body.Insert(insertionPoint + 3, netValueAssignment);
        }

        private void ModifyTransferLogicForTax(FunctionDefinition transferFunction)
        {
            var updateCallIndex = FindUpdateCallIndex(transferFunction);
            if (updateCallIndex >= 0)
            {
                var updateCall = transferFunction.Body[updateCallIndex];
                if (updateCall.Expression?.Arguments?.Count >= 3)
                {
                    updateCall.Expression.Arguments[2] = new ExpressionDefinition 
                    { 
                        Kind = ExpressionKind.Identifier, 
                        Identifier = "netValue" 
                    };
                }
            }
        }

        private void AddTaxDistributionLogic(FunctionDefinition transferFunction)
        {
            var updateCallIndex = FindUpdateCallIndex(transferFunction);
            if (updateCallIndex < 0) return;

            var insertionPoint = updateCallIndex;

            var taxCondition = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.Binary,
                            Operator = BinaryOperator.GreaterThan,
                            Left = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amountToTax" },
                            Right = new ExpressionDefinition { Kind = ExpressionKind.Literal, LiteralValue = "0" }
                        },
                        Body = CreateTaxDistributionBody()
                    }
                }
            };
            
            transferFunction.Body.Insert(insertionPoint, taxCondition);
        }
        
        private List<FunctionStatementDefinition> CreateTaxDistributionBody()
        {
            var body = new List<FunctionStatementDefinition>();

            var taxUpdateCall = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = new ExpressionDefinition
                {
                    Kind = ExpressionKind.FunctionCall,
                    Callee = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_update" },
                    Arguments = new List<ExpressionDefinition>
                    {
                        new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "from" },
                        new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_owner" },
                        new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amountToTax" }
                    }
                }
            };

            body.Add(taxUpdateCall);
            return body;
        }

        private int GetSafeInsertionPoint(FunctionDefinition transferFunction)
        {
            var validationCount = 0;
            for (int i = 0; i < transferFunction.Body.Count; i++)
            {
                var statement = transferFunction.Body[i];
                if (IsValidationStatement(statement))
                {
                    validationCount++;
                }
                else
                {
                    break;
                }
            }
            
            return Math.Min(validationCount, transferFunction.Body.Count);
        }

        private bool IsValidationStatement(FunctionStatementDefinition statement)
        {
            if (statement.Kind == FunctionStatementKind.Condition)
            {
                return true;
            }

            if (statement.Kind == FunctionStatementKind.Expression && 
                statement.Expression?.Callee?.Identifier == "require")
            {
                return true;
            }

            return false;
        }

        private int FindUpdateCallIndex(FunctionDefinition transferFunction)
        {
            for (int i = 0; i < transferFunction.Body.Count; i++)
            {
                var statement = transferFunction.Body[i];
                if (statement.Kind == FunctionStatementKind.Expression &&
                    statement.Expression?.Callee?.Identifier == "_update")
                {
                    return i;
                }
            }
            return -1;
        }
    }
}