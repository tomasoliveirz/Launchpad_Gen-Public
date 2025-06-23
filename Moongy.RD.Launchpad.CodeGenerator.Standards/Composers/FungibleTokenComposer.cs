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
        
        AddERC20Storage(moduleFile, standard);
        AddERC20Events(moduleFile);
        AddERC20Errors(moduleFile);
        AddERC20Functions(moduleFile, standard);
        
        return moduleFile;
    }

    private void AddERC20Storage(ModuleFileDefinition moduleFile, FungibleTokenModel standard)
    {
        var mainModule = moduleFile.Modules.FirstOrDefault();
        if (mainModule == null) return;
        
        mainModule.Fields.Add(TokenBalancesDefinition());
        mainModule.Fields.Add(TokenAllowancesDefinition());
        mainModule.Fields.Add(TokenTotalSupplyDefinition());
    }

    private void AddERC20Events(ModuleFileDefinition moduleFile)
    {
        var mainModule = moduleFile.Modules.FirstOrDefault();
        if (mainModule == null) return;

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

        mainModule.Triggers.Add(transferEvent);
        mainModule.Triggers.Add(approvalEvent);
    }

    private void AddERC20Errors(ModuleFileDefinition moduleFile)
    {
        var mainModule = moduleFile.Modules.FirstOrDefault();
        if (mainModule == null) return;

        var errors = new[]
        {
            new TriggerDefinition
            {
                Name = "ERC20InsufficientBalance",
                Kind = TriggerKind.Error,
                Parameters = new List<ParameterDefinition>
                {
                    new() { Name = "sender", Type = DataTypeReference.Address },
                    new() { Name = "balance", Type = DataTypeReference.Uint256 },
                    new() { Name = "needed", Type = DataTypeReference.Uint256 }
                }
            },
            new TriggerDefinition
            {
                Name = "ERC20InvalidApprover",
                Kind = TriggerKind.Error,
                Parameters = new List<ParameterDefinition>
                {
                    new() { Name = "approver", Type = DataTypeReference.Address }
                }
            },
            new TriggerDefinition
            {
                Name = "ERC20InvalidSpender",
                Kind = TriggerKind.Error,
                Parameters = new List<ParameterDefinition>
                {
                    new() { Name = "spender", Type = DataTypeReference.Address }
                }
            },
            new TriggerDefinition
            {
                Name = "ERC20InsufficientAllowance",
                Kind = TriggerKind.Error,
                Parameters = new List<ParameterDefinition>
                {
                    new() { Name = "spender", Type = DataTypeReference.Address },
                    new() { Name = "allowance", Type = DataTypeReference.Uint256 },
                    new() { Name = "needed", Type = DataTypeReference.Uint256 }
                }
            },
            new TriggerDefinition
            {
                Name = "ERC20InvalidSender",
                Kind = TriggerKind.Error,
                Parameters = new List<ParameterDefinition>
                {
                    new() { Name = "sender", Type = DataTypeReference.Address }
                }
            },
            new TriggerDefinition
            {
                Name = "ERC20InvalidReceiver",
                Kind = TriggerKind.Error,
                Parameters = new List<ParameterDefinition>
                {
                    new() { Name = "receiver", Type = DataTypeReference.Address }
                }
            },
            new TriggerDefinition
            {
                Name = "ERC20ExceedsMaxSupply",
                Kind = TriggerKind.Error,
                Parameters = new List<ParameterDefinition>
                {
                    new() { Name = "totalSupply", Type = DataTypeReference.Uint256 },
                    new() { Name = "maxSupply", Type = DataTypeReference.Uint256 }
                }
            }
        };

        foreach (var error in errors)
        {
            mainModule.Triggers.Add(error);
        }
    }

    private void AddERC20Functions(ModuleFileDefinition moduleFile, FungibleTokenModel standard)
    {
        var mainModule = moduleFile.Modules.FirstOrDefault();
        if (mainModule == null) return;

        // Constructor - ONLY premint if specified 
        mainModule.Functions.Add(ConstructorDefinition(standard.Premint));
        
        // View functions
        mainModule.Functions.Add(NameFunctionDefinition());
        mainModule.Functions.Add(SymbolFunctionDefinition());
        mainModule.Functions.Add(DecimalsFunctionDefinition());
        mainModule.Functions.Add(TotalSupplyFunctionDefinition());
        mainModule.Functions.Add(BalanceOfFunctionDefinition());
        mainModule.Functions.Add(AllowanceFunctionDefinition());
        
        // State-changing functions
        mainModule.Functions.Add(TransferFunctionDefinition());
        mainModule.Functions.Add(ApproveFunctionDefinition());
        mainModule.Functions.Add(TransferFromFunctionDefinition());
        
        // Internal functions
        mainModule.Functions.Add(_TransferFunctionDefinition());
        mainModule.Functions.Add(_UpdateFunctionDefinition());
        mainModule.Functions.Add(_ApproveFunction());
        mainModule.Functions.Add(SpendAllowanceDefinition());
        mainModule.Functions.Add(_MintFunctionDefinition());
        mainModule.Functions.Add(_BurnFunctionDefinition());
    }

    #region Function Builders

    private FunctionDefinition ConstructorDefinition(ulong premintValue)
    {
        var constructor = new ERC20Constructor();
        return constructor.Build(premintValue);
    }

    private FunctionDefinition _MintFunctionDefinition()
    {
        var mint = new MintFunction();
        return mint.Build();
    }

    private FunctionDefinition _BurnFunctionDefinition()
    {
        var burn = new BurnFunction();
        return burn.Build();
    }

    private FunctionDefinition TransferFunctionDefinition()
    {
        var transfer = new TransferFunction(); 
        return transfer.Build();
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

    #endregion

    #region Field Builders

    private FieldDefinition TokenTotalSupplyDefinition() 
    {
        var totalSupply = new TokenTotalSupplyField();
        return totalSupply.Build();
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

    #endregion
}