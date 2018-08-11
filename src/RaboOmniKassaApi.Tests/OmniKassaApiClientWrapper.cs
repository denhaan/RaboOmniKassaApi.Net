using RaboOmniKassaApi.Net;
using RaboOmniKassaApi.Net.Connectors;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests
{
    public class OmniKassaApiClientWrapper : OmniKassaApiClient
    {
        public OmniKassaApiClientWrapper(IConnector connector, SigningKey signingKey) : base(connector, signingKey) { }
    }
}
