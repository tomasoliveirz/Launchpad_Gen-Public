using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Imports;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers;
public class SolidityImportSyntaxRenderer : BaseSoliditySyntaxRenderer<ImportModel>
{
    public override string Render(ImportModel model)
    {
        return RenderImport(model);
    }

    public static string RenderImports(ImportModel[] imports)
    {
        var importsByPath = new Dictionary<string, List<string>>();
        var fullFileImports = new HashSet<string>();

        foreach (var import in imports)
        {
            if (import.PathName is null) continue;

            switch (import)
            {
                case AbstractionImportModel abstraction:
                    AddPartialImport(import.PathName, abstraction.Name);
                    break;
                case InterfaceImportModel @interface:
                    AddPartialImport(import.PathName, @interface.Name);
                    break;
                case TypeUtilityImportModel typeUtility:
                    AddPartialImport(import.PathName, typeUtility.Name);
                    break;
                default:
                    fullFileImports.Add(import.PathName);
                    break;
            }
        }

        var lines = new List<string>();

        foreach (var path in fullFileImports)
        {
            lines.Add($"import \"{path}\";");
        }

        foreach (var kvp in importsByPath)
        {
            var path = kvp.Key;
            var names = string.Join(", ", kvp.Value.Distinct());
            lines.Add($"import {{ {names} }} from \"{path}\";");
        }

        return string.Join(Environment.NewLine, lines);

        void AddPartialImport(string path, string name)
        {
            if (!importsByPath.TryGetValue(path, out var list))
            {
                list = new List<string>();
                importsByPath[path] = list;
            }
            list.Add(name);
        }
    }

    public static string RenderImport(ImportModel model)
    {
        return model switch
        {
            AbstractionImportModel abstraction => RenderAbstraction(abstraction),
            InterfaceImportModel @interface => RenderInterface(@interface),
            TypeUtilityImportModel typeUtility => RenderTypeUtility(typeUtility),
            _ => throw new NotSupportedException("Unknown type reference")
        };
    }

    private static string RenderTypeUtility(TypeUtilityImportModel typeUtility)
    {
        return $"{typeUtility.Name} for {RenderTypeReference(typeUtility.Type)}";
    }

    private static string RenderInterface(InterfaceImportModel @interface)
    {
        return @interface.Name;
    }

    private static string RenderAbstraction(AbstractionImportModel abstraction)
    {
        return abstraction.Name;
    }

    private static string RenderTypeReference(TypeReference typeReference)
    {
        return SolidityReferenceTypeSyntaxRenderer.RenderTypeReference(typeReference);
    }
}
