using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    ///  rtMainStatus indicates the RT main status
    /// </summary>
    [DataContract]
    public enum MainState
    {
        /// <summary>
        /// Not in service (MF)
        /// </summary>
        NotInService = 1,
        /// <summary>
        /// In service (RT)
        /// </summary>
        InService = 2,
    }

    /// <summary>
    ///  rtSubStatus indicates the RT sub status
    /// </summary>
    [DataContract]
    public enum SubState
    {
        /// <summary>
        /// No certificates
        /// </summary>
        NoCertificates = 2,
        /// <summary>
        /// Incomplete certificates
        /// </summary>
        IncompleteCertificates3 = 3,
        /// <summary>
        /// Incomplete certificates
        /// </summary>
        IncompleteCertificates4 = 4,
        /// <summary>
        /// ICertificates loaded and ready to register (Da Censire)
        /// </summary>
        CertificatesReadyToRegister = 5,
        /// <summary>
        /// Registered (Censito)
        /// </summary>
        Registered = 6,
        /// <summary>
        /// Activated (Attivato)
        /// </summary>
        Activated = 7,
        /// <summary>
        /// Switch date set (Programmato RT)
        /// </summary>
        SwitchDateSet = 8,
        /// <summary>
        /// End of Life (Dismesso)
        /// </summary>
        EndofLife = 9
    }
}
