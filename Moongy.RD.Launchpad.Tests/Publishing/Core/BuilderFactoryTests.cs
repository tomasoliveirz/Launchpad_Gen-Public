using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Builders;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Registry;
using Moq;

namespace Moongy.RD.Launchpad.Tests.Publishing.Core
{
    public class BuilderFactoryTests
    {
        [Fact]
        public void CreateBuilder_RegisteredLanguage_ReturnsBuilder()
        {
            // Arrange
            var registry = new BuilderProviderRegistry();
            var mockProvider = new Mock<IBuilderProvider>();
            var mockBuilder = new Mock<IContractBuilder>();
            
            mockProvider.Setup(p => p.Language).Returns("TestLang");
            mockProvider.Setup(p => p.CreateBuilder()).Returns(mockBuilder.Object);
            
            registry.RegisterProvider(mockProvider.Object);
            var factory = new BuilderFactory(registry);
            
            // Act
            var builder = factory.CreateBuilder("TestLang");
            
            // Assert
            Assert.Same(mockBuilder.Object, builder);
            mockProvider.Verify(p => p.CreateBuilder(), Times.Once);
        }
        
        [Fact]
        public void CreateBuilder_UnregisteredLanguage_ThrowsException()
        {
            // Arrange
            var registry = new BuilderProviderRegistry();
            var factory = new BuilderFactory(registry);
            
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => factory.CreateBuilder("UnknownLang"));
            Assert.Contains("No provider registered", exception.Message);
        }
        
        [Fact]
        public void IsLanguageSupported_RegisteredLanguage_ReturnsTrue()
        {
            // Arrange
            var registry = new BuilderProviderRegistry();
            var mockProvider = new Mock<IBuilderProvider>();
            
            mockProvider.Setup(p => p.Language).Returns("TestLang");
            registry.RegisterProvider(mockProvider.Object);
            
            var factory = new BuilderFactory(registry);
            
            // Act
            var isSupported = factory.IsLanguageSupported("TestLang");
            
            // Assert
            Assert.True(isSupported);
        }
        
        [Fact]
        public void IsLanguageSupported_UnregisteredLanguage_ReturnsFalse()
        {
            // Arrange
            var registry = new BuilderProviderRegistry();
            var factory = new BuilderFactory(registry);
            
            // Act
            var isSupported = factory.IsLanguageSupported("UnknownLang");
            
            // Assert
            Assert.False(isSupported);
        }
        
        [Fact]
        public void GetSupportedLanguages_ReturnsAllRegisteredLanguages()
        {
            // Arrange
            var registry = new BuilderProviderRegistry();
            
            var mockProvider1 = new Mock<IBuilderProvider>();
            mockProvider1.Setup(p => p.Language).Returns("Lang1");
            
            var mockProvider2 = new Mock<IBuilderProvider>();
            mockProvider2.Setup(p => p.Language).Returns("Lang2");
            
            registry.RegisterProvider(mockProvider1.Object);
            registry.RegisterProvider(mockProvider2.Object);
            
            var factory = new BuilderFactory(registry);
            
            // Act
            var languages = factory.GetSupportedLanguages();
            
            // Assert
            Assert.Contains("Lang1", languages);
            Assert.Contains("Lang2", languages);
            Assert.Equal(2, languages.Count());
        }
    }
}