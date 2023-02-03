using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{

    /// <summary>
    /// queryPrinterStatus response
    /// </summary>
    [DataContract]
    public class PrinterStatus
    {
        /// <summary>
        /// cpuRel indicates the fiscal board firmware version
        /// </summary>
        [DataMember]
        public string FiscalBoardFirmwareVersion { get; set; }

        /// <summary>
        ///  mfRel indicates the MPR (fiscal memory) firmware version
        /// </summary>
        [DataMember]
        public string MprFirmwareVersion { get; set; }

        /// <summary>
        /// mfStatus indicates the MPR (fiscal memory) status (OK, FULL etc.)
        /// </summary>
        [DataMember]
        public string MprStatus{ get; set; }

        /// <summary>
        ///  fpStatus indicates the printer status 
        /// </summary>
        [DataMember]
        public FpStatus FpStatus { get; set; }

        /// <summary>
        ///  rtMainStatus indicates the RT main status *
        /// </summary>
        [DataMember]
        public MainState MainState { get; set; }

        /// <summary>
        ///  rtMainStatus indicates the RT main status *
        /// </summary>
        [DataMember]
        public SubState SubState { get; set; }

        /// <summary>
        ///  rtDailyOpen = indicates the logical DAY OPENED logical condition (0=closed{false} and 1=open{true})
        /// </summary>
        [DataMember]
        public bool DailyOpen { get; set; }

        /// <summary>
        /// rtNoWorkingPeriod indicates whether a Z report must be performed or not (0=no and 1=yes)
        /// </summary>
        [DataMember]
        public bool ZReportNeeded { get; set; }

        /// <summary>
        /// rtFileToSend indicates the number of files due to be sent to the tax authority
        /// </summary>
        [DataMember]
        public int FilesToSend { get; set; }

        /// <summary>
        /// rtOldFileToSend indicates the number of files due to be sent to the tax authority but still
        /// waiting on the printer after a configurable number of days(SET 15/25)
        /// </summary>
        [DataMember]
        public int OldFilesToSend { get; set; }

        /// <summary>
        /// rtFileRejected indicates the number of files rejected by the tax authority
        /// </summary>
        [DataMember]
        public int FilesRejected { get; set; }

        /// <summary>
        /// rtExpiryCD indicates the device certificate expiry date in the yyyymmdd format
        /// </summary>
        [DataMember]
        public string ExpireDeviceCertificateDate { get; set; }

        /// <summary>
        /// rtExpiryCA = indicates the tax authority communication certificate expiry date in the yyyymmdd format
        /// </summary>
        [DataMember]
        public string ExpireTACommunicationCertificateDate { get; set; }

        /// <summary>
        ///  indicates the mode *  
        /// </summary>
        [DataMember]
        public bool TrainingMode { get; set; }

        /// <summary>
        ///   indicates the last firmware update outcome *
        /// </summary>
        [DataMember]
        public string UpgradeResult  { get; set; }
    }
}
