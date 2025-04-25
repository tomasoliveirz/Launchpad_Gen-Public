using Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Builders;

namespace Moongy.RD.Launchpad.Tests.Publishing.Core
{
    public class SolidityContractBuilderTests
    {
        [Fact]
        public void Build_EmptyBuilder_GeneratesMinimalContract()
        {
            // creating a basic builder with just a contract name
            var builder = new SolidityContractBuilder();
            builder.WithContractDeclaration("EmptyContract");
            
            // building the contract
            var result = builder.Build();
            
            // verifying the minimal contract structure is generated
            Assert.Contains("contract EmptyContract {", result);
            Assert.Contains("}", result);
        }
        
        [Fact]
        public void Build_WithBasicElements_GeneratesCompleteContract()
        {
            // creating a builder with all the basic solidity contract elements
            var builder = new SolidityContractBuilder();
            builder.WithLicense("MIT")
                  .WithPragma("^0.8.0")
                  .WithImports("import \"@openzeppelin/contracts/token/ERC20/ERC20.sol\";")
                  .WithContractDeclaration("MyToken", new List<string> { "ERC20" })
                  .WithStateVariable("uint256 private _totalSupply;")
                  .WithEvent("event Transfer(address indexed from, address indexed to, uint256 value);")
                  .WithFunction("function transfer(address to, uint256 amount) public returns (bool) { return true; }");
            
            // building the contract with all components
            var result = builder.Build();
            
            // verifying all elements are included in the generated contract
            Assert.Contains("// SPDX-License-Identifier: MIT", result);
            Assert.Contains("pragma solidity ^0.8.0;", result);
            Assert.Contains("import \"@openzeppelin/contracts/token/ERC20/ERC20.sol\";", result);
            Assert.Contains("contract MyToken is ERC20 {", result);
            Assert.Contains("uint256 private _totalSupply;", result);
            Assert.Contains("event Transfer(address indexed from, address indexed to, uint256 value);", result);
            Assert.Contains("function transfer(address to, uint256 amount) public returns (bool) { return true; }", result);
        }
        
        [Fact]
        public void WithFunction_MultipleFunctions_AllIncluded()
        {
            // creating a builder with multiple function definitions
            var builder = new SolidityContractBuilder();
            builder.WithContractDeclaration("MultiFunction")
                  .WithFunction("function function1() public {}")
                  .WithFunction("function function2() public {}")
                  .WithFunction("function function3() public {}");
            
            // building the contract with multiple functions
            var result = builder.Build();
            
            // verifying all function definitions are included
            Assert.Contains("function function1() public {}", result);
            Assert.Contains("function function2() public {}", result);
            Assert.Contains("function function3() public {}", result);
        }
        
        [Fact]
        public void Clear_ResetsBuilder()
        {
            // creating a builder with initial content
            var builder = new SolidityContractBuilder();
            builder.WithContractDeclaration("BeforeClear")
                  .WithFunction("function beforeClear() {}");
            
            // clearing the builder and adding new content
            builder.Clear();
            builder.WithContractDeclaration("AfterClear");
            var result = builder.Build();
            
            // verifying the initial content is gone and only new content remains
            Assert.Contains("contract AfterClear {", result);
            Assert.DoesNotContain("BeforeClear", result);
            Assert.DoesNotContain("beforeClear", result);
        }
    }
}