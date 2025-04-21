

namespace Moongy.RD.Launchpad.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SmartContractTypeAttribute : Attribute
    {
        public string? PropertyName { get; set; }
        public Type? Target { get; set; }
        public bool Omit { get; set; }
    }
}
