using System;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;

#if NETCOREAPP3_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
        public class ChargeItem
        {

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("ftChargeItemId")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftChargeItemId { get; set; }
#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("Quantity")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public decimal Quantity { get; set; } = 1m;

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("Description")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string Description { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("Amount")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public decimal Amount { get; set; } = 0;

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("VATRate")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public decimal VATRate { get; set; } = 0;

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("ftChargeItemCase")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public ChargeItemCase ftChargeItemCase { get; set; } = 0;

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("ftChargeItemCaseData")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public object? ftChargeItemCaseData { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("VATAmount")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public decimal? VATAmount { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("Moment")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public DateTime? Moment { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("Position")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public decimal Position { get; set; } = 0;

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("AccountNumber")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? AccountNumber { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("CostCenter")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? CostCenter { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("ProductGroup")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? ProductGroup { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("ProductNumber")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? ProductNumber { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("ProductBarcode")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? ProductBarcode { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("Unit")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? Unit { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("UnitQuantity")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public decimal? UnitQuantity { get; set; }

#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("UnitPrice")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public decimal? UnitPrice { get; set; }

                [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("Currency")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
#endif
                [DataMember(Order = 170, EmitDefaultValue = false, IsRequired = false)]
                public Currency Currency { get; set; }

                [Newtonsoft.Json.JsonProperty("DecimalPrecisionMultiplier", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if NETCOREAPP3_0_OR_GREATER
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
#if NETCOREAPP3_0_OR_GREATER
        [JsonIgnore]
#endif
                public int DecimalPrecisionMultiplier { get; set; } = 1;
        }
}