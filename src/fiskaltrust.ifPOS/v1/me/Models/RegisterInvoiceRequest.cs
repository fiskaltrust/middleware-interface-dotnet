using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable enable
namespace fiskaltrust.ifPOS.v1.me
{
    [DataContract]
    public class RegisterInvoiceRequest
    {
        /// <summary>
        /// Message identifier, equal to ftQueueItemId when used by fiskaltrust.
        /// </summary>
        [DataMember(Order = 10)]
        public Guid RequestId { get; set; }

        /// <summary>
        /// Unique code of the cash register, assigned by the central invoice register (CIS) service when registering a cash register.
        /// </summary>
        [DataMember(Order = 20)]
        public string TcrCode { get; set; }

        /// <summary>
        /// The moment in which the invoice is created and issued.
        /// </summary>
        [DataMember(Order = 30)]
        public DateTime Moment { get; set; }

        /// <summary>
        /// When not null, signalizes that this invoice was late-signed because of the given reason. Null when the invoice was not processed in late-signing mode.
        /// </summary>
        [DataMember(Order = 40)]
        public SubsequentDeliveryType? SubsequentDeliveryType { get; set; }

        /// <summary>
        /// Specified if the issuer is registered in the VAT system.
        /// </summary>
        [DataMember(Order = 50)]
        public bool IsIssuerInVATSystem { get; set; }

        /// <summary>
        /// Code of the business unit in which the invoice is issued.
        /// </summary>
        /// <remarks>
        /// Must have the following format: [a-z]{2}[0-9]{3}[a-Z]{2}[0-9]{3} (e.g. ab123ab123)
        /// </remarks>
        [DataMember(Order = 60)]
        public string BusinessUnitCode { get; set; }

        /// <summary>
        /// Code of the operator who issued the invoice.
        /// </summary>
        /// <remarks>
        /// Must have the following format: [a-z]{2}[0-9]{3}[a-Z]{2}[0-9]{3} (e.g. ab123ab123)
        /// </remarks>
        [DataMember(Order = 70)]
        public string OperatorCode { get; set; }

        /// <summary>
        /// Code of the software used for issuing the invoice.
        /// </summary>
        /// <remarks>
        /// Must have the following format: [a-z]{2}[0-9]{3}[a-Z]{2}[0-9]{3} (e.g. ab123ab123)
        /// </remarks>
        [DataMember(Order = 80)]
        public string SoftwareCode { get; set; }

        /// <summary>
        /// Details of the invoice and the included items and payments.
        /// </summary>
        [DataMember(Order = 90)]
        public InvoiceDetails InvoiceDetails { get; set; }
    }
}
