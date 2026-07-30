#if !WCF
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.ifPOS.v2.pl
{
    /// <summary>
    /// A single slot of the PTU VAT rate table programmed on the fiscal device. A slot either carries a
    /// percentage rate or is marked tax-exempt (zwolniona, "zw."); slots not programmed on the device are omitted.
    /// </summary>
    [DataContract]
    public class PLVatRateTableEntry
    {
        /// <summary>The PTU slot letter ("A" to "G").</summary>
        [DataMember(Order = 10)]
        [JsonPropertyName("PtuSlot")]
        public string PtuSlot { get; set; }

        /// <summary>The VAT rate in percent (e.g. 23 for the standard rate), or null if the slot is exempt.</summary>
        [DataMember(Order = 20)]
        [JsonPropertyName("VatRatePercent")]
        public decimal? VatRatePercent { get; set; }

        /// <summary>True if the slot is programmed as tax-exempt (zwolniona, "zw.").</summary>
        [DataMember(Order = 30)]
        [JsonPropertyName("IsExempt")]
        public bool? IsExempt { get; set; }
    }
}
#endif
