using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class FinishTransactionResponse
    {
        [DataMember(Order = 1)]
        public long TransactionNumber { get; set; }

        [DataMember(Order = 2)]
        public DateTime StartTime { get; set; }

        [DataMember(Order = 3)]
        public DateTime EndTime { get; set; }

        [DataMember(Order = 4)]
        public string CertificateSerialNumber { get; set; }

        [DataMember(Order = 5)]
        public string ClientId { get; set; }

        [DataMember(Order = 6)]
        public string ProcessType { get; set; }

        [DataMember(Order = 7)]
        public string ProcessDataBase64 { get; set; }

        [DataMember(Order = 8)]
        public TseLogData LogData { get; set; }

        [DataMember(Order = 9)]
        public TseSignatureData SignatureData { get; set; }

        [DataMember(Order = 10)]
        public bool MoreExportDataAvailable { get; set; }
    }
}
