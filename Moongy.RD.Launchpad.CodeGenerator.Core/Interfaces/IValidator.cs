namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces
{
    public interface IValidator<T>
    {
        public void Validate(T obj);
    }
}
