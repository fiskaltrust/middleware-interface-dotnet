#if SYSTEM_TEXT_JSON
using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.es;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    public class HttpESSSCD : IESSSCD
    {
        private readonly HttpClient _httpClient;

        public HttpESSSCD(HttpESSSCDClientOptions options)
        {
            _httpClient = GetClient(options);
        }
        public async Task<EchoResponse> EchoAsync(EchoRequest echoRequest) => await ExecuteHttpPostAsync<EchoResponse>("Echo", echoRequest).ConfigureAwait(false);
        public async Task<ESSSCDInfo> GetInfoAsync() => await ExecuteHttpGetAsync<ESSSCDInfo>("GetInfo").ConfigureAwait(false);

        public async Task<ProcessResponse> ProcessReceiptAsync(ProcessRequest request) => await ExecuteHttpPostAsync<ProcessResponse>("ProcessReceipt", request).ConfigureAwait(false);


        private async Task<T> ExecuteHttpPostAsync<T>(string urlPath, object parameter = null)
        {
            var url = ConstructPath(urlPath);
            StringContent stringContent = null;

            if (parameter != null)
            {
                var json = JsonSerializer.Serialize(parameter);
                stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            }
            var response = await _httpClient.PostAsync(url, stringContent).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new Exception($"Request failed with status {response.StatusCode}: {errorContent}", new HttpRequestException(errorContent));
            }

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(result);
        }

        private async Task<T> ExecuteHttpGetAsync<T>(string urlPath)
        {
            var url = ConstructPath(urlPath);
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new Exception($"Request failed with status {response.StatusCode}: {errorContent}", new HttpRequestException(errorContent));
            }

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(result);
        }

        private string ConstructPath(string urlPath) => Path.Combine("v2", urlPath);

        private HttpClient GetClient(HttpESSSCDClientOptions options)
        {
            HttpClient httpClient;
            var url = options.Url.ToString().EndsWith("/") ? options.Url : new Uri($"{options.Url}/");
            if (options.DisableSslValidation.HasValue && options.DisableSslValidation.Value)
            {
                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                };

                httpClient = new HttpClient(handler) { BaseAddress = url };
            }
            else
            {
                httpClient = new HttpClient { BaseAddress = url };
            }
            if (options.CashboxId.HasValue)
            {
                httpClient.DefaultRequestHeaders.Add("x-cashbox-id", options.CashboxId.ToString());
            }
            if (!string.IsNullOrEmpty(options.AccessToken))
            {
                httpClient.DefaultRequestHeaders.Add("x-cashbox-accesstoken", options.AccessToken);
            }

            return httpClient;
        }
    }
}
#endif