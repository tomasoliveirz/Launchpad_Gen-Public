using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.LaunchPad.DataAccess.Interfaces;

public interface IFeatureInContractTypeDataAccessObject : IBaseDataAccessObject<FeatureInContractType>
{
    Task<IEnumerable<FeatureInContractType>> GetFeatureInContractTypes();

    Task<FeatureInContractType> GetFeatureInContractType(Guid contractFeatureUuid);
}
