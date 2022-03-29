using System;
using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterTCRRequestHeaderType
    {
        public string UUID { get; set; }
        public DateTime SendDateTime { get; set; }
    }
}