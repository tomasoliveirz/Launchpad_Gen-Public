using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Data.Pocos;

namespace Moongy.RD.Launchpad.Business.Interfaces
{
    public interface IFormsBusinessObject
    {
        public Task<OperationResult<SelectOptions>> GetVotingOptions();
        public Task<OperationResult<SelectOptions>> GetAccessOptions();
        public Task<OperationResult<SelectOptions>> GetUpgradeabilityOptions();
        public Task<OperationResult<TokenWizardResponse>> GetToken(TokenWizardRequest request);
        public Task<OperationResult<TokenWeighterResponse>> GetTokenWeight(TokenWeighterRequest request);
    }
}
