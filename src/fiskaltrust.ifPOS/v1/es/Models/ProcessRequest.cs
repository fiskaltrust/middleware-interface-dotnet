using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.es

{
    [DataContract]
    public class ProcessRequest
    {
        [DataMember(Order = 10)]
        public required ReceiptRequest ReceiptRequest { get; set; }

        [DataMember(Order = 20)]
        public required ReceiptResponse ReceiptResponse { get; set; }

        [DataMember(Order = 30)]
        public required ReceiptRequest? PreviousReceiptRequest { get; set; }

        [DataMember(Order = 40)]
        public required ReceiptResponse? PreviousReceiptResponse { get; set; }
    }
}
