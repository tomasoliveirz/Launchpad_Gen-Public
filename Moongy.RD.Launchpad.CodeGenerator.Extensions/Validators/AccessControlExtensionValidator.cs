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

            bool hasValidOwner = !string.IsNullOrWhiteSpace(obj.Owner) && 
                                !obj.Owner.Equals(ZeroAddress, StringComparison.OrdinalIgnoreCase);
            
            bool hasValidRoles = obj.HasRoles && obj.Roles?.Count > 0;

            if (hasValidOwner && hasValidRoles)
                throw new ValidationException("Cannot have both Owner and Roles defined. Choose either Owner-based or Role-based access control, not both.");

            if (!hasValidOwner && !hasValidRoles)
                throw new ValidationException("Must have either a valid Owner or valid Roles defined for access control.");

            if (!obj.HasRoles)
            {
                if (string.IsNullOrWhiteSpace(obj.Owner))
                    throw new ValidationException("Owner cannot be null or empty when not using role-based access control.");
                
                if (obj.Owner.Equals(ZeroAddress, StringComparison.OrdinalIgnoreCase))
                    throw new ValidationException("Owner cannot be the zero address.");
            }

            if (obj.HasRoles)
            {
                if (obj.Roles == null || obj.Roles.Count == 0)
                    throw new ValidationException("Roles list cannot be empty when HasRoles is true.");

                if (obj.Roles.Distinct(StringComparer.OrdinalIgnoreCase).Count() != obj.Roles.Count)
                    throw new ValidationException("Roles must be unique.");
                
                if (!string.IsNullOrWhiteSpace(obj.Owner))
                    throw new ValidationException("Owner must be null or empty when using role-based access control.");
            }
        }
    }
}