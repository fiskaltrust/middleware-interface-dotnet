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
        [OperationContract(Name = "v2/GetTseInfo")]
        Task<TseInfo> GetTseInfoAsync();

        /// <summary>
        /// Returns the input message (can be used for a communication test with the SCU).
        /// </summary>
        [OperationContract(Name = "v2/Echo")]
        Task<ScuItEchoResponse> EchoAsync(ScuItEchoRequest request);

        /// <summary>
        /// Send FiscalReceipt to the printer.
        /// </summary>
        [OperationContract(Name = "v2/FiscalReceipt")]
        Task<FiscalReceiptResponse> FiscalReceipt(FiscalReceiptRequest request);


        [OperationContract(Name = "v2/StartExportSession")]
        Task<StartExportSessionResponse> StartExportSessionAsync(StartExportSessionRequest request);


        [OperationContract(Name = "v2/EndExportSession")]
        Task<EndExportSessionResponse> EndExportSessionAsync(EndExportSessionRequest request);
    }
}