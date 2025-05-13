using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Directives;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Imports;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;
using Moongy.RD.Launchpad.Core.Validators;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators;
public class ModuleFileValidator : LaunchpadValidator<ModuleFileDefinition>
{
    private DirectiveValidator _directiveValidator;
    private ImportValidator _importValidator;
    private EnumValidator _enumValidator;
    private StructValidator _structValidator;
    private InterfaceValidator _interfaceValidator;
    private ModuleValidator _moduleValidator;

    public override void Validate(ModuleFileDefinition o)
    {
        base.Validate(o);
        ValidateDuplicates(o);
        o.Directives.ForEach(ValidateDirective);
        o.Imports.ForEach(ValidateImport);
        o.Enums.ForEach(ValidateEnum);
        o.Structs.ForEach(ValidateStruct);
        o.Interfaces.ForEach(ValidateInterface);
        o.Modules.ForEach(ValidateModule);
    }

    private void ValidateDirective(DirectiveDefinition d)
    {
        _directiveValidator ??= new DirectiveValidator();
        _directiveValidator.Validate(d);
    }

    private void ValidateImport(ImportDefinition i)
    {
        _importValidator ??= new ImportValidator();
        _importValidator.Validate(i);
    }

    private void ValidateEnum(EnumDefinition e)
    {
        _enumValidator ??= new EnumValidator();
        _enumValidator.Validate(e);
    }

    private void ValidateStruct(StructDefinition s)
    {
        _structValidator ??= new StructValidator();
        _structValidator.Validate(s);
    }

    private void ValidateInterface(InterfaceDefinition iface)
    {
        _interfaceValidator ??= new InterfaceValidator();
        _interfaceValidator.Validate(iface);
    }

    private void ValidateModule(ModuleDefinition m)
    {
        _moduleValidator ??= new ModuleValidator();
        _moduleValidator.Validate(m);
    }


    private void ValidateDuplicates(ModuleFileDefinition o)
    {
        ValidateDuplicateDirectives(o.Directives);
        ValidateDuplicateImports(o.Imports);

        var enums = new List<List<EnumDefinition>>();
        var structs = new List<List<StructDefinition>>();
        foreach (var module in o.Modules)
        {
            var tempEnums = new List<EnumDefinition>();
            tempEnums.AddRange(module.Enums);
            tempEnums.AddRange(o.Enums);
            enums.Add(tempEnums);

            var tempStructs = new List<StructDefinition>();
            tempStructs.AddRange(module.Structs);
            tempStructs.AddRange(o.Structs);
            structs.Add(tempStructs);
        }
        foreach (var enumItem in enums)
        {
            ValidateDuplicateEnums(enumItem);
        }

        foreach (var structItem in structs)
        {
            ValidateDuplicateStructs(structItem);
        }

        ValidateDuplicateInterfaces(o.Interfaces);
        ValidateDuplicateModules(o.Modules);
    }

    private void ValidateDuplicateDirectives(List<DirectiveDefinition> directives)
    {
        var dupKinds = directives
        .GroupBy(x => x.Kind)
        .Where(g => g.Count() > 1)
        .Select(g => g.Key)
        .ToList();

        if (dupKinds.Count != 0)
        {
            var list = string.Join(", ", dupKinds);
            throw new ValidationException($"Duplicate directives found for kinds: {list}");
        }
    }

    private void ValidateDuplicateImports(List<ImportDefinition> imports)
    {
        var byPath = imports.GroupBy(i => i.Path, StringComparer.OrdinalIgnoreCase);
        foreach (var group in byPath)
        {
            var path = group.Key;

            var bareCount = group.Count(i => string.IsNullOrWhiteSpace(i.Name) && string.IsNullOrWhiteSpace(i.Alias));
            if (bareCount > 1)
                throw new ValidationException(
                    $"Multiple imports from '{path}' without name or alias are not allowed.");

            var noNameAliased = group.Where(i => string.IsNullOrWhiteSpace(i.Name) && !string.IsNullOrWhiteSpace(i.Alias));
            if (noNameAliased.Count() > 1)
            {
                var aliases = string.Join(", ", noNameAliased.Select(i => i.Alias));
                throw new ValidationException(
                    $"Multiple imports from '{path}' with no name but aliases [{aliases}] are not allowed.");
            }

            var aliasDuplicates = group
                .Where(i => !string.IsNullOrWhiteSpace(i.Alias))
                .GroupBy(i => i.Alias!, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (aliasDuplicates.Any())
            {
                throw new ValidationException(
                    $"Duplicate import aliases for '{path}': {string.Join(", ", aliasDuplicates)}");
            }

            var nameDuplicates = group
                .Where(i => !string.IsNullOrWhiteSpace(i.Name))
                .GroupBy(i => i.Name!, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (nameDuplicates.Any())
            {
                throw new ValidationException(
                    $"Duplicate import names for '{path}': {string.Join(", ", nameDuplicates)}");
            }

        }
    }

    private void ValidateDuplicateEnums(List<EnumDefinition> enums)
    {
        var dups = enums
            .GroupBy(e => e.Name, StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        if (dups.Any())
            throw new ValidationException(
                $"Duplicate enum names found: {string.Join(", ", dups)}");
    }

    private void ValidateDuplicateStructs(List<StructDefinition> structs)
    {
        var dups = structs
            .GroupBy(s => s.Name, StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        if (dups.Any())
            throw new ValidationException(
                $"Duplicate struct names found: {string.Join(", ", dups)}");
    }

    private void ValidateDuplicateInterfaces(List<InterfaceDefinition> ifaces)
    {
        var dups = ifaces
            .GroupBy(i => i.Name, StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        if (dups.Any())
            throw new ValidationException(
                $"Duplicate interface names found: {string.Join(", ", dups)}");
    }

    private void ValidateDuplicateModules(List<ModuleDefinition> modules)
    {
        var dups = modules
            .GroupBy(m => m.Name, StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        if (dups.Any())
            throw new ValidationException(
                $"Duplicate module (contract/library/interface) names found: {string.Join(", ", dups)}");
    }
}
