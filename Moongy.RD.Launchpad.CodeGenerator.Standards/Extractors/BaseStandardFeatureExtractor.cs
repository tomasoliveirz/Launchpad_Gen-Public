using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Standards.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Extractors
{
    public class BaseStandardFeatureExtractor<T>(StandardEnum standard) : IFeatureExtractor<T> where T:BaseContractModel, new()
    {
        public virtual T Extract(object form)
        {
            CheckStandardType(form);

            var model = new T();

            foreach (var (property, attr) in form.GetStandardProperties().Where(p => p.Item2.Source.Equals(standard)))
            {
                var modelProp = typeof(T).GetProperty(attr.Name ?? property.Name);
                if (modelProp == null)
                    continue;

                var value = property.GetValue(form);

                if (value != null && !modelProp.PropertyType.IsAssignableFrom(value.GetType()))
                    throw new InvalidOperationException($"Property type mismatch for '{modelProp.Name}'. Expected: {modelProp.PropertyType}, Received: {value.GetType()}.");

                modelProp.SetValue(model, value);
            }
            return model;

        }

        private void CheckStandardType(object form)
        {
            var formStandard = form.GetStandardFromForm();
            if (formStandard.HasValue)
            {
                if (formStandard != standard)
                    throw new InvalidOperationException($"Form standard ({formStandard}) does not match extractor standard ({standard}).");
            }
            else if (form.HasConflictingStandards(standard))
            {
                throw new InvalidOperationException($"Detected standard does not match extractor standard ({standard}).");
            }
        }
    }
}
