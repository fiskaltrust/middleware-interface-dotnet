using System.Collections.Generic;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    ///printerFiscalReceipt
    ///Emission of commercial documents (documenti commerciali)
    /// </summary>
    [DataContract]
    public abstract class FiscalReceiptRequest
    {
        /// <summary>
        /// Sends text messages to the customer display. You cannot insert carriage returns or line feeds so use 
        /// spaces to pad out line 1 and begin line 2. This sub-element has two attributes; one to indicate the
        /// operator and one for the text itself.The maximum number of characters is 40. This reduces to 20 if 
        /// used with printerTicket files.
        /// </summary>
        [DataMember]
        public DisplayText DisplayText { get; set; }

        /// <summary>
        ///Barcodes codes are printed at the end of the commercial document after the additional trailer
        ///lines but before the FOOTER. Only one barcode can be printed in a commercial document unless the
        ///paper cut native command is used(1-137).
        /// </summary>
        [DataMember]
        public string Barcode { get; set; }

        /// <summary>
        ///QRcodes codes are printed at the end of the commercial document after the additional trailer
        ///lines but before the FOOTER.
        /// </summary>
        [DataMember]
        public string QRcode { get; set; }

        /// <summary>
        /// printRecSubtotalAdjustment: Prints discount or surcharge applied on the subtotal.
        /// </summary>
        [DataMember]
        public List<RecSubtotalAdjustment> RecSubtotalAdjustments { get; set; }

        /// <summary>
        /// printRecTotal: One or more commands can be sent; more than one means that the payment is composed of several 
        /// partial payments.In this case, once the total has been reached or exceeded, no more payment
        /// commands can be sent
        /// </summary>
        [DataMember]
        public List<RecTotal> RecTotals { get; set; }
    }
}
