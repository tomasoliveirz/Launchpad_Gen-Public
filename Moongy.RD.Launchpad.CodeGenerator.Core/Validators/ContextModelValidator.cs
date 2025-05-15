using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Interfaces;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators;

public class ContextModelValidator<T> : LaunchpadValidator<T>
{
    public static void ValidateDuplicateInterfaces(List<InterfaceDefinition> ifaces)
    {
        var dups = ifaces
            .GroupBy(i => i.Name, StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        if (dups.Count != 0)
            throw new System.ComponentModel.DataAnnotations.ValidationException(
                $"Duplicate interface names found: {string.Join(", ", dups)}");
    }
}
