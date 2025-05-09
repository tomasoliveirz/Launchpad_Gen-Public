using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Data.Forms
{
    public class StableCoinForm : FungibleTokenForm
    {
        public bool Custodian { get; set; }
        public bool Allowlist { get; set; }
        public bool Blocklist { get; set; }
    }
}
