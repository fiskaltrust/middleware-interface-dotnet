using System.ServiceModel;
namespace fiskaltrust.ifPOS.v2.me
{
    [ServiceContract]
    public interface IMESSCD
    {
        [OperationContract(Name = "v2/RegisterInvoice")]
        RegisterInvoiceResponse RegisterInvoice(RegisterInvoiceRequest RegisterInvoiceRequest);

        [OperationContract(Name = "v2/RegisterTCR")]
        RegisterTCRResponse RegisterTCR(RegisterTCRRequest RegisterTCRRequest);

        [OperationContract(Name = "v2/RegisterCashDeposit")]
        RegisterCashDepositResponse RegisterCashDeposit(RegisterCashDepositRequest RegisterCashDepositRequest);
    }
}