using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// OperationType determines modifier operation to perform
    /// </summary>
    [DataContract]
    public enum OperationType
    {
        /// <summary>
        /// Deposit (Acconto)
        /// </summary>
        [DataMember(Order = 10)]
        Acconto = 10,
        /// <summary>
        /// Omaggio (Free of Charge)
        /// </summary>
        [DataMember(Order = 20)]
        FreeOfCharge = 11,
        /// <summary>
        /// Buono monouso (single-use voucher)
        /// </summary>
        [DataMember(Order = 30)]
        SingleUseVoucher = 12,
    }


    /// <summary>
    /// Refunds (Goods return or reso in Italy are not supported in 
    /// invoices.They are converted to corrections(storni).
    /// Modifiers are not supported in invoices
    /// </summary>
    [DataContract]
    public class Refund : Item
    {
        /// <summary>
        /// OperationType determines modifier operation to perform
        /// </summary>
        [DataMember(Order = 10)]
        public OperationType? OperationType { get; set; }

    }
}
