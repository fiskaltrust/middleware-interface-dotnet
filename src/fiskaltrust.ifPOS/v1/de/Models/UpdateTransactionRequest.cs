using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class UpdateTransactionRequest
    {
        [DataMember(Order = 1)]
        public string ClientId { get; set; }

        [DataMember(Order = 2)]
        public ulong TransactionNumber { get; set; }

        [DataMember(Order = 3)]
        public string ProcessType { get; set; }

        [DataMember(Order = 4)]
        public string ProcessDataBase64 { get; set; }

        [DataMember(Order = 5)]
        public Guid QueueItemId { get; set; }

        [DataMember(Order = 6)]
        public bool IsRetry { get; set; }
    }
}
