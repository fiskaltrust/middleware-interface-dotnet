using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.es;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Common.RetryLogic
{
    public class ESSSCDRetryProxyClient : IESSSCD
    {
        private readonly IRetryPolicyHandler<IESSSCD> _retryPolicyHelper;
        public ESSSCDRetryProxyClient(IRetryPolicyHandler<IESSSCD> retryPolicyHelper) => _retryPolicyHelper = retryPolicyHelper;

        public async Task<EchoResponse> EchoAsync(EchoRequest echoRequest) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.EchoAsync(echoRequest));
        public async Task<ESSSCDInfo> GetInfoAsync() => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.GetInfoAsync());

        public async Task<ProcessResponse> ProcessReceiptAsync(ProcessRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.ProcessReceiptAsync(request));
    }
}
