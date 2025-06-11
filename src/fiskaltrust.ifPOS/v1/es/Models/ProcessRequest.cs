using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.es

{
    [DataContract]
    public class ProcessRequest
    {
        [DataMember(Order = 10)]
        public ReceiptRequest ReceiptRequest { get; set; }

        [DataMember(Order = 20)]
        public ReceiptResponse ReceiptResponse { get; set; }

        [DataMember(Order = 30)]
        public ReceiptRequest? PreviousReceiptRequest { get; set; }

        [DataMember(Order = 40)]
        public ReceiptResponse? PreviousReceiptResponse { get; set; }
    }
}
