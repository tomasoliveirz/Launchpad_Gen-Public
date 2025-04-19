using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Registry;
using Moq;

namespace Moongy.RD.Launchpad.Tests.Publishing.Core
{
    public class TemplateProviderRegistryTests
    {
        [Fact]
        public void RegisterProvider_ValidProvider_CanBeRetrieved()
        {
            // creating a new registry and registering a mock provider
            var registry = new TemplateProviderRegistry();
            var mockProvider = new Mock<ITemplateProvider>();
            mockProvider.Setup(p => p.Language).Returns("TestLang");
            
            // registering the provider and then retrieving it
            registry.RegisterProvider(mockProvider.Object);
            var provider = registry.GetProvider("TestLang");
            
            // verifying the same provider instance is returned
            Assert.Same(mockProvider.Object, provider);
        }
        
        [Fact]
        public void RegisterProvider_NullProvider_ThrowsArgumentNullException()
        {
            // creating a new registry
            var registry = new TemplateProviderRegistry();
            
            // verifying that trying to register null throws the correct exception
            Assert.Throws<ArgumentNullException>(() => registry.RegisterProvider(null));
        }
        
        [Fact]
        public void RegisterProvider_EmptyLanguage_ThrowsArgumentException()
        {
            // creating a registry and a provider with an empty language string
            var registry = new TemplateProviderRegistry();
            var mockProvider = new Mock<ITemplateProvider>();
            mockProvider.Setup(p => p.Language).Returns("");
            
            // verifying that registering a provider with empty language throws the correct exception
            var exception = Assert.Throws<ArgumentException>(() => registry.RegisterProvider(mockProvider.Object));
            Assert.Contains("language cannot be null or empty", exception.Message);
        }
        
        [Fact]
        public void GetProvider_UnregisteredLanguage_ThrowsInvalidOperationException()
        {
            // creating a new empty registry
            var registry = new TemplateProviderRegistry();
            
            // verifying that requesting an unregistered language throws the correct exception
            var exception = Assert.Throws<InvalidOperationException>(() => registry.GetProvider("UnknownLang"));
            Assert.Contains("No template provider registered", exception.Message);
        }
        
        [Fact]
        public void HasProvider_RegisteredLanguage_ReturnsTrue()
        {
            // creating a registry and registering a provider
            var registry = new TemplateProviderRegistry();
            var mockProvider = new Mock<ITemplateProvider>();
            mockProvider.Setup(p => p.Language).Returns("TestLang");
            registry.RegisterProvider(mockProvider.Object);
            
            // checking if the registry has the registered provider
            var hasProvider = registry.HasProvider("TestLang");
            
            // verifying the method correctly identifies registered providers
            Assert.True(hasProvider);
        }
        
        [Fact]
        public void HasProvider_UnregisteredLanguage_ReturnsFalse()
        {
            // creating a new empty registry
            var registry = new TemplateProviderRegistry();
            
            // checking if the registry has an unregistered provider
            var hasProvider = registry.HasProvider("UnknownLang");
            
            // verifying the method correctly reports when a provider isn't registered
            Assert.False(hasProvider);
        }
    }
}