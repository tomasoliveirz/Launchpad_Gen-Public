namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;

public interface ITokenomicExtractor
{
    bool CanHandle(object tokenomicFormSection);
    object Extract(object tokenomicFormSection);
}