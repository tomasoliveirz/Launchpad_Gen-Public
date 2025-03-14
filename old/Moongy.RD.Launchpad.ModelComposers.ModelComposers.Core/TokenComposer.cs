using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Interfaces;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core
{
    public abstract class TokenComposer<T, TValidator, TParser>(TValidator validator, TParser parser) 
                            where T : TokenModel 
                            where TValidator : ITokenValidator<T>
                            where TParser : ITokenParser<T>
    {
        public virtual SmartContractModel Compose(T token)
        {
            validator.Validate(token);
            return parser.Parse(token);
        }

        public virtual void Validate(T token)
        {
            validator.Validate(token);
        }
    }
}
