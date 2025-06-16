using System;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;

#if !WCF
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    public class ChargeItem
    {

#if !WCF
        [JsonPropertyName("ftChargeItemId")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftChargeItemId { get; set; }
#if !WCF
        [JsonPropertyName("Quantity")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal Quantity { get; set; } = 1m;

#if !WCF
        [JsonPropertyName("Description")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Description { get; set; }

#if !WCF
        [JsonPropertyName("Amount")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal Amount { get; set; } = 0;

#if !WCF
        [JsonPropertyName("VATRate")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal VATRate { get; set; } = 0;

#if !WCF
        [JsonPropertyName("ftChargeItemCase")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public ChargeItemCase ftChargeItemCase { get; set; } = 0;

#if !WCF
        [JsonPropertyName("ftChargeItemCaseData")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public object? ftChargeItemCaseData { get; set; }

#if !WCF
        [JsonPropertyName("VATAmount")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? VATAmount { get; set; }

#if !WCF
        [JsonPropertyName("Moment")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public DateTime? Moment { get; set; }

#if !WCF
        [JsonPropertyName("Position")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal Position { get; set; } = 0;

#if !WCF
        [JsonPropertyName("AccountNumber")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? AccountNumber { get; set; }

#if !WCF
        [JsonPropertyName("CostCenter")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? CostCenter { get; set; }

#if !WCF
        [JsonPropertyName("ProductGroup")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductGroup { get; set; }

#if !WCF
        [JsonPropertyName("ProductNumber")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductNumber { get; set; }

#if !WCF
        [JsonPropertyName("ProductBarcode")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductBarcode { get; set; }

#if !WCF
        [JsonPropertyName("Unit")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Unit { get; set; }

#if !WCF
        [JsonPropertyName("UnitQuantity")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? UnitQuantity { get; set; }

#if !WCF
        [JsonPropertyName("UnitPrice")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? UnitPrice { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
#if !WCF
        [JsonPropertyName("Currency")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
#endif
        [DataMember(Order = 170, EmitDefaultValue = false, IsRequired = false)]
        public Currency Currency { get; set; }

        [Newtonsoft.Json.JsonProperty("DecimalPrecisionMultiplier", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
        [JsonPropertyName("DecimalPrecisionMultiplier")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        [DataMember(Order = 180, EmitDefaultValue = false, IsRequired = false)]
        public int DecimalPrecisionMultiplierSerialization
        {
            get => DecimalPrecisionMultiplier == 1 ? 0 : DecimalPrecisionMultiplier;
            set => DecimalPrecisionMultiplier = value == 0 ? 1 : value;
        }

        [Newtonsoft.Json.JsonIgnore]
#if !WCF
        [JsonIgnore]
#endif
        public int DecimalPrecisionMultiplier { get; set; } = 1;
    }
}