using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
public class AtLeastOneAttribute : ValidationAttribute
{
    public override void Validate(object o)
    {
        if (o is IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ValidationException("Collection must contain at least one element.");
        }
        else
        {
            throw new ValidationException("Value must be a collection.");
        }
    }
}
