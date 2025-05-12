using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class ReceiptRequest
    {
        [JsonProperty("cbTerminalID")]
        [DataMember(Order = 10, EmitDefaultValue = false, IsRequired = false)]
        public string? cbTerminalID { get; set; }

        [JsonProperty("cbReceiptReference")]
        [DataMember(Order = 20, EmitDefaultValue = true, IsRequired = true)]
        public string cbReceiptReference { get; set; }

        [JsonProperty("cbReceiptMoment")]
        [DataMember(Order = 30, EmitDefaultValue = true, IsRequired = true)]
        public DateTime cbReceiptMoment { get; set; }

        [JsonProperty("cbChargeItems")]
        [DataMember(Order = 40, EmitDefaultValue = true, IsRequired = true)]
        public List<ChargeItem> cbChargeItems { get; set; }

        [JsonProperty("cbPayItems")]
        [DataMember(Order = 50, EmitDefaultValue = true, IsRequired = true)]
        public List<PayItem> cbPayItems { get; set; }

        [JsonProperty("ftCashBoxID")]
        [DataMember(Order = 60, EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftCashBoxID { get; set; }

        [JsonProperty("ftPosSystemId")]
        [DataMember(Order = 70, EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftPosSystemId { get; set; }

        [JsonProperty("ftReceiptCase")]
        [DataMember(Order = 80, EmitDefaultValue = false, IsRequired = false)]
        public ReceiptCase  ftReceiptCase { get; set; } = 0;

        [JsonProperty("ftReceiptCaseData")]
        [DataMember(Order = 90, EmitDefaultValue = false, IsRequired = false)]
        public object? ftReceiptCaseData { get; set; }

        [JsonProperty("ftQueueID")]
        [DataMember(Order = 100, EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftQueueID { get; set; }

        [JsonProperty("cbPreviousReceiptReference")]
        [DataMember(Order = 110, EmitDefaultValue = false, IsRequired = false)]
        public string? cbPreviousReceiptReference { get; set; }

        [JsonProperty("cbReceiptAmount")]
        [DataMember(Order = 120, EmitDefaultValue = false, IsRequired = false)]
        public decimal? cbReceiptAmount { get; set; }

        [JsonProperty("cbUser")]
        [DataMember(Order = 130, EmitDefaultValue = false, IsRequired = false)]
        public object? cbUser { get; set; }

        [JsonProperty("cbArea")]
        [DataMember(Order = 140, EmitDefaultValue = false, IsRequired = false)]
        public object? cbArea { get; set; }

        [JsonProperty("cbCustomer")]
        [DataMember(Order = 150, EmitDefaultValue = false, IsRequired = false)]
        public object? cbCustomer { get; set; }

        [JsonProperty("cbSettlement")]
        [DataMember(Order = 160, EmitDefaultValue = false, IsRequired = false)]
        public object? cbSettlement { get; set; }

        [JsonProperty("Currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Order = 170, EmitDefaultValue = false, IsRequired = false)]
        public Currency Currency { get; set; }

        [JsonProperty("DecimalPrecisionMultiplier")]
        [DataMember(Order = 180, EmitDefaultValue = false, IsRequired = false)]
        public int DecimalPrecisionMultiplierSerialization
        {
            get => DecimalPrecisionMultiplier == 1 ? 0 : DecimalPrecisionMultiplier;
            set => DecimalPrecisionMultiplier = value == 0 ? 1 : value;
        }

        [JsonIgnore]
        public int DecimalPrecisionMultiplier { get; set; } = 1;
    }
}