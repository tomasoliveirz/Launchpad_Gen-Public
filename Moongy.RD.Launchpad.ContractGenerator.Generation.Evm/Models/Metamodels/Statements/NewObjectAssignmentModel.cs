using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Assigments;

public class NewObjectAssignmentModel : AssignmentModel
{
    /// <summary>
    /// The type of the object being instantiated (e.g., "MyStruct", "uint256").
    /// </summary>
    public TypeReference DataType { get; set; }

    /// <summary>
    /// The location keyword, typically "memory", "storage", or "calldata" in Solidity.
    /// Optional — can be null or empty.
    /// </summary>
    public SolidityMemoryLocation Location { get; set; }

    /// <summary>
    /// The name of the variable being assigned.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The value or constructor parameters used for instantiation.
    /// Example: "1, 2" or "SomeStruct({a:1, b:2})"
    /// </summary>
    public string Value { get; set; }
}
