﻿using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.me
{
    [DataContract]
    public class RegisterInvoiceResponse
    {
        /// <summary>
        /// Verification code (also called 'JIKR') generated by the central invoice register (CIS) service.
        /// </summary>
        [DataMember(Order = 10)]
        public string FIC { get; set; }
    }
}