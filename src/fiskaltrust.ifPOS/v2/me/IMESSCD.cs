using System.ServiceModel;
using System.Threading.Tasks;
namespace fiskaltrust.ifPOS.v2.me
{
    [ServiceContract]
    public interface IMESSCD
    {
        [OperationContract(Name = "v2/RegisterInvoice")]
        Task<RegisterInvoiceResponse> RegisterInvoiceAsync(RegisterInvoiceRequest registerInvoiceRequest);

        [OperationContract(Name = "v2/RegisterTCR")]
        Task<RegisterTCRResponse> RegisterTCRAsync(RegisterTCRRequest registerTCRRequest);

        [OperationContract(Name = "v2/RegisterCashDeposit")]
        Task<RegisterCashDepositResponse> RegisterCashDepositAsync(RegisterCashDepositRequest registerCashDepositRequest);
    }
}