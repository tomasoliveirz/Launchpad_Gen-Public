using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl.Ownable;

/*
     function renounceOwnership() public virtual onlyOwner {
       _transferOwnership(address(0));
   }
 */
public class RenounceOwnershipFunction
{
    public FunctionDefinition Build()
    {
        var zerroAddress = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "address(0)"
        };

        var transferCall = new ExpressionDefinition
        {
            Kind = ExpressionKind.FunctionCall,
            Callee = new ExpressionDefinition { Identifier = "_transferOwnership" },
            Arguments = [zerroAddress]
        };

        var transferStatement = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Expression,
            Expression = transferCall
        };

        return new FunctionDefinition
        {
            Name = "renounceOwnership",
            Visibility = Visibility.Public,
            Modifiers = [new ModifierDefinition { Name = "onlyOwner" }],
            Body = [transferStatement],
        };
    }
}