using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Exceptions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;


namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class BlockchainNetworkBusinessObject(IBlockchainNetworkDataAccessObject dao) : EntityBusinessObject<BlockchainNetwork>(dao), IBlockchainNetworkBusinessObject
{
    public override async Task<OperationResult<Guid>> CreateAsync(BlockchainNetwork blockchainNetwork)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(blockchainNetwork.Name)) throw new InvalidModelException("name is missing");
            var result = await dao.CreateAsync(blockchainNetwork);
            return result;
        });
    }

    public override async Task<OperationResult> UpdateAsync(Guid uuid, BlockchainNetwork blockchainNetwork)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(blockchainNetwork.Name)) throw new InvalidModelException("name is missing");
            var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Blockchain Network", uuid.ToString());
            oldRecord.Name = blockchainNetwork.Name;
            oldRecord.Description = blockchainNetwork.Description;
            oldRecord.Image = blockchainNetwork.Image;
            await dao.UpdateAsync(oldRecord);
        });
    }
}
