using fiskaltrust.ifPOS.v1.it;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    internal class HttpITSSCD : IITSSCD
    {
        private readonly HttpClient _httpClient;

        public HttpITSSCD(ClientOptions options)
        {
            _httpClient = GetClient(options);
        }

        public async Task<PrinterStatus> GetPrinterInfoAsync() => await ExecuteHttpGetAsync<PrinterStatus>("v1", "printerInfo").ConfigureAwait(false);

        public async Task<ScuItEchoResponse> EchoAsync(ScuItEchoRequest request) => await ExecuteHttpGetAsync<ScuItEchoResponse>("v1", "echo").ConfigureAwait(false);

        public async Task<FiscalReceiptResponse> FiscalReceiptInvoiceAsync(FiscalReceiptInvoice request) => await ExecuteHttpGetAsync<FiscalReceiptResponse>("v1", "invoice").ConfigureAwait(false);

        public async Task<FiscalReceiptResponse> FiscalReceiptRefundAsync(FiscalReceiptRefund request) => await ExecuteHttpGetAsync<FiscalReceiptResponse>("v1", "refund").ConfigureAwait(false);

        public async Task<StartExportSessionResponse> StartExportSessionAsync(StartExportSessionRequest request) => await ExecuteHttpGetAsync<StartExportSessionResponse>("v1", "startExport").ConfigureAwait(false);

        public async Task<EndExportSessionResponse> EndExportSessionAsync(EndExportSessionRequest request) => await ExecuteHttpGetAsync<EndExportSessionResponse>("v1", "endExport").ConfigureAwait(false);

        private async Task<T> ExecuteHttpGetAsync<T>(string urlVersion, string urlMethod)
        {
            var url = Path.Combine(urlVersion, urlMethod);
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(result);
        }

        private HttpClient GetClient(ClientOptions options)
        {
            var url = options.Url.ToString().EndsWith("/") ? options.Url : new Uri($"{options.Url}/");
            return new HttpClient { BaseAddress = url };
        }
    }
}