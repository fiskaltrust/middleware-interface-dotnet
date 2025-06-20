using System.Runtime.Serialization;

#if NETCOREAPP3_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    /// <summary>
    /// Response to the EchoRequest. Returns the message provided in the EchoRequest if successfull.
    /// </summary>
    public class EchoResponse
    {
#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("Message")]
#endif
        [DataMember(Order = 1, EmitDefaultValue = true, IsRequired = true)]
        public string Message { get; set; }
    }
}