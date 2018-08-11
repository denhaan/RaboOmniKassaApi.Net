using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models.Response
{
    /// <summary>
    /// With an instance of this class you can retrieve the order statuses at the Rabobank OmniKassa.
    /// </summary>
    [DataContract]
    public class AnnouncementResponse : Response
    {
        [DataMember(Name = "poiId", EmitDefaultValue = false, IsRequired = true, Order = 1)]
        public int PoiId { get; set; }

        [DataMember(Name = "authentication", EmitDefaultValue = false, IsRequired = true, Order = 2)]
        public string Authentication { get; set; }

        [DataMember(Name = "expiry", EmitDefaultValue = false, IsRequired = true, Order = 3)]
        public DateTime Expiry { get; set; }

        [DataMember(Name = "eventName", EmitDefaultValue = false, IsRequired = true, Order = 4)]
        public string EventName { get; set; }

        public override List<string> GetSignatureData()
        {
            return new List<string> { Authentication, Expiry.ToString("yyyy-MM-ddTHH:mm:sszzz"), EventName, PoiId.ToString() };
        }
    }
}