using System.Collections.Generic;
using Xunit;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Builders;

namespace Moongy.RD.Launchpad.Tests.Publishing
{
    public class SolidityContractBuilderTests
    {
        [Fact]
        public void Build_EmptyBuilder_GeneratesMinimalContract()
        {
            // Arrange
            var builder = new SolidityContractBuilder();
            builder.WithContractDeclaration("EmptyContract");
            
            // Act
            var result = builder.Build();
            
            // Assert
            Assert.Contains("contract EmptyContract {", result);
            Assert.Contains("}", result);
        }
        
        [Fact]
        public void Build_WithBasicElements_GeneratesCompleteContract()
        {
            // Arrange
            var builder = new SolidityContractBuilder();
            builder.WithLicense("MIT")
                  .WithPragma("^0.8.0")
                  .WithImports("import \"@openzeppelin/contracts/token/ERC20/ERC20.sol\";")
                  .WithContractDeclaration("MyToken", new List<string> { "ERC20" })
                  .WithStateVariable("uint256 private _totalSupply;")
                  .WithEvent("event Transfer(address indexed from, address indexed to, uint256 value);")
                  .WithFunction("function transfer(address to, uint256 amount) public returns (bool) { return true; }");
            
            // Act
            var result = builder.Build();
            
            // Assert
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
            // Arrange
            var builder = new SolidityContractBuilder();
            builder.WithContractDeclaration("MultiFunction")
                  .WithFunction("function function1() public {}")
                  .WithFunction("function function2() public {}")
                  .WithFunction("function function3() public {}");
            
            // Act
            var result = builder.Build();
            
            // Assert
            Assert.Contains("function function1() public {}", result);
            Assert.Contains("function function2() public {}", result);
            Assert.Contains("function function3() public {}", result);
        }
        
        [Fact]
        public void Clear_ResetsBuilder()
        {
            // Arrange
            var builder = new SolidityContractBuilder();
            builder.WithContractDeclaration("BeforeClear")
                  .WithFunction("function beforeClear() {}");
            
            // Act
            builder.Clear();
            builder.WithContractDeclaration("AfterClear");
            var result = builder.Build();
            
            // Assert
            Assert.Contains("contract AfterClear {", result);
            Assert.DoesNotContain("BeforeClear", result);
            Assert.DoesNotContain("beforeClear", result);
        }
    }
}