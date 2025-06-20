using System;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
#if NETCOREAPP3_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
        public class SignatureItem
        {
#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftSignatureItemId")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftSignatureItemId { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftSignatureFormat")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public SignatureFormat ftSignatureFormat { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("ftSignatureType")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public SignatureType ftSignatureType { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("Caption")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? Caption { get; set; }

#if NETCOREAPP3_0_OR_GREATER
                [JsonPropertyName("Data")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string Data { get; set; }
        }
}