#if SYSTEM_TEXT_JSON
using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.es;
using System;
using System.Net.Cache;
using System.Numerics;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Tests.Helpers
{
    public class DummyESSSCD : ifPOS.v2.es.IESSSCD
    {
        public Task<EchoResponse> EchoAsync(EchoRequest echoRequest)
            => Task.FromResult(new EchoResponse
            {
                Message = echoRequest.Message
            });

        public Task<ESSSCDInfo> GetInfoAsync()
            => Task.FromResult(new ESSSCDInfo
            {
            });

        public Task<ProcessResponse> ProcessReceiptAsync(ProcessRequest request)
            => Task.FromResult(new ProcessResponse
            {
                ReceiptResponse = new ReceiptResponse
                {
                    ftCashBoxID = Guid.NewGuid()
                }
            });
    }
}
#endif