using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class TseEchoRequest
    {
        [DataMember(Order = 10)]
        public string Message { get; set; }
    }
}
