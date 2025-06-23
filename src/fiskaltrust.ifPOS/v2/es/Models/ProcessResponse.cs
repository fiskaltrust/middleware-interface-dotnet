using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.es
{
    [DataContract]
    public class ProcessResponse
    {
        [DataMember(Order = 10)]
        public ReceiptResponse ReceiptResponse { get; set; }
    }

}
