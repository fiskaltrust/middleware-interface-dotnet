using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.es

{
    [DataContract]
    public class ProcessRequest
    {
        [DataMember(Order = 10)]
        public ReceiptRequest ReceiptRequest { get; set; }

        [DataMember(Order = 20)]
        public ReceiptResponse ReceiptResponse { get; set; }


        [DataMember(Order = 50)]
        public ReceiptRequest? ReferencedReceiptRequest { get; set; }

        [DataMember(Order = 60)]
        public ReceiptResponse? ReferencedReceiptResponse { get; set; }
    }
}
