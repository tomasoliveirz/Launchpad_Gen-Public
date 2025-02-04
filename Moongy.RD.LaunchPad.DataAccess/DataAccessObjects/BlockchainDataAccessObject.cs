using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.LaunchPad.DataAccess.DataAccessObjects;

public class BlockchainDataAccessObject(LaunchpadContext context) : UniversalDataAccessObject<ContractVariant>
{
    private readonly LaunchpadContext _context = context;
}
