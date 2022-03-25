using fiskaltrust.ifPOS.v1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    internal class HttpPos : IPOS
    {
        private readonly HttpPosClientOptions _options;
        private readonly HttpClient _httpClient;

        private delegate string AsyncEchoCaller(string message);
        private delegate ifPOS.v0.ReceiptResponse AsyncSignCaller(ifPOS.v0.ReceiptRequest request);
        private delegate Stream AsyncJournalCaller(long ftJournalType, long from, long to);


        public HttpPos(HttpPosClientOptions options)
        {
            _httpClient = GetClient(options);
            _options = options;

        }

        private HttpClient GetClient(HttpPosClientOptions options)
        {
            var url = options.Url.ToString().EndsWith("/") ? options.Url : new Uri($"{options.Url}/");
            var client = new HttpClient { BaseAddress = url };

            if (options.CashboxId.HasValue)
                client.DefaultRequestHeaders.Add("cashboxid", options.CashboxId.Value.ToString());

            if (!string.IsNullOrEmpty(options.AccessToken))
                client.DefaultRequestHeaders.Add("accesstoken", options.AccessToken);

            return client;
        }

        async Task<EchoResponse> IPOS.EchoAsync(EchoRequest message)
        {
            var jsonstring = JsonSerializer.Serialize(message);
            var jsonContent = new StringContent(jsonstring, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync("v2/Echo", jsonContent))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EchoResponse>(content.ToString());
            }
        }

        async Task<ReceiptResponse> IPOS.SignAsync(ReceiptRequest request)
        {
            var jsonstring = JsonSerializer.Serialize(request);
            var jsonContent = new StringContent(jsonstring, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync("v2/Sign", jsonContent))
            {
                var content = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();

                return JsonSerializer.Deserialize<ReceiptResponse>(content);
            }
        }

        async IAsyncEnumerable<JournalResponse> IPOS.JournalAsync(JournalRequest request)
        {
            using (var response = await _httpClient.GetAsync($"v2/Journal?type={request.ftJournalType}&from={request.From}&to={request.To}"))
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                response.EnsureSuccessStatusCode();

                yield return new JournalResponse { Chunk = content.ToList() };
                yield break;
            }
        }

        [Obsolete("BeginEcho is deprecated, please use EchoAsync instead.")]
        IAsyncResult ifPOS.v0.IPOS.BeginEcho(string message, AsyncCallback callback, object state)
        {
            throw new NotImplementedException("Sync methods are not implemented");
        }

        [Obsolete("EndEcho is deprecated, please use EchoAsync instead.")]
        string ifPOS.v0.IPOS.EndEcho(IAsyncResult result)
        {
            throw new NotImplementedException("Sync methods are not implemented");
        }

        [Obsolete("Echo is deprecated, please use EchoAsync instead.")]
        string ifPOS.v0.IPOS.Echo(string message)
        {
            throw new NotImplementedException("Sync methods are not implemented");
        }

        [Obsolete("BeginSign is deprecated, please use SignAsync instead.")]
        IAsyncResult ifPOS.v0.IPOS.BeginSign(ifPOS.v0.ReceiptRequest data, AsyncCallback callback, object state)
        {
            throw new NotImplementedException("Sync methods are not implemented");
        }

        [Obsolete("EndSign is deprecated, please use SignAsync instead.")]
        ifPOS.v0.ReceiptResponse ifPOS.v0.IPOS.EndSign(IAsyncResult result)
        {
            throw new NotImplementedException("Sync methods are not implemented");
        }

        [Obsolete("Sign is deprecated, please use SignAsync instead.")]
        ifPOS.v0.ReceiptResponse ifPOS.v0.IPOS.Sign(ifPOS.v0.ReceiptRequest data)
        {
            throw new NotImplementedException("Sync methods are not implemented");
        }

        [Obsolete("BeginJournal is deprecated, please use JournalAsync instead.")]
        IAsyncResult ifPOS.v0.IPOS.BeginJournal(long ftJournalType, long from, long to, AsyncCallback callback, object state)
        {
            throw new NotImplementedException("Sync methods are not implemented");
        }

        [Obsolete("EndJournal is deprecated, please use JournalAsync instead.")]
        Stream ifPOS.v0.IPOS.EndJournal(IAsyncResult result)
        {
            throw new NotImplementedException("Sync methods are not implemented");
        }

        [Obsolete("Journal is deprecated, please use JournalAsync instead.")]
        Stream ifPOS.v0.IPOS.Journal(long ftJournalType, long from, long to)
        {
            throw new NotImplementedException("Sync methods are not implemented");
        }
    }
}
