
namespace Moongy.RD.Launchpad.Business.Base
{
    public class OperationResult
    {
        public Exception? Exception { get; set; }
        public bool IsSuccessful => Exception == null;
    }

    public class OperationResult<T> : OperationResult
    {
        public T? Result { get; set; }
    }
}
