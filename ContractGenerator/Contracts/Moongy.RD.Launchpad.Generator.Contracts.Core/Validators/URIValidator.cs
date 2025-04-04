using Moongy.RD.Launchpad.Core.Exceptions;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class URIValidator
{
    public static void Validate(string? uri, bool isRequired = false)
    {
        if (string.IsNullOrWhiteSpace(uri))
        {
            if (isRequired)
                throw new RequiredURIException();
            return;
        }

        bool isValidUrl = Uri.TryCreate(uri, UriKind.Absolute, out _);
        bool isValidIpfs = uri.StartsWith("ipfs://") && uri.Length > 7;
        bool isValidArweave = uri.StartsWith("ar://") && uri.Length > 5;

        if (!isValidUrl && !isValidIpfs && !isValidArweave)
            throw new InvalidURIException(uri);
    }
}