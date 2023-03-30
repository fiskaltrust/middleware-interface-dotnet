using System.ServiceModel;
using System.Threading.Tasks;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// The interface to communicate with fiskaltrust's SCUs for Italy.
    /// </summary>
    [ServiceContract]
    public interface IITSSCD
    {
        /// <summary>
        /// Returns printer information 
        /// </summary>
        [OperationContract(Name = "v1/GetPrinterInfo")]
        Task<PrinterStatus> GetPrinterInfoAsync();

        /// <summary>
        /// Returns the input message (can be used for a communication test with the SCU).
        /// </summary>
        [OperationContract(Name = "v1/Echo")]
        Task<ScuItEchoResponse> EchoAsync(ScuItEchoRequest request);

        /// <summary>
        /// Send FiscalInvoice to the printer.
        /// </summary>
        [OperationContract(Name = "v1/FiscalReceiptInvoice")]
        Task<FiscalReceiptResponse> FiscalReceiptInvoiceAsync(FiscalReceiptInvoice request);

        /// <summary>
        /// Send FiscalRefund to the printer.
        /// </summary>
        [OperationContract(Name = "v1/FiscalReceiptRefund")]
        Task<FiscalReceiptResponse> FiscalReceiptRefundAsync(FiscalReceiptRefund request);

        /// <summary>
        /// Get Data of the last Record
        /// </summary>
        [OperationContract(Name = "v1/GetLastRecordDataAsync")]
        Task<RecordData> GetLastRecordDataAsync();
    }
}