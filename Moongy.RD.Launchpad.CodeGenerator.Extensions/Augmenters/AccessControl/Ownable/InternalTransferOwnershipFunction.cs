using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl.Ownable;

/*
     function _transferOwnership(address newOwner) internal virtual {
       address oldOwner = _owner;
       _owner = newOwner;
       emit OwnershipTransferred(oldOwner, newOwner);
   }
 */
public class InternalTransferOwnershipFunction
{
    public FunctionDefinition Build()
    {
        var newOwnerParam = new ParameterDefinition
        {
            Name = "newOwner",
            Type = DataTypeReference.Address
        };

        var ownerExpr = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "_owner"
        };
        
        var newOwnerExpr = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "newOwner"
        };
        
        var oldOwnerDeclaration = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.LocalDeclaration,
            LocalParameter = new ParameterDefinition
            {
                Name = "oldOwner",
                Type = DataTypeReference.Address,
                Value = "_owner"
            }
        };
        
        var ownerAssignment = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Assignment,
            ParameterAssignment = new AssignmentDefinition
            {
                Left = ownerExpr,
                Right = newOwnerExpr
            }
        };

        var oldOwnerExpr = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "oldOwner"
        };

        var emitEvent = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Trigger,
            Trigger = new TriggerDefinition
            {
                Kind = TriggerKind.Log,
                Name = "OwnershipTransferred",
                Parameters =
                [
                    new() { Name = "oldOwner", Type = DataTypeReference.Address },
                    new() { Name = "newOwner", Type = DataTypeReference.Address }
                ]
            },
            TriggerArguments = [oldOwnerExpr, newOwnerExpr]
        };

        return new FunctionDefinition
        {
            Name = "_transferOwnership",
            Visibility = Visibility.Internal,
            Parameters = [newOwnerParam],
            Body = new List<FunctionStatementDefinition>
            {
                oldOwnerDeclaration,
                ownerAssignment,
                emitEvent
            }
        };
    }
}