using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// When printing invoices based on the last commercial document, any 38-
    /// character descriptions are truncated to 37 characters
    /// </summary>
    [DataContract]
    public class Payment
    {
        /// <summary>
        ///Payment Description
        /// </summary>
        [DataMember(Order = 10)]
        public string Description { get; set; }

        /// <summary>
        ///Payment Amount
        /// </summary>
        [DataMember(Order = 20)]
        public decimal Amount { get; set; }

        /// <summary>
        ///PaymentType
        /// </summary>
        [DataMember(Order = 30)]
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
        [DataMember(Order = 40)]
        public int Index { get; set; }

        /// <summary>
        /// AdditionalInformation
        /// </summary>
        [DataMember(Order = 50)]
        public string AdditionalInformation { get; set; }
    }
}
