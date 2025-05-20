using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

public class AccessControlExtensionModel
{
    [ContextProperty(Name = "owner", Type = PrimitiveType.Address, Visibility = Visibility.Public)]
    public string? Owner { get; set; }
    
    [ContextProperty(Name = "hasRoles", Type = PrimitiveType.Bool, Visibility = Visibility.Public)]
    public bool HasRoles { get; set; }
    
    // this does not go directly to the context -- the augmenter will probably convert it to a mapping
    public List<string> Roles { get; set; }
}