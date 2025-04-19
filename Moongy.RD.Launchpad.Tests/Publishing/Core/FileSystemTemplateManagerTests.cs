using System;
using System.IO;
using Xunit;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates;

namespace Moongy.RD.Launchpad.Tests.Publishing
{
    public class FileSystemTemplateManagerTests : IDisposable
    {
        private readonly string _testDir;
        
        public FileSystemTemplateManagerTests()
        {
            // Criar diretório de teste temporário
            _testDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testDir);
            Directory.CreateDirectory(Path.Combine(_testDir, "Solidity"));
            
            // Criar um template de teste
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/Function.scriban"),
                "function {{Model.Name}}() { {{Model.Body}} }"
            );
        }
        
        [Fact]
        public void GetTemplate_ExistingTemplate_ReturnsContent()
        {
            // Arrange
            var manager = new FileSystemTemplateManager(_testDir);
            
            // Act
            var content = manager.GetTemplate("Solidity/Function.scriban");
            
            // Assert
            Assert.Equal("function {{Model.Name}}() { {{Model.Body}} }", content);
        }
        
        [Fact]
        public void GetTemplate_WithCaching_ReturnsCachedContent()
        {
            // Arrange
            var manager = new FileSystemTemplateManager(_testDir);
            
            // Act - Primeiro acesso
            var content1 = manager.GetTemplate("Solidity/Function.scriban");
            
            // Modificar o arquivo
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/Function.scriban"),
                "// Modified content"
            );
            
            // Act - Segundo acesso (deve retornar do cache)
            var content2 = manager.GetTemplate("Solidity/Function.scriban");
            
            // Assert
            Assert.Equal(content1, content2);
        }
        
        [Fact]
        public void ClearCache_ForcesReload()
        {
            // Arrange
            var manager = new FileSystemTemplateManager(_testDir);
            
            // Primeiro acesso
            manager.GetTemplate("Solidity/Function.scriban");
            
            // Modificar o arquivo
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/Function.scriban"),
                "// Modified content"
            );
            
            // Limpar cache
            manager.ClearCache();
            
            // Act
            var content = manager.GetTemplate("Solidity/Function.scriban");
            
            // Assert
            Assert.Equal("// Modified content", content);
        }
        
        public void Dispose()
        {
            // Limpar o diretório de teste
            if (Directory.Exists(_testDir))
            {
                Directory.Delete(_testDir, true);
            }
        }
    }
}