using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    [TestClass]
    public class MerchantOrderResponseTest
    {
        [TestMethod]
        public void TestThatObjectIsCorrectlyConstructed()
        {
            var response = MerchantOrderResponseBuilder.NewInstance();
            
            Assert.AreEqual("http://localhost/redirect/url", response.RedirectUrl);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSignatureException))]
        public void TestThatInvalidSignatureExceptionIsThrownWhenTheSignaturesDoNotMatch()
        {
            var response = MerchantOrderResponseBuilder.InvalidSignatureInstance();
            //response.ValidateSignature(MerchantOrderResponseBuilder.GetSigningKey());
        }
    }
}
