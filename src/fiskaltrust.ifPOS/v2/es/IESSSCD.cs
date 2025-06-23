using System.ServiceModel;
using System.Threading.Tasks;

namespace fiskaltrust.ifPOS.v2.es
{
    [ServiceContract]
    public interface IESSSCD
    {
        [OperationContract(Name = "v1/ProcessReceipt")]
        Task<ProcessResponse> ProcessReceiptAsync(ProcessRequest request);

        [OperationContract(Name = "v1/GetInfo")]
        Task<ESSSCDInfo> GetInfoAsync();
    }
}
