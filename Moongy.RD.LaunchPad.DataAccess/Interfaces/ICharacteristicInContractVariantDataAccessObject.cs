using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.Interfaces;

public interface ICharacteristicInContractVariantDataAccessObject : IBaseDataAccessObject<CharacteristicInContractVariant>
{
    Task<IEnumerable<CharacteristicInContractVariant>> GetCharacteristicsInContractVariants();

    Task<CharacteristicInContractVariant> GetCharacteristicInContractVariant(Guid contractCharacteristicUuid);
}
