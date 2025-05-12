using System;
using System.Runtime.Serialization;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using Newtonsoft.Json;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class SignatureItem
    {
        [JsonProperty("ftSignatureItemId")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftSignatureItemId { get; set; }

        [JsonProperty("ftSignatureFormat")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public SignatureFormat  ftSignatureFormat { get; set; }

        [JsonProperty("ftSignatureType")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public SignatureType  ftSignatureType { get; set; }

        [JsonProperty("Caption")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Caption { get; set; }

        [JsonProperty("Data")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Data { get; set; }
    }
}