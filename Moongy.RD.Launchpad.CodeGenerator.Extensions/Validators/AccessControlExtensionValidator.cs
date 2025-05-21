using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Validators
{
    public class AccessControlExtensionValidator : IValidator<AccessControlExtensionModel>
    {
        private const string ZeroAddress = "0x0000000000000000000000000000000000000000";

        public void Validate(AccessControlExtensionModel obj)
        {
            
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "AccessControlExtensionModel cannot be null.");
            
            if(string.IsNullOrWhiteSpace(obj.Owner))
                throw new ValidationException("Owner cannot be null or empty.");
            
            if (obj.Owner.Equals(ZeroAddress, StringComparison.OrdinalIgnoreCase))
                throw new ValidationException("Owner cannot be the zero address.");

            if (obj.Roles.Count > 0)
            {
                if (obj.Roles.Distinct(StringComparer.OrdinalIgnoreCase).Count() != obj.Roles.Count)
                    throw new ValidationException("Roles must be unique.");
            }
        }

    }
}