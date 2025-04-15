using Moongy.RD.Launchpad.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Tools.TokenWizard.Models
{
    public class TokenWizardResponse
    {
        public string Token { get; set; }
        public string Question { get; set; }
        public IEnumerable<TokenClassification> Tags { get; set; }
        public IEnumerable<string> PossibleAnswers { get; set; }
        public IEnumerable<string> PreviousResponses { get; set; }
    }
}
