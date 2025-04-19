using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates;

namespace Moongy.RD.Launchpad.Tests.Publishing.Core
{
    public class FileSystemTemplateManagerTests : IDisposable
    {
        private readonly string _testDir;
        
        public FileSystemTemplateManagerTests()
        {
            // creating a temporary test directory
            _testDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testDir);
            Directory.CreateDirectory(Path.Combine(_testDir, "Solidity"));
            
            // creating a test template file
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/Function.scriban"),
                "function {{Model.Name}}() { {{Model.Body}} }"
            );
        }
        
        [Fact]
        public void GetTemplate_ExistingTemplate_ReturnsContent()
        {
            // creating a template manager that points to our test directory
            var manager = new FileSystemTemplateManager(_testDir);
            
            // retrieving a template that exists
            var content = manager.GetTemplate("Solidity/Function.scriban");
            
            // verifying the template content is returned correctly
            Assert.Equal("function {{Model.Name}}() { {{Model.Body}} }", content);
        }
        
        [Fact]
        public void GetTemplate_WithCaching_ReturnsCachedContent()
        {
            // creating a template manager that points to our test directory
            var manager = new FileSystemTemplateManager(_testDir);
            
            // first access should load the template from disk
            var content1 = manager.GetTemplate("Solidity/Function.scriban");
            
            // modifying the file after the first access
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/Function.scriban"),
                "// Modified content"
            );
            
            // second access should return the cached version, not the modified file
            var content2 = manager.GetTemplate("Solidity/Function.scriban");
            
            // verifying the cached content remains unchanged
            Assert.Equal(content1, content2);
        }
        
        [Fact]
        public void ClearCache_ForcesReload()
        {
            // creating a template manager that points to our test directory
            var manager = new FileSystemTemplateManager(_testDir);
            
            // load a template to populate the cache
            manager.GetTemplate("Solidity/Function.scriban");
            
            // modifying the file after it's been cached
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/Function.scriban"),
                "// Modified content"
            );
            
            // clearing the cache
            manager.ClearCache();
            
            // getting the template again should reload from disk
            var content = manager.GetTemplate("Solidity/Function.scriban");
            
            // verifying the updated content is returned after cache clear
            Assert.Equal("// Modified content", content);
        }
        
        public void Dispose()
        {
            // cleaning up the test directory
            if (Directory.Exists(_testDir))
            {
                Directory.Delete(_testDir, true);
            }
        }
    }
}