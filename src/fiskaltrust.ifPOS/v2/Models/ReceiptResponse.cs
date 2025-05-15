using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using Newtonsoft.Json;

#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class ReceiptResponse
    {
        [JsonProperty("ftQueueID")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftQueueID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftQueueID { get; set; }

        [JsonProperty("ftQueueItemID")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftQueueItemID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftQueueItemID { get; set; }

        [JsonProperty("ftQueueRow")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftQueueRow")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public long ftQueueRow { get; set; }

        [JsonProperty("ftCashBoxIdentification")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftCashBoxIdentification")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string ftCashBoxIdentification { get; set; }

        [JsonProperty("ftCashBoxID")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftCashBoxID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid? ftCashBoxID { get; set; }

        [JsonProperty("cbTerminalID")]
#if NETSTANDARD2_1
        [JsonPropertyName("cbTerminalID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string? cbTerminalID { get; set; }

        [JsonProperty("cbReceiptReference")]
#if NETSTANDARD2_1
        [JsonPropertyName("cbReceiptReference")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string? cbReceiptReference { get; set; }

        [JsonProperty("ftReceiptIdentification")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftReceiptIdentification")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string ftReceiptIdentification { get; set; }

        [JsonProperty("ftReceiptMoment")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftReceiptMoment")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public DateTime ftReceiptMoment { get; set; }

        [JsonProperty("ftReceiptHeader")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftReceiptHeader")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftReceiptHeader { get; set; }

        [JsonProperty("ftChargeItems")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftChargeItems")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<ChargeItem>? ftChargeItems { get; set; }

        [JsonProperty("ftChargeLines")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftChargeLines")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftChargeLines { get; set; }

        [JsonProperty("ftPayItems")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftPayItems")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<PayItem>? ftPayItems { get; set; }

        [JsonProperty("ftPayLines")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftPayLines")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftPayLines { get; set; }

        [JsonProperty("ftSignatures")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftSignatures")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<SignatureItem> ftSignatures { get; set; }

        [JsonProperty("ftReceiptFooter")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftReceiptFooter")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftReceiptFooter { get; set; }

        [JsonProperty("ftState")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftState")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public State ftState { get; set; }

        [JsonProperty("ftStateData")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftStateData")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public object? ftStateData { get; set; }
    }
}