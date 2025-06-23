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
                [Newtonsoft.Json.JsonProperty("ftPayItemId", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftPayItemId")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
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

                [Newtonsoft.Json.JsonProperty("Description", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("Description")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string Description { get; set; }

                [Newtonsoft.Json.JsonProperty("Amount", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("Amount")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public decimal Amount { get; set; }

                [Newtonsoft.Json.JsonProperty("ftPayItemCase", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftPayItemCase")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public PayItemCase ftPayItemCase { get; set; } = 0;

                [Newtonsoft.Json.JsonProperty("ftPayItemCaseData", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftPayItemCaseData")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public object? ftPayItemCaseData { get; set; }

                [Newtonsoft.Json.JsonProperty("Moment", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("Moment")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public DateTime? Moment { get; set; }

                [Newtonsoft.Json.JsonProperty("Position", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("Position")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public decimal Position { get; set; } = 0;

                [Newtonsoft.Json.JsonProperty("AccountNumber", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("AccountNumber")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? AccountNumber { get; set; }

                [Newtonsoft.Json.JsonProperty("CostCenter", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("CostCenter")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? CostCenter { get; set; }

                [Newtonsoft.Json.JsonProperty("MoneyGroup", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("MoneyGroup")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? MoneyGroup { get; set; }

                [Newtonsoft.Json.JsonProperty("MoneyNumber", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("MoneyNumber")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? MoneyNumber { get; set; }

                [Newtonsoft.Json.JsonProperty("MoneyBarcode", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("MoneyBarcode")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? MoneyBarcode { get; set; }

                [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
                [Newtonsoft.Json.JsonProperty("Currency", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("Currency")]
                [JsonConverter(typeof(JsonStringEnumConverter))]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
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