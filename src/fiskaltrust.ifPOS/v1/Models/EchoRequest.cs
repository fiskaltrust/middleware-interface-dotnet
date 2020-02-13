using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1
{
    [DataContract]
    public class EchoRequest
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}
