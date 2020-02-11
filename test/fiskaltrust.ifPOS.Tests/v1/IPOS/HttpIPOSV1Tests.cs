#if WCF

using fiskaltrust.ifPOS.Tests.Helpers;
using fiskaltrust.ifPOS.Tests.Helpers.Wcf;
using fiskaltrust.ifPOS.v1;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.ServiceModel;

namespace fiskaltrust.ifPOS.Tests.v1.IPOS
{
    // If these tests are failing you have to execute the following command as an Administrator
    // netsh http add urlacl url=http://+:8008/ user=Everyone listen=yes
    // To add the url that is used for binding
    public class HttpIPOSV1Tests : IPOSV1Tests
    {
        private string _url;
        private ServiceHost _serviceHost;

        public HttpIPOSV1Tests()
        {
            _url = $"http://localhost:12080/pos/{Guid.NewGuid()}";
        }

        ~HttpIPOSV1Tests()
        {
            _serviceHost.Close();
            _serviceHost = null;
        }


        protected override ifPOS.v1.IPOS CreateClient() => WcfHelper.GetRestProxy<ifPOS.v1.IPOS>(_url);

        protected override void StartHost() => _serviceHost = WcfHelper.StartRestHost<ifPOS.v1.IPOS>(_url, new DummyPOSV1());

        [Test]
        public void SignV0_ShouldReturnSameQueueId_For_WebClient()
        {
            var queueId = Guid.NewGuid().ToString();
            using (var webClient = new WebClient())
            {
                webClient.Headers["content-type"] = "application/json";
                var json = JsonConvert.SerializeObject(new ReceiptRequest
                {
                    ftQueueID = queueId,
                    ftCashBoxID = "",
                    cbTerminalID = "",
                    cbReceiptReference = "",
                    cbReceiptMoment = DateTime.Now,
                    cbChargeItems = new ChargeItem[5],
                    cbPayItems = new PayItem[5],
                    ftReceiptCase = 100
                }, new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                });
                var result = webClient.UploadString(new Uri(_url + "/v0/sign"), json);
                var response = JsonConvert.DeserializeObject<ReceiptResponse>(result, new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                }); 
                response.ftQueueID.Should().Be(queueId);
            }
        }

        [Test]
        public void SignV1_ShouldReturnSameQueueId_For_WebClient()
        {
            var queueId = Guid.NewGuid().ToString();
            using (var webClient = new WebClient())
            {
                webClient.Headers["content-type"] = "application/json";
                var json = JsonConvert.SerializeObject(new ReceiptRequest
                {
                    ftQueueID = queueId,
                    ftCashBoxID = "",
                    cbTerminalID = "",
                    cbReceiptReference = "",
                    cbReceiptMoment = DateTime.Now,
                    cbChargeItems = new ChargeItem[5],
                    cbPayItems = new PayItem[5],
                    ftReceiptCase = 100
                }, new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                });
                var result = webClient.UploadString(new Uri(_url + "/v1/sign"), json);
                var response = JsonConvert.DeserializeObject<ReceiptResponse>(result, new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                });
                response.ftQueueID.Should().Be(queueId);
            }
        }

        [Test]
        public void EchoV0_ShouldReturnSameMessage_For_WebClient()
        {
            var inMessage = "Hello World!";
            using (var webClient = new WebClient())
            {
                webClient.Headers["content-type"] = "application/json";
                var json = JsonConvert.SerializeObject(inMessage);
                var result = webClient.UploadString(new Uri(_url + "/v0/echo"), json);
                var response = JsonConvert.DeserializeObject<string>(result);
                response.Should().Be(inMessage);
            }
        }

        [Test]
        public void Echov1_ShouldReturnSameMessage_For_WebClient()
        {
            var inMessage = "Hello World!";
            using (var webClient = new WebClient())
            {
                webClient.Headers["content-type"] = "application/json";
                var json = JsonConvert.SerializeObject(new EchoRequest
                {
                    Message = inMessage
                });
                var result = webClient.UploadString(new Uri(_url + "/v1/echo"), "POST", json);
                var response = JsonConvert.DeserializeObject<EchoResponse>(result);
                response.Message.Should().Be(inMessage);
            }
        }


        [Test]
        public void JournalV0_ShouldReturnSameMessage_For_WebClient()
        {
            var inMessage = "Hello World!";
            using (var webClient = new WebClient())
            {
                webClient.Headers["content-type"] = "application/json";
                var json = JsonConvert.SerializeObject(inMessage);
                var result = webClient.UploadString(new Uri(_url + "/v0/journal?type=0&from=0&to=0"), json);
                var response = JsonConvert.DeserializeObject<string>(result);
                response.Should().Be(inMessage);
            }
        }

        [Test]
        public void JournalV1_ShouldReturnSameMessage_For_WebClient()
        {
            var inMessage = "Hello World!";
            using (var webClient = new WebClient())
            {
                webClient.Headers["content-type"] = "application/json";
                var json = JsonConvert.SerializeObject(new JournalRequest
                {
                    From = 0x0,
                    To = 0x0,
                    ftJournalType = 0x0
                });
                var result = webClient.UploadString(new Uri(_url + "/v1/journal"), "POST", json);
                var response = JsonConvert.DeserializeObject<EchoResponse>(result);
                response.Message.Should().Be(inMessage);
            }
        }
    }
}

#endif