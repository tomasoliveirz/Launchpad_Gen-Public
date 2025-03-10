using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.LaunchPad.DataAccess;

public static class LaunchpadSeeder
{
    public static void Seed(LaunchpadContext context)
    {
        var contractTypes = new List<ContractType>
        {
            new ContractType { Uuid = Guid.NewGuid(), Name = "teste1234" }

        };

        context.AddRange(contractTypes);
        context.SaveChanges();
    }
}
