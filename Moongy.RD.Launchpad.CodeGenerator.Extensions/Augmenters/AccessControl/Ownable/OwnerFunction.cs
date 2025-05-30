using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl.Ownable;

public class OwnerFunction
{
    public FunctionDefinition Build()
    {
        var ownerIdentifier = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "_owner"
        };
        
        var returnStatement = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Return,
            ReturnValues = [ownerIdentifier]
        };

        return new FunctionDefinition
        {
            Name = "owner",
            Visibility = Visibility.Public,
            ReturnParameters = [new ParameterDefinition { Name = "", Type = DataTypeReference.Address }],
            Body = [returnStatement],
        };
    }
}