using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Interfaces;
namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Validators.Common
{
    public static class UriValidator
    {
        public static void Validate(string? uri, bool isRequired = false)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                if (isRequired)
                    throw new ValidationException("URI is required.");
                return;
            }
            
            bool isValidUrl = Uri.TryCreate(uri, UriKind.Absolute, out _);
            bool isValidIpfs = uri.StartsWith("ipfs://") && uri.Length > 7;
            bool isValidArweave = uri.StartsWith("ar://") && uri.Length > 5;
            
            if (!isValidUrl && !isValidIpfs && !isValidArweave)
                throw new ValidationException($"Invalid URI format: {uri}");
        }
    }
}