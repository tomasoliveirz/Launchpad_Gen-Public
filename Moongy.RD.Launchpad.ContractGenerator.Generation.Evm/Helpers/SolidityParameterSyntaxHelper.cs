using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers
{
    public class SolidityParameterSyntaxHelper : BaseSoliditySyntaxHelper<ParameterModel>
    {
        public override string Render(ParameterModel parameter)
        {
            return parameter switch
            {
                EventParameterModel @event => RenderEventParameter(@event),
                ConstructorParameterModel constructor => RenderConstructorParameter(constructor),
                FunctionParameterModel function => RenderFunctionParameter(function),
                ReturnParameterModel @return => RenderReturnParameter(@return),
                ErrorParameterModel error => RenderErrorParameter(error),
                _ => throw new NotSupportedException("Unknown type reference")
            };
        }

        private string RenderErrorParameter(ErrorParameterModel error)
        {
            throw new NotImplementedException();
        }

        private string RenderReturnParameter(ReturnParameterModel @return)
        {
            throw new NotImplementedException();
        }

        private string RenderFunctionParameter(FunctionParameterModel function)
        {
            throw new NotImplementedException();
        }

        private string RenderEventParameter(EventParameterModel @event)
        {
            throw new NotImplementedException();
        }

        private string RenderConstructorParameter(ConstructorParameterModel constructor)
        {
            throw new NotImplementedException();
        }

        public string[] Render(ParameterModel[] parameters)
        {
            return [.. parameters.Select(Render)];
        }

    }
}
