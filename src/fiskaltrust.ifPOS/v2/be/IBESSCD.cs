using System.ServiceModel;
using System.Threading.Tasks;

namespace fiskaltrust.ifPOS.v2.be
{
    public interface IBESSCD
    {
        [OperationContract(Name = "v2/Echo")]
        Task<EchoResponse> EchoAsync(EchoRequest echoRequest);

        [OperationContract(Name = "v2/ProcessReceipt")]
        Task<ProcessResponse> ProcessReceiptAsync(ProcessRequest request);

        [OperationContract(Name = "v2/GetInfo")]
        Task<BESSCDInfo> GetInfoAsync();
    }
}
