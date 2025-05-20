namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;

public interface IExtensionExtractor
{
    bool CanHandle(object extensionFormSection);
    object Extract(object extensionFormSection);
}