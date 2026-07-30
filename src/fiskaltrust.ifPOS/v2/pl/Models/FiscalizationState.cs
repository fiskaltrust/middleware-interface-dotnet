#if !WCF
namespace fiskaltrust.ifPOS.v2.pl
{
    /// <summary>
    /// The fiscalization state of the connected Polish fiscal device (online cash register / fiscal printer).
    /// Fiscalizing a device is a certified-technician (serwis) act and can never be triggered through this interface.
    /// </summary>
    public enum FiscalizationState
    {
        /// <summary>The state could not be determined (e.g. the device was not reachable yet).</summary>
        Unknown = 0,

        /// <summary>The device is operating in non-fiscal (training) mode and must not be used for sales.</summary>
        NonFiscal = 1,

        /// <summary>The device is fiscalized and legally usable for recording sales.</summary>
        Fiscalized = 2,

        /// <summary>The fiscal memory was closed (end of life); the device can only be read, not used for sales.</summary>
        ReadOnly = 3,
    }
}
#endif
