using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Providers;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Base;
using Moq;

namespace Moongy.RD.Launchpad.Tests.ContractGenerator.Core
{
    public class SolidityTemplateProviderTests
    {
        // Classe mock para ContractProperty
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
            // Arrange
            var mockTemplateManager = new Mock<ITemplateManager>();
            mockTemplateManager
                .Setup(m => m.GetTemplate("Solidity/Function.scriban"))
                .Returns("function {{Model.Name}}() { return true; }");
                
            var provider = new SolidityTemplateProvider(mockTemplateManager.Object);
            
            var function = new SmartContractFunction { Name = "transfer" };
            
            // Act
            var result = provider.GenerateFunction(function);
            
            // Assert
            Assert.Equal("function transfer() { return true; }", result);
            mockTemplateManager.Verify(m => m.GetTemplate("Solidity/Function.scriban"), Times.Once);
        }
        
        [Fact]
        public void GenerateEvent_ValidEvent_GeneratesCode()
        {
            // Arrange
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
            
            // Act
            var result = provider.GenerateEvent(eventProperty);
            
            // Assert
            Assert.Equal("event Transfer();", result);
            mockTemplateManager.Verify(m => m.GetTemplate("Solidity/Event.scriban"), Times.Once);
        }
        
        [Fact]
        public void GenerateImports_ValidImports_GeneratesCode()
        {
            // Arrange
            var mockTemplateManager = new Mock<ITemplateManager>();
            mockTemplateManager
                .Setup(m => m.GetTemplate("Solidity/Import.scriban"))
                .Returns("import \"{{Model[0].Path}}\";");
                
            var provider = new SolidityTemplateProvider(mockTemplateManager.Object);
            
            var imports = new List<Import>
            {
                new Import { Path = "@openzeppelin/contracts/token/ERC20/ERC20.sol" }
            };
            
            // Act
            var result = provider.GenerateImports(imports);
            
            // Assert
            Assert.Equal("import \"@openzeppelin/contracts/token/ERC20/ERC20.sol\";", result);
            mockTemplateManager.Verify(m => m.GetTemplate("Solidity/Import.scriban"), Times.Once);
        }
    }
}