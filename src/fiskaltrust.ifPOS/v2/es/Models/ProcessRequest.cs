#if !WCF
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.ifPOS.v2.es

{
    [DataContract]
    public class ProcessRequest
    {
        [DataMember(Order = 10)]
        [JsonPropertyName("ReceiptRequest")]
        public ReceiptRequest ReceiptRequest { get; set; }

        [DataMember(Order = 20)]
        [JsonPropertyName("ReceiptResponse")]
        public ReceiptResponse ReceiptResponse { get; set; }
    }
}
#endif
