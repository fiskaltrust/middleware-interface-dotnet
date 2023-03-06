using fiskaltrust.ifPOS.v1.it;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Common.RetryLogic
{
    public class ITSSCDRetryProxyClient : IITSSCD
    {
        private readonly IRetryPolicyHandler<IITSSCD> _retryPolicyHelper;
        public ITSSCDRetryProxyClient(IRetryPolicyHandler<IITSSCD> retryPolicyHelper) => _retryPolicyHelper = retryPolicyHelper;

        public async Task<PrinterStatus> GetPrinterInfoAsync() => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.GetPrinterInfoAsync());

        public async Task<ScuItEchoResponse> EchoAsync(ScuItEchoRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.EchoAsync(request));

        public async Task<FiscalReceiptResponse> FiscalReceiptInvoiceAsync(FiscalReceiptInvoice request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.FiscalReceiptInvoiceAsync(request));

        public async Task<FiscalReceiptResponse> FiscalReceiptRefundAsync(FiscalReceiptRefund request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.FiscalReceiptRefundAsync(request));

        public async Task<StartExportSessionResponse> StartExportSessionAsync(StartExportSessionRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.StartExportSessionAsync(request));

        public async Task<EndExportSessionResponse> EndExportSessionAsync(EndExportSessionRequest request) => await _retryPolicyHelper.RetryFuncAsync(async (proxy) => await proxy.EndExportSessionAsync(request));
    }
}
