using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class StartTransactionResponse
    {
        [DataMember(Order = 1)]
        public ulong TransactionNumber { get; set; }

        [DataMember(Order = 2)]
        public DateTime TimeStamp { get; set; }

        [DataMember(Order = 3)]
        public string TseSerialNumberOctet { get; set; }

        [DataMember(Order = 4)]
        public string ClientId { get; set; }

        [DataMember(Order = 5)]
        public TseSignatureData SignatureData { get; set; }
    }
}
