using System.Collections.Generic;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class TseInfo
    {
        [DataMember(Order = 1)]
        public long MaxNumberOfClients { get; set; }

        [DataMember(Order = 2)]
        public long CurrentNumberOfClients { get; set; }

        [DataMember(Order = 3)]
        public long MaxNumberOfTransactions { get; set; }

        [DataMember(Order = 4)]
        public long CurrentNumberOfTransactions { get; set; }

        [DataMember(Order = 5)]
        public IEnumerable<CertificateInfo> Certificates { get; set; }

        [DataMember(Order = 6)]
        public IEnumerable<string> SerialNumbersBase64 { get; set; }

        [DataMember(Order = 7)]
        public TseState CurrentState { get; set; }

        /// <summary>
        /// Should contain other generic infomations:
        ///  - Count of transactions specified by type (SE API's GetSupportedTransactionUpdateVariants)
        ///  - SCU model specifics
        ///  - ...
        /// </summary>
        [DataMember(Order = 8)]
        public Dictionary<string, object> Info { get; set; }
    }
}
