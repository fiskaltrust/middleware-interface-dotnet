using System.Collections.Generic;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class TseExportDataResult
    {
        /// <remarks>
        /// Export of the resulting logs caused by the Transaction
        /// </remarks>
        [DataMember(Order = 1)]
        public IEnumerable<LogInfo> TransactionLogs { get; set; }

        /// <remarks>
        /// Export of the Audit logs
        /// </remarks>
        [DataMember(Order = 2)]
        public IEnumerable<LogInfo> AuditLogs { get; set; }

        /// <remarks>
        /// Export of the System logs
        /// </remarks>
        [DataMember(Order = 3)]
        public IEnumerable<LogInfo> SystemLogs { get; set; }

        [DataMember(Order = 4)]
        public IEnumerable<CertificateInfo> Certificates { get; set; }

        [DataMember(Order = 5)]
        public IEnumerable<string> SerialNumbersBase64 { get; set; }
    }
}
