using System;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    public class SignatureItem
    {
#if NETSTANDARD2_1
        [JsonPropertyName("ftSignatureItemId")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftSignatureItemId { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftSignatureFormat")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public SignatureFormat ftSignatureFormat { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftSignatureType")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public SignatureType ftSignatureType { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("Caption")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Caption { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("Data")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Data { get; set; }
    }
}