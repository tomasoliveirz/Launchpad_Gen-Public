using Moongy.RD.Launchpad.Tools.Aissistant.Models;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Interfaces;
public interface IAissistant
{
    Task<AissistantResponse> Execute(AissistantRequest request);
}
