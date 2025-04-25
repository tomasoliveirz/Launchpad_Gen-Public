using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Core.ExtensionMethods;
using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Data.Pocos;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Tools.TokenWeighter;
using Moongy.RD.Launchpad.Tools.TokenWeighter.Models;
using Moongy.RD.Launchpad.Tools.TokenWizard;
using Moongy.RD.Launchpad.Tools.TokenWizard.Models;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;
public class FormsBusinessObject(ITokenWizard tokenWizard, ITokenWeighter tokenWeighter) : BaseBusinessObject, IFormsBusinessObject
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

    public async Task<OperationResult<TokenWizardResponse>> GetToken(TokenWizardRequest request)
    {
        return await ExecuteOperation(async () =>
        {
            return await Task.Run(() =>
            {
                return tokenWizard.GetToken(request);
            });
        });
    }

    public Task<OperationResult<TokenWeighterResponse>> GetTokenWeight(TokenWeighterRequest request)
    {
        throw new NotImplementedException();
    }
}
