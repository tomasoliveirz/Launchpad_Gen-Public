using System.Reflection;
using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators;
public class LaunchpadValidator<T> : IValidator<T>
{
    /// <summary>
    /// Validates the properties' attributes for a model
    /// </summary>
    public virtual void Validate(T o)
    {
        if (o == null) return;
        var properties = o.GetType().GetProperties().Where(x => x.GetCustomAttributes<ValidationAttribute>().Any());
        foreach (var property in properties)
        {
            var validatorAttributes = property.GetCustomAttributes<ValidationAttribute>();
            var required = property.GetCustomAttribute<RequiredAttribute>();
            if (required != null) required.Validate(o);
            foreach (var attribute in validatorAttributes)
            {
                var val = property.GetValue(o);
                if (val == null) continue;
                attribute.Validate(val);
            }
        }
    }
}


