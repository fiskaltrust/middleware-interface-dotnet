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

        /// <summary>
        /// Serial number of the fiscal register: the numer fabryczny for hardware devices, or the
        /// numer unikatowy for software registers (kasy wirtualne), which have no factory number.
        /// Null for SCUs without a register (e.g. KSeF invoice SCUs).
        /// </summary>
        [DataMember(Order = 20)]
        [JsonPropertyName("SerialNumber")]
        public string SerialNumber { get; set; }
    }
}
#endif
