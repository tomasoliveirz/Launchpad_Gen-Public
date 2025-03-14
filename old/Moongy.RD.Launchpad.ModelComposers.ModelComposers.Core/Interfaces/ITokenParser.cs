using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Interfaces
{
    public interface ITokenParser<T> where T : TokenModel
    {
        SmartContractModel Parse(T model);
    }

}
