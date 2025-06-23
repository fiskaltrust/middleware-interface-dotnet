using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
#if !WCF
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
        public class ReceiptResponse
        {
#if !WCF
                [JsonPropertyName("ftQueueID")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public Guid ftQueueID { get; set; }

#if !WCF
                [JsonPropertyName("ftQueueItemID")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public Guid ftQueueItemID { get; set; }

#if !WCF
                [JsonPropertyName("ftQueueRow")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public long ftQueueRow { get; set; }

#if !WCF
                [JsonPropertyName("ftCashBoxIdentification")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string ftCashBoxIdentification { get; set; }

#if !WCF
                [JsonPropertyName("ftCashBoxID")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public Guid? ftCashBoxID { get; set; }

#if !WCF
                [JsonPropertyName("cbTerminalID")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string? cbTerminalID { get; set; }

#if !WCF
                [JsonPropertyName("cbReceiptReference")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string? cbReceiptReference { get; set; }

#if !WCF
                [JsonPropertyName("ftReceiptIdentification")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string ftReceiptIdentification { get; set; }

#if !WCF
                [JsonPropertyName("ftReceiptMoment")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public DateTime ftReceiptMoment { get; set; }

#if !WCF
                [JsonPropertyName("ftReceiptHeader")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftReceiptHeader { get; set; }

#if !WCF
                [JsonPropertyName("ftChargeItems")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<ChargeItem>? ftChargeItems { get; set; }

#if !WCF
                [JsonPropertyName("ftChargeLines")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftChargeLines { get; set; }

#if !WCF
                [JsonPropertyName("ftPayItems")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<PayItem>? ftPayItems { get; set; }

#if !WCF
                [JsonPropertyName("ftPayLines")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftPayLines { get; set; }

#if !WCF
                [JsonPropertyName("ftSignatures")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<SignatureItem> ftSignatures { get; set; } = new List<SignatureItem>();

#if !WCF
                [JsonPropertyName("ftReceiptFooter")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftReceiptFooter { get; set; }

#if !WCF
                [JsonPropertyName("ftState")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public State ftState { get; set; }

#if !WCF
                [JsonPropertyName("ftStateData")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public object? ftStateData { get; set; }
        }
}