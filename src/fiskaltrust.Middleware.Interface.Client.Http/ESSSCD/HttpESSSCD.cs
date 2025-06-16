using fiskaltrust.ifPOS.v1.es;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
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

        public async Task<ESSSCDInfo> GetInfoAsync() => await ExecuteHttpGetAsync<ESSSCDInfo>("v1", "GetInfo").ConfigureAwait(false);

        public async Task<ProcessResponse> ProcessReceiptAsync(ProcessRequest request)=> await ExecuteHttpPostAsync<ProcessResponse>("v1", "ProcessReceipt", request).ConfigureAwait(false);
       
       
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
                httpClient= new HttpClient { BaseAddress = url };
            }
            if (options.CashboxId.HasValue)
            {
                httpClient.DefaultRequestHeaders.Add("x-cashbox-id", options.CashboxId.ToString());
            }
            if (!string.IsNullOrEmpty(options.AccessToken))
            {
               httpClient.DefaultRequestHeaders.Add("x-cashbox-accesstoken", options.AccessToken);
            }

            return _httpClient;
        }
    }
}