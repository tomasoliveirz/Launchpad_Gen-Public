namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;
public class FileHeaderRenderingModel
{
    public required string Version { get; set; }
    public required string License { get; set; }
    public bool HasLicense => License != null;
}
