using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.ContractModels;
using Moongy.RD.Launchpad.Data.Pocos;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Extensions.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.SmartContractGenerator.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects
{
    public class CodeGeneratorBusinessObject(ISmartContractGenerator generator) : ICodeGeneratorBusinessObject
    {
        public Task<OperationResult<CodeGenerationResult<FungibleTokenForm>>> Generate(FungibleTokenForm form)
        {
            var model = ToModel<FungibleTokenModel>(form);
            var tokenomics = ExtractTokenomics(form);
            var extensions = ExtractExtensions(form);
            var result = generator.Generate(model);
        }



        
    }
}
