using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax.Functions
{

    // function _transfer(address sender, address recipient, uint256 amount) internal {
    //     require(sender != address(0), "transfer from zero address");
    //     require(recipient != address(0), "transfer to zero address");
    //     require(_balances[sender] >= amount, "insufficient balance");
    //     
    //     uint256 amountToTax = amount * taxFee / 100;      
    //     uint256 value = amount - amountToTax;             
    //     
    //     _balances[sender] -= amount;                      
    //     _balances[recipient] += value;                    
    //     
    //     if (amountToTax > 0) {                            
    //         _balances[owner] += amountToTax;              
    //         emit Transfer(sender, owner, amountToTax);    
    //     }                                                 
    //     
    //     emit Transfer(sender, recipient, value);          
    // }
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
            
            // modifies existing balance assignments 
            ModifyBalanceAssignments(transferFunction);
            
            // adds logic to distribute tax to the owner
            AddTaxDistributionLogic(transferFunction);
            
            // modifies existing transfer events to use 'value' instead of 'amount'
            ModifyTransferEvents(transferFunction);
        }
        
        private bool HasTaxLogic(FunctionDefinition transferFunction)
        {
            return transferFunction.Body.Any(s => 
                s.Kind == FunctionStatementKind.LocalDeclaration &&
                (s.LocalParameter?.Name == "amountToTax"));
        }

        // uint256 amountToTax = amount * taxFee / 100;
        // uint256 value = amount - amountToTax;
        private void AddTaxVariableDeclarations(FunctionDefinition transferFunction)
        {
            var insertionPoint = FindVariableDeclarationPoint(transferFunction);

            var amountToTaxDeclaration = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.LocalDeclaration,
                LocalParameter = new ParameterDefinition
                {
                    Name = "amountToTax",
                    Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 },
                    Value = "amount * taxFee / 100"
                }
            };

            var valueDeclaration = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.LocalDeclaration,
                LocalParameter = new ParameterDefinition
                {
                    Name = "value",
                    Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 },
                    Value = "amount - amountToTax"
                }
            };

            transferFunction.Body.Insert(insertionPoint, amountToTaxDeclaration);
            transferFunction.Body.Insert(insertionPoint + 1, valueDeclaration); 
            
        }
        
        private int FindVariableDeclarationPoint(FunctionDefinition transferFunction)
        {
            // insert after the last require statement, but before the first balance operation
            int lastRequireIndex = -1;
            
            for (int i = 0; i < transferFunction.Body.Count; i++)
            {
                var statement = transferFunction.Body[i];
                
                // find last require statement
                if (statement.Kind == FunctionStatementKind.Expression &&
                    statement.Expression?.Callee?.Identifier == "require")
                {
                    lastRequireIndex = i;
                }
                
                // if we find a balance operation, we stop searching
                if (IsBalanceOperation(statement))
                {
                    return Math.Max(lastRequireIndex + 1, i);
                }
            }
            
            return lastRequireIndex + 1;
        }

        // identifies if the statement is a balance operation
        private bool IsBalanceOperation(FunctionStatementDefinition statement)
        {
            return statement.Kind == FunctionStatementKind.Assignment &&
                   statement.ParameterAssignment?.Left?.IndexCollection?.Identifier == "_balances";
        }
        
        // modifies existing balance assignments to use value instead of amount
        private void ModifyBalanceAssignments(FunctionDefinition transferFunction)
        {
            for (int i = 0; i < transferFunction.Body.Count; i++)
            {
                var statement = transferFunction.Body[i];
                
                if (IsBalanceOperation(statement))
                {
                    var assignment = statement.ParameterAssignment;
                    
                    // if its a recipient balance assignment, replace 'amount' with 'value'
                    if (IsRecipientBalanceAssignment(assignment))
                    {
                        ReplaceAmountWithValue(assignment.Right);
                    }
                }
            }
        }
        
        // detects if the assignment is for recipient's balance
        private bool IsRecipientBalanceAssignment(AssignmentDefinition assignment)
        {
            // if its an add operation, it means recipient is receiving value
            return assignment.Right?.Kind == ExpressionKind.Binary &&
                   assignment.Right.Operator == BinaryOperator.Add;
        }
        
        // replaces amount with value in expressions
        private void ReplaceAmountWithValue(ExpressionDefinition expression)
        {
            if (expression == null) return;

            // recursively replace amount with value
            if (expression.Kind == ExpressionKind.Identifier && expression.Identifier == "amount")
            {
                expression.Identifier = "value";
            }
            else if (expression.Kind == ExpressionKind.Binary)
            {
                ReplaceAmountWithValue(expression.Left);
                ReplaceAmountWithValue(expression.Right);
            }
        }


        // if (amountToTax > 0) {
        //     _balances[owner] += amountToTax;
        //     emit Transfer(sender, owner, amountToTax);
        // }
        private void AddTaxDistributionLogic(FunctionDefinition transferFunction)
        {
            var insertionPoint = FindTaxDistributionPoint(transferFunction);

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
        
        // finds the point in the transfer function body to insert tax distribution logic
        private int FindTaxDistributionPoint(FunctionDefinition transferFunction)
        {
            for (int i = transferFunction.Body.Count - 1; i >= 0; i--)
            {
                var statement = transferFunction.Body[i];
                
                if (IsBalanceOperation(statement))
                {
                    return i + 1;
                }
            }
            
            return transferFunction.Body.Count;
        }
        
        
        // _balances[owner] += amountToTax;
        // emit Transfer(sender, owner, amountToTax);
        private List<FunctionStatementDefinition> CreateTaxDistributionBody()
        {
            var body = new List<FunctionStatementDefinition>();

            // _balances[owner] += amountToTax;
            var ownerBalanceUpdate = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.IndexAccess,
                        IndexCollection = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_balances" },
                        Index = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "owner" }
                    },
                    Right = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.Add,
                        Left = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.IndexAccess,
                            IndexCollection = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_balances" },
                            Index = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "owner" }
                        },
                        Right = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amountToTax" }
                    }
                }
            };

            // emit Transfer(sender, owner, amountToTax);
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
                    new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "sender" },
                    new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "owner" },
                    new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amountToTax" }
                }
            };

            body.Add(ownerBalanceUpdate);
            body.Add(taxTransferEvent);
            
            return body;
        }


        // modifies transfer events to use 'value' instead of 'amount'
        private void ModifyTransferEvents(FunctionDefinition transferFunction)
        {
            for (int i = 0; i < transferFunction.Body.Count; i++)
            {
                var statement = transferFunction.Body[i];
                
                if (statement.Kind == FunctionStatementKind.Trigger &&
                    statement.Trigger?.Name == "Transfer" &&
                    statement.TriggerArguments?.Count >= 3)
                {
                    // mnodify the third argument from 'amount' to 'value'
                    var valueArg = statement.TriggerArguments[2];
                    if (valueArg.Kind == ExpressionKind.Identifier && valueArg.Identifier == "amount")
                    {
                        valueArg.Identifier = "value";
                    }
                }
            }
        }
    }
}