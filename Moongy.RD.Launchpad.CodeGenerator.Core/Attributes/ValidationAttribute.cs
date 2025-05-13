namespace Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
public abstract class ValidationAttribute : Attribute
{
    public abstract void Validate(object o);
}
