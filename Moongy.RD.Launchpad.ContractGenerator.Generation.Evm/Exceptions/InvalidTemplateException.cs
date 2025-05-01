namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Exceptions;

public class InvalidTemplateException(string path) : Exception($"No template found at {path}. Make sure to check it as Embedded on the Build Action")
{
}
