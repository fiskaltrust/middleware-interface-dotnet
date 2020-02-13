using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class FinishTransactionRequest
    {
        [DataMember(Order = 1)]
        public string ClientId { get; set; }

        [DataMember(Order = 2)]
        public long TransactionNumber { get; set; }

        [DataMember(Order = 3)]
        public string ProcessType { get; set; }

        [DataMember(Order = 4)]
        public string ProcessDataBase64 { get; set; }

        [DataMember(Order = 5)]
        public Guid ReceiptRequestItemId { get; set; }

        [DataMember(Order = 6)]
        public string ReceiptRequestHash { get; set; }

        [DataMember(Order = 7)]
        public bool ShouldRetrySending { get; set; }
    }
}
