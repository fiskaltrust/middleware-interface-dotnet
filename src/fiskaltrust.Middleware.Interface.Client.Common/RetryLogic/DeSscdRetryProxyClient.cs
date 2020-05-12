using fiskaltrust.ifPOS.v1.de;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Common.RetryLogic
{
    public class DeSscdRetryProxyClient : IDESSCD
    {
        private readonly IRetryPolicyHandler<IDESSCD> _retryPolicyHelper;

        public DeSscdRetryProxyClient(IRetryPolicyHandler<IDESSCD> retryPolicyHelper) => _retryPolicyHelper = retryPolicyHelper;

        public async Task<TseExportDataResult> ExportDataAsync() => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.ExportDataAsync());

        public async Task<FinishTransactionResponse> FinishTransactionExportDataAsync(FinishTransactionRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.FinishTransactionExportDataAsync(request));

        public async Task<TseInfo> GetTseInfoAsync() => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.GetTseInfoAsync());

        public async Task<TseState> SetTseStateAsync(TseState state) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.SetTseStateAsync(state));

        public async Task<StartTransactionResponse> StartTransactionExportDataAsync(StartTransactionRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.StartTransactionExportDataAsync(request));

        public async Task<UpdateTransactionResponse> UpdateTransactionExportDataAsync(UpdateTransactionRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.UpdateTransactionExportDataAsync(request));
    }
}
