using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class ReceiptResponse
    {
        [JsonProperty("ftQueueID")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftQueueID { get; set; }

        [JsonProperty("ftQueueItemID")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftQueueItemID { get; set; }

        [JsonProperty("ftQueueRow")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public long ftQueueRow { get; set; }

        [JsonProperty("ftCashBoxIdentification")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string ftCashBoxIdentification { get; set; }

        [JsonProperty("ftCashBoxID")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid? ftCashBoxID { get; set; }

        [JsonProperty("cbTerminalID")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string? cbTerminalID { get; set; }

        [JsonProperty("cbReceiptReference")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string? cbReceiptReference { get; set; }

        [JsonProperty("ftReceiptIdentification")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string ftReceiptIdentification { get; set; }

        [JsonProperty("ftReceiptMoment")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public DateTime ftReceiptMoment { get; set; }

        [JsonProperty("ftReceiptHeader")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftReceiptHeader { get; set; }

        [JsonProperty("ftChargeItems")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<ChargeItem>? ftChargeItems { get; set; }

        [JsonProperty("ftChargeLines")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftChargeLines { get; set; }

        [JsonProperty("ftPayItems")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<PayItem>? ftPayItems { get; set; }

        [JsonProperty("ftPayLines")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftPayLines { get; set; }

        [JsonProperty("ftSignatures")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<SignatureItem> ftSignatures { get; set; }

        [JsonProperty("ftReceiptFooter")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftReceiptFooter { get; set; }

        [JsonProperty("ftState")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public long ftState { get; set; }

        [JsonProperty("ftStateData")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public object? ftStateData { get; set; }
    }
}