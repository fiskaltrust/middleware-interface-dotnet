#if !WCF
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.ifPOS.v2.pl
{
    /// <summary>
    /// Information about the Polish SCU and the fiscal device it is connected to. In Poland the certified
    /// device owns the compliance state (fiscal memory, numbering, VAT table, CRK transmission), so all
    /// properties are optional and reflect the last state reported by the device.
    /// </summary>
    [DataContract]
    public class PLSSCDInfo
    {
        /// <summary>The manufacturing serial number of the fiscal device (numer fabryczny).</summary>
        [DataMember(Order = 10)]
        [JsonPropertyName("DeviceSerialNumber")]
        public string DeviceSerialNumber { get; set; }

        /// <summary>The unique device number assigned by the Ministry of Finance (numer unikatowy).</summary>
        [DataMember(Order = 20)]
        [JsonPropertyName("UniqueDeviceNumber")]
        public string UniqueDeviceNumber { get; set; }

        /// <summary>The registration number assigned by the tax office (numer ewidencyjny).</summary>
        [DataMember(Order = 30)]
        [JsonPropertyName("RegistrationNumber")]
        public string RegistrationNumber { get; set; }

        /// <summary>The fiscalization state of the device.</summary>
        [DataMember(Order = 40)]
        [JsonPropertyName("FiscalizationState")]
        public FiscalizationState? FiscalizationState { get; set; }

        /// <summary>The PTU VAT rate table (slots A–G) currently programmed on the device.</summary>
        [DataMember(Order = 50)]
        [JsonPropertyName("VatRateTable")]
        public List<PLVatRateTableEntry> VatRateTable { get; set; }

        /// <summary>Whether the device can currently reach the CRK (Central Repository of Cash Registers).</summary>
        [DataMember(Order = 60)]
        [JsonPropertyName("CrkReachable")]
        public bool? CrkReachable { get; set; }

        /// <summary>The moment of the last successful transmission to the CRK, as reported by the device.</summary>
        [DataMember(Order = 70)]
        [JsonPropertyName("CrkLastTransmission")]
        public DateTime? CrkLastTransmission { get; set; }

        /// <summary>Whether the device supports issuing e-receipts (e-paragony).</summary>
        [DataMember(Order = 80)]
        [JsonPropertyName("EReceiptCapable")]
        public bool? EReceiptCapable { get; set; }

        /// <summary>The number of the current daily (Z) report period.</summary>
        [DataMember(Order = 90)]
        [JsonPropertyName("CurrentZReportNumber")]
        public int? CurrentZReportNumber { get; set; }
    }
}
#endif
