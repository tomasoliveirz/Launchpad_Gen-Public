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
            // setting up a registry with a mock provider for a test language
            var registry = new BuilderProviderRegistry();
            var mockProvider = new Mock<IBuilderProvider>();
            var mockBuilder = new Mock<IContractBuilder>();
            
            mockProvider.Setup(p => p.Language).Returns("TestLang");
            mockProvider.Setup(p => p.CreateBuilder()).Returns(mockBuilder.Object);
            
            registry.RegisterProvider(mockProvider.Object);
            var factory = new BuilderFactory(registry);
            
            // getting a builder for a registered language
            var builder = factory.CreateBuilder("TestLang");
            
            // verifying we get the expected builder and the provider was called once
            Assert.Same(mockBuilder.Object, builder);
            mockProvider.Verify(p => p.CreateBuilder(), Times.Once);
        }
        
        [Fact]
        public void CreateBuilder_UnregisteredLanguage_ThrowsException()
        {
            // creating a factory with an empty registry
            var registry = new BuilderProviderRegistry();
            var factory = new BuilderFactory(registry);
            
            // verifying that requesting an unknown language throws the correct exception
            var exception = Assert.Throws<ArgumentException>(() => factory.CreateBuilder("UnknownLang"));
            Assert.Contains("No provider registered", exception.Message);
        }
        
        [Fact]
        public void IsLanguageSupported_RegisteredLanguage_ReturnsTrue()
        {
            // setting up a registry with a supported language
            var registry = new BuilderProviderRegistry();
            var mockProvider = new Mock<IBuilderProvider>();
            
            mockProvider.Setup(p => p.Language).Returns("TestLang");
            registry.RegisterProvider(mockProvider.Object);
            
            var factory = new BuilderFactory(registry);
            
            // checking if the language is supported
            var isSupported = factory.IsLanguageSupported("TestLang");
            
            // verifying the method correctly identifies supported languages
            Assert.True(isSupported);
        }
        
        [Fact]
        public void IsLanguageSupported_UnregisteredLanguage_ReturnsFalse()
        {
            // creating a factory with an empty registry
            var registry = new BuilderProviderRegistry();
            var factory = new BuilderFactory(registry);
            
            // checking if an unknown language is supported
            var isSupported = factory.IsLanguageSupported("UnknownLang");
            
            // verifying the method correctly reports unsupported languages
            Assert.False(isSupported);
        }
        
        [Fact]
        public void GetSupportedLanguages_ReturnsAllRegisteredLanguages()
        {
            // setting up a registry with two different languages
            var registry = new BuilderProviderRegistry();
            
            var mockProvider1 = new Mock<IBuilderProvider>();
            mockProvider1.Setup(p => p.Language).Returns("Lang1");
            
            var mockProvider2 = new Mock<IBuilderProvider>();
            mockProvider2.Setup(p => p.Language).Returns("Lang2");
            
            registry.RegisterProvider(mockProvider1.Object);
            registry.RegisterProvider(mockProvider2.Object);
            
            var factory = new BuilderFactory(registry);
            
            // getting all supported languages
            var languages = factory.GetSupportedLanguages();
            
            // verifying that all registered languages are returned
            Assert.Contains("Lang1", languages);
            Assert.Contains("Lang2", languages);
            Assert.Equal(2, languages.Count());
        }
    }
}