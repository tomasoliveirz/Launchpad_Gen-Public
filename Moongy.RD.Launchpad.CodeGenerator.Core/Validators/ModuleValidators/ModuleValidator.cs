using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;
using Moongy.RD.Launchpad.CodeGenerator.Core.Validators.FileComponents;
using Moongy.RD.Launchpad.CodeGenerator.Core.Validators.Functions;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators.ModuleValidators
{
    public class ModuleValidator : ContextModelValidator<ModuleDefinition>
    {
        private StructValidator? _structValidator;
        private EnumValidator? _enumValidator;
        private InterfaceValidator? _interfaceValidator;
        private FunctionValidator? _functionValidator;

        public override void Validate(ModuleDefinition m)
        {
            base.Validate(m);

            if (string.IsNullOrWhiteSpace(m.Name))
                throw new ValidationException("Module must have a name.");

            var dupBases = m.BaseModules
                .GroupBy(b => b.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (dupBases.Count != 0)
                throw new ValidationException(
                    $"Duplicate base modules in '{m.Name}': {string.Join(", ", dupBases)}");

            var dupIfaces = m.Implements
                .GroupBy(i => i.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (dupIfaces.Count != 0)
                throw new ValidationException(
                    $"Duplicate interfaces in '{m.Name}': {string.Join(", ", dupIfaces)}");

            foreach (var s in m.Structs)
            {
                _structValidator ??= new StructValidator();
                _structValidator.Validate(s);
            }

            foreach (var e in m.Enums)
            {
                _enumValidator ??= new EnumValidator();
                _enumValidator.Validate(e);
            }

            foreach (var iface in m.Implements)
            {
                _interfaceValidator ??= new InterfaceValidator();
                _interfaceValidator.Validate(iface);
            }

            foreach (var f in m.Functions)
            {
                _functionValidator ??= new FunctionValidator();
                _functionValidator.Validate(f);
            }

            var dupStructNames = m.Structs
                .GroupBy(s => s.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (dupStructNames.Count != 0)
                throw new ValidationException(
                    $"Duplicate struct definitions in '{m.Name}': {string.Join(", ", dupStructNames)}");

            var dupEnumNames = m.Enums
                .GroupBy(e => e.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (dupEnumNames.Count != 0)
                throw new ValidationException(
                    $"Duplicate enum definitions in '{m.Name}': {string.Join(", ", dupEnumNames)}");

            var dupFields = m.Fields
                .GroupBy(f => f.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (dupFields.Count != 0)
                throw new ValidationException(
                    $"Duplicate fields in '{m.Name}': {string.Join(", ", dupFields)}");

            var dupFunctionNames = m.Functions
                .GroupBy(f => f.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (dupFunctionNames.Count != 0)
                throw new ValidationException(
                    $"Duplicate functions in '{m.Name}': {string.Join(", ", dupFunctionNames)}");
        }
    }
}
