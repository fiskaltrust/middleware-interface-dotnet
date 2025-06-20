using System;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;

#if NETCOREAPP3_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
        public class PayItem
        {
#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftPayItemId")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftPayItemId { get; set; }

                [Newtonsoft.Json.JsonProperty("Quantity", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("Quantity")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public decimal? QuantitySerialization
                {
                        get => Quantity == 1 ? null : Quantity;
                        set => Quantity = !value.HasValue ? 1 : value.Value;
                }

                [Newtonsoft.Json.JsonIgnore]
#if NETCOREAPP3_0_OR_GREATER
                [JsonIgnore]
#endif
                public decimal Quantity { get; set; } = 1;

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("Description")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string Description { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("Amount")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public decimal Amount { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftPayItemCase")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public PayItemCase ftPayItemCase { get; set; } = 0;

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftPayItemCaseData")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public object? ftPayItemCaseData { get; set; }

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
                [JsonPropertyName("MoneyGroup")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? MoneyGroup { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("MoneyNumber")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? MoneyNumber { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("MoneyBarcode")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? MoneyBarcode { get; set; }

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