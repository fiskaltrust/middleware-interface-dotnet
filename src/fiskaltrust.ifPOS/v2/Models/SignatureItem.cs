using System;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
#if !WCF
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
        public class SignatureItem
        {
#if !WCF
                [JsonPropertyName("ftSignatureItemId")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftSignatureItemId { get; set; }

#if !WCF
                [JsonPropertyName("ftSignatureFormat")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public SignatureFormat ftSignatureFormat { get; set; }

#if !WCF
                [JsonPropertyName("ftSignatureType")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public SignatureType ftSignatureType { get; set; }

#if !WCF
                [JsonPropertyName("Caption")]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? Caption { get; set; }

#if !WCF
                [JsonPropertyName("Data")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string Data { get; set; }
        }
}