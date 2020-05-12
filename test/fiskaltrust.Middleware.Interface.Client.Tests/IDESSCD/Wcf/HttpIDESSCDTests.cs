#if WCF

using fiskaltrust.ifPOS.v1.de;
using fiskaltrust.Middleware.Interface.Client.Http;
using fiskaltrust.Middleware.Interface.Client.Tests.Helpers;
using fiskaltrust.Middleware.Interface.Client.Tests.Helpers.Grpc;
using System;
using System.ServiceModel;

namespace fiskaltrust.Middleware.Interface.Client.Tests.IDESSCD.Wcf
{
    // If these tests are failing you have to execute the following command as an Administrator
    // netsh http add urlacl url=http://+:12080/ user=Everyone listen=yes
    // To add the url that is used for binding
    public class HttpIDESSCDTests : IDESSCDTests
    {
        private string _url;
        private ServiceHost _serviceHost;

        public HttpIDESSCDTests()
        {
            _url = $"http://localhost:12080/pos/{Guid.NewGuid()}";
        }

        ~HttpIDESSCDTests()
        {
            if (_serviceHost != null)
                StopHost();
        }

        protected override ifPOS.v1.de.IDESSCD CreateClient() => HttpDeSscdFactory.CreateSscdAsync(new HttpPosClientOptions { Url = new Uri(_url), RetryPolicyOptions = _retryPolicyOptions }).Result;

        protected override void StartHost()
        {
            if (_serviceHost == null)
                _serviceHost = WcfHelper.StartRestHost<ifPOS.v1.de.IDESSCD>(_url, new DummyDESSCD());
        }

        protected override void StopHost()
        {
            _serviceHost.Close();
            _serviceHost = null;
        }
    }
}

#endif