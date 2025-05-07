using System.Reflection;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.Core.Attributes;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Composers
{
    public class GenericMetaModelComposer
    {
        public static SolidityContractModel Compose<T>(T token) where T : class
           
        {
            // TODO imcomplete
            var metaModel = new SolidityContractModel() { Name= "TODO"};

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var metaModelProperty = property.GetCustomAttribute<MetaModelPropertyAttribute>();

                if (metaModelProperty == null)
                {
                    continue;
                }

            }

            return metaModel;
        }
    }
}
