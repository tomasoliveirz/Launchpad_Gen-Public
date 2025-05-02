using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;
using Moongy.RD.Launchpad.Core.Enums;
using System;

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