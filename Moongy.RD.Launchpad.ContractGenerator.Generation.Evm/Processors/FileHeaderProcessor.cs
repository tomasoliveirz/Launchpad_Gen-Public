using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Expressions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Version;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;
using Moongy.RD.Launchpad.Core.Helpers;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public class FileHeaderProcessor() : BaseSolidityTemplateProcessor<SolidityFile>("FileHeader")
    {
        public override string Render(SolidityFile model)
        {
            var renderModel = Transform(model);
            return Render(renderModel);
        }

        private static FileHeaderRenderingModel Transform(SolidityFile model)
        {
            var version = TransformVersion(model.FileHeader.Version);
            var license = SPDXLicenseHelper.GetValue(model.FileHeader.License);
            return new() { License = license, Version = version };
        }

        private static string VersionToString(SoftwareVersion? version) => version == null ? string.Empty : $"{version.Major}.{version.Minor}.{version.Revision}";

        private static string TransformVersion(VersionModel model)
        {
            if (model.Minimum is null && model.Maximum is null)
                throw new ArgumentException("At least one of Minimum or Maximum version must be specified.", nameof(model));

            var min = VersionToString(model.Minimum);
            var max = VersionToString(model.Maximum);

            if (min == max) return $"{SoliditySymbols.SpecificVersion}{min}";

            var versionRestrains = new List<string>();
            if (!string.IsNullOrEmpty(min))
                versionRestrains.Add($">= {min}");
            if (!string.IsNullOrEmpty(max))
                versionRestrains.Add($"<= {max}");
            return string.Join(" ", versionRestrains);
        }
    }
}