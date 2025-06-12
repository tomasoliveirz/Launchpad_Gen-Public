using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax.Functions
{
    public class TransferFunctionModifier
    {
        public void ModifyForTax(FunctionDefinition transferFunction)
        {
            // verifies if the transfer function already has tax logic implemented
            if (HasTaxLogic(transferFunction))
            {
                return;
            }
            // add tax variable declarations
            AddTaxVariableDeclarations(transferFunction);
            
            // adds logic to distribute tax to the owner
            AddTaxDistributionLogic(transferFunction);
        }
        
        private bool HasTaxLogic(FunctionDefinition transferFunction)
        {
            return transferFunction.Body.Any(s => 
                s.Kind == FunctionStatementKind.LocalDeclaration &&
                (s.LocalParameter?.Name == "amountToTax"));
        }

        // uint256 amountToTax = value * taxFee / 100;
        // uint256 netValue = value - amountToTax;
        private void AddTaxVariableDeclarations(FunctionDefinition transferFunction)
        {
            var insertionPoint = 2; // After require statements

            var amountToTaxDeclaration = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.LocalDeclaration,
                LocalParameter = new ParameterDefinition
                {
                    Name = "amountToTax",
                    Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 },
                    Value = "value * taxFee / 100" 
                }
            };

            var netValueDeclaration = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.LocalDeclaration,
                LocalParameter = new ParameterDefinition
                {
                    Name = "netValue",
                    Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 },
                    Value = "value - amountToTax" 
                }
            };

            transferFunction.Body.Insert(insertionPoint, amountToTaxDeclaration);
            transferFunction.Body.Insert(insertionPoint + 1, netValueDeclaration); 
        }

        // if (amountToTax > 0) {
        //     _balances[owner] += amountToTax;
        //     emit Transfer(sender, owner, amountToTax);
        // }
        private void AddTaxDistributionLogic(FunctionDefinition transferFunction)
        {
            var insertionPoint = transferFunction.Body.Count; 

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
        
        // _balances[owner] += amountToTax;
        // emit Transfer(from, owner, amountToTax);
        private List<FunctionStatementDefinition> CreateTaxDistributionBody()
        {
            var body = new List<FunctionStatementDefinition>();

            var ownerBalanceUpdate = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.IndexAccess,
                        Target = new ExpressionDefinition 
                        { 
                            Kind = ExpressionKind.Identifier, 
                            Identifier = "_balances" 
                        },
                        Index = new ExpressionDefinition 
                        { 
                            Kind = ExpressionKind.Identifier, 
                            Identifier = "_owner" 
                        }
                    },
                    Right = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.Add,
                        Left = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.IndexAccess,
                            Target = new ExpressionDefinition 
                            { 
                                Kind = ExpressionKind.Identifier, 
                                Identifier = "_balances" 
                            },
                            Index = new ExpressionDefinition 
                            { 
                                Kind = ExpressionKind.Identifier, 
                                Identifier = "_owner" 
                            }
                        },
                        Right = new ExpressionDefinition 
                        { 
                            Kind = ExpressionKind.Identifier, 
                            Identifier = "amountToTax" 
                        }
                    }
                }
            };

            var taxTransferEvent = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Trigger,
                Trigger = new TriggerDefinition
                {
                    Kind = TriggerKind.Log,
                    Name = "Transfer",
                    Parameters = new List<ParameterDefinition>
                    {
                        new() { Name = "from", Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address } },
                        new() { Name = "to", Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address } },
                        new() { Name = "value", Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 } }
                    }
                },
                TriggerArguments = new List<ExpressionDefinition>
                {
                    new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "from" }, 
                    new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_owner" }, 
                    new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amountToTax" }
                }
            };

            body.Add(ownerBalanceUpdate);
            body.Add(taxTransferEvent);
            
            return body;
        }
    }
}