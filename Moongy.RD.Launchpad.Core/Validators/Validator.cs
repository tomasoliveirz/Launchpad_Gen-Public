using System.Reflection;
using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Interfaces;

namespace Moongy.RD.Launchpad.Core.Validators;
public class Validator<T> : IValidator<T>
{
    /// <summary>
    /// Validates the properties' attributes for a model
    /// </summary>
    public void Validate(T o)
    {
        if (o == null) return;
        var properties = o.GetType().GetProperties().Where(x => x.GetCustomAttributes<ValidationAttribute>().Any());
        foreach (var property in properties)
        {
            var validatorAttributes = property.GetCustomAttributes<ValidationAttribute>();
            foreach (var attribute in validatorAttributes)
            {
                var val = property.GetValue(o);
                if (val == null) continue;
                attribute.Validate(val);
            }
        }
    }
}
