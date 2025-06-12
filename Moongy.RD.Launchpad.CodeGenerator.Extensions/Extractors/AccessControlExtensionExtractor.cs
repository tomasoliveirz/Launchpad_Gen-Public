using System.Collections;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public class AccessControlExtensionExtractor : BaseExtensionExtractor<AccessControlExtensionModel>
{
    // uses the base extractor to extract the common properties;
    // add specific logic to process the roles list
    // converts generic collections to a specific List<string>
    // also checks if HasRoles is true before processing the roles
    // and uses a pattern matching is IEnumerable<string> to convert the roles to a list
    public override AccessControlExtensionModel Extract(object extensionFormSection)
    {
        var model = base.Extract(extensionFormSection) ?? new AccessControlExtensionModel();

        var rolesProp  = extensionFormSection.GetType().GetProperty("Roles");
        var rawRoles   = rolesProp?.GetValue(extensionFormSection) as IEnumerable;
        
        if (rawRoles != null)
            model.Roles = rawRoles.Cast<string>().ToList();
        
        if (model.Roles.Any())
            model.HasRoles = true;        

        return model;
    }

}