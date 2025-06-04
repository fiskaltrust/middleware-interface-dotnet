using System.Runtime.Serialization;

#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    /// <summary>
    /// Request to check if the communication to the Middleware is up and running. Body Contains a Message e.g. "Hello World!"
    /// </summary>
    public class EchoRequest
    {
#if NETSTANDARD2_1
        [JsonPropertyName("Message")]
#endif
        [DataMember(Order = 1, EmitDefaultValue = true, IsRequired = true)]
        public string Message { get; set; }
    }
}