using System;
using System.Runtime.Serialization;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using Newtonsoft.Json;

#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class SignatureItem
    {
        [JsonProperty("ftSignatureItemId")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftSignatureItemId")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftSignatureItemId { get; set; }

        [JsonProperty("ftSignatureFormat")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftSignatureFormat")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public SignatureFormat ftSignatureFormat { get; set; }

        [JsonProperty("ftSignatureType")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftSignatureType")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public SignatureType ftSignatureType { get; set; }

        [JsonProperty("Caption")]
#if NETSTANDARD2_1
        [JsonPropertyName("Caption")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Caption { get; set; }

        [JsonProperty("Data")]
#if NETSTANDARD2_1
        [JsonPropertyName("Data")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Data { get; set; }
    }
}