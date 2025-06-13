// TESTE DE INTEGRAÇÃO COMPLETO - SoliditySynthesizer + SolidityCodeGenerator

using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Directives;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Synthesizer;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Generators;
using System.Collections.Generic;
using System;

public static class SolidityCodeGeneratorTest
{
    public static void TestCompleteFlow()
    {
        Console.WriteLine("🚀 Testando fluxo completo: ContextMetamodel → SoliditySynthesizer → SolidityCodeGenerator\n");

        // 1. Criar dados de entrada (ContextMetamodel)
        var contextModel = CreateTestContextModel();

        // 2. Sintetizar para Solidity
        var synthesizer = new SoliditySynthesizer();
        var solidityModel = synthesizer.Synthesize(contextModel);

        // 3. Gerar código Solidity
        var generator = new SolidityCodeGenerator();
        string solidityCode = generator.Generate(solidityModel);

        // 4. Mostrar resultado
        Console.WriteLine("=== CÓDIGO SOLIDITY GERADO ===");
        Console.WriteLine(solidityCode);
        Console.WriteLine("===============================\n");

        // 5. Salvar em arquivo
        string fileName = "IntegrationTest.sol";
        File.WriteAllText(fileName, solidityCode);
        Console.WriteLine($"✅ Código salvo em {fileName}");

        // 6. Validações básicas
        ValidateOutput(solidityCode);
    }

    private static ContextMetamodel CreateTestContextModel()
    {
        // Criar um ModuleFileDefinition (entrada do synthesizer)
        var moduleFile = new ModuleFileDefinition
        {
            // Directives (SPDX + pragma)
            Directives = new List<DirectiveDefinition>
            {
                new DirectiveDefinition
                {
                    Kind = DirectiveKind.License,
                    Value = "MIT"
                },
                new DirectiveDefinition
                {
                    Kind = DirectiveKind.Version,
                    Value = "0.8.20"
                }
            },

            // Imports (se houver)
            Imports = new List<ImportDefinition>(),

            // Enums globais (se houver)
            Enums = new List<EnumDefinition>(),

            // Modules (contratos)
            Modules = new List<ModuleDefinition>
            {
                CreateTestModule()
            }
        };

        return moduleFile; // ContextMetamodel é alias para ModuleFileDefinition
    }

    private static ModuleDefinition CreateTestModule()
    {
        return new ModuleDefinition
        {
            Name = "TestToken",

            // Fields (state variables)
            Fields = new List<FieldDefinition>
            {
                new FieldDefinition
                {
                    Name = "totalSupply",
                    Type = new TypeReference
                    {
                        Kind = TypeReferenceKind.Simple,
                        Primitive = PrimitiveType.Uint256
                    },
                    Visibility = Visibility.Public,
                    IsImmutable = false,
                    Value = "1000000"
                },
                new FieldDefinition
                {
                    Name = "owner",
                    Type = new TypeReference
                    {
                        Kind = TypeReferenceKind.Simple,
                        Primitive = PrimitiveType.Address
                    },
                    Visibility = Visibility.Private,
                    IsImmutable = true
                }
            },

            // Enums
            Enums = new List<EnumDefinition>
            {
                new EnumDefinition
                {
                    Name = "Status",
                    Members = new List<string> { "PENDING", "ACTIVE", "SUSPENDED" }
                }
            },

            // Triggers (events/errors)
            Triggers = new List<TriggerDefinition>
            {
                // Event
                new TriggerDefinition
                {
                    Name = "Transfer",
                    Kind = TriggerKind.Log,
                    Parameters = new List<ParameterDefinition>
                    {
                        new ParameterDefinition
                        {
                            Name = "from",
                            Type = new TypeReference
                            {
                                Kind = TypeReferenceKind.Simple,
                                Primitive = PrimitiveType.Address
                            }
                        },
                        new ParameterDefinition
                        {
                            Name = "to",
                            Type = new TypeReference
                            {
                                Kind = TypeReferenceKind.Simple,
                                Primitive = PrimitiveType.Address
                            }
                        },
                        new ParameterDefinition
                        {
                            Name = "amount",
                            Type = new TypeReference
                            {
                                Kind = TypeReferenceKind.Simple,
                                Primitive = PrimitiveType.Uint256
                            }
                        }
                    }
                },
                
                // Error
                new TriggerDefinition
                {
                    Name = "InsufficientBalance",
                    Kind = TriggerKind.Error,
                    Parameters = new List<ParameterDefinition>
                    {
                        new ParameterDefinition
                        {
                            Name = "available",
                            Type = new TypeReference
                            {
                                Kind = TypeReferenceKind.Simple,
                                Primitive = PrimitiveType.Uint256
                            }
                        },
                        new ParameterDefinition
                        {
                            Name = "required",
                            Type = new TypeReference
                            {
                                Kind = TypeReferenceKind.Simple,
                                Primitive = PrimitiveType.Uint256
                            }
                        }
                    }
                }
            },

            // Functions
            Functions = new List<FunctionDefinition>
            {
                // Constructor
                new FunctionDefinition
                {
                    Kind = FunctionKind.Constructor,
                    Parameters = new List<ParameterDefinition>
                    {
                        new ParameterDefinition
                        {
                            Name = "_initialSupply",
                            Type = new TypeReference
                            {
                                Kind = TypeReferenceKind.Simple,
                                Primitive = PrimitiveType.Uint256
                            },
                            Value = "_initialSupply" // Para assignment
                        }
                    }
                }
            },

            // Outros campos necessários
            Structs = new List<StructDefinition>(),
            Modifiers = new List<ModifierDefinition>(),
            Implements = new List<InterfaceDefinition>()
        };
    }

    private static void ValidateOutput(string solidityCode)
    {
        Console.WriteLine("  Validando saída...");

        var checks = new List<(string description, Func<string, bool> check)>
        {
            ("Contém SPDX license", code => code.Contains("SPDX-License-Identifier")),
            ("Contém pragma", code => code.Contains("pragma solidity")),
            ("Contém contract", code => code.Contains("contract TestToken")),
            ("Contém enum", code => code.Contains("enum Status")),
            ("Contém error", code => code.Contains("error InsufficientBalance")),
            ("Contém event", code => code.Contains("event Transfer")),
            ("Contém state variables", code => code.Contains("uint256 public totalSupply")),
            ("Não contém erros", code => !code.Contains("Error generating"))
        };

        bool allPassed = true;
        foreach (var (description, check) in checks)
        {
            bool passed = check(solidityCode);
            Console.WriteLine($"{(passed ? "✅" : "❌")} {description}");
            if (!passed) allPassed = false;
        }

        Console.WriteLine();
        Console.WriteLine(allPassed ? "🎉 Todos os testes passaram!" : "⚠️ Alguns testes falharam");
    }
}

// PARA EXECUTAR EM PROGRAM.CS:
// IntegrationTest.TestCompleteFlow();