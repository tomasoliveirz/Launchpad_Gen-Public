using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers;



public class FungibleTokenComposer : BaseStandardComposer<FungibleTokenModel>, IStandardComposer<FungibleTokenModel>
{
    public override ModuleFileDefinition Compose(FungibleTokenModel standard)
    {
        var moduleFile = base.Compose(standard);

        return moduleFile;
    }

    private FunctionDefinition MintFunctionDefinition()
    {
        var mint = new MintFunction();
        return mint.Build();
    }

    private FunctionDefinition BurnFunctionDefinition()
    {
        var burn = new BurnFunction();
        return burn.Build();
    }

    private FunctionDefinition ConstructorDefinition()
    {
        var constructor = new ERC20Constructor();
        return constructor.Build();
    }

    private FunctionDefinition TransferFunctionDefinition()
    {
        var transfer = new _TransferFunction();
        return transfer.Build();
    }

    private FunctionDefinition _TransferFunctionDefinition()
    {
        var transfer = new _TransferFunction();
        return transfer.Build();
    }

    private FunctionDefinition TransferFromFunctionDefinition()
    {
        var transferFrom = new TransferFromFunction();
        return transferFrom.Build();
    }

    private FunctionDefinition ApproveFunctionDefinition()
    {
        var approve = new ApproveFunction();
        return approve.Build();
    }
}
