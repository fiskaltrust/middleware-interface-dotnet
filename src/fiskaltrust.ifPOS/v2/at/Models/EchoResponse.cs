using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.at
{
    [DataContract]
    public class EchoResponse
    {
        [DataMember(Order = 10)]
        public string Message { get; set; }
    }
}