using System.Runtime.Serialization;

#if NETCOREAPP3_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    /// <summary>
    /// Request to check if the communication to the Middleware is up and running. Body Contains a Message e.g. "Hello World!"
    /// </summary>
    public class EchoRequest
    {
#if NETCOREAPP3_0_OR_GREATER
        [JsonPropertyName("Message")]
#endif
        [DataMember(Order = 1, EmitDefaultValue = true, IsRequired = true)]
        public string Message { get; set; }
    }
}