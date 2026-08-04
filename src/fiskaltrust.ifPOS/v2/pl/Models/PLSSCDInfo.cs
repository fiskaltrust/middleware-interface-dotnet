#if !WCF
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.ifPOS.v2.pl
{
    /// <summary>
    /// Details about the operational status of the Polish SCU and its connected fiscal device.
    /// </summary>
    [DataContract]
    public class PLSSCDInfo
    {
        /// <summary>
        /// Contains a json serialized object with generic info for the given device.
        /// </summary>
        [DataMember(Order = 10)]
        [JsonPropertyName("InfoData")]
        public string InfoData { get; set; }
    }
}
#endif
