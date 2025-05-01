namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Exceptions;
public class ErrorParsingTemplateException(string path, string message) : Exception($"Error parsing {path} template: {message}")
{
}
