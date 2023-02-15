using System.Collections.Generic;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    ///printerFiscalReceipt
    ///Emission of commercial documents (documenti commerciali)
    /// </summary>
    [DataContract]
    public class FiscalReceiptInvoice : FiscalReceiptRequest
    {
        /// <summary>
        /// printRecItems: Prints sale items on a commercial sale document.
        /// </summary>
        public List<Item> Items { get; set; }
    }
}
