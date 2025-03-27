using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Core.ExtensionMethods;
using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;
public class FormsBusinessObject : BaseBusinessObject, IFormsBusinessObject
{
    public async Task<OperationResult<SelectOptions>> GetAccessOptions()
    {
        return await ExecuteOperation(async () =>
        {
            return await Task.Run(() => {
                var result = new SelectOptions();
                result.IsMandatory = false;
                result.Options = EnumExtensionMethods.ToOptionLabelValue<AccessEnum>();
                return result;
            });
        });
    }

    public async Task<OperationResult<SelectOptions>> GetVotingOptions()
    {
        return await ExecuteOperation(async () =>
        {
            return await Task.Run(() => {
                var result = new SelectOptions();
                result.IsMandatory = false;
                result.Options = EnumExtensionMethods.ToOptionLabelValue<VotingEnum>();

                return result;
            });
        });
    }

    public async Task<OperationResult<SelectOptions>> GetUpgradeabilityOptions()
    {
        return await ExecuteOperation(async () =>
        {
            return await Task.Run(() => {
                var result = new SelectOptions();
                result.IsMandatory = false;
                result.Options = EnumExtensionMethods.ToOptionLabelValue<UpgradeabilityEnum>();

                return result;
            });
        });
    }
}
