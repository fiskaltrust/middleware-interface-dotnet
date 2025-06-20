using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
using fiskaltrust.ifPOS.v2.Converters;

#if NETCOREAPP3_0_OR_GREATER
using System.Text.Json.Serialization;
using System.Text.Json;
#endif

namespace fiskaltrust.ifPOS.v2
{
        public class ReceiptRequest
        {
#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbTerminalID")]
#endif
                [DataMember(Order = 10, EmitDefaultValue = false, IsRequired = false)]
                public string? cbTerminalID { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbReceiptReference")]
#endif
                [DataMember(Order = 20, EmitDefaultValue = true, IsRequired = true)]
                public string cbReceiptReference { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbReceiptMoment")]
#endif
                [DataMember(Order = 30, EmitDefaultValue = true, IsRequired = true)]
                public DateTime cbReceiptMoment { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbChargeItems")]
#endif
                [DataMember(Order = 40, EmitDefaultValue = true, IsRequired = true)]
                public List<ChargeItem> cbChargeItems { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbPayItems")]
#endif
                [DataMember(Order = 50, EmitDefaultValue = true, IsRequired = true)]
                public List<PayItem> cbPayItems { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftCashBoxID")]
#endif
                [DataMember(Order = 60, EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftCashBoxID { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftPosSystemId")]
#endif
                [DataMember(Order = 70, EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftPosSystemId { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftReceiptCase")]
#endif
                [DataMember(Order = 80, EmitDefaultValue = false, IsRequired = false)]
                public ReceiptCase ftReceiptCase { get; set; } = 0;

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftReceiptCaseData")]
#endif
                [DataMember(Order = 90, EmitDefaultValue = false, IsRequired = false)]
                public object? ftReceiptCaseData { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftQueueID")]
#endif
                [DataMember(Order = 100, EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftQueueID { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbPreviousReceiptReference")]
#endif
                [DataMember(Order = 110, EmitDefaultValue = false, IsRequired = false)]
                public cbPreviousReceiptReference? cbPreviousReceiptReference { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbReceiptAmount")]
#endif
                [DataMember(Order = 120, EmitDefaultValue = false, IsRequired = false)]
                public decimal? cbReceiptAmount { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbUser")]
#endif
                [DataMember(Order = 130, EmitDefaultValue = false, IsRequired = false)]
                public object? cbUser { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbArea")]
#endif
                [DataMember(Order = 140, EmitDefaultValue = false, IsRequired = false)]
                public object? cbArea { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbCustomer")]
#endif
                [DataMember(Order = 150, EmitDefaultValue = false, IsRequired = false)]
                public object? cbCustomer { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbSettlement")]
#endif
                [DataMember(Order = 160, EmitDefaultValue = false, IsRequired = false)]
                public object? cbSettlement { get; set; }

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
