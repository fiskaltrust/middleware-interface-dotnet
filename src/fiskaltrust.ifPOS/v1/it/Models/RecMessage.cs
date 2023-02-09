using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// Defines the row type to be printed:
    /// </summary>
    [DataContract]
    public enum MessageType
    {
        /// <summary>
        /// Header. Sends text messages to the customer display
        /// </summary>
        Header = 0,
        /// <summary>
        /// Additional header. This type must be placed before the beginFiscalReceipt sub-element
        /// </summary>
        AdditionalHeader = 1,
        /// <summary>
        /// Trailer (after NUMERO CONFEZIONI and before NUMERO CASSA)
        /// </summary>
        Trailer = 2,
        /// <summary>
        ///  Additional trailer (promo lines after NUMERO CASSA and before barcode or QR code)
        /// </summary>
        AdditionalTrailer = 3,
        /// <summary>
        ///  Additional trailer (promo lines after NUMERO CASSA and before barcode or QR code)
        /// </summary>
        AdditionalDescription = 4

    }

    /// <summary>
    /// Sale items on a commercial sale document.
    /// </summary>
    [DataContract]
    public class DisplayText
    {
        /// <summary>
        /// MessageType
        /// </summary>
        [DataMember]
        public MessageType MessageType { get; set; }

        /// <summary>
        /// index indicates the line number:
        /// </summary>
        [DataMember]
        public int Index { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        [DataMember]
        public string Text { get; set; }
    }
}
