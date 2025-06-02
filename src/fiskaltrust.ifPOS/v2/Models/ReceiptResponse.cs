using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    public class ReceiptResponse
    {
#if NETSTANDARD2_1
        [JsonPropertyName("ftQueueID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftQueueID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftQueueItemID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftQueueItemID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftQueueRow")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public long ftQueueRow { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftCashBoxIdentification")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string ftCashBoxIdentification { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftCashBoxID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid? ftCashBoxID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbTerminalID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string? cbTerminalID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbReceiptReference")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string? cbReceiptReference { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftReceiptIdentification")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string ftReceiptIdentification { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftReceiptMoment")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public DateTime ftReceiptMoment { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftReceiptHeader")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftReceiptHeader { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftChargeItems")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<ChargeItem>? ftChargeItems { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftChargeLines")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftChargeLines { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftPayItems")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<PayItem>? ftPayItems { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftPayLines")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftPayLines { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftSignatures")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<SignatureItem> ftSignatures { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftReceiptFooter")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public List<string>? ftReceiptFooter { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftState")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public State ftState { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftStateData")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public object? ftStateData { get; set; }
    }
}