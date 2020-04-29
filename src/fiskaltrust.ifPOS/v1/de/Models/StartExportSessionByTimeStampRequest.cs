using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class StartExportSessionByTimeStampRequest
    {
        [DataMember(Order = 10)]
        public DateTime From { get; set; }

        [DataMember(Order = 20)]
        public DateTime To { get; set; }
    }
}
