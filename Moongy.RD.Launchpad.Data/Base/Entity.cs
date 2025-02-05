using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Data.Base
{
    public class Entity
    {
        [JsonIgnore]
        [Key]
        public int Id { get; set; }

        public Guid? Uuid { get; set; }

    }
}
