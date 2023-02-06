using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// Commercial Document Response File
    /// </summary>
    [DataContract]
    public class FiscalReceiptResponse
    {
        /// <summary>
        /// Indigating success
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

        /// <summary>
        ///  FiscalReceiptNumber: Daily sequence number without Z prefix
        /// </summary>
        [DataMember]
        public string Number { get; set; }

        /// <summary>
        /// Document datetime
        /// </summary>
        [DataMember]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// FiscalReceiptAmount – Document value. Zero in the case of an automatic void
        /// </summary>
        [DataMember]
        public DateTime Amount { get; set; }

        /// <summary>
        /// ZRepNumber – Z report sequence number for the day
        /// </summary>
        [DataMember]
        public ulong ZRepNumber { get; set; }

        /// <summary>
        /// STATUS given by the 5 bytes Alphanumeric
        /// </summary>
        [DataMember]
        public DeviceStatus DeviceStatus { get; set; }
    }
}
