using fiskaltrust.ifPOS.v1.de;
using fiskaltrust.ifPOS.v2.me;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Common.RetryLogic
{
    public class MESSCDRetryProxyClient : IMESSCD
    {
        private readonly IRetryPolicyHandler<IMESSCD> _retryPolicyHelper;
        public MESSCDRetryProxyClient(IRetryPolicyHandler<IMESSCD> retryPolicyHelper) => _retryPolicyHelper = retryPolicyHelper;
        public async Task<RegisterCashDepositResponse> RegisterCashDepositAsync(RegisterCashDepositRequest registerCashDepositRequest) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.RegisterCashDepositAsync(registerCashDepositRequest));
        public async Task<RegisterInvoiceResponse> RegisterInvoiceAsync(RegisterInvoiceRequest registerInvoiceRequest) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.RegisterInvoiceAsync(registerInvoiceRequest));
        public async Task<RegisterTCRResponse> RegisterTCRAsync(RegisterTCRRequest registerTCRRequest) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.RegisterTCRAsync(registerTCRRequest));
    }
}
