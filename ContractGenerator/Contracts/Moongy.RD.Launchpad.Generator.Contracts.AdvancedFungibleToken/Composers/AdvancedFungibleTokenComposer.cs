using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Validators;


namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Composers
{
    public class AdvancedFungibleTokenComposer : FungibleTokenComposer, IAdvancedFungibleTokenComposer
    {
        private readonly ExecutionComposer _executionComposer = new();

        public new SmartContractModel Compose(AdvancedFungibleTokenModel tokenModel)
        {
            Validate(tokenModel);

            var smartContractModel = base.Compose(tokenModel);

            if (tokenModel.PreTransferHooks != null && tokenModel.PreTransferHooks.Any())
            {
                smartContractModel.Executions.AddRange(tokenModel.PreTransferHooks.Select(hook => _executionComposer.Compose(hook)));

                foreach (var hook in tokenModel.PreTransferHooks)
                {
                    smartContractModel.SmartContractFunctions.Add(new SmartContractFunction
                    {
                        Name = $"_preTransferHook_{hook.Name}",
                        Visibility = Visibility.Internal,
                        Statements = new List<Statement>
                        {
                            new InvocationStatement { ExecutionName = hook.Name }
                        }
                    });
                }
            }

            if (tokenModel.PostTransferHooks != null && tokenModel.PostTransferHooks.Any())
            {
                smartContractModel.Executions.AddRange(tokenModel.PostTransferHooks.Select(hook => _executionComposer.Compose(hook)));

                foreach (var hook in tokenModel.PostTransferHooks)
                {
                    smartContractModel.SmartContractFunctions.Add(new SmartContractFunction
                    {
                        Name = $"_postTransferHook_{hook.Name}",
                        Visibility = Visibility.Internal,
                        Statements = new List<Statement>
                        {
                            new InvocationStatement { ExecutionName = hook.Name }
                        }
                    });
                }
            }

            return smartContractModel;
        }

        public new void Validate(AdvancedFungibleTokenModel tokenModel)
        {
            AdvancedFungibleTokenValidator _validator = new();
            _validator.Validate(tokenModel);
        }
    }
    public class ExecutionComposer
    {
        public Execution Compose(Execution executionModel)
        {
            if (executionModel == null || string.IsNullOrEmpty(executionModel.Name))
            {
                return null;
            }
            return new Execution
            {
                Name = executionModel.Name,
            };
        }
    }
}
