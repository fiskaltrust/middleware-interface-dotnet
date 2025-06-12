using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;

#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    public class ReceiptRequest
    {
#if NETSTANDARD2_1
        [JsonPropertyName("cbTerminalID")]
#endif
        [DataMember(Order = 10, EmitDefaultValue = false, IsRequired = false)]
        public string? cbTerminalID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbReceiptReference")]
#endif
        [DataMember(Order = 20, EmitDefaultValue = true, IsRequired = true)]
        public string cbReceiptReference { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbReceiptMoment")]
#endif
        [DataMember(Order = 30, EmitDefaultValue = true, IsRequired = true)]
        public DateTime cbReceiptMoment { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbChargeItems")]
#endif
        [DataMember(Order = 40, EmitDefaultValue = true, IsRequired = true)]
        public List<ChargeItem> cbChargeItems { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbPayItems")]
#endif
        [DataMember(Order = 50, EmitDefaultValue = true, IsRequired = true)]
        public List<PayItem> cbPayItems { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftCashBoxID")]
#endif
        [DataMember(Order = 60, EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftCashBoxID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftPosSystemId")]
#endif
        [DataMember(Order = 70, EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftPosSystemId { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftReceiptCase")]
#endif
        [DataMember(Order = 80, EmitDefaultValue = false, IsRequired = false)]
        public ReceiptCase ftReceiptCase { get; set; } = 0;

#if NETSTANDARD2_1
        [JsonPropertyName("ftReceiptCaseData")]
#endif
        [DataMember(Order = 90, EmitDefaultValue = false, IsRequired = false)]
        public object? ftReceiptCaseData { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftQueueID")]
#endif
        [DataMember(Order = 100, EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftQueueID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbPreviousReceiptReference")]
#endif
        [DataMember(Order = 110, EmitDefaultValue = false, IsRequired = false)]
        public object? cbPreviousReceiptReference { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbReceiptAmount")]
#endif
        [DataMember(Order = 120, EmitDefaultValue = false, IsRequired = false)]
        public decimal? cbReceiptAmount { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbUser")]
#endif
        [DataMember(Order = 130, EmitDefaultValue = false, IsRequired = false)]
        public object? cbUser { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbArea")]
#endif
        [DataMember(Order = 140, EmitDefaultValue = false, IsRequired = false)]
        public object? cbArea { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbCustomer")]
#endif
        [DataMember(Order = 150, EmitDefaultValue = false, IsRequired = false)]
        public object? cbCustomer { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbSettlement")]
#endif
        [DataMember(Order = 160, EmitDefaultValue = false, IsRequired = false)]
        public object? cbSettlement { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
#if NETSTANDARD2_1
        [JsonPropertyName("Currency")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
#endif
        [DataMember(Order = 170, EmitDefaultValue = false, IsRequired = false)]
        public Currency Currency { get; set; }

        [Newtonsoft.Json.JsonProperty("DecimalPrecisionMultiplier", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
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
