using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Structs;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels;
public class SolidityContractModel : SolidityModel
{
    public required string Name { get; set; }
    public List<ImportModel> Imports { get; set; } = [];
    public List<AbstractionImportModel> BaseContracts { get; set; } = [];
    public List<InterfaceImportModel> Interfaces { get; set; } = [];
    public List<TypeUtilityImportModel> TypeUtilities { get; set; } = [];
    public List<ConstructorParameterModel> ConstructorParameters { get; set; } = [];
    public List<StatePropertyModel> StateProperties { get; set; } = [];
    public List<EnumModel> Enums { get; set; } = [];
    public List<StructModel> Structs { get; set; } = [];
    public List<ModifierModel> Modifiers { get; set; } = [];
    public List<ErrorModel> Errors { get; set; } = [];
    public List<EventModel> Events { get; set; } = [];
    public List<BaseFunctionModel> Functions { get; set; } = []; 
    public bool IsInterface {  get; set; }
    public bool IsAbstract { get; set; }
}