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
        /// Information on Error
        /// </summary>
        [DataMember]
        public string ErrorInfo { get; set; }

        /// <summary>
        ///  FiscalReceiptNumber: Daily sequence number without Z prefix
        /// </summary>
        [DataMember]
        public ulong Number { get; set; }

        /// <summary>
        /// Document datetime
        /// </summary>
        [DataMember]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// FiscalReceiptAmount – Document value. Zero in the case of an automatic void
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }
    }
}
