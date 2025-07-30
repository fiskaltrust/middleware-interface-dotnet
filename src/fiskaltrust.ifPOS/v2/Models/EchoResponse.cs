using System.Runtime.Serialization;

#if !WCF
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    /// <summary>
    /// Response to the EchoRequest. Returns the message provided in the EchoRequest if successfull.
    /// </summary>
    public class EchoResponse
    {
#if !WCF
        [JsonPropertyName("Message")]
#endif
        [DataMember(Order = 1, EmitDefaultValue = true, IsRequired = true)]
        public string Message { get; set; }
    }
}