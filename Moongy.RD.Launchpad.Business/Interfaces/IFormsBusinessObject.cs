using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.Business.Interfaces
{
    public interface IFormsBusinessObject
    {
        public Task<OperationResult<SelectOptions>> GetVotingOptions();
        public Task<OperationResult<SelectOptions>> GetAccessOptions();
        public Task<OperationResult<SelectOptions>> GetUpgradeabilityOptions();
    }
}
