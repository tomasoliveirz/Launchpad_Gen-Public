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
            // Arrange
            var registry = new TemplateProviderRegistry();
            var mockProvider = new Mock<ITemplateProvider>();
            mockProvider.Setup(p => p.Language).Returns("TestLang");
            
            // Act
            registry.RegisterProvider(mockProvider.Object);
            var provider = registry.GetProvider("TestLang");
            
            // Assert
            Assert.Same(mockProvider.Object, provider);
        }
        
        [Fact]
        public void RegisterProvider_NullProvider_ThrowsArgumentNullException()
        {
            // Arrange
            var registry = new TemplateProviderRegistry();
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => registry.RegisterProvider(null));
        }
        
        [Fact]
        public void RegisterProvider_EmptyLanguage_ThrowsArgumentException()
        {
            // Arrange
            var registry = new TemplateProviderRegistry();
            var mockProvider = new Mock<ITemplateProvider>();
            mockProvider.Setup(p => p.Language).Returns("");
            
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => registry.RegisterProvider(mockProvider.Object));
            Assert.Contains("language cannot be null or empty", exception.Message);
        }
        
        [Fact]
        public void GetProvider_UnregisteredLanguage_ThrowsInvalidOperationException()
        {
            // Arrange
            var registry = new TemplateProviderRegistry();
            
            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => registry.GetProvider("UnknownLang"));
            Assert.Contains("No template provider registered", exception.Message);
        }
        
        [Fact]
        public void HasProvider_RegisteredLanguage_ReturnsTrue()
        {
            // Arrange
            var registry = new TemplateProviderRegistry();
            var mockProvider = new Mock<ITemplateProvider>();
            mockProvider.Setup(p => p.Language).Returns("TestLang");
            registry.RegisterProvider(mockProvider.Object);
            
            // Act
            var hasProvider = registry.HasProvider("TestLang");
            
            // Assert
            Assert.True(hasProvider);
        }
        
        [Fact]
        public void HasProvider_UnregisteredLanguage_ReturnsFalse()
        {
            // Arrange
            var registry = new TemplateProviderRegistry();
            
            // Act
            var hasProvider = registry.HasProvider("UnknownLang");
            
            // Assert
            Assert.False(hasProvider);
        }
    }
}