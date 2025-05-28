using Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public class PermissionExtensionExtractor : BaseExtensionExtractor<PermissionExtensionModel>
{
    public override PermissionExtensionModel? Extract(object form)
    {
        var model = base.Extract(form);
        var hasPermission = form.IsExtensionActive(Enums.ExtensionEnum.Permission);
        return model != null ? model : hasPermission ? new PermissionExtensionModel() : null;
    }
}
