using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Business.Interfaces;

public interface ICharacteristicInContractVariantBusinessObject : IEntityBusinessObject<CharacteristicInContractVariant>
{
    public Task<OperationResult<Guid>> CreateAsync(CharacteristicInContractVariant characteristicInContractVariant, Guid contractVariantUuid, Guid contractCharacteristicUuid);

    public Task<OperationResult> UpdateAsync(Guid uuid, CharacteristicInContractVariant characteristicInContractVariant, Guid? contractVariantUuid, Guid? contractCharacteristicUuid);

    public Task<OperationResult<IEnumerable<CharacteristicInContractVariant>>> GetCharacteristicsInContractVariants();

    public Task<OperationResult<CharacteristicInContractVariant>> GetCharacteristicInContractVariant(Guid contractCharacteristicUuid);
}
