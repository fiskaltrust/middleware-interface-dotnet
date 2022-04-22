using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class UnregisterTcrRequest
    {
        /// <summary>
        /// Message identifier, equal to ftQueueItemId when used by fiskaltrust.
        /// </summary>
        [DataMember(Order = 10)]
        public Guid RequestId { get; set; }

        /// <summary>
        /// Code of the business unit in which the invoice is issued.
        /// </summary>
        [DataMember(Order = 20)]
        public string BusinessUnitCode { get; set; }

        /// <summary>
        /// Element representing the internal identification of the TCR.
        /// </summary>
        [DataMember(Order = 30)]
        public string InternalTcrIdentifier { get; set; }
    }
}
