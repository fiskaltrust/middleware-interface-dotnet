using System.Collections.Generic;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// Refunds (Goods return or reso in Italy are not supported in 
    /// invoices.They are converted to corrections(storni).
    /// Modifiers are not supported in invoices
    /// </summary>
    [DataContract]
    public class FiscalReceiptRefund : FiscalReceiptRequest
    {
        /// <summary>
        /// printRecRefunds: Prints Refunds/Voids on a commercial sale document.
        /// </summary>
        [DataMember]
        public List<Refund> Refunds { get; set; }
    }
}
