namespace Moongy.RD.Launchpad.Core.Models.Metamodel
{
    public class FileProgram
    {
        SoftwareLicense Licence { get; set; }
        SoftwareVersion? Version { get; set; }
        List<SmartContractModel> SmartContractModels { get; set; } = [];

    }
}
