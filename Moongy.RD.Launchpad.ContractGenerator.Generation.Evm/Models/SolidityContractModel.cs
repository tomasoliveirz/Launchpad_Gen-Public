using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Events;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Imports;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Structs;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models;
public class SolidityContractModel : SolidityModel
{
    public required string Name { get; set; }
    public List<ImportModel> Imports { get; set; } = [];
    public List<AbstractionImportModel> BaseContracts { get; set; } = [];
    public List<InterfaceImportModel> Interfaces { get; set; } = [];
    public List<StateVariableModel> StateVariables { get; set; } = [];
    public List<EnumModel> Enums { get; set; } = [];
    public List<StructModel> Structs { get; set; } = [];
    public List<ModifierModel> Modifiers { get; set; } = [];
    public List<ErrorModel> Errors { get; set; } = [];
    public List<EventModel> Events { get; set; } = [];
    public List<FunctionModel> Functions { get; set; } = [];
    public bool IsInterface {  get; set; }
    public bool IsAbstract { get; set; }
}
