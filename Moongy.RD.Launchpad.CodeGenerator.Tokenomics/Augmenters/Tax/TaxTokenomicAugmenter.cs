using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax;

public class TaxTokenomicAugmenter : BaseTokenomicAugmenter<TaxTokenomicModel>
{
    public override IReadOnlyCollection<FeatureKind> Requires => new[] { FeatureKind.AccessControl };

    public override IReadOnlyCollection<FeatureKind> Provides => new[] { FeatureKind.Tax };

    public override void Augment(ContextMetamodel ctx, TaxTokenomicModel model)
    {
        var mod = Main(ctx);
        
        BuildTaxStorage(mod, model);
        BuildTaxFunctions(mod);
        ModifyTransferFunction(mod);
    }


    private void BuildTaxStorage(ModuleDefinition mod, TaxTokenomicModel model)
    {
        // uint256 private taxFee;
        AddOnce(mod.Fields, f => f.Name == "taxFee", () => new FieldDefinition
            {
                Name = "taxFee",
                Type = T(PrimitiveType.Uint256),
                Visibility = Visibility.Private,
                Value = model.TaxFee.ToString()
            }
        );
    }
    
    private void BuildTaxFunctions(ModuleDefinition mod)
    {
        // function setTaxFee(uint256 newTaxFee) public onlyOwner 
        AddOnce(mod.Functions, f => f.Name == "setTaxFee", () => new SetTaxFeeFunction().Build());

        // function getTaxFee() public view returns (uint256)
        AddOnce(mod.Functions, f => f.Name == "getTaxFee", () => new GetTaxFeeFunction().Build());
    }

    private void ModifyTransferFunction(ModuleDefinition mod)
    {
        var existingTransfer = TransferFn(mod);
        var updatedTransfer = new ModifiedTransferFunction().Build();
        OverrideFunction(mod, existingTransfer, updatedTransfer);
    }
}
