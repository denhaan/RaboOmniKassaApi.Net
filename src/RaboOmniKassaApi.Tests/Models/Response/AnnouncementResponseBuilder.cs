using System;
using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    public static class AnnouncementResponseBuilder
    {
        public static AnnouncementResponse NewInstance()
        {
            return Net.Models.Response.Response.CreateInstance<AnnouncementResponse>(GetTestData(1000), GetSigningKey());
        }

        public static AnnouncementResponse InvalidSignatureInstance()
        {
            return Net.Models.Response.Response.CreateInstance<AnnouncementResponse>(GetTestData(100), GetSigningKey());
        }

        private static string GetTestData(int poiId)
        {
            var announcementResponse = new AnnouncementResponse
            {
                PoiId = poiId,
                Authentication = "MyJwt",
                Expiry = new DateTime(1970, 1, 1),
                EventName = "merchant.order.status.changed",
                //Signature = "0a755b4e422a12db0d0ca8347dd42c4dec2ef153d7cb5e8e13198a7a91df7bbefc17ebd189107555ca62b9283085d3fadc29f69e39101c81a064edbdf6e764a5"
                //Signature = "ec0f64d23b91debd1249ee56b1b67540bf1720760cb23a9a286acc222724a8d15c7e33f193710e7e0322ced44d066f8cc6a2fdbd9398f36fb1f0a277431034aa"
            };
            CalculateAndSetSignature(announcementResponse, GetSigningKey());

            return JsonHelper.Serialize(announcementResponse);
        }

        public static SigningKey GetSigningKey()
        {
            return new SigningKey("secret");
        }

        private static void CalculateAndSetSignature(AnnouncementResponse announcementResponse, SigningKey signingKey)
        {
            var signatureData = announcementResponse.GetSignatureData();
            var preparedSignatureData = string.Join(",", signatureData);
            announcementResponse.Signature = HashHelper.GetHash(HashHelper.HashType.HmacSha512, preparedSignatureData, signingKey.GetSigningData());
        }
    }
}
