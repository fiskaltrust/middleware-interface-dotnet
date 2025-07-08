using System.ServiceModel;
using System.Threading.Tasks;

namespace fiskaltrust.ifPOS.v2.es
{
    [ServiceContract]
    public interface IESSSCD
    {
        [OperationContract(Name = "v2/ProcessReceipt")]
        Task<ProcessResponse> ProcessReceiptAsync(ProcessRequest request);

        [OperationContract(Name = "v2/GetInfo")]
        Task<ESSSCDInfo> GetInfoAsync();
    }
}
