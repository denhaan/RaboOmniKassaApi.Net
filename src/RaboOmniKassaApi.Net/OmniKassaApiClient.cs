using System;
using System.Net;
using RaboOmniKassaApi.Net.Connectors;
using RaboOmniKassaApi.Net.Connectors.Http;
using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models.Request;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net
{
    /// <summary>
    /// Summary description for OmniKassaApiClient
    /// </summary>
    public class OmniKassaApiClient
    {
        private readonly string _baseUrl = "https://betalen.rabobank.nl/omnikassa-api";
        private readonly IConnector _connector;
        private readonly SigningKey _signingKey;

        public OmniKassaApiClient(string refreshToken, string signingKey, bool testMode = false)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var tokenProvider = new InMemoryTokenProvider(refreshToken);
            if (testMode) _baseUrl += "-sandbox";
            _connector = new ApiConnector(new HttpClientRestTemplate(_baseUrl), tokenProvider);
            _signingKey = new SigningKey(Convert.FromBase64String(signingKey));
        }

        protected OmniKassaApiClient(IConnector connector, SigningKey signingKey)
        {
            _connector = connector;
            _signingKey = signingKey;
        }

        public string AnnounceMerchantOrder(MerchantOrder merchantOrder)
        {
            var request = new MerchantOrderRequest(merchantOrder, _signingKey);
            var responseAsJson = _connector.AnnounceMerchantOrder(request);
            var response = Response.CreateInstance<MerchantOrderResponse>(responseAsJson, _signingKey);
            return response.RedirectUrl;
        }

        public MerchantOrderStatusResponse RetrieveAnnouncement(AnnouncementResponse announcementResponse)
        {
            var announcementDataAsJson = _connector.GetAnnouncementData(announcementResponse);
            var merchantOrderStatusResponse = JsonHelper.Deserialize<MerchantOrderStatusResponse>(announcementDataAsJson);
            merchantOrderStatusResponse.ValidateSignature(_signingKey);
            return merchantOrderStatusResponse;
        }
    }
}