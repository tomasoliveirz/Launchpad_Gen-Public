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

    private FunctionDefinition GenerateMintFunctionDefinition()
    {
        var mint = new MintFunctionGenerator();
        return mint.Build();
    }

    private FunctionDefinition GenerateBurnFunctionDefinition()
    {
        var burn = new BurnFunctionGenerator();
        return burn.Build();
    }

}
