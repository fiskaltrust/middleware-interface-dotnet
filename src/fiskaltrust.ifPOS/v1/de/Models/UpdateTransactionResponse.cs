using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class UpdateTransactionResponse
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
        public string ProcessType { get; set; }

        [DataMember(Order = 6)]
        public string ProcessDataBase64 { get; set; }

        [DataMember(Order = 7)]
        public TseSignatureData SignatureData { get; set; }

    }
}
