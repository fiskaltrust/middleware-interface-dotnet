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

        public async Task<DeviceInfo> GetDeviceInfoAsync() => await ExecuteHttpGetAsync<DeviceInfo>("v1", "GetDeviceInfo").ConfigureAwait(false);

        public async Task<ScuItEchoResponse> EchoAsync(ScuItEchoRequest request) => await ExecuteHttpGetAsync<ScuItEchoResponse>("v1", "Echo").ConfigureAwait(false);

        public async Task<FiscalReceiptResponse> FiscalReceiptInvoiceAsync(FiscalReceiptInvoice request) => await ExecuteHttpGetAsync<FiscalReceiptResponse>("v1", "FiscalReceiptInvoice").ConfigureAwait(false);

        public async Task<FiscalReceiptResponse> FiscalReceiptRefundAsync(FiscalReceiptRefund request) => await ExecuteHttpGetAsync<FiscalReceiptResponse>("v1", "FiscalReceiptRefund").ConfigureAwait(false);

        public async Task<DailyClosingResponse> ExecuteDailyClosingAsync(DailyClosingRequest request) => await ExecuteHttpGetAsync<DailyClosingResponse>("v1", "ExecuteDailyClosing").ConfigureAwait(false);


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