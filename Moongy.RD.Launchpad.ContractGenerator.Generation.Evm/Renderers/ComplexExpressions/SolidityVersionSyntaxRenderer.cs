

using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Expressions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.Core.Models.Metamodel;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.ComplexExpressions
{
    public class SolidityVersionSyntaxRenderer : BaseSoliditySyntaxRenderer<VersionModel>
    {
        public override string Render(VersionModel model)
        {
            if (model.Minimum is null && model.Maximum is null)
                throw new ArgumentException("At least one of Minimum or Maximum version must be specified.", nameof(model));

            var min = VersionToString(model.Minimum);
            var max = VersionToString(model.Maximum);

            if (min == max) return $"{SoliditySymbols.SpecificVersion}{min}";

            var versionRestrains = new List<string>();
            if (!string.IsNullOrEmpty(min))
                versionRestrains .Add($">= {min}");
            if (!string.IsNullOrEmpty(max))
                versionRestrains.Add($"<= {max}");
            return string.Join(" ", versionRestrains);
        }

        private static string VersionToString(SoftwareVersion? version) => version == null? string.Empty : $"{version.Major}.{version.Minor}.{version.Revision}";
    }
}
