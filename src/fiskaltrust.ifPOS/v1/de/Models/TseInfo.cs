using System.Collections.Generic;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class TseInfo
    {
        [DataMember(Order = 10)]
        public long MaxNumberOfClients { get; set; }

        [DataMember(Order = 20)]
        public long CurrentNumberOfClients { get; set; }

        [DataMember(Order = 30)]
        public IEnumerable<string> CurrentClientIds { get; set; }

        [DataMember(Order = 40)]
        public long MaxNumberOfTransactions { get; set; }

        [DataMember(Order = 50)]
        public long CurrentNumberOfTransactions { get; set; }

        /*
        [DataMember(Order = 60)]
        public IEnumerable<CertificateInfo> Certificates { get; set; }

        [DataMember(Order = 70)]
        public IEnumerable<string> SerialNumbersBase64 { get; set; }
        */

        [DataMember(Order = 60)]
        public string TsePublicKeyBase64 { get; set; }

        [DataMember(Order = 70)]
        public string TseSerialNumberOctet { get; set; }

        [DataMember(Order = 80)]
        public TseState CurrentState { get; set; }

        [DataMember(Order = 90)]
        public Dictionary<string, object> Info { get; set; }
    }
}
