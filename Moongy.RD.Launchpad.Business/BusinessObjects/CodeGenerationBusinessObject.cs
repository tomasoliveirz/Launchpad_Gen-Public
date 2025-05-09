using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Business.Models;
using Moongy.RD.Launchpad.Data.Forms;
using Moongy.RD.Launchpad.SmartContractGenerator.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects
{
    public class CodeGenerationBusinessObject(ISmartContractGenerator generator) : BaseBusinessObject, ICodeGenerationBusinessObject
    {

        public async Task<OperationResult<GenerationResult<FungibleTokenForm>>> GenerateFungibleToken(FungibleTokenForm form)
        {
            return ExecuteOperation(async () =>
            {
                return null;
            });
        }

        public async Task<OperationResult<GenerationResult<GovernorForm>>> GenerateGovernor(GovernorForm form)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<GenerationResult<NonFungibleTokenForm>>> GenerateNonFungibleToken(NonFungibleTokenForm form)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<GenerationResult<RealWorldAssetForm>>> GenerateRealWorldAsset(RealWorldAssetForm form)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<GenerationResult<SemiFungibleTokenForm>>> GenerateSemiFungibleToken(SemiFungibleTokenForm form)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<GenerationResult<StableCoinForm>>> GenerateStablecoin(StableCoinForm form)
        {
            throw new NotImplementedException();
        }
    }
}
