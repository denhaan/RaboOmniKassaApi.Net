using RaboOmniKassaApi.Net.Connectors;
using RaboOmniKassaApi.Net.Connectors.Http;

namespace RaboOmniKassaApi.Tests.Connectors
{
    public class ApiConnectorWrapper : ApiConnector
    {
        public ApiConnectorWrapper(IRestTemplate restTemplate, TokenProvider tokenProvider) : base(restTemplate, tokenProvider) { }
    }
}
