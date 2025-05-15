using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators;
public class TypeReferenceValidator : ContextModelValidator<TypeReference>
{
    public override void Validate(TypeReference o)
    {
        switch (o.Kind)
        {
            case TypeReferenceKind.Simple:
                if (o.Primitive == PrimitiveType.None) throw new ValidationException("Invalid data type!");
                break;
            case TypeReferenceKind.Tuple:
                if (o.ElementTypes.Count < 2) throw new ValidationException("Invalid data type!");
                break;
            case TypeReferenceKind.Custom:
                if (string.IsNullOrEmpty(o.TypeName)) throw new ValidationException("Invalid data type!");
                break;
            case TypeReferenceKind.Array:
                if (o.ElementType == null) throw new ValidationException("Invalid data type!");
                break;
            case TypeReferenceKind.Mapping:
                if (o.KeyType == null || o.ValueType == null) throw new ValidationException("Invalid data type!");
                break;
        }
    }
}
