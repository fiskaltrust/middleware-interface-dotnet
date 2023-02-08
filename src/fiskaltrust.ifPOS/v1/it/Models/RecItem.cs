using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{

    /// <summary>
    /// Sale items on a commercial sale document.
    /// </summary>
    [DataContract]
    public class RecItem
    {
        /// <summary>
        /// Department ID number (range 1 to 99)
        /// </summary>
        [DataMember]
        public int Department { get; set; }

        /// <summary>
        /// When printing invoices based on the last commercial document, any 38-
        /// character descriptions are truncated to 37 characters
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Epson fiscal printers can compute quantities from 0.001 up to 9999.999
        /// </summary>
        [DataMember]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Epson fiscal printers can accept prices from 0.00 up to 9999999.99. The 
        /// FpMate CGI service automatically rounds down amounts with more than
        ///two decimal places.If it exceeds 9999999.99, an error is returned.Either
        ///a comma or a full stop (period) can represent the decimal point. Thousand
        ///separators should not be used.
        ///The unitPrice and payment attributes can be zero.
        /// </summary>
        [DataMember]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Epson fiscal printers can accept prices from 0.00 up to 9999999.99. The 
        /// FpMate CGI service automatically rounds down amounts with more than
        ///two decimal places.If it exceeds 9999999.99, an error is returned.Either
        ///a comma or a full stop (period) can represent the decimal point. Thousand
        ///separators should not be used.
        ///The amount attribute cannot be zero!
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }

        /// <summary>
        /// Epson fiscal printers can accept prices from 0.00 up to 9999999.99. The 
        /// FpMate CGI service automatically rounds down amounts with more than
        ///two decimal places.If it exceeds 9999999.99, an error is returned.Either
        ///a comma or a full stop (period) can represent the decimal point. Thousand
        ///separators should not be used.
        ///The unitPrice and payment attributes can be zero.
        /// </summary>
        [DataMember]
        public decimal Payment { get; set; }
    }
}
