namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

public class AccessControlDefinition
{
    public string? OwnerIdentifier { get; set; }
    public List<string> Roles { get; set; } = [];
}
