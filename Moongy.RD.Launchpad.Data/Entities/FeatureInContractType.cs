using Moongy.RD.Launchpad.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Data.Entities
{
    public class FeatureInContractType : Entity
    {

        [ForeignKey(nameof(ContractType))]
        public int ContractTypeId { get; set; }

        [ForeignKey(nameof(ContractFeature))]
        public int ContractFeatureId { get; set; }

        public virtual ContractType? ContractType { get; set; }

        public virtual ContractFeature? ContractFeature { get; set; }
        public string? DefaultValue { get; set; }

        [JsonIgnore]
        public virtual ICollection<GenerationResultFeatureValue> GenerationResultFeatureValue { get; set; } = [];
    }
}
