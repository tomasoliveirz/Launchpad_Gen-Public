using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public class AccessControlExtensionExtractor(ExtensionEnum extension) : BaseExtensionExtractor<AccessControlExtensionModel>(extension)
{
    public override bool CanHandle(object extensionFormSection)
    {
        return extensionFormSection.HasExtensionSource(ExtensionEnum.AccessControl);
    }
    
    
    // uses the base extractor to extract the common properties;
    // add specific logic to process the roles list
    // converts generic collections to a specific List<string>
    // also checks if HasRoles is true before processing the roles
    // and uses a pattern matching is IEnumerable<string> to convert the roles to a list
    public override object Extract(object extensionFormSection)
    {
        var model = (AccessControlExtensionModel)base.Extract(extensionFormSection);
        
        var rolesProp = extensionFormSection.GetType().GetProperty("Roles");
        if (model.HasRoles && rolesProp?.GetValue(extensionFormSection) is IEnumerable<string> roles)
        {
            model.Roles = roles.ToList();
        }
        return model;
    }
}