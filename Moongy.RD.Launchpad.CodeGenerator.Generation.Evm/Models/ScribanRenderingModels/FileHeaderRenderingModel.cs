namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;
public class FileHeaderRenderingModel
{
    public required string Version { get; set; }
    public required string License { get; set; }
    public bool HasLicense => License != null;
}
