using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public class FileHeader() : BaseSolidityTemplateProcessor<SolidityFile>("FileHeader")
    {
        public override string Render(SolidityFile model)
        {
            var renderModel = Transform(model);
            return Render(new { imports = renderModel });
        }
    }
}
