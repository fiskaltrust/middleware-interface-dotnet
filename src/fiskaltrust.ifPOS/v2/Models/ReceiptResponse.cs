using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
#if NETCOREAPP3_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
        public class ReceiptResponse
        {
#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftQueueID")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public Guid ftQueueID { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftQueueItemID")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public Guid ftQueueItemID { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftQueueRow")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public long ftQueueRow { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftCashBoxIdentification")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string ftCashBoxIdentification { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftCashBoxID")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public Guid? ftCashBoxID { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbTerminalID")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string? cbTerminalID { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("cbReceiptReference")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string? cbReceiptReference { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftReceiptIdentification")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string ftReceiptIdentification { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftReceiptMoment")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public DateTime ftReceiptMoment { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftReceiptHeader")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftReceiptHeader { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftChargeItems")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<ChargeItem>? ftChargeItems { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftChargeLines")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftChargeLines { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftPayItems")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<PayItem>? ftPayItems { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftPayLines")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftPayLines { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftSignatures")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<SignatureItem> ftSignatures { get; set; } = new List<SignatureItem>();

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftReceiptFooter")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftReceiptFooter { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftState")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public State ftState { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftStateData")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public object? ftStateData { get; set; }
        }
}