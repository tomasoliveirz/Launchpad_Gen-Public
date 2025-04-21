using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Base;
using Moongy.RD.Launchpad.Core.Models.Metamodel;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Composers
{
    public class GenericMetaModelComposer
    {
        public static SmartContractModel Compose<T>(T token) where T : class
        {
            var metaModel = new SmartContractModel();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var metaModelProperty = property.GetCustomAttribute<MetaModelPropertyAttribute>();

                if (metaModelProperty == null)
                {
                    continue;
                }

                var contractProperty = new ContractProperty
                {
                    Name = metaModelProperty.Name ?? property.Name,
                    PropertyType = metaModelProperty.PropertyType,
                    DataType = metaModelProperty.DataType
                };


                metaModel.Properties.Add(contractProperty);
            }

            return metaModel;
        }
    }
}
