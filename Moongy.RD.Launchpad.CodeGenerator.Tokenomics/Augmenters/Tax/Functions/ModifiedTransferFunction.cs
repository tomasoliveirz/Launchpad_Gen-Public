using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax.Functions;

public class ModifiedTransferFunction
{
    public FunctionDefinition Build()
    {
        var parameters = BuildParameters();
        
        var senderExpr = new ExpressionDefinition{Kind = ExpressionKind.Identifier, Identifier = "sender"};
        var recipientExpr = new ExpressionDefinition{Kind = ExpressionKind.Identifier, Identifier = "recipient"};
        var amountExpr = new ExpressionDefinition{Kind = ExpressionKind.Identifier, Identifier = "amount"};
        var ownerExpr = new ExpressionDefinition{Kind = ExpressionKind.Identifier, Identifier = "owner"};
        var taxFeeExpr = new ExpressionDefinition{Kind = ExpressionKind.Identifier, Identifier = "taxFee"};
        var zeroAddress = new ExpressionDefinition{Kind = ExpressionKind.Identifier, Identifier = "address(0)"};
        var hundredExpr = new ExpressionDefinition{Kind = ExpressionKind.Literal, Identifier = "100"};
        var zeroExrpession = new ExpressionDefinition{Kind = ExpressionKind.Literal, Identifier = "0"};

        var balancesSender = new ExpressionDefinition
        {
            Kind = ExpressionKind.IndexAccess,
            IndexCollection = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_balances" },
            Index = recipientExpr,
        };
        
        var balancesRecipient = new ExpressionDefinition
        {
            Kind = ExpressionKind.IndexAccess,
            IndexCollection = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_balances" },
            Index = recipientExpr,
        };
        
        var balancesOwner = new ExpressionDefinition
        {
            Kind = ExpressionKind.IndexAccess,
            IndexCollection = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_balances" },
            Index = ownerExpr,
        };

        var senderNotZeroCheck = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Expression,
            Expression = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "require" },
                Arguments = new List<ExpressionDefinition>
                {
                    new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.NotEqual,
                        Left = senderExpr,
                        Right = zeroExrpession
                    },
                    new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Literal, LiteralValue = "\"ERC20: transfer from the zero address\"" 
                    }
                }
            }
        };
        var recipientNotZeroCheck = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Expression,
            Expression = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "require" },
                Arguments = new List<ExpressionDefinition>
                {
                    new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.NotEqual,
                        Left = recipientExpr,
                        Right = zeroAddress
                    },
                    new ExpressionDefinition { Kind = ExpressionKind.Literal, LiteralValue = "\"ERC20: transfer to the zero address\"" }
                }
            }
        };

        var balanceCheck = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Expression,
            Expression = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "require" },
                Arguments = new List<ExpressionDefinition>
                {
                    new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.GreaterOrEqualThan,
                        Left = balancesSender,
                        Right = amountExpr
                    },
                    new ExpressionDefinition
                        { Kind = ExpressionKind.Literal, LiteralValue = "\"ERC20: transfer amount exceeds balance\"" }
                }
            }
        };
        
        var amountToTaxDeclaration = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.LocalDeclaration,
            LocalParameter = new ParameterDefinition
            {
                Name = "amountToTax",
                Type = new TypeReference
                {
                    Kind      = TypeReferenceKind.Simple,
                    Primitive = PrimitiveType.Uint256
                },
                Value = "amount * taxFee / 100"
            }
        };
        
        var valueDeclaration = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.LocalDeclaration,
            LocalParameter = new ParameterDefinition
            {
                Name = "value",
                Type = new TypeReference
                {
                    Kind      = TypeReferenceKind.Simple,
                    Primitive = PrimitiveType.Uint256
                },
                Value = "amount - amountToTax"
            }
        };
        
        var amountToTaxExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amountToTax" };
        var valueExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "value" };
        var updateSenderBalance = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Assignment,
            ParameterAssignment = new AssignmentDefinition
            {
                Left = balancesSender,
                Right = new ExpressionDefinition
                {
                    Kind = ExpressionKind.Binary,
                    Operator = BinaryOperator.Subtract,
                    Left = balancesSender,
                    Right = amountExpr
                }
            }
        };

        var updateRecipientBalance = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Assignment,
            ParameterAssignment = new AssignmentDefinition
            {
                Left = balancesRecipient,
                Right = new ExpressionDefinition
                {
                    Kind = ExpressionKind.Binary,
                    Operator = BinaryOperator.Add,
                    Left = balancesRecipient,
                    Right = valueExpr
                }
            }
        };
        var taxCondition = new ExpressionDefinition
        {
            Kind = ExpressionKind.Binary,
            Operator = BinaryOperator.GreaterThan,
            Left = amountToTaxExpr,
            Right = zeroExrpession
        };

        var updateOwnerBalance = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Assignment,
            ParameterAssignment = new AssignmentDefinition
            {
                Left = balancesOwner,
                Right = new ExpressionDefinition
                {
                    Kind = ExpressionKind.Binary,
                    Operator = BinaryOperator.Add,
                    Left = balancesOwner,
                    Right = amountToTaxExpr
                }
            }
        };
        
        
        var emitTaxTransfer = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Trigger,
            Trigger = new TriggerDefinition
            {
                Kind = TriggerKind.Log,
                Name = "Transfer",
                Parameters = new List<ParameterDefinition>
                {
                    new() { Name = "sender", Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address } },
                    new() {Name = "owner", Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address } },
                    new() { Name = "amountToTax", Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 } }
                }
            },
            TriggerArguments = new List<ExpressionDefinition> { senderExpr, ownerExpr, amountToTaxExpr }
        };

        var taxIfStatement = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Condition,
            ConditionBranches = new List<ConditionBranch>
            {
                new ConditionBranch
                {
                    Condition = taxCondition,
                    Body = new List<FunctionStatementDefinition>
                    {
                        updateOwnerBalance,
                        emitTaxTransfer
                    }
                }
            }
        };
        var emitMainTransfer = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Trigger,
            Trigger = new TriggerDefinition
            {
                Kind = TriggerKind.Log,
                Name = "Transfer",
                Parameters = new List<ParameterDefinition>
                {
                    new() { Name = "sender", Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address } },
                    new() { Name = "recipient", Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address } },
                    new() { Name = "value", Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 } }
                }
            },
            TriggerArguments = new List<ExpressionDefinition> { senderExpr, recipientExpr, valueExpr }
        };
        
        
        var result = new FunctionDefinition
        {
            Name = "_transfer",
            Visibility = Visibility.Internal,
            Parameters = parameters,
            Body = new List<FunctionStatementDefinition>
            {
                senderNotZeroCheck,
                recipientNotZeroCheck,
                balanceCheck,
                amountToTaxDeclaration,
                valueDeclaration,
                updateSenderBalance,
                updateRecipientBalance,
                taxIfStatement,
                emitMainTransfer
            }
        };
        
        return result;
        
    }


    private static List<ParameterDefinition> BuildParameters()
    {
        return new List<ParameterDefinition>
        {
            // address sender
            new ParameterDefinition
            {
                Name = "sender",
                Type = new TypeReference
                {
                    Kind = TypeReferenceKind.Simple,
                    Primitive = PrimitiveType.Address
                }
            },

            // address recipient
            new ParameterDefinition
            {
                Name = "recipient",
                Type = new TypeReference
                {
                    Kind = TypeReferenceKind.Simple,
                    Primitive = PrimitiveType.Address
                }
            },

            // uint256 amount
            new ParameterDefinition
            {
                Name = "amount",
                Type = new TypeReference
                {
                    Kind = TypeReferenceKind.Simple,
                    Primitive = PrimitiveType.Uint256
                }
            }
        };

    }

}