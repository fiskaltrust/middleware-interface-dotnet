using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.at
{
    [DataContract]
    public class SignResponse
    {
        [DataMember(Order = 10)]
        public byte[] SignedData { get; set; }
    }
}