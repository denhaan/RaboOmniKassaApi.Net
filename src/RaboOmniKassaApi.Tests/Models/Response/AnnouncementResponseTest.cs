using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    [TestClass]
    public class AnnouncementResponseTest
    {
        [TestMethod]
        public void TestThatObjectIsCorrectlyConstructed()
        {
            var response = AnnouncementResponseBuilder.NewInstance();

            Assert.AreEqual(1000, response.PoiId);
            Assert.AreEqual("MyJwt", response.Authentication);
            Assert.AreEqual(new DateTime(1970, 1, 1), response.Expiry);
            Assert.AreEqual("merchant.order.status.changed", response.EventName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSignatureException))]
        public void TestThatInvalidSignatureExceptionIsThrownWhenTheSignaturesDoNotMatch()
        {
            var response = AnnouncementResponseBuilder.InvalidSignatureInstance();
            //response.ValidateSignature(AnnouncementResponseBuilder.GetSigningKey());
        }
    }
}
