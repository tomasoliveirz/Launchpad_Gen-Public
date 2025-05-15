namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
public interface IFeatureExtractor<T>
{
    public T Extract(object form);
}
