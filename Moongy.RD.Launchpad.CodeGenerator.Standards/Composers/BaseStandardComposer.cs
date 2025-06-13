using System.Reflection;
using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;
using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers
{
    public abstract class BaseStandardComposer<T> : IStandardComposer<T> where T : BaseContractModel
    {
        public virtual ModuleFileDefinition Compose(T standard)
        {
            var fields = GenerateFieldDefinitions(standard);

            var module = new ModuleDefinition
            {
                Name = standard.Name!,
                Fields = fields
            };

            return new ModuleFileDefinition
            {
                Modules = new List<ModuleDefinition> { module }
            };
        }

        protected List<FieldDefinition> GenerateFieldDefinitions(T standard)
        {
            var fields = new List<FieldDefinition>();
            foreach (var prop in typeof(T).GetProperties())
            {
                Console.WriteLine($"Processing property: {prop.Name}, Type: {prop.PropertyType.Name}, Value: {prop.GetValue(standard)}");
                var attr = prop.GetCustomAttribute<ContextPropertyAttribute>(inherit: true);
                if (attr == null) continue;
                var field = new FieldDefinition
                {
                    Name = attr.Name ?? prop.Name,
                    Type = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = attr.Type },
                    Visibility = attr.Visibility,
                    IsImmutable = false
                };

                if (attr.HasDefaultValue)
                {
                    var value = prop.GetValue(standard);
                    if (value == null) continue;
                    field.Value = value.ToString();
                }
                fields.Add(field);
            }
            return fields;
        }
    }
}
