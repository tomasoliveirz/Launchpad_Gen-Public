using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Business.Interfaces;

public interface IContractVariantBusinessObject : IEntityBusinessObject<ContractVariant>
{
    public Task<OperationResult<Guid>> CreateAsync(ContractVariant contractVariant, Guid contractTypeUuid);
    public Task<OperationResult> UpdateAsync(Guid uuid, ContractVariant contractVariant, Guid? contractTypeUuid);
}
