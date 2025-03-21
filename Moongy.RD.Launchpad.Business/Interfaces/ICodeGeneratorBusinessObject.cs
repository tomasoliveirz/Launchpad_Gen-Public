using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.ContractModels;
using Moongy.RD.Launchpad.Data.Pocos;

namespace Moongy.RD.Launchpad.Business.Interfaces
{
    public interface ICodeGeneratorBusinessObject
    {
        Task<OperationResult<CodeGenerationResult<FungibleTokenModel>>> Generate(FungibleTokenModel model);
        //Task<OperationResult<CodeGenerationResult<NonFungibleTokenModel>>> Generate(NonFungibleTokenModel model);
    }
}
