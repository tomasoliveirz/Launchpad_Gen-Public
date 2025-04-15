using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Data.Pocos
{
    public class TokenWizardResponse
    {
        public string Token { get; set; }
        public string Question { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public List<string> PreviousResponses { get; set; }
    }
}
