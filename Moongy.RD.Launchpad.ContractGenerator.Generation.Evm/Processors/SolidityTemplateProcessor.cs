using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public static class SolidityTemplateProcessor
    {
        public static ConstructorProcessor Constructor { get; } = new ConstructorProcessor();
        public static ContractHeaderProcessor ContractHeader { get; } = new ContractHeaderProcessor();
        public static EventProcessor Events { get; } = new EventProcessor();
        public static EnumProcessor Enums { get; } = new EnumProcessor();
        public static ModifierProcessor Modifiers { get; } = new ModifierProcessor();
        public static FileHeaderProcessor FileHeader { get; } = new FileHeaderProcessor();
        public static ImportProcessor Imports { get; } = new ImportProcessor();
        public static StatePropertyProcessor StateProperties { get; } = new StatePropertyProcessor();
        public static FunctionProcessor Functions { get; } = new FunctionProcessor();
        public static StructProcessor Structs { get; } = new StructProcessor();
        public static ErrorProcessor Errors { get; } = new ErrorProcessor();
    }
}
