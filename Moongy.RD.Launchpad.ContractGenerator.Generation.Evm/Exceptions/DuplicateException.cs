namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Exceptions;

public class DuplicateException(string type, string name, string group) : Exception($"{type} {name} has duplicate {group}")
{
}
