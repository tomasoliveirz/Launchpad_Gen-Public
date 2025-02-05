using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.DataAccessObjects;

public class ContractFeatureDataAccessObject(LaunchpadContext context) : BaseDataAccessObject<ContractFeature>(context), IContractFeatureDataAccessObject
{
}
