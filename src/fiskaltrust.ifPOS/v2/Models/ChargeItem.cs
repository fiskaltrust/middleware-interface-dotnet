using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class ChargeItem
    {
        [JsonProperty("ftChargeItemId")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftChargeItemId { get; set; }

        [JsonProperty("Quantity")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal Quantity { get; set; } = 1m;

        [JsonProperty("Description")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Description { get; set; }

        [JsonProperty("Amount")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal Amount { get; set; } = 0;

        [JsonProperty("VATRate")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal VATRate { get; set; } = 0;

        [JsonProperty("ftChargeItemCase")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public long ftChargeItemCase { get; set; } = 0;

        [JsonProperty("ftChargeItemCaseData")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public object? ftChargeItemCaseData { get; set; }

        [JsonProperty("VATAmount")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? VATAmount { get; set; }

        [JsonProperty("Moment")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public DateTime? Moment { get; set; }

        [JsonProperty("Position")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal Position { get; set; } = 0;

        [JsonProperty("AccountNumber")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? AccountNumber { get; set; }

        [JsonProperty("CostCenter")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? CostCenter { get; set; }

        [JsonProperty("ProductGroup")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductGroup { get; set; }

        [JsonProperty("ProductNumber")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductNumber { get; set; }

        [JsonProperty("ProductBarcode")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductBarcode { get; set; }

        [JsonProperty("Unit")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Unit { get; set; }

        [JsonProperty("UnitQuantity")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? UnitQuantity { get; set; }

        [JsonProperty("UnitPrice")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? UnitPrice { get; set; }

        [JsonProperty("Currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Order = 170, EmitDefaultValue = false, IsRequired = false)]
        public Currency Currency { get; set; }

        [JsonProperty("DecimalPrecisionMultiplier")]
        [DataMember(Order = 180, EmitDefaultValue = false, IsRequired = false)]
        public int DecimalPrecisionMultiplierSerialization
        {
            get => DecimalPrecisionMultiplier == 1 ? 0 : DecimalPrecisionMultiplier;
            set => DecimalPrecisionMultiplier = value == 0 ? 1 : value;
        }

        [JsonIgnore]
        public int DecimalPrecisionMultiplier { get; set; } = 1;
    }
}