using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models.Request
{
    /// <summary>
    /// Summary description for MerchantOrderRequest
    /// </summary>
    [DataContract]
    public class MerchantOrderRequest : MerchantOrder
    {
        [DataMember(Name = "timestamp", EmitDefaultValue = false, IsRequired = true)]
        public DateTime Timestamp { get; set; }

        private MerchantOrderRequest() { }
        public MerchantOrderRequest(MerchantOrder merchantOrder, SigningKey signingKey) : base(merchantOrder)
        {
            Timestamp = DateTime.Now;
            CalculateAndSetSignature(signingKey);
        }

        public override List<string> GetSignatureData()
        {
            var signatureData = new List<string>
            {
                Timestamp.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                MerchantOrderId
            };
            signatureData.AddRange(Amount.GetSignatureData());
            signatureData.Add(Language);
            signatureData.Add(Description);
            signatureData.Add(MerchantReturnUrl);
            if (OrderItems != null)
            {
                signatureData.AddRange(GetOrderItemSignatureData());
            }
            if (ShippingDetail != null)
            {
                signatureData.AddRange(ShippingDetail.GetSignatureData());
            }
            if (PaymentBrand.HasValue)
            {
                signatureData.Add(PaymentBrand.Value.ToFriendlyString());
            }
            if (PaymentBrandForce.HasValue)
            {
                signatureData.Add(PaymentBrandForce.Value.ToFriendlyString());
            }
            if (CustomerInformation != null)
            {
                signatureData.AddRange(CustomerInformation.GetSignatureData());
            }
            if (BillingDetail != null)
            {
                signatureData.AddRange(BillingDetail.GetSignatureData());
            }
            return signatureData;
        }

        private List<string> GetOrderItemSignatureData()
        {
            List<string> orderItemsSignatureData = new List<string>();
            foreach (var orderItem in OrderItems)
            {
                orderItemsSignatureData.AddRange(orderItem.GetSignatureData());
            }
            return orderItemsSignatureData;
        }
    }
}
