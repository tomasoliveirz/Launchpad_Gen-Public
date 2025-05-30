using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Expressions;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Version;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Processors
{
    public class FileHeaderProcessor() : BaseSolidityTemplateProcessor<SolidityFileModel>("FileHeader")
    {
        public override string Render(SolidityFileModel model)
        {
            var renderModel = Transform(model);
            return Render(renderModel);
        }

        private static FileHeaderRenderingModel Transform(SolidityFileModel model)
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