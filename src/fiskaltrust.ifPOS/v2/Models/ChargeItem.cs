using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using fiskaltrust.ifPOS.v2.Cases;

#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    public class ChargeItem
    {

#if NETSTANDARD2_1
        [JsonPropertyName("ftChargeItemId")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftChargeItemId { get; set; }
#if NETSTANDARD2_1
        [JsonPropertyName("Quantity")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal Quantity { get; set; } = 1m;

#if NETSTANDARD2_1
        [JsonPropertyName("Description")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Description { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("Amount")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal Amount { get; set; } = 0;

#if NETSTANDARD2_1
        [JsonPropertyName("VATRate")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal VATRate { get; set; } = 0;

#if NETSTANDARD2_1
        [JsonPropertyName("ftChargeItemCase")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public ChargeItemCase ftChargeItemCase { get; set; } = 0;

#if NETSTANDARD2_1
        [JsonPropertyName("ftChargeItemCaseData")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public object? ftChargeItemCaseData { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("VATAmount")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? VATAmount { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("Moment")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public DateTime? Moment { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("Position")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal Position { get; set; } = 0;

#if NETSTANDARD2_1
        [JsonPropertyName("AccountNumber")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? AccountNumber { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("CostCenter")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? CostCenter { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ProductGroup")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductGroup { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ProductNumber")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductNumber { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ProductBarcode")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductBarcode { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("Unit")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Unit { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("UnitQuantity")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? UnitQuantity { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("UnitPrice")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? UnitPrice { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
#if NETSTANDARD2_1
        [JsonPropertyName("Currency")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
#endif
        [DataMember(Order = 170, EmitDefaultValue = false, IsRequired = false)]
        public Currency Currency { get; set; }

        [Newtonsoft.Json.JsonProperty("DecimalPrecisionMultiplier")]
#if NETSTANDARD2_1
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
#if NETSTANDARD2_1
        [JsonIgnore]
#endif
        public int DecimalPrecisionMultiplier { get; set; } = 1;
    }
}