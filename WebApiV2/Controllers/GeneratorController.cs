using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Structs;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;
using Moongy.RD.Launchpad.Core.Enums;

namespace WebApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {
        [HttpGet]
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
            var symbolArgument = new ConstructorParameterModel() { Name = "symbol", Type = stringType, Index = 1, Value="MTK" };
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
            var property3 = new StructPropertyModel() { Name = "struct", DataType = int256Type };


            var struct1 = new StructModel() { Name = "TestStruct", Properties = [property1, property2, property3] };
            var struct2 = new StructModel() { Name = "TestStruct2", Properties = [property3, property2, property1] };
            var struct3 = new StructModel() { Name = "TestStruct3", Properties = [property2, property1, property3] };
            var structs = new StructModel[] { struct1, struct2, struct3 };
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

            foreach(var errorModel in errors)
            {
                var renderError = SolidityTemplateProcessor.Errors.Render(errorModel);
                result += renderError;
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