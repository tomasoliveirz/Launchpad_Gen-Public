using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.Language.Core.Interfaces
{
    public interface ICodeEmitter
    {
        public string Emit(SmartContractModel model);
    }
}
