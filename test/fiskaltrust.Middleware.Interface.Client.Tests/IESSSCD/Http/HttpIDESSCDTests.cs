#if !WCF
using fiskaltrust.Middleware.Interface.Client.Tests.Helpers;
using System.ServiceModel;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using fiskaltrust.ifPOS.v2.es;
using System.Text;
using fiskaltrust.ifPOS.v2;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.IO;
using fiskaltrust.Middleware.Interface.Client.Http;
using FluentAssertions;

namespace fiskaltrust.Middleware.Interface.Client.Tests.IESSSCD
{
    public class HttpIESSSCDTests : IESSSCDTests
    {
        private string _url;
        private IWebHost _webHost;

        public HttpIESSSCDTests()
        {
            _url = $"http://localhost:8080/scu/{Guid.NewGuid()}";
        }

        ~HttpIESSSCDTests()
        {
            _webHost?.Dispose();
        }

        protected override ifPOS.v2.es.IESSSCD CreateClient() => HttpESSSCDFactory.CreateSSCDAsync(new HttpESSSCDClientOptions {
            Url = new Uri(_url)
        }).Result;


        protected override void StartHost()
        {
            var uri = new Uri(_url);
            var baseUrl = $"{uri.Scheme}://{uri.Host}:{uri.Port}";
            var pathBase = uri.AbsolutePath.TrimEnd('/');
            
            _webHost = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(baseUrl)
                .ConfigureServices(services =>
                {
                    services.AddRouting();
                    services.AddSingleton<ifPOS.v2.es.IESSSCD, DummyESSSCD>();
                })
                .Configure(app =>
                {
                    app.UsePathBase(pathBase);
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapPost("/v2/processreceipt", async context =>
                        {
                            var scd = context.RequestServices.GetService<ifPOS.v2.es.IESSSCD>();
                            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                            var request = JsonSerializer.Deserialize<ProcessRequest>(requestBody);
                            var response = await scd.ProcessReceiptAsync(request);
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        });

                        endpoints.MapPost("/v2/echo", async context =>
                        {
                            var scd = context.RequestServices.GetService<ifPOS.v2.es.IESSSCD>();
                            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                            var request = JsonSerializer.Deserialize<EchoRequest>(requestBody);
                            var response = await scd.EchoAsync(request);
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        });

                        endpoints.MapGet("/v2/getinfo", async context =>
                        {
                            var scd = context.RequestServices.GetService<ifPOS.v2.es.IESSSCD>();
                            var response = await scd.GetInfoAsync();
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        });
                    });
                })
                .Build();

            _webHost.Start();
        }

        protected override void StopHost()
        {
            _webHost?.Dispose();
            _webHost = null;
        }

        [Test]
        public async Task ProcessReceipt_ShouldReturn()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var requestData = new ProcessRequest();
                    var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                    var result = await httpClient.PostAsync(new Uri(_url + "/v2/processreceipt"), content);
                    result.EnsureSuccessStatusCode();
                    var response = JsonSerializer.Deserialize<ProcessResponse>(await result.Content.ReadAsStringAsync());
                    response.ReceiptResponse.Should().NotBeNull();
                    response.ReceiptResponse.ftCashBoxID.ToString().Should().NotBeNullOrEmpty();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Test]
        public async Task Echo_ShouldReturn()
        {
            using (var httpClient = new HttpClient())
            {
                var requestData = new EchoRequest();
                var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(new Uri(_url + "/v2/echo"), content);
                result.EnsureSuccessStatusCode();
            }
        }

        [Test]
        public async Task GetInfo_ShouldReturn()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync(new Uri(_url + "/v2/getinfo"));
                result.EnsureSuccessStatusCode();
            }
        }
    }
}
#endif