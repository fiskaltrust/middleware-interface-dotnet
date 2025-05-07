using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class ReceiptResponse
    {
        [JsonPropertyName("ftQueueID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required Guid ftQueueID { get; set; } = Guid.Empty;

        [JsonPropertyName("ftQueueItemID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required Guid ftQueueItemID { get; set; } = Guid.Empty;

        [JsonPropertyName("ftQueueRow")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required ulong ftQueueRow { get; set; } = 0;

        [JsonPropertyName("ftCashBoxIdentification")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required string ftCashBoxIdentification { get; set; } = "undefined";

        [JsonPropertyName("ftReceiptIdentification")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required string ftReceiptIdentification { get; set; } = "ft0#";

        [JsonPropertyName("ftReceiptMoment")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required DateTime ftReceiptMoment { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("ftSignatures")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required List<SignatureItem> ftSignatures { get; set; } = new List<SignatureItem>();

        [JsonPropertyName("ftState")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required ulong ftState { get; set; } = 0;

        [JsonPropertyName("ftStateData")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public object? ftStateData { get; set; }

        [JsonPropertyName("ftCashBoxID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftCashBoxID { get; set; } = Guid.Empty;

        [JsonPropertyName("cbTerminalID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string cbTerminalID { get; set; } = "undefined";

        [JsonPropertyName("cbReceiptReference")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string cbReceiptReference { get; set; } = "undefined";

        [JsonPropertyName("ftReceiptHeader")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public List<string>? ftReceiptHeader { get; set; }

        [JsonPropertyName("ftChargeItems")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public List<ChargeItem>? ftChargeItems { get; set; }

        [JsonPropertyName("ftChargeLines")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public List<string>? ftChargeLines { get; set; }

        [JsonPropertyName("ftPayItems")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public List<PayItem>? ftPayItems { get; set; }

        [JsonPropertyName("ftPayLines")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public List<string>? ftPayLines { get; set; }

        [JsonPropertyName("ftReceiptFooter")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public List<string>? ftReceiptFooter { get; set; }
    }
}