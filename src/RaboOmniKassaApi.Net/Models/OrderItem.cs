using System.Collections.Generic;
using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models
{
    /// <summary>
    /// Summary description for OrderItem
    /// </summary>
    [DataContract]
    public class OrderItem : ISignatureDataProvider
    {
        [DataMember(Name = "id", EmitDefaultValue = false, IsRequired = false, Order = 1)]
        public string Id { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false, IsRequired = true, Order = 2)]
        public string Name { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false, IsRequired = false, Order = 3)]
        public string Description { get; set; }

        [DataMember(Name = "quantity", EmitDefaultValue = false, IsRequired = true, Order = 4)]
        public int Quantity { get; set; }

        [DataMember(Name = "amount", EmitDefaultValue = false, IsRequired = true, Order = 5)]
        public Money Amount { get; set; }

        [DataMember(Name = "tax", EmitDefaultValue = false, IsRequired = false, Order = 6)]
        public Money Tax { get; set; }

        [DataMember(Name = "category", EmitDefaultValue = false, IsRequired = true, Order = 7)]
        public ProductType? Category { get; set; }

        [DataMember(Name = "vatCategory", EmitDefaultValue = false, IsRequired = false, Order = 8)]
        public VatCategory? VatCategory { get; set; }

        public List<string> GetSignatureData()
        {
            var data = new List<string>();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                data.Add(Id);
            }
            data.Add(Name);
            data.Add(Description);
            data.Add(Quantity.ToString());
            data.AddRange(Amount.GetSignatureData());
            data.AddRange(Tax != null ? Tax.GetSignatureData() : new List<string> { null });
            data.Add(Category.HasValue ? Category.Value.ToFriendlyString(): null);
            if (VatCategory.HasValue)
            {
                data.Add(VatCategory.Value.ToFriendlyString());
            }
            return data;
        }
    }
}