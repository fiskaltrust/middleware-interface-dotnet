#if SYSTEM_TEXT_JSON
using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.pl;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Common.RetryLogic
{
    public class PLSSCDRetryProxyClient : IPLSSCD
    {
        private readonly IRetryPolicyHandler<IPLSSCD> _retryPolicyHelper;
        public PLSSCDRetryProxyClient(IRetryPolicyHandler<IPLSSCD> retryPolicyHelper) => _retryPolicyHelper = retryPolicyHelper;

        public async Task<EchoResponse> EchoAsync(EchoRequest echoRequest) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.EchoAsync(echoRequest));
        public async Task<PLSSCDInfo> GetInfoAsync() => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.GetInfoAsync());

        public async Task<ProcessResponse> ProcessReceiptAsync(ProcessRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.ProcessReceiptAsync(request));
    }
}
#endif
