using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Exceptions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Validator
{
    public class SoliditySmartContractModelValidator
    {
        public void Validate(SolidityContractModel model)
        {
            ValidateErrors(model.Errors, model.Name);
            ValidateEvents(model.Events, model.Name);
            ValidateModifiers(model.Modifiers, model.Name);
            ValidateStateProperties(model.StateProperties, model.Name);
        }

        private void ValidateErrors(IEnumerable<ErrorModel> errors, string contractName) 
        {
            if (errors.Count() != errors.DistinctBy(e => e.Name).Count())
                throw new DuplicateException("Contract", contractName, "errors");
        }
        
        private void ValidateEvents(IEnumerable<EventModel> events, string contractName) 
        {
            if (events.Count() != events.DistinctBy(e => e.Name).Count())
                throw new DuplicateException("Contract", contractName, "events");
        }

        private void ValidateModifiers(IEnumerable<ModifierModel> modifiers, string contractName) 
        {
            if (modifiers.Count() != modifiers.DistinctBy(m => m.Name).Count())
                throw new DuplicateException("Contract", contractName, "modifiers");
        }

        private void ValidateStateProperties(IEnumerable<StatePropertyModel> properties, string contractName)
        {
            if (properties.Count() != properties.DistinctBy(p => p.Name).Count())
                throw new DuplicateException("Contract", contractName, "state properties");
        }
    }
}