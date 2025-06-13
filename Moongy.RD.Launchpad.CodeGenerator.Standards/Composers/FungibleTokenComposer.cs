using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Fields;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Functions;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers;

public class FungibleTokenComposer : BaseStandardComposer<FungibleTokenModel>, IStandardComposer<FungibleTokenModel>
{
    public override ModuleFileDefinition Compose(FungibleTokenModel standard)
    {
        var moduleFile = base.Compose(standard);
        
        AddERC20Fields(moduleFile, standard);
        AddERC20Functions(moduleFile, standard);
        
        return moduleFile;
    }

    private void AddERC20Fields(ModuleFileDefinition moduleFile, FungibleTokenModel standard)
    {
        var mainModule = moduleFile.Modules.FirstOrDefault();
        if (mainModule == null) return;

        mainModule.Fields.Add(TokenBalancesDefinition());
        mainModule.Fields.Add(TokenAllowancesDefinition());
        mainModule.Fields.Add(TokenTotalSupplyDefinition());
        mainModule.Fields.Add(TokenNameDefinition());
        mainModule.Fields.Add(TokenSymbolDefinition());
        mainModule.Fields.Add(TokenDecimalsDefinition());
        
        AddERC20Events(mainModule);
    }

    private void AddERC20Events(ModuleDefinition module)
    {
        var transferEvent = new TriggerDefinition
        {
            Name = "Transfer",
            Kind = TriggerKind.Log,
            Parameters = new List<ParameterDefinition>
            {
                new() { Name = "from", Type = DataTypeReference.Address },
                new() { Name = "to", Type = DataTypeReference.Address },
                new() { Name = "value", Type = DataTypeReference.Uint256 }
            }
        };

        var approvalEvent = new TriggerDefinition
        {
            Name = "Approval",
            Kind = TriggerKind.Log,
            Parameters = new List<ParameterDefinition>
            {
                new() { Name = "owner", Type = DataTypeReference.Address },
                new() { Name = "spender", Type = DataTypeReference.Address },
                new() { Name = "value", Type = DataTypeReference.Uint256 }
            }
        };

        if (!module.Triggers.Any(t => t.Name == "Transfer"))
        {
            module.Triggers.Add(transferEvent);
        }

        if (!module.Triggers.Any(t => t.Name == "Approval"))
        {
            module.Triggers.Add(approvalEvent);
        }
    }
    // TODO: ADD BURN AND MINT FUNCTIONS -- HANDLING AND THEN CALL HERE
    private void AddERC20Functions(ModuleFileDefinition moduleFile, FungibleTokenModel standard)
    {
        var mainModule = moduleFile.Modules.FirstOrDefault();
        if (mainModule == null) return;
        // TODO REVIEW THIS FUNCTIONS
        mainModule.Functions.Add(ConstructorDefinition());
        mainModule.Functions.Add(NameFunctionDefinition());
        mainModule.Functions.Add(SymbolFunctionDefinition());
        mainModule.Functions.Add(DecimalsFunctionDefinition());
        mainModule.Functions.Add(TotalSupplyFunctionDefinition());
        mainModule.Functions.Add(BalanceOfFunctionDefinition());
        mainModule.Functions.Add(TransferFunctionDefinition());
        mainModule.Functions.Add(AllowanceFunctionDefinition());
        mainModule.Functions.Add(ApproveFunctionDefinition());
        mainModule.Functions.Add(TransferFromFunctionDefinition());
        mainModule.Functions.Add(_TransferFunctionDefinition());
        mainModule.Functions.Add(_UpdateFunctionDefinition());
        mainModule.Functions.Add(MintFunctionDefinition());
        mainModule.Functions.Add(BurnFunctionDefinition());
        mainModule.Functions.Add(FirstApproveFunctionDefinition());
        mainModule.Functions.Add(_ApproveFunction());
        mainModule.Functions.Add(SpendAllowanceDefinition());
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
        var transfer = new TransferFunction(); 
        return transfer.Build();
    }

    private FieldDefinition TokenTotalSupplyDefinition() 
    {
        var totalSupply = new TokenTotalSupplyField();
        return totalSupply.Build();
    }

    private FieldDefinition TokenNameDefinition()
    {
        var tokenName = new TokenNameField();
        return tokenName.Build();
    }

    private FieldDefinition TokenSymbolDefinition()
    {
        var tokenSymbol = new TokenSymbolField();
        return tokenSymbol.Build();
    }

    private FieldDefinition TokenDecimalsDefinition()
    {
        var tokenDecimals = new TokenDecimalsField();
        return tokenDecimals.Build();
    }

    private FunctionDefinition TotalSupplyFunctionDefinition()
    {
        var totalSupply = new TotalSupplyFunction();
        return totalSupply.Build();
    }

    private FunctionDefinition DecimalsFunctionDefinition()
    {
        var decimals = new DecimalsFunction();
        return decimals.Build();
    }

    private FunctionDefinition SymbolFunctionDefinition()
    {
        var symbol = new SymbolFunction();
        return symbol.Build();
    }

    private FunctionDefinition NameFunctionDefinition()
    {
        var name = new NameFunction();
        return name.Build();
    }

    private FieldDefinition TokenBalancesDefinition()
    {
        var tokenBalance = new TokenBalancesField();
        return tokenBalance.Build();
    }

    private FieldDefinition TokenAllowancesDefinition()
    {
        var tokenAllowance = new TokenAllowancesField();
        return tokenAllowance.Build();
    }

    private FunctionDefinition BalanceOfFunctionDefinition()
    {
        var balanceOf = new BalanceOfFunction();
        return balanceOf.Build();
    }

    private FunctionDefinition AllowanceFunctionDefinition()
    {
        var allowance = new AllowanceFunction();
        return allowance.Build();
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

    private FunctionDefinition FirstApproveFunctionDefinition()
    {
        var approve = new FirstApproveFunction();
        return approve.Build();
    }

    private FunctionDefinition _UpdateFunctionDefinition()
    {
        var update = new _UpdateFunction();
        return update.Build();
    }

    private FunctionDefinition _ApproveFunction()
    {
        var approve = new _ApproveFunction();
        return approve.Build();
    }

    private FunctionDefinition SpendAllowanceDefinition()
    {
        var spendAllowance = new SpendAllowanceFunction();
        return spendAllowance.Build();
    }
}