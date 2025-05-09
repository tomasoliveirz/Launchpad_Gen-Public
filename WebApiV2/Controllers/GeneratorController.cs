using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Structs;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Version;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Forms;
using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Models;

namespace WebApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController(ICodeGenerationBusinessObject codeGenerationService) : ControllerBase
    {
        [HttpPost("fungible")]
        public async Task<IActionResult> GenerateFungibleToken([FromBody] FungibleTokenForm form)
        {
            var result = await codeGenerationService.GenerateFungibleToken(form);
            return HandleResult(result);
        }

        [HttpPost("semi-fungible")]
        public async Task<IActionResult> GenerateSemiFungibleToken([FromBody] SemiFungibleTokenForm form)
        {
            var result = await codeGenerationService.GenerateSemiFungibleToken(form);
            return HandleResult(result);
        }

        [HttpPost("non-fungible")]
        public async Task<IActionResult> GenerateNonFungibleToken([FromBody] NonFungibleTokenForm form)
        {
            var result = await codeGenerationService.GenerateNonFungibleToken(form);
            return HandleResult(result);
        }

        [HttpPost("stablecoin")]
        public async Task<IActionResult> GenerateStablecoin([FromBody] StableCoinForm form)
        {
            var result = await codeGenerationService.GenerateStablecoin(form);
            return HandleResult(result);
        }

        [HttpPost("real-world-asset")]
        public async Task<IActionResult> GenerateRealWorldAsset([FromBody] RealWorldAssetForm form)
        {
            var result = await codeGenerationService.GenerateRealWorldAsset(form);
            return HandleResult(result);
        }

        [HttpPost("governor")]
        public async Task<IActionResult> GenerateGovernor([FromBody] GovernorForm form)
        {
            var result = await codeGenerationService.GenerateGovernor(form);
            return HandleResult(result);
        }

        private IActionResult HandleResult<T>(OperationResult<GenerationResult<T>> result) where T : TokenBaseModel
        {
            if (result.IsSuccessful)
                return Ok(result.Result);

            return BadRequest(new { error = result.Exception?.Message });
        }


        [HttpGet("test1")]
        public ActionResult GenerateSolidityContract()
        {

            #region Constructor parameters
            var int256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
            var stringType = new SimpleTypeReference(SolidityDataTypeEnum.String);
            var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);

            var nameParameter = new ConstructorParameterModel() { Name = "name", Type = stringType, Index = 0, Value = "MyToken" };
            var symbolParameter = new ConstructorParameterModel() { Name = "symbol", Type = stringType, Index = 1, Value = "MTK" };
            var initialOwnerParameter = new ConstructorParameterModel() { Name = "initialOwner", Type = addressType };
            var toParameter = new ConstructorParameterModel() { Name = "to", Type = addressType };
            var amountParameter = new ConstructorParameterModel() { Name = "amount", Type = int256Type };
            #endregion

            #region Modifiers 
            var onlyOwnerModifier = new ModifierModel() { Name = "onlyOwner", Body = "" };
            #endregion

            var erc20PermitDependency = new AbstractionImportModel()
            {
                Name = "ERC20Permit",
                PathName = "@openzeppelin/contracts/token/ERC20/extensions/ERC20Permit.sol",
                ConstructorParameters = [nameParameter]
            };
            var erc20Dependency = new AbstractionImportModel()
            {
                Name = "ERC20",
                PathName = "@openzeppelin/contracts/token/ERC20/ERC20.sol",
                ConstructorParameters = [nameParameter, symbolParameter]
            };

            var ownableDependency = new AbstractionImportModel()
            {
                Name = "Ownable",
                PathName = "@openzeppelin/contracts/access/Ownable.sol",
                ConstructorParameters = [initialOwnerParameter]
            };

            var constructorParameters = new List<ConstructorParameterModel>
            {
                nameParameter, symbolParameter, initialOwnerParameter
            };

            var mintFunction = new FunctionModel()
            {
                Name = "mint",
                Parameters = [toParameter, amountParameter],
                Modifiers = [onlyOwnerModifier],
                Body = "_mint(to, amount);"
            };

            var contract = new SolidityContractModel() { Name = "MyToken", BaseContracts = [erc20Dependency, erc20PermitDependency, ownableDependency],
            ConstructorParameters = constructorParameters,
            Functions = [mintFunction]
            };


            var version = new SoftwareVersion() { Major = 0, Minor = 8, Revision = 27 };
            var fileHeader = new FileHeaderModel() { License = SpdxLicense.MIT, Version = new() { Maximum = version, Minimum = version } };

            var file = new SolidityFile() { FileHeader = fileHeader, Contracts = [contract] };

            return Ok(file);
        }


        [HttpGet("test2")]
        public ActionResult TryToGenerateSolidityContract()
        {
            #region file header
            FileHeaderModel fileHeader = new()
            {
                License = SpdxLicense.Apache_2_0,
                Version = new() { Minimum = new() { Major = 0, Minor = 8, Revision = 20 }, Maximum = new() { Major = 0, Minor = 8, Revision = 20 } }
            };
            #endregion

            #region Constructor parameters
            var int256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
            var stringType = new SimpleTypeReference(SolidityDataTypeEnum.String);

            var nameArgument = new ConstructorParameterModel() { Name = "name", Type = stringType, Index = 0, Value="MyToken" };
            var symbolArgument = new ConstructorParameterModel() { Name = "symbol", Type = stringType, Index = 1, Value="MTK", Location = SolidityMemoryLocation.Memory };
            var maxUserCountArgument = new ConstructorParameterModel() { Name = "maxUserCount", Type = int256Type, AssignedTo="_maxUserCount" };
            #endregion

            #region Dependencies
            var erc20Dependency = new AbstractionImportModel()
            {
                Name = "ERC20",
                PathName = "@openzeppelin/contracts/token/ERC20/ERC20.sol",
                ConstructorParameters = [nameArgument, symbolArgument]
            };

            var erc202Dependency = new AbstractionImportModel()
            {
                Name = "ERC202",
                PathName = "@openzeppelin/contracts/token/ERC20/ERC20.sol",
                ConstructorParameters = [nameArgument, symbolArgument]
            };

            var erc20PermitDependency = new AbstractionImportModel()
            {
                Name = "ERC20Permit",
                PathName = "@openzeppelin/contracts/token/ERC20/extensions/ERC20Permit.sol",
                ConstructorParameters = [nameArgument]
            };

            var testImport = new ImportModel() { PathName = "a/b/c", Alias = "ABC" };
            var testImportC = new ImportModel() { PathName = "a/c/b" };
            var testImportB = new ImportModel() { PathName = "a/b/c" };
            #endregion

            #region Events

            var nameEventArgument = new EventParameterModel() { Name = "name", Type = stringType, Index = 1 };
            var roundEventArgument = new EventParameterModel() { Name = "round", IsIndexed=true, Type = int256Type, Index = 0 };

            var versionEventArgument = new EventParameterModel() { Name = "version", Type = stringType};


            var event1 = new EventModel() { Name = "Test", Parameters = [nameEventArgument, roundEventArgument] };
            var event2 = new EventModel() { Name = "Build", Parameters = [versionEventArgument, nameEventArgument] };
            var event3 = new EventModel() { Name = "Use", Parameters = [] };

            var events = new EventModel[]{ event1, event2, event3 };
            #endregion

            #region Structs
            var property1 = new StructPropertyModel() { Name = "test", DataType = int256Type };
            var property2 = new StructPropertyModel() { Name = "build", DataType = stringType };
            var property3 = new StructPropertyModel() { Name = "amount", DataType = int256Type };


            var struct1 = new StructModel() { Name = "TestStruct", Properties = [property1, property2, property3] };
            var struct2 = new StructModel() { Name = "TestStruct2", Properties = [property3, property2, property1] };
            var struct3 = new StructModel() { Name = "TestStruct3", Properties = [property2, property1, property3] };
            var structs = new StructModel[] { struct1, struct2, struct3 };
            #endregion

            #region Enums

            var enum1 = new EnumModel() { Name = "TestEnum", Values = ["A", "B", "C"] };
            var enum2 = new EnumModel() { Name = "TestEnum2", Values = ["A", "B", "C"] };
            var enum3 = new EnumModel() { Name = "TestEnum3", Values = ["A", "B", "C"] };
            var enums = new EnumModel[] { enum1, enum2, enum3 };
            #endregion

            #region Modifiers

            var booleanType = new SimpleTypeReference(SolidityDataTypeEnum.Bool);
            var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);

            var ownerModifierParam = new ModifierParameterModel() { Name = "owner", Type = addressType };
            var activeModifierParam = new ModifierParameterModel() { Name = "isActive", Type = booleanType };

            var modifier1 = new ModifierModel() { Name = "TestModifier", Body = "require(msg.sender == owner())" };
            var modifier2 = new ModifierModel() { Name = "TestModifier2", Body = "require(msg.sender == owner())" };
            var modifier3 = new ModifierModel() { Name = "TestModifier3", Body = "require(msg.sender == owner())" };
            var modifier4 = new ModifierModel()
            {
                Name = "TestModifier4",
                Parameters = [ownerModifierParam, activeModifierParam],
                Body = "require(msg.sender == owner && isActive == true)"
            };
            var modifiers = new ModifierModel[] { modifier1, modifier2, modifier3, modifier4 };
            #endregion
            #region State 
            var maxUserCountProperty = new StatePropertyModel() { Name = "_maxUserCount", Type = int256Type };
            #endregion

            #region Contracts

            SolidityContractModel contract = new()
            {
                Name = "MyToken",
                BaseContracts = [erc20Dependency, erc202Dependency, erc20PermitDependency, erc20Dependency],
                Imports = [testImport, testImportC,testImportB],
                ConstructorParameters = [nameArgument, symbolArgument, maxUserCountArgument],
                Interfaces = [],
                StateProperties = [maxUserCountProperty]
            };
            #endregion
            #region Errors
            var errorParameter1 = new ErrorParameterModel() { Name = "name", Type = stringType, Index = 0 };
            var errorParameter2 = new ErrorParameterModel() { Name = "round", Type = int256Type, Index = 1 };
            var errorParameter3 = new ErrorParameterModel() { Name = "version", Type = stringType };

            var error1 = new ErrorModel() { Name = "TestError", Parameters = [errorParameter3, errorParameter2, errorParameter1] };
            var error2 = new ErrorModel() { Name = "BuildError", Parameters = [errorParameter2, errorParameter3] };
            var error3 = new ErrorModel() { Name = "UseError", Parameters = [errorParameter3] };


            var errors = new ErrorModel[] { error1, error2, error3 };
            #endregion

            #region Functions
            var functionParameter1 = new FunctionParameterModel() { Name = "to", Type = addressType, Index = 0 };
            var functionParameter2 = new FunctionParameterModel() { Name = "amount", Type = int256Type, Index = 1 };

            var function1 = new FunctionModel()
            {
                Name = "TestFunction",
                Parameters = [functionParameter2, functionParameter1],
                Body = "require(msg.sender == owner())"
            };

            var functions = new FunctionModel[] { function1 };
            #endregion


            SolidityFile file = new()
            {
                FileHeader = fileHeader,
                Contracts = [contract]
            };

     

            var result = SolidityTemplateProcessor.FileHeader.Render(file);
            result += Environment.NewLine;
            result += SolidityTemplateProcessor.Imports.Render(file);
            result += Environment.NewLine;
            result += Environment.NewLine;
            foreach(var contractModel in file.Contracts)
            {
                result += SolidityTemplateProcessor.ContractHeader.Render(contractModel);
            }
            result += Environment.NewLine;
            foreach(var contractModel in file.Contracts)
            {
                result += SolidityTemplateProcessor.ContractHeader.Render(contractModel);
                result += Environment.NewLine;
    
                foreach (var stateProperty in contractModel.StateProperties)
                {
                    try 
                    {
                        var statePropertyCode = SolidityTemplateProcessor.StateProperties.Render(stateProperty);
                        result += statePropertyCode;
                        result += Environment.NewLine;
                    }
                    catch (Exception ex)
                    {
                        result += $"// Error rendering state property {stateProperty.Name}: {ex.Message}" + Environment.NewLine;
                    }
                }
            }
            foreach (var @event in events)
            {
                var renderEvent = SolidityTemplateProcessor.Events.Render(@event);
                result += renderEvent;
                result += Environment.NewLine;
            }
            foreach(var structModel in structs)
            {
                var renderStruct = SolidityTemplateProcessor.Structs.Render(structModel);
                result += renderStruct;
                result += Environment.NewLine;
            }
            result += Environment.NewLine;
            foreach (var @enum in enums)
            {
                var renderEnum = SolidityTemplateProcessor.Enums.Render(@enum);
                result += renderEnum;
                result += Environment.NewLine;
            }
            result += Environment.NewLine;
            foreach (var modifier in modifiers)
            {
                var renderModifier = SolidityTemplateProcessor.Modifiers.Render(modifier);
                result += renderModifier;
                result += Environment.NewLine;
            }
            result += Environment.NewLine;

            foreach(var errorModel in errors)
            {
                var renderError = SolidityTemplateProcessor.Errors.Render(errorModel);
                result += renderError;
                result += Environment.NewLine;
            }

            result += Environment.NewLine;
            foreach (var function in functions)
            {
                var renderFunction = SolidityTemplateProcessor.Functions.Render(function);
                result += renderFunction;
                result += Environment.NewLine;
            }


            #region Constructor
            result += Environment.NewLine;
            foreach (var contractModel in file.Contracts)
            {
                try
                {
                    var constructorCode = SolidityTemplateProcessor.Constructor.Render(contractModel);
                    result += constructorCode;
                    result += Environment.NewLine;
                }
                catch (Exception ex)
                {
                    result += $"// Error rendering constructor for {contractModel.Name}: {ex.Message}" + Environment.NewLine;
                }
            }
            #endregion

            return Ok(result);
        }

    }
}