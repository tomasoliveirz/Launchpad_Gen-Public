namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;

public interface ITokenomicExtractor<T>
{
    public T Extract(object tokenomicFormSection);
}