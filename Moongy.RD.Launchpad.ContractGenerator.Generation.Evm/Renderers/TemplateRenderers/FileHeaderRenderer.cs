using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.ComplexExpressions;
using Moongy.RD.Launchpad.Core.Helpers;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.Templates
{
    public class FileHeaderRenderer : BaseTemplateRenderer<FileHeaderModel>
    {
        public FileHeaderRenderer() : base("FileHeader")
        {

        }

        public override string Render(FileHeaderModel model)
        {
            var version = SoliditySyntaxRenderer.Version.Render(model.Version);
            var license = SPDXLicenseHelper.GetValue(model.License);
            return Render(new { version, license });
        }
    }
}
