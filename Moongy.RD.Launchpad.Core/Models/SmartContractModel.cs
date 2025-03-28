namespace Moongy.RD.Launchpad.Core.Models;
public class SmartContractModel
{
    String? Licence { get; set; }
    SoftwareVersion? Version { get; set; }
    List<Import> Imports { get; set; } = [];
}
