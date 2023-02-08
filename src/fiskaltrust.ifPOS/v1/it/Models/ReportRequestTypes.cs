using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    ///The following three report types are supported:
    /// </summary>
    [DataContract]
    public enum ReportRequestTypes
    {
        /// <summary>
        ///Financial report only
        /// </summary>
        XReport = 1,
        /// <summary>
        ///t Fiscal daily closure only 
        /// </summary>
        ZReport = 2,
        /// <summary>
        ///inancial report and fiscal daily closure (in that order)
        /// </summary>
        XZReport = 3,

    }
}
