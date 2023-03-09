using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// printerFiscalReport
    /// The FpMate CGI service can instruct the printer to perform a daily fiscal closure (Z report), a daily 
    ///financial report(X report) or both.Only the operator attribute is necessary.   ReportRequestTypes
    /// </summary>
    [DataContract]
    public class StartExportSessionRequest
    {
        /// <summary>
        /// Operator
        /// </summary>
        [DataMember(Order = 10)]
        public string Operator { get; set; }

        /// <summary>
        /// ReportRequestTypes
        /// </summary>
        [DataMember(Order = 20)]
        public ReportRequestTypes ReportRequestTypes { get; set; }

        /// <summary>
        /// Prepares data deletion at session end.
        /// </summary>
        [DataMember(Order = 30)]
        public bool Erase { get; set; }
    }
}
