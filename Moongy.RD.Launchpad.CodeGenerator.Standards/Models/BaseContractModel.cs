using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
public class BaseContractModel
{
    [Required(Name="Name")]
    [ContextProperty(Name = "_name", Type = PrimitiveType.String, Visibility = Visibility.Private, HasDefaultValue = true)]
    public string? Name { get; set; }
}
