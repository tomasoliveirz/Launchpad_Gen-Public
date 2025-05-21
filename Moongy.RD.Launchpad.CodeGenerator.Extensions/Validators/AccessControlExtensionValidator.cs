using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Validators
{
    public class AccessControlExtensionValidator : IValidator<AccessControlExtensionModel>
    {
        public void Validate(AccessControlExtensionModel obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
        }

    }
}