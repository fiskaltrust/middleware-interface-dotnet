using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// When printing invoices based on the last commercial document, any 38-
    /// character descriptions are truncated to 37 characters
    /// </summary>
    [DataContract]
    public class RecTotal
    {
        /// <summary>
        ///Payment Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///Payment Amount
        /// </summary>
        public decimal Payment { get; set; }

        /// <summary>
        ///PaymentType
        /// </summary>
        public PaymentType PaymentType { get; set; }

        /// <summary>
        ///index can be used with cash, credit cards or tickets to select a specific totaliser. In the case of 
        ///multiple tickets, index is used to indicate the quantity.With respect to Not paid and Payment
        ///discount, index specifies the sub-type.index has no relevance with cheques.The following ranges
        ///are available:
        /// - Cash = 0 to 5
        /// - Credit = 0(same as paymentType 5 index 0)
        /// - Credit card = 1 to 10
        /// - Ticket = 1 to 10
        /// - Multiple tickets = 1 to 99
        /// - Not paid is as follows:
        ///  • 0 = Mixed (goods and services)
        ///  • 1 = Goods
        ///  • 2 = Services
        ///  • 3 = Invoice to follow
        ///  • 4 = RT invoice(for future use)
        ///  • 5 = SSN(National Health Service)
        /// - Payment discount is as follows:
        ///  • 0 = Generic
        ///  • 1 = Multi-use voucher
        /// </summary>
        public int Index { get; set; }
    }
}
