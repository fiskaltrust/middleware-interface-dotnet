#if !WCF
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.ifPOS.v2.fr
{
    [DataContract]
    public class ProcessResponse
    {
        [DataMember(Order = 10)]
        [JsonPropertyName("ReceiptResponse")]
        public ReceiptResponse ReceiptResponse { get; set; }
    }

}
#endif
