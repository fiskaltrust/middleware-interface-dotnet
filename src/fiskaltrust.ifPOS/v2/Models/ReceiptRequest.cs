using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
using fiskaltrust.ifPOS.v2.Converters;

#if !WCF
using System.Text.Json.Serialization;
using System.Text.Json;
#endif

namespace fiskaltrust.ifPOS.v2
{
        public class ReceiptRequest
        {
                [Newtonsoft.Json.JsonProperty("cbTerminalID", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("cbTerminalID")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]

#endif
                [DataMember(Order = 10, EmitDefaultValue = false, IsRequired = false)]
                public string? cbTerminalID { get; set; }

                [Newtonsoft.Json.JsonProperty("cbReceiptReference", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("cbReceiptReference")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(Order = 20, EmitDefaultValue = true, IsRequired = true)]
                public string cbReceiptReference { get; set; }

                [Newtonsoft.Json.JsonProperty("cbReceiptMoment", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("cbReceiptMoment")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(Order = 30, EmitDefaultValue = true, IsRequired = true)]
                public DateTime cbReceiptMoment { get; set; }

                [Newtonsoft.Json.JsonProperty("cbChargeItems", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("cbChargeItems")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(Order = 40, EmitDefaultValue = true, IsRequired = true)]
                public List<ChargeItem> cbChargeItems { get; set; }

                [Newtonsoft.Json.JsonProperty("cbPayItems", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("cbPayItems")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(Order = 50, EmitDefaultValue = true, IsRequired = true)]
                public List<PayItem> cbPayItems { get; set; }

                [Newtonsoft.Json.JsonProperty("ftCashBoxID", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftCashBoxID")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 60, EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftCashBoxID { get; set; }

                [Newtonsoft.Json.JsonProperty("ftPosSystemId", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftPosSystemId")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 70, EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftPosSystemId { get; set; }

                [Newtonsoft.Json.JsonProperty("ftReceiptCase", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftReceiptCase")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(Order = 80, EmitDefaultValue = false, IsRequired = false)]
                public ReceiptCase ftReceiptCase { get; set; } = 0;

                [Newtonsoft.Json.JsonProperty("ftReceiptCaseData", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftReceiptCaseData")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 90, EmitDefaultValue = false, IsRequired = false)]
                public object? ftReceiptCaseData { get; set; }

                [Newtonsoft.Json.JsonProperty("ftQueueID", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftQueueID")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 100, EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftQueueID { get; set; }

                [Newtonsoft.Json.JsonProperty("cbPreviousReceiptReference", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("cbPreviousReceiptReference")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 110, EmitDefaultValue = false, IsRequired = false)]
                public cbPreviousReceiptReference? cbPreviousReceiptReference { get; set; }

                [Newtonsoft.Json.JsonProperty("cbReceiptAmount", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("cbReceiptAmount")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 120, EmitDefaultValue = false, IsRequired = false)]
                public decimal? cbReceiptAmount { get; set; }

                [Newtonsoft.Json.JsonProperty("cbUser", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("cbUser")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 130, EmitDefaultValue = false, IsRequired = false)]
                public object? cbUser { get; set; }

                [Newtonsoft.Json.JsonProperty("cbArea", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("cbArea")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 140, EmitDefaultValue = false, IsRequired = false)]
                public object? cbArea { get; set; }

                [Newtonsoft.Json.JsonProperty("cbCustomer", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("cbCustomer")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 150, EmitDefaultValue = false, IsRequired = false)]
                public object? cbCustomer { get; set; }

                [Newtonsoft.Json.JsonProperty("cbSettlement", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("cbSettlement")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 160, EmitDefaultValue = false, IsRequired = false)]
                public object? cbSettlement { get; set; }

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
