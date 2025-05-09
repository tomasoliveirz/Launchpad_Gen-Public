using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Models;
using Moongy.RD.Launchpad.Data.Forms;

namespace Moongy.RD.Launchpad.Business.Interfaces
{
    public interface ICodeGenerationBusinessObject
    {
        public Task<OperationResult<GenerationResult<FungibleTokenForm>>> GenerateFungibleToken(FungibleTokenForm form);
        public Task<OperationResult<GenerationResult<SemiFungibleTokenForm>>> GenerateSemiFungibleToken(SemiFungibleTokenForm form);
        public Task<OperationResult<GenerationResult<NonFungibleTokenForm>>> GenerateNonFungibleToken(NonFungibleTokenForm form);
        public Task<OperationResult<GenerationResult<RealWorldAssetForm>>> GenerateRealWorldAsset(RealWorldAssetForm form);
        public Task<OperationResult<GenerationResult<StableCoinForm>>> GenerateStablecoin(StableCoinForm form);
        public Task<OperationResult<GenerationResult<GovernorForm>>> GenerateGovernor(GovernorForm form);
    }
}
