using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;

public enum SolidityDataTypeEnum
{
    [Column(SolidityDataTypes.Address)]
    Uint256,
    Uint128,
    Uint64,
    Uint32,
    Uint16,
    Uint8,
    Int256,
    Int128,
    Int64,
    Int32,
    Int16,
    Int8,
    Address,
    Bool,
    String,
    Bytes,
    Custom,
    Array,
    Mapping
}