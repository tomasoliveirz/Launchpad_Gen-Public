using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Builders;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Registry;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Services;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Providers;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Base;
using Xunit.Abstractions;

namespace Moongy.RD.Launchpad.Tests.Publishing.Core
{
    public class ContractGenerationIntegrationTests : IDisposable
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly string _testDir;
        private readonly ServiceProvider _serviceProvider;

        private class TestContractProperty : ContractProperty
        {
            public TestContractProperty() => Arguments = new List<Argument>();
        }

        private class TestArgument : Argument
        {
            // flag to mark arguments as indexed in solidity events
            public bool IsIndexed { get; set; }
            
            // property to expose the type to the template
            public string Type { get; }
            
            public TestArgument(string name, string type, DataLocation loc = DataLocation.None)
                : base(name, DataType.None, loc, type)
            {
                Type = type; // we need to store the type so templates can access it
            }
        }

        // constructor - creates temporary directory for templates + configures dependency injection
        public ContractGenerationIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            // create temporary directory with unique name using GUID
            _testDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(Path.Combine(_testDir, "Solidity"));

            // write templates to file system ------------------
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/Import.scriban"),
                @"{{ if Model }}
                        {{ for itm in Model }}
                        {{ if itm.Path }}
                        import ""{{ itm.Path }}""; 
                        {{ end }}
                        {{ end }}
                        {{ end }}");

            // template for events
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/Event.scriban"),
                @"{{ if Model.PropertyType == ""Event"" }}
                        event {{ Model.Name }}({{ for arg in Model.Arguments }}{{ arg.Type }} {{ if arg.IsIndexed }}indexed {{ end }}{{ arg.Name }}{{ if !for.last }}, {{ end }}{{ end }});
                        {{ end }}");

            // template for state variables
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/StateVariable.scriban"),
                @"{{ if Model.PropertyType == ""None"" }}
                        {{ Model.DataType }}{{ if Model.Visibility }} {{ Model.Visibility | string.downcase }}{{ end }}{{ if Model.IsConstant }} constant{{ end }} {{ Model.Name }}{{ if Model.InitialValue }} = {{ Model.InitialValue }}{{ end }};
                        {{ end }}");

            // template for constructors
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/Constructor.scriban"),
                @"{{ if Model.Arguments }}
                        constructor(
                          {{ for arg in Model.Arguments }}
                            {{ arg.Type }}{{ if arg.Location and arg.Location != ""None"" }} {{ arg.Location | string.downcase }}{{ end }} {{ arg.Name }}{{ if !for.last }}, {{ end }}
                          {{ end }}
                        )
                        {{ end }}

                        {{ if Model.BaseConstructorArgs and Model.BaseName }}
                        : {{ Model.BaseName }}(
                          {{ for bc in Model.BaseConstructorArgs }}
                            {{ bc }}{{ if !for.last }}, {{ end }}
                          {{ end }}
                        )
                        {{ end }}
                        {
                          {{ if Model.Operations }}
                            {{ for op in Model.Operations }}
                              {{ op.Value }}
                            {{ end }}
                          {{ end }}
                        }" );

            // template for functions
            File.WriteAllText(
                Path.Combine(_testDir, "Solidity/Function.scriban"),
                @"function {{ Model.Name }}({{ for a in Model.SmartContractArguments }}{{ a.Type }} {{ a.Name }}{{ if !for.last }}, {{ end }}{{ end }}) public {
                          // body stub
                        }" );

            // dependency injection container configuration
            var sc = new ServiceCollection();
            // register services needed for code generation
            sc.AddSingleton<ITemplateManager>(_ => new FileSystemTemplateManager(_testDir));
            sc.AddSingleton<ITemplateProviderRegistry, TemplateProviderRegistry>();
            sc.AddSingleton<BuilderProviderRegistry>();
            sc.AddSingleton<ITemplateProvider, SolidityTemplateProvider>();
            sc.AddSingleton<SolidityBuilderProvider>();
            sc.AddSingleton<BuilderFactory>();
            sc.AddScoped<CodeGenerationService>();
            sc.AddSingleton<ITemplateProviderConfigurator>(p =>
                new TemplateProviderConfigurator(r => r.RegisterProvider(p.GetRequiredService<ITemplateProvider>())));
            sc.AddSingleton<Action<BuilderProviderRegistry>>(p =>
                reg => reg.RegisterProvider(p.GetRequiredService<SolidityBuilderProvider>()));

            _serviceProvider = sc.BuildServiceProvider();

            // run configurators to register providers
            _serviceProvider.GetRequiredService<ITemplateProviderConfigurator>()
                           .Configure(_serviceProvider.GetRequiredService<ITemplateProviderRegistry>());
            _serviceProvider.GetRequiredService<Action<BuilderProviderRegistry>>()
                           .Invoke(_serviceProvider.GetRequiredService<BuilderProviderRegistry>());
        }

        [Fact]
        public void GenerateCode_SimpleContract_ContainsAllPieces()
        {
            // get code generation service from the container
            var svc = _serviceProvider.GetRequiredService<CodeGenerationService>();
            
            // create a smart contract model for testing
            var contract = new SmartContractModel
            {
                Name = "SimpleToken",
                // import the ERC20 library from OpenZeppelin
                Imports = new List<Import> { new Import { Path = "@openzeppelin/contracts/token/ERC20/ERC20.sol" } },
                Properties = new List<ContractProperty>
                {
                    // define a transfer event compatible with ERC20
                    new TestContractProperty
                    {
                        Name = "Transfer",
                        PropertyType = PropertyType.Event,
                        Arguments = new List<Argument>
                        {
                            new TestArgument("from", "address") { IsIndexed = true },
                            new TestArgument("to",   "address") { IsIndexed = true },
                            new TestArgument("value", "uint256")
                        }
                    },
                    // define a state variable for the total supply
                    new TestContractProperty
                    {
                        Name = "totalSupply",
                        PropertyType = PropertyType.None,
                        DataType = "uint256", 
                        Visibility = "private"
                    }
                },
                // define contract functions
                SmartContractFunctions = new List<SmartContractFunction>
                {
                    // transfer function
                    new SmartContractFunction
                    {
                        Name = "transfer",
                        SmartContractArguments = new List<Argument>
                        {
                            new TestArgument("recipient", "address"),
                            new TestArgument("amount",    "uint256")
                        }
                    },
                    // function to check balance
                    new SmartContractFunction
                    {
                        Name = "balanceOf",
                        SmartContractArguments = new List<Argument>
                        {
                            new TestArgument("account", "address")
                        }
                    }
                }
            };
            
            // generate solidity code from the model
            var code = svc.GenerateCode(contract, "Solidity", SoftwareLicense.MIT, "^0.8.0");
            // print the generated code for debugging
            _testOutputHelper.WriteLine(code);
            
            // verify that the code contains all expected elements
            Assert.Contains("// SPDX-License-Identifier: MIT", code);
            Assert.Contains("pragma solidity ^0.8.0;", code);
            Assert.Contains("import \"@openzeppelin/contracts/token/ERC20/ERC20.sol\";", code);
            Assert.Contains("event Transfer(address indexed from, address indexed to, uint256 value);", code);
            Assert.Contains("uint256 private totalSupply;", code);
            Assert.Contains("function transfer(address recipient, uint256 amount) public {", code);
            Assert.Contains("function balanceOf(address account) public {", code);
        }

        // clean up resources
        public void Dispose()
        {
            _serviceProvider.Dispose();
            // delete temporary template directory
            if (Directory.Exists(_testDir)) Directory.Delete(_testDir, true);
        }
    }
}