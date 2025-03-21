<<<<<<< HEAD
﻿using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models;
=======
﻿using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
>>>>>>> bcf6f34b90c52fa04696589956f2afccba2cf1b7

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Models;
public class ReflectionsTokenomicModel : BaseTokenomicModel
{
    public enum ReflectionsType
    {
        Static,
        Dynamic
    }
    public decimal ReflectionsPercentage { get; set; }
    public ReflectionsType Type { get; set; }
}
