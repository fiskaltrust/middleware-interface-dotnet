using fiskaltrust.ifPOS.v1.de;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    internal class HttpDeSscd : IDESSCD
    {
        private readonly HttpClient _httpClient;

        public HttpDeSscd(ClientOptions options)
        {
            _httpClient = GetClient(options);
        }

        public async Task<TseExportDataResult> ExportDataAsync() => await ExecuteHttpGetAsync<TseExportDataResult>("v1", "exportdata").ConfigureAwait(false);

        public async Task<FinishTransactionResponse> FinishTransactionExportDataAsync(FinishTransactionRequest request) => await ExecuteHttpPostAsync<FinishTransactionResponse>("v1", "finishtransactionexportdata", request).ConfigureAwait(false);

        public async Task<TseInfo> GetTseInfoAsync() => await ExecuteHttpGetAsync<TseInfo>("v1", "tseinfo").ConfigureAwait(false);

        public async Task<TseState> SetTseStateAsync(TseState state) => await ExecuteHttpPostAsync<TseState>("v1", "tsestate", state).ConfigureAwait(false);

        public async Task<StartTransactionResponse> StartTransactionExportDataAsync(StartTransactionRequest request) => await ExecuteHttpPostAsync<StartTransactionResponse>("v1", "starttransactionexportdata", request).ConfigureAwait(false);

        public async Task<UpdateTransactionResponse> UpdateTransactionExportDataAsync(UpdateTransactionRequest request) => await ExecuteHttpPostAsync<UpdateTransactionResponse>("v1", "updatetransactionexportdata", request).ConfigureAwait(false);

        private async Task<T> ExecuteHttpPostAsync<T>(string urlVersion, string urlMethod, object parameter = null)
        {
            var url = Path.Combine(urlVersion, urlMethod);
            StringContent stringContent = null;

            if (parameter != null)
            {
                var json = JsonConvert.SerializeObject(parameter);
                stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            }
            var response = await _httpClient.PostAsync(url, stringContent).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(result);
        }

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