
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
            throw new NotImplementedException();
        }
        
        private void ValidateEvents(IEnumerable<EventModel> events, string contractName) 
        {
            if (events.Count() != events.DistinctBy(e => e.Name).Count())
                throw new DuplicateException("Contract", contractName, "events");
        }

        private void ValidateModifiers(IEnumerable<ModifierModel> modifiers, string contractName) 
        {
            throw new NotImplementedException();
        }

        private void ValidateStateProperties(IEnumerable<StatePropertyModel> properties, string contractName)
        {
            throw new NotImplementedException();
        }
    }
}
