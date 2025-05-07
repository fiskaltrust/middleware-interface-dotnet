using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class ReceiptRequest
    {
        [JsonPropertyName("cbTerminalID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 10, EmitDefaultValue = false, IsRequired = false)]
        public string? cbTerminalID { get; set; } = "undefined";

        [JsonPropertyName("cbReceiptReference")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(Order = 20, EmitDefaultValue = true, IsRequired = true)]
        public string cbReceiptReference { get; set; } = "undefined";

        [JsonPropertyName("cbReceiptMoment")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(Order = 30, EmitDefaultValue = true, IsRequired = true)]
        public DateTime cbReceiptMoment { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("cbChargeItems")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(Order = 40, EmitDefaultValue = true, IsRequired = true)]
        public List<ChargeItem> cbChargeItems { get; set; } = new List<ChargeItem>();

        [JsonPropertyName("cbPayItems")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(Order = 50, EmitDefaultValue = true, IsRequired = true)]
        public List<PayItem> cbPayItems { get; set; } = new List<PayItem>();

        [JsonPropertyName("ftCashBoxID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 60, EmitDefaultValue = false, IsRequired = false)]
        public Guid ftCashBoxID { get; set; } = Guid.Empty;

        [JsonPropertyName("ftPosSystemId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 70, EmitDefaultValue = false, IsRequired = false)]
        public Guid ftPosSystemId { get; set; } = Guid.Empty;

        [JsonPropertyName("ftReceiptCase")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(Order = 80, EmitDefaultValue = false, IsRequired = false)]
        public ulong ftReceiptCase { get; set; } = 0;

        [JsonPropertyName("ftReceiptCaseData")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 90, EmitDefaultValue = false, IsRequired = false)]
        public object? ftReceiptCaseData { get; set; }

        [JsonPropertyName("ftQueueID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 100, EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftQueueID { get; set; }

        [JsonPropertyName("cbPreviousReceiptReference")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 110, EmitDefaultValue = false, IsRequired = false)]
        public string? cbPreviousReceiptReference { get; set; }

        [JsonPropertyName("cbReceiptAmount")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 120, EmitDefaultValue = false, IsRequired = false)]
        public decimal? cbReceiptAmount { get; set; }

        [JsonPropertyName("cbUser")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 130, EmitDefaultValue = false, IsRequired = false)]
        public object? cbUser { get; set; }

        [JsonPropertyName("cbArea")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 140, EmitDefaultValue = false, IsRequired = false)]
        public object? cbArea { get; set; }

        [JsonPropertyName("cbCustomer")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 150, EmitDefaultValue = false, IsRequired = false)]
        public object? cbCustomer { get; set; }

        [JsonPropertyName("cbSettlement")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(Order = 160, EmitDefaultValue = false, IsRequired = false)]
        public object? cbSettlement { get; set; }

        [JsonPropertyName("Currency")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [DataMember(Order = 170, EmitDefaultValue = false, IsRequired = false)]
        public Currency Currency { get; set; } = Currency.EUR;

        [JsonPropertyName("DecimalPrecisionMultiplier")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
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