namespace Moongy.RD.Launchpad.Core.Attributes;



[AttributeUsage(AttributeTargets.Property)]
public abstract class ValidationAttribute : Attribute
{
    public abstract void Validate(object value);
}
