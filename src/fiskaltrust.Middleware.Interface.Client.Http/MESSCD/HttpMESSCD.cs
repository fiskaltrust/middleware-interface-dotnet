using fiskaltrust.ifPOS.v1.me;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    internal class HttpMESSCD : IMESSCD
    {
        private readonly HttpClient _httpClient;

        public HttpMESSCD(ClientOptions options)
        {
            _httpClient = GetClient(options);
        }

        public async Task<RegisterInvoiceResponse> RegisterInvoiceAsync(RegisterInvoiceRequest registerInvoiceRequest) => await ExecuteHttpGetAsync<RegisterInvoiceResponse>("v1", "Invoice").ConfigureAwait(false);

        public async Task<RegisterTcrResponse> RegisterTcrAsync(RegisterTcrRequest registerTcrRequest) => await ExecuteHttpGetAsync<RegisterTcrResponse>("v1", "Register").ConfigureAwait(false);

        public async Task<RegisterCashDepositResponse> RegisterCashDepositAsync(RegisterCashDepositRequest registerCashDepositRequest) => await ExecuteHttpGetAsync<RegisterCashDepositResponse>("v1", "Deposit").ConfigureAwait(false);

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

        public async Task UnregisterTcrAsync(RegisterTcrRequest registerTCRRequest) => await ExecuteHttpGetAsync<RegisterInvoiceResponse>("v1", "Unregister").ConfigureAwait(false);

        public async Task<RegisterCashWithdrawalResponse> RegisterCashWithdrawalAsync(RegisterCashWithdrawalRequest registerCashDepositRequest) => await ExecuteHttpGetAsync<RegisterCashWithdrawalResponse>("v1", "Withdrawl").ConfigureAwait(false);

        public async Task<ScuMeEchoResponse> EchoAsync(ScuMeEchoRequest request) => await ExecuteHttpGetAsync<ScuMeEchoResponse>("v1", "Echo").ConfigureAwait(false);
    }
}