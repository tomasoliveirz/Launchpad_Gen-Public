using System.Diagnostics.Contracts;
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
    private FieldDefinition _taxFeeField = new() 
    {
        Name = "taxFee",
        Type = T(PrimitiveType.Uint256),
        Visibility = Visibility.Private
    };
    
    public override void Augment(ContextMetamodel ctx, TaxTokenomicModel model)
    {
        var mod = Main(ctx);
        AddStateVariables(mod);
        InitializeVariables(ctx, model);
        AddAccessFunctions(mod);
        ModifyTransferFunction(mod);
    }
    
    private void AddStateVariables(ModuleDefinition contract)
    {
        AddOnce(contract.Fields, f => f.Name == "taxFee", () => _taxFeeField);
    }
    
    private void InitializeVariables(ContextMetamodel ctx, TaxTokenomicModel model)
    {
        var contract = Main(ctx);
        var constructor = contract.Functions.FirstOrDefault(f => f.Kind == FunctionKind.Constructor);
        
        if (constructor == null)
        {
            constructor = new FunctionDefinition
            {
                Kind = FunctionKind.Constructor,
                Name = "constructor",
                Visibility = Visibility.Public,
                Parameters = new List<ParameterDefinition>(),
                Body = new List<FunctionStatementDefinition>()
            };
            contract.Functions.Add(constructor);
        }

        var taxFeeAssignment = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Assignment,
            ParameterAssignment = new AssignmentDefinition
            {
                Left = new ExpressionDefinition
                {
                    Kind = ExpressionKind.Identifier,
                    Identifier = "taxFee"
                },
                Right = new ExpressionDefinition
                {
                    Kind = ExpressionKind.Literal,
                    LiteralValue = model.TaxFee.ToString()
                }
            }
        };
        
        constructor.Body.Add(taxFeeAssignment);
    }

    private void AddAccessFunctions(ModuleDefinition contract)
    {
        var getTaxFeeFunction = new GetTaxFeeFunction().Build();
        AddOnce(contract.Functions, f => f.Name == "getTaxFee", () => getTaxFeeFunction);
        
        var setTaxFeeFunction = new SetTaxFeeFunction().Build();
        AddOnce(contract.Functions, f => f.Name == "setTaxFee", () => setTaxFeeFunction);
    }

    private void ModifyTransferFunction(ModuleDefinition mod)
    {
        var transferFunction = mod.Functions.FirstOrDefault(f => f.Name == "_transfer");
        if (transferFunction != null)
        {
            var modifier = new TransferFunctionModifier();
            modifier.ModifyForTax(transferFunction);
        }
    }
}