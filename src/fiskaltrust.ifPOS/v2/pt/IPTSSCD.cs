#if !WCF
using System.ServiceModel;
using System.Threading.Tasks;

namespace fiskaltrust.ifPOS.v2.pt
{
    public interface IPTSSCD
    {
        [OperationContract(Name = "v2/Echo")]
        Task<EchoResponse> EchoAsync(EchoRequest echoRequest);

        [OperationContract(Name = "v2/ProcessReceipt")]
        Task<(ProcessResponse response, string hash)> ProcessReceiptAsync(ProcessRequest request, string? lastHash);

        [OperationContract(Name = "v2/GetInfo")]
        Task<PTSSCDInfo> GetInfoAsync();
    }
}
#endif
