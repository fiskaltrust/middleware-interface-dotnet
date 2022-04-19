using System.ServiceModel;
using System.Threading.Tasks;
namespace fiskaltrust.ifPOS.v2.me
{
    [ServiceContract]
    public interface IMESSCD
    {
        [OperationContract(Name = "v2/RegisterInvoice")]
        Task<RegisterInvoiceResponse> RegisterInvoice(RegisterInvoiceRequest RegisterInvoiceRequest);

        [OperationContract(Name = "v2/RegisterTCR")]
        Task<RegisterTCRResponse> RegisterTCR(RegisterTCRRequest RegisterTCRRequest);

        [OperationContract(Name = "v2/RegisterCashDeposit")]
        Task<RegisterCashDepositResponse> RegisterCashDeposit(RegisterCashDepositRequest RegisterCashDepositRequest);
    }
}