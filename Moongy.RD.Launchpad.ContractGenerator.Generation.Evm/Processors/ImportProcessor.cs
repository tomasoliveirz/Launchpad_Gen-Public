using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public class ImportProcessor() : BaseSolidityTemplateProcessor<SolidityFile>("Imports")
    {
        public override string Render(SolidityFile model)
        {
            var renderModel = Transform(model);
            return Render(new { imports = renderModel });
        }

        #region Model Transformations
        private static List<ImportRenderModel> Transform(SolidityFile model)
        {
            var imports = GetModels(model);
            var result = new List<ImportRenderModel>();

            foreach (var import in imports)
            {
                if (import.PathName is null) continue;
                switch (import)
                {
                    case AbstractionImportModel abstraction:
                        AddSpecificImport(abstraction.PathName!, abstraction.Name);
                        break;
                    case InterfaceImportModel @interface:
                        AddSpecificImport(import.PathName, @interface.Name);
                        break;
                    case TypeUtilityImportModel typeUtility:
                        AddSpecificImport(import.PathName, typeUtility.Name);
                        break;
                    default:
                        AddImport(import.PathName, import.Alias);
                        break;
                }
            }

            void AddImport(string path, string? alias) 
            {
                if (result.Any(x => x.Path == path)) return;
                result.Add(new() { Path = path, Alias = alias });
            }

            void AddSpecificImport(string path, string name)
            {
                var item = result.FirstOrDefault(x => x.Path == path);
                if (item == null) result.Add(new() { Path = path, NamedElements = [name] });
                else if (item.NamedElements.Any(x => x == name)) return;
                else item.NamedElements.Add(name);
            }

            return result;
        }

        private static List<ImportModel> GetModels(SolidityFile model) 
        {
            var imports = model.Contracts.SelectMany(x => x.Imports).ToList();
            imports.AddRange(model.Contracts.SelectMany(x => x.BaseContracts));
            imports.AddRange(model.Contracts.SelectMany(x => x.Imports));
            imports.AddRange(model.Contracts.SelectMany(x => x.TypeUtilities));
            return imports;
        }
        #endregion
    }
}
