#if NET461

using fiskaltrust.ifPOS.Tests.Helpers;
using System.ServiceModel;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using fiskaltrust.ifPOS.v1.de;
using Newtonsoft.Json;
using System.Text;
using fiskaltrust.ifPOS.Tests.Helpers.Wcf;

namespace fiskaltrust.ifPOS.Tests.v1.IDESSCD
{
    public class HttpIDESSCDTests : IDESSCDTests
    {
        private string _url;
        private ServiceHost _serviceHost;

        public HttpIDESSCDTests()
        {
            _url = $"http://localhost:8080/scu/{Guid.NewGuid()}";
        }

        ~HttpIDESSCDTests()
        {
            _serviceHost.Close();
            _serviceHost = null;
        }

        protected override ifPOS.v1.de.IDESSCD CreateClient() => WcfHelper.GetRestProxy<ifPOS.v1.de.IDESSCD>(_url);

        protected override void StartHost() => _serviceHost = WcfHelper.StartRestHost(_url, new DummyDESSCD());

        [Test]
        public async Task ExportDataV1_ShouldReturn()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync(new Uri(_url + "/v1/exportdata"));
                result.EnsureSuccessStatusCode();
            }
        }

        [Test]
        public async Task GetTseInfoV1_ShouldReturn()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync(new Uri(_url + "/v1/tseinfo"));
                result.EnsureSuccessStatusCode();
            }
        }

        [Test]
        public async Task StartTransactionV1_ShouldReturn()
        {
            using (var httpClient = new HttpClient())
            {
                var requestData = new StartTransactionRequest();
                var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(new Uri(_url + "/v1/starttransactionexportdata"), content);
                result.EnsureSuccessStatusCode();
            }
        }

        [Test]
        public async Task UpdateTransactionV1_ShouldReturn()
        {
            using (var httpClient = new HttpClient())
            {
                var requestData = new UpdateTransactionRequest();
                var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(new Uri(_url + "/v1/updatetransactionexportdata"), content);
                result.EnsureSuccessStatusCode();
            }
        }

        [Test]
        public async Task FinishTransactionV1_ShouldReturn()
        {
            using (var httpClient = new HttpClient())
            {
                var requestData = new FinishTransactionRequest();
                var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(new Uri(_url + "/v1/finishtransactionexportdata"), content);
                result.EnsureSuccessStatusCode();
            }
        }

        [Test]
        public async Task SetTseStateV1_ShouldReturn()
        {
            using (var httpClient = new HttpClient())
            {
                var requestData = new TseState();
                var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(new Uri(_url + "/v1/tsestate"), content);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}

#endif