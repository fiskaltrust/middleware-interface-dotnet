using fiskaltrust.ifPOS.v1.de;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Common.RetryLogic
{
    public class DeSscdRetryProxyClient : IDESSCD
    {
        private readonly IRetryPolicyHandler<IDESSCD> _retryPolicyHelper;

        public DeSscdRetryProxyClient(IRetryPolicyHandler<IDESSCD> retryPolicyHelper) => _retryPolicyHelper = retryPolicyHelper;

        public async Task<ScuEchoResponse> EchoAsync(ScuEchoRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.EchoAsync(request));

        public async Task<EndExportSessionResponse> EndExportSessionAsync(EndExportSessionRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.EndExportSessionAsync(request));

        public async Task ExecuteSelfTestAsync() => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.ExecuteSelfTestAsync());

        public async Task ExecuteSetTseTimeAsync() => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.ExecuteSetTseTimeAsync());

        public async Task<ExportDataResponse> ExportDataAsync(ExportDataRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.ExportDataAsync(request));

        public async Task<FinishTransactionResponse> FinishTransactionAsync(FinishTransactionRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.FinishTransactionAsync(request));

        public async Task<TseInfo> GetTseInfoAsync() => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.GetTseInfoAsync());

        public async Task<RegisterClientIdResponse> RegisterClientId(RegisterClientIdRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.RegisterClientId(request));

        public async Task<TseState> SetTseStateAsync(TseState state) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.SetTseStateAsync(state));

        public async Task<StartExportSessionResponse> StartExportSessionAsync() => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.StartExportSessionAsync());

        public async Task<StartExportSessionResponse> StartExportSessionByTimeStampAsync(StartExportSessionByTimeStampRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.StartExportSessionByTimeStampAsync(request));

        public async Task<StartExportSessionResponse> StartExportSessionByTransactionAsync(StartExportSessionByTransactionRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.StartExportSessionByTransactionAsync(request));

        public async Task<StartTransactionResponse> StartTransactionAsync(StartTransactionRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.StartTransactionAsync(request));

        public async Task<UnregisterClientIdResponse> UnregisterClientId(UnregisterClientIdRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.UnregisterClientId(request));

        public async Task<UpdateTransactionResponse> UpdateTransactionAsync(UpdateTransactionRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.UpdateTransactionAsync(request));
    }
}
