namespace Moongy.RD.Launchpad.Core.Exceptions;

public class RequiredURIStorageLocationException()
    : TokenValidationException("HasURI = true, but URIStorageLocation is null or empty.");