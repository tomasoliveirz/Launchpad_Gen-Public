using System.Transactions;
using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects
{
    public class ContractTypeBusinessObject(IContractTypeDataAccessObject dao) : EntityBusinessObject<ContractType>(dao), IContractTypeBusinessObject
    {
        public override async Task<OperationResult<Guid>> CreateAsync(ContractType contractType)
        {
            return await ExecuteOperation(async () =>
            {
                var result = await dao.CreateAsync(contractType);
                return result;
            });
        }
 
        public override async Task<OperationResult> UpdateAsync(Guid uuid, ContractType entity)
        {
            return await ExecuteOperation(async () => {
                if (string.IsNullOrEmpty(entity.Name)) throw new Exception("Invalid model exception: name is missing");
                var oldRecord = await dao.GetAsync(uuid);
                if (oldRecord == null) throw new Exception("Record not found");
                oldRecord.Name = entity.Name;
                oldRecord.Description = entity.Description;
                await dao.UpdateAsync(oldRecord);
            });
        }
    }
}
