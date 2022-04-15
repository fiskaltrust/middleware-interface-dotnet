using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.at
{
    [DataContract]
    public class ZdaResponse
    {
        [DataMember(Order = 10)]
        public string ZDA { get; set; }
    }
}