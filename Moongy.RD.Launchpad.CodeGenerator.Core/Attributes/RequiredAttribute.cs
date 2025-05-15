

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Attributes
{
    public class RequiredAttribute : ValidationAttribute
    {
        public required string Name { get; set; }
        public override void Validate(object o)
        {
            if (o == null) throw new Exception($"{Name} is required");
        }
    }
}
