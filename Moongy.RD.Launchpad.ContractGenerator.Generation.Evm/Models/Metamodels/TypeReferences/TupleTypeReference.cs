using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences
{
    public class TupleTypeReference(TypeReference[] elementTypes) : TypeReference(SolidityDataTypeEnum.Tuple)
    {
        public TypeReference[] ElementTypes { get; init; } = elementTypes;

    }
}
