using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public abstract class BaseTokenValidator<TToken> : ITokenValidator<TToken> where TToken : BaseTokenModel
{
    public virtual void Validate(TToken obj)
    {
        //Validate Name
        MintValidator.Validate(obj);

        if(!obj.IsMintable && !obj.IsBurnable)
        {
            AccessValidator.Validate(obj.Access);
        }


        ValidateCommon(obj);
        ValidateTokenomics(obj);
        //ValidateSpecific(obj);
    }

    protected virtual void ValidateCommon(TToken token)
    {
        if (string.IsNullOrWhiteSpace(token.Name))
        {
            throw new ArgumentException("Token name cannot be empty");
        }

        if (token.IsMintable && !token.HasAccess)
        {
            throw new ArgumentException("Token must have access to be mintable");
        }

        if (token.HasAccess && (token.Access == null || token.Access.Count == 0))
            throw new ArgumentException("HasAccess is true but the Access dictionary is null or empty");

        if (token.HasPermission && (token.Permisssion == null || token.Permisssion.Count == 0))
            throw new ArgumentException("HasPermission is true but the Permission dictionary is null or empty");

        if (token.HasPermission)
        {
            foreach (var perm in token.Permisssion)
            {
                if (perm.Value == null || perm.Value.Count == 0)
                {
                    throw new ArgumentException($"Permission key '{perm.Key}' está configurada, mas não possui valores.");
                }
            }
        }
    }

    protected virtual void ValidateTokenomics(TToken token)
    {

    }

    //protected abstract void ValidateSpecific(TToken token);

}