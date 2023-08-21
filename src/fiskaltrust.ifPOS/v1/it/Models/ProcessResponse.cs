using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    [DataContract]
    public class ProcessResponse
    {
        [DataMember(Order = 10)]
        public ReceiptRequest ReceiptRequest { get; set; }

        [DataMember(Order = 20)]
        public ReceiptResponse ReceiptResponse { get; set; }


        [DataMember(Order = 30)]
        public bool Print { get; set; }
    }
}
