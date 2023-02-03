using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// When printing invoices based on the last commercial document, any 38-
    /// character descriptions are truncated to 37 characters
    /// </summary>
    public class RecSubtotalAdjustment
    {
        /// <summary>
        /// When printing invoices based on the last commercial document, any 38-
        /// character descriptions are truncated to 37 characters
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        ///determines discount/surcharge operation to perform:
        ///1 = Discount on subtotal with subtotal printed out
        ///2 = Discount on subtotal without subtotal printed out
        /// 6 = Surcharge on subtotal with subtotal printed out
        /// 7 = Surcharge on subtotal without subtotal printed out
        /// </summary>
        [DataMember]
        public int AdjustmentType { get; set; }

        /// <summary>
        ///  A zero amount will throw a printer error 16.
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }
    }
}
