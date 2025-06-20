using System;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;

#if !WCF
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
        public class PayItem
        {
#if !WCF
                [JsonPropertyName("ftPayItemId")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftPayItemId { get; set; }

                [Newtonsoft.Json.JsonProperty("Quantity", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
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
#if !WCF
                [JsonIgnore]
#endif
                public decimal Quantity { get; set; } = 1;

#if !WCF
                [JsonPropertyName("Description")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string Description { get; set; }

#if !WCF
                [JsonPropertyName("Amount")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public decimal Amount { get; set; }

#if !WCF
                [JsonPropertyName("ftPayItemCase")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public PayItemCase ftPayItemCase { get; set; } = 0;

#if !WCF
                [JsonPropertyName("ftPayItemCaseData")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public object? ftPayItemCaseData { get; set; }

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
                [JsonPropertyName("MoneyGroup")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? MoneyGroup { get; set; }

#if !WCF
                [JsonPropertyName("MoneyNumber")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? MoneyNumber { get; set; }

#if !WCF
                [JsonPropertyName("MoneyBarcode")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? MoneyBarcode { get; set; }

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