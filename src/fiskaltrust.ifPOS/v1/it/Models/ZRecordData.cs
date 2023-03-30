using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// ZRecordData
    /// </summary>
    [DataContract]
    public class ZRecordData
    {
        /// <summary>
        /// ZRecNumber
        /// </summary>
        [DataMember(Order = 20)]
        public int ZRecNumber { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [DataMember(Order = 30)]
        public string Data { get; set; }
    }
}
