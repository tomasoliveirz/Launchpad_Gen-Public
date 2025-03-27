namespace Moongy.RD.Launchpad.Core.Exceptions;

public class InvalidURIException(string uri)
    : URIException($"URI '{uri}' must be a valid URL, IPFS hash, or Arweave hash.");