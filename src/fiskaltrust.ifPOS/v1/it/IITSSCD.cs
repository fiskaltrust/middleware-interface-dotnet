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
        /// Returns device information from, e.g. from the printer or server
        /// </summary>
        [OperationContract(Name = "v1/GetDeviceInfo")]
        Task<DeviceInfo> GetDeviceInfoAsync();

        /// <summary>
        /// Returns the input message (can be used for a communication test with the SCU).
        /// </summary>
        [OperationContract(Name = "v1/Echo")]
        Task<ScuItEchoResponse> EchoAsync(ScuItEchoRequest request);

        /// <summary>
        /// Send a request to fiscalize an invoice to the printer or server
        /// </summary>
        [OperationContract(Name = "v1/FiscalReceiptInvoice")]
        Task<FiscalReceiptResponse> FiscalReceiptInvoiceAsync(FiscalReceiptInvoice request);

        /// <summary>
        /// Send a request to fiscalize a refund to the printer or server
        /// </summary>
        [OperationContract(Name = "v1/FiscalReceiptRefund")]
        Task<FiscalReceiptResponse> FiscalReceiptRefundAsync(FiscalReceiptRefund request);

        /// <summary>
        /// Send a request to execute (and print) a daily closing receipt to the printer or server
        /// </summary>
        [OperationContract(Name = "v1/ExecuteDailyClosing")]
        Task<DailyClosingResponse> ExecuteDailyClosingAsync(DailyClosingRequest request);

        /// <summary>
        /// Send a request to fiscalize an invoice to the printer or server
        /// </summary>
        [OperationContract(Name = "v1/NonFiscalReceipt")]
        Task<Response> NonFiscalReceiptAsync(NonFiscalRequest request);
    }
}