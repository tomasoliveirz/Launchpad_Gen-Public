using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Providers;
using Moongy.RD.Launchpad.Core.Enums;
using Moq;

namespace Moongy.RD.Launchpad.Tests.Publishing.Core
{
    public class SolidityTemplateProviderTests
    {
        // mock class for contract property testing
        private class TestContractProperty : ContractProperty
        {
            public TestContractProperty()
            {
                Arguments = new List<Argument>();
            }
        }
        
        [Fact]
        public void GenerateFunction_ValidFunction_GeneratesCode()
        {
            // creating a mock template manager that returns a simple function template
            var mockTemplateManager = new Mock<ITemplateManager>();
            mockTemplateManager
                .Setup(m => m.GetTemplate("Solidity/Function.scriban"))
                .Returns("function {{Model.Name}}() { return true; }");
                
            var provider = new SolidityTemplateProvider(mockTemplateManager.Object);
            
            var function = new FunctionModel { Name = "transfer", Body = "" };
            
            // generating code from the function definition
            var result = provider.GenerateFunction(function);
            
            // verifying the generated code matches the expected output
            Assert.Equal("function transfer() { return true; }", result);
            mockTemplateManager.Verify(m => m.GetTemplate("Solidity/Function.scriban"), Times.Once);
        }
        
        [Fact]
        public void GenerateEvent_ValidEvent_GeneratesCode()
        {
            // creating a mock template manager that returns a simple event template
            var mockTemplateManager = new Mock<ITemplateManager>();
            mockTemplateManager
                .Setup(m => m.GetTemplate("Solidity/Event.scriban"))
                .Returns("event {{Model.Name}}();");
                
            var provider = new SolidityTemplateProvider(mockTemplateManager.Object);
            
            var eventProperty = new TestContractProperty
            {
                Name = "Transfer",
                PropertyType = PropertyType.Event
            };
            
            // generating code from the event definition
            var result = provider.GenerateEvent(eventProperty);
            
            // verifying the generated code matches the expected output
            Assert.Equal("event Transfer();", result);
            mockTemplateManager.Verify(m => m.GetTemplate("Solidity/Event.scriban"), Times.Once);
        }
        
        [Fact]
        public void GenerateImports_ValidImports_GeneratesCode()
        {
            // creating a mock template manager that returns a simple import template
            var mockTemplateManager = new Mock<ITemplateManager>();
            mockTemplateManager
                .Setup(m => m.GetTemplate("Solidity/Import.scriban"))
                .Returns("import \"{{Model[0].Path}}\";");
                
            var provider = new SolidityTemplateProvider(mockTemplateManager.Object);
            
            var imports = new List<ImportModel>
            {
                new() { PathName = "@openzeppelin/contracts/token/ERC20/ERC20.sol" }
            };
            
            // generating code from the import definition
            var result = provider.GenerateImports(imports);
            
            // verifying the generated code matches the expected output
            Assert.Equal("import \"@openzeppelin/contracts/token/ERC20/ERC20.sol\";", result);
            mockTemplateManager.Verify(m => m.GetTemplate("Solidity/Import.scriban"), Times.Once);
        }
    }
}