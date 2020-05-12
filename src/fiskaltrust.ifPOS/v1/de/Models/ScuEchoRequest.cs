using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class ScuEchoRequest
    {
        [DataMember(Order = 10)]
        public string Message { get; set; }
    }
}
