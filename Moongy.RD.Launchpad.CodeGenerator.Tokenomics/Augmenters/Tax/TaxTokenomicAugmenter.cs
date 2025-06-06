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
    // uint256 private taxFee;
    private FieldDefinition _taxFeeField = new() 
    {
        Name = "taxFee",
        Type = T(PrimitiveType.Uint256),
        Visibility = Visibility.Private
    };
    // address[] private _taxRecipients;
    private FieldDefinition _taxRecipientsField = new()
    {
        Name = "_taxRecipients",
        Type = new TypeReference
        {
            Kind = TypeReferenceKind.Array,
            ElementType = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address }
        },        
        Visibility = Visibility.Private
    };
    
    public override void Augment(ContextMetamodel ctx, TaxTokenomicModel model)
    {
        var mod = Main(ctx);
        // first we add needed state variables
        AddStateVariables(mod);
        // then initialize variables in the constructor if needed
        InitializeVariables(ctx, model);
        // add getter and setter functions
        AddAccessFunctions(mod);
        ModifyTransferFunction(mod);
    }
    
    // add fields to the contract
    // uint256 private taxFee;
    // address[] private _taxRecipients;
    private void AddStateVariables(ModuleDefinition contract)
    {
        AddOnce(contract.Fields, f => f.Name == "taxFee", () => _taxFeeField);
        AddOnce(contract.Fields, f => f.Name == "_taxRecipients", () => _taxRecipientsField);
    }
    
    // modifies the constructor or creates it if it doesn't exist
    // taxFee = model.TaxFee;
    // _taxRecipients = model.TaxRecipients;
    private void InitializeVariables(ContextMetamodel ctx, TaxTokenomicModel model)
    {
        var contract = Main(ctx);
        var constructor = contract.Functions.FirstOrDefault(f => f.Name == "constructor");
        if (constructor == null)
        {
            constructor = new FunctionDefinition
            {
                Kind = FunctionKind.Constructor,
                Name = "constructor",
                Visibility = Visibility.Public,
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

        if (model.TaxRecipients?.Any() == true)
        {
            foreach (var recipient in model.TaxRecipients)
            {
                var recipientAssignment = new FunctionStatementDefinition
                {
                    Kind = FunctionStatementKind.Expression,
                    Expression = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.FunctionCall,
                        Callee = new ExpressionDefinition { Kind = ExpressionKind.MemberAccess, MemberName = "_taxRecipients.push" },
                        Arguments = new List<ExpressionDefinition>
                        {
                            new ExpressionDefinition { Kind = ExpressionKind.Literal, LiteralValue = $"\"{recipient.Address}\"" }
                        }
                    }
                };
                constructor.Body.Add(recipientAssignment);
            }
        }
    }
    // function getTaxFee() public view returns (uint256) {
    //     return taxFee;
    // }
    // 
    // function setTaxFee(uint256 newTaxFee) public onlyOwner {
    //     require(newTaxFee <= 100, "Tax fee cannot exceed 100%");
    //     taxFee = newTaxFee;
    private void AddAccessFunctions(ModuleDefinition contract)
    {
        var ownerModifier = contract.Modifiers.FirstOrDefault(m => m.Name == "onlyOwner");
        
        var getTaxFeeFunction = new GetTaxFeeFunction().Build();
        AddOnce(contract.Functions, f => f.Name == "getTaxFee", () => getTaxFeeFunction);
        
        var setTaxFeeFunction = new SetTaxFeeFunction().Build();
        if (ownerModifier != null && !setTaxFeeFunction.Modifiers.Any(m => m.Name == "onlyOwner"))
        {
            setTaxFeeFunction.Modifiers.Add(ownerModifier);
        }
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
