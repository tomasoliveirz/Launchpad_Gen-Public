using System.Data.Common;
using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers;



public class FungibleTokenComposer : BaseStandardComposer<FungibleTokenModel>, IStandardComposer<FungibleTokenModel>
{
    public override ModuleFileDefinition Compose(FungibleTokenModel standard)
    {
        var moduleFile = base.Compose(standard);

        return moduleFile;
    }

    private FunctionDefinition GenerateMintFunctionDefinition()
    {
        var addressType = new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address };
        var uint256Type = new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 };

        var accountParameter = new ParameterDefinition() { Name = "account", Type = addressType};
        var valueParameter = new ParameterDefinition() { Name = "value", Type = uint256Type};
        
        var updateCallee = new ExpressionDefinition() { Identifier = "_update" };
        var zeroAddress = new ExpressionDefinition() { Identifier = "address(0)" };
        var accountExpression = new ExpressionDefinition() { Identifier = nameof(accountParameter) };
        var valueExpression = new ExpressionDefinition() { Identifier = nameof(valueParameter) };
        var erc20InvalidReceiverCallee = new ExpressionDefinition() { Identifier = "ERC20InvalidReceiver" };


        var revert = new FunctionStatementDefinition() { };
        var updateFunctionCall = new ExpressionDefinition() { Kind = ExpressionKind.FunctionCall, Callee = updateCallee, Arguments = [zeroAddress, accountExpression, valueExpression] };
        var body = new List<FunctionStatementDefinition>();

        var parameters = new List<ParameterDefinition>() { accountParameter, valueParameter};
        var result = new FunctionDefinition() { 
            Name = "_mint", 
            Visibility = Visibility.Internal, 
            Body = body,
        Parameters = parameters};
        return result;
    }

}
