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
                [Newtonsoft.Json.JsonProperty("ftChargeItemId", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftChargeItemId")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftChargeItemId { get; set; }

                [Newtonsoft.Json.JsonProperty("Quantity", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("Quantity")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public decimal Quantity { get; set; } = 1m;

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
                public decimal Amount { get; set; } = 0;

                [Newtonsoft.Json.JsonProperty("VATRate", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("VATRate")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public decimal VATRate { get; set; } = 0;

                [Newtonsoft.Json.JsonProperty("ftChargeItemCase", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftChargeItemCase")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public ChargeItemCase ftChargeItemCase { get; set; } = 0;

                [Newtonsoft.Json.JsonProperty("ftChargeItemCaseData", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftChargeItemCaseData")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public object? ftChargeItemCaseData { get; set; }

                [Newtonsoft.Json.JsonProperty("VATAmount", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("VATAmount")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public decimal? VATAmount { get; set; }

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

                [Newtonsoft.Json.JsonProperty("ProductGroup", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ProductGroup")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? ProductGroup { get; set; }

                [Newtonsoft.Json.JsonProperty("ProductNumber", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ProductNumber")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? ProductNumber { get; set; }

                [Newtonsoft.Json.JsonProperty("ProductBarcode", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ProductBarcode")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? ProductBarcode { get; set; }

                [Newtonsoft.Json.JsonProperty("Unit", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("Unit")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? Unit { get; set; }

                [Newtonsoft.Json.JsonProperty("UnitQuantity", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("UnitQuantity")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public decimal? UnitQuantity { get; set; }

                [Newtonsoft.Json.JsonProperty("UnitPrice", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("UnitPrice")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public decimal? UnitPrice { get; set; }

                [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
                [Newtonsoft.Json.JsonProperty("Currency", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("Currency")]
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