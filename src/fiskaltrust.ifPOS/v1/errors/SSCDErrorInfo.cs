using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace fiskaltrust.ifPOS.v1.errors
{
    /// <summary>
    /// Sale items on a commercial sale document.
    /// </summary>
    [DataContract]
    public enum SSCDErrorType
    {
        /// <summary>
        /// General Error
        /// </summary>
        [EnumMember]
        General = 0,
        /// <summary>
        /// Connection Error
        /// </summary>
        [EnumMember]
        Connection = 1,
        /// <summary>
        /// Device Error
        /// </summary>
        [EnumMember]
        Device = 2
    }

    /// <summary>
    /// SSCDErrorInfo
    /// </summary>
    [DataContract]
    public class SSCDErrorInfo : Exception
    {
        /// <summary>
        /// SSCDErrorType
        /// </summary>
        [DataMember(Order = 10)]
        public SSCDErrorType Type { get; private set; }
        /// <summary>
        /// SSCD Error Info
        /// </summary>
        [DataMember(Order = 20)]
        public string Info { get; private set; }

        /// <summary>
        /// SSCDErrorInfo
        /// </summary>
        public SSCDErrorInfo(string errorInfo)
        {
            Info = errorInfo;
            Type = SSCDErrorType.General;
        }

        /// <summary>
        /// SSCDErrorInfo From Connection SSCDErrorType
        /// </summary>
        public static SSCDErrorInfo FromConnection(string errorInfo)
        {
            return new(errorInfo)
            {
                Type = SSCDErrorType.Connection
            };
        }

        /// <summary>
        /// SSCDErrorInfo From Device SSCDErrorType
        /// </summary>
        public static SSCDErrorInfo FromDevice(string errorInfo)
        {
            return new(errorInfo)
            {
                Type = SSCDErrorType.Device
            };
        }

        /// <summary>
        /// SSCDErrorInfo SerializationInfo
        /// </summary>
        protected SSCDErrorInfo(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}