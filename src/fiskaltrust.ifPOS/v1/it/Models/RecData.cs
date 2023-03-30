using System.Collections.Generic;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// Get Printer data from the last receipt
    /// </summary>
    [DataContract]
    public class RecData
    {
        /// <summary>
        /// RecNumber
        /// </summary>
        [DataMember(Order = 10)]
        public int RecNumber { get; set; }

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
