namespace Moongy.RD.Launchpad.Core.Interfaces
{
    public interface IValidator<T>
    {
        public void Validate(T obj);
    }
}
