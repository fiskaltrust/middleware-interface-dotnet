#if SYSTEM_TEXT_JSON
using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.pl;
using System;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Tests.Helpers
{
    public class DummyPLSSCD : ifPOS.v2.pl.IPLSSCD
    {
        public Task<EchoResponse> EchoAsync(EchoRequest echoRequest)
            => Task.FromResult(new EchoResponse
            {
                Message = echoRequest.Message
            });

        public Task<PLSSCDInfo> GetInfoAsync()
            => Task.FromResult(new PLSSCDInfo
            {
                InfoData = "{\"serialNumber\":\"ABC1234567\",\"uniqueDeviceNumber\":\"ZAS1234567890\",\"crkReachable\":true}"
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
