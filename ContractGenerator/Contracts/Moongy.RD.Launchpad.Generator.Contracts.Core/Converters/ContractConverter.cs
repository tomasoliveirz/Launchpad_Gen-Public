

using System.Reflection;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Exceptions;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Converters
{
    public class ContractConverter<T> where T : new()
    {
        public T Convert(object contractObject)
        {
            var contractAttribute = contractObject.GetType().GetCustomAttribute<ContractAttribute>(false);
            if (contractAttribute == null || contractAttribute.ContractType != typeof(T) ) throw new NotValidContractException();
            return new();



        }
    }
}
