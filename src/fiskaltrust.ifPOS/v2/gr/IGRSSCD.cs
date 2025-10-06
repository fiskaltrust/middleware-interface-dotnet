#if !WCF
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using fiskaltrust.ifPOS.v2;

namespace fiskaltrust.ifPOS.v2.gr
{
    public interface IGRSSCD
    {
        [OperationContract(Name = "v2/Echo")]
        Task<EchoResponse> EchoAsync(EchoRequest echoRequest);

        [OperationContract(Name = "v2/ProcessReceipt")]
        Task<ProcessResponse> ProcessReceiptAsync(ProcessRequest request, List<(ReceiptRequest, ReceiptResponse)> receiptReferences);

        [OperationContract(Name = "v2/GetInfo")]
        Task<GRSSCDInfo> GetInfoAsync();
    }
}
#endif