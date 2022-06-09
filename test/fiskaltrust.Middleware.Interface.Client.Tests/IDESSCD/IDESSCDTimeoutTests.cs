using System;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using fiskaltrust.ifPOS.v1.de;
using System.Net;
using System.Linq;
using System.Net.NetworkInformation;

namespace fiskaltrust.Middleware.Interface.Client.Tests.IDESSCD
{
    public class IDESSCDTimeoutTests
    {
        protected RetryPolicyOptions _retryPolicyOptions = new RetryPolicyOptions
        {
            Retries = 0,
            ClientTimeout = TimeSpan.FromMilliseconds(10)
        };

        [Test]
        public async Task HttpShouldTimeout()
        {
            using HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5502/");

            var client = await Client.Http.HttpDESSCDFactory.CreateSSCDAsync(new Client.Grpc.GrpcClientOptions
            {
                RetryPolicyOptions = _retryPolicyOptions,
                Url = new Uri("http://localhost:5502")
            });

            listener.Start();

            try
            {
                await client.EchoAsync(new ScuDeEchoRequest { Message = "test" });
            } catch(RetryPolicyException) { }
        }

        [Test]
        public async Task GrpcShouldTimeout()
        {
            using HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5501/");

            var client = await Client.Grpc.GrpcDESSCDFactory.CreateSSCDAsync(new Client.Grpc.GrpcClientOptions
            {
                RetryPolicyOptions = _retryPolicyOptions,
                Url = new Uri("grpc://localhost:5501")
            });

            listener.Start();

            try
            {
                await client.EchoAsync(new ScuDeEchoRequest { Message = "test" });
            }
            catch (RetryPolicyException) { }
        }
    }
}