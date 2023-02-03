using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{

    /// <summary>
    /// Sale items on a commercial sale document.
    /// </summary>
    [DataContract]
    public enum PaymentType
    {
        /// <summary>
        ///Bar
        /// </summary>
        Cash = 0,
        /// <summary>
        ///Cheque
        /// </summary>
        Cheque = 1,
        /// <summary>
        ///Credit or credit card. Credit now interpreted as mixed not paid.
        /// </summary>
        CreditCard = 2,
        /// <summary>
        ///Ticket
        /// </summary>
        Ticket = 3,
        /// <summary>
        ///In the case of multiple tickets, index is used to indicate the quantity
        /// </summary>
        MultipleTickets = 4,
        /// <summary>
        ///With respect to Not paid, index specifies the sub-type. Definded in NotPaidIndex.
        /// </summary>
        NotPaid = 5,
        /// <summary>
        ///With respect to Payment discount, index specifies the sub-type. Definded in PaymentDiscountIndex
        /// </summary>
        PaymentDiscount = 6
    }
}
