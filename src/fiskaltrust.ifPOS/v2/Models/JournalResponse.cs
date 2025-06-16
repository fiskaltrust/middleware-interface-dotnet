using System.Collections.Generic;
using System.Runtime.Serialization;

#if !WCF
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    /// <summary>
    /// The fiskaltrust.Middleware returns the requested JournalType as a FileStream.
    /// </summary>
    public class JournalResponse
    {
#if !WCF
        [JsonPropertyName("Chunk")]
#endif
        [DataMember(Order = 1, EmitDefaultValue = true, IsRequired = true)]
        public List<byte> Chunk { get; set; }
    }
}