namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;

public interface IExtensionExtractor<T>
{
    public T? Extract(object extensionFormSection);
}