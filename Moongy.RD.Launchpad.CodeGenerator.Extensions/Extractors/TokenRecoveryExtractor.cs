using Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Validators;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;
public class TokenRecoveryExtractor : BaseExtensionExtractor<TokenRecoveryExtensionModel>
{
    public override TokenRecoveryExtensionModel? Extract(object form)
    {
        var model = base.Extract(form);
        var responsible = form.GetExtensionValue<string>(Enums.ExtensionEnum.TokenRecovery);
        if (string.IsNullOrEmpty(responsible)) return null;
        if(model == null) model = new TokenRecoveryExtensionModel();
        if (responsible.IsEthAddress()) model.RecoveryAddress = responsible;
        else model.RecoveryRole = responsible;
        return model;
    }
}
