using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StandardAttribute : Attribute
    {
        public required StandardEnum Source { get; set; }
    }
}
