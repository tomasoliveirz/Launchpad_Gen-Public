using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences
{
    public class TupleTypeReference(TypeReference[] elementTypes) : TypeReference(SolidityDataTypeEnum.Tuple)
    {
        public TypeReference[] ElementTypes { get; init; } = elementTypes;

    }
}
