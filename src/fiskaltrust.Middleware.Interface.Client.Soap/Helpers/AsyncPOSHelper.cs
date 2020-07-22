using fiskaltrust.ifPOS.v1;
using fiskaltrust.Middleware.Interface.Client.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace fiskaltrust.Middleware.Interface.Client.Soap.Helpers
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ReceiptRequestUnion
    {
        [FieldOffset(0)]
        public fiskaltrust.ifPOS.v0.ReceiptRequest v0;
        [FieldOffset(0)]
        public fiskaltrust.ifPOS.v1.ReceiptRequest v1;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ReceiptResponseUnion
    {
        [FieldOffset(0)]
        public fiskaltrust.ifPOS.v0.ReceiptResponse v0;
        [FieldOffset(0)]
        public fiskaltrust.ifPOS.v1.ReceiptResponse v1;
    }


    public class AsyncPOSHelper : IPOS
    {
        private readonly IPOS _innerPOS;
        public AsyncPOSHelper(IPOS innerPOS)
        {
            _innerPOS = innerPOS;
        }

        public IAsyncResult BeginEcho(string message, AsyncCallback callback, object state) => _innerPOS.BeginEcho(message, callback, state);

        public IAsyncResult BeginJournal(long ftJournalType, long from, long to, AsyncCallback callback, object state) => _innerPOS.BeginJournal(ftJournalType, from, to, callback, state);

        public IAsyncResult BeginSign(ifPOS.v0.ReceiptRequest data, AsyncCallback callback, object state) => _innerPOS.BeginSign(data, callback, state);

        public string Echo(string message) => _innerPOS.Echo(message);

        public string EndEcho(IAsyncResult result) => _innerPOS.EndEcho(result);

        public Stream EndJournal(IAsyncResult result) => _innerPOS.EndJournal(result);

        public ifPOS.v0.ReceiptResponse EndSign(IAsyncResult result) => _innerPOS.EndSign(result);

        public ifPOS.v0.ReceiptResponse Sign(ifPOS.v0.ReceiptRequest data) => _innerPOS.Sign(data);

        public Task<ifPOS.v1.ReceiptResponse> SignAsync(ifPOS.v1.ReceiptRequest request) => Task.Run(() => {
            var req = new ReceiptRequestUnion
            {
                v1 = request
            };

            var res = new ReceiptResponseUnion
            {
                v1 = _innerPOS.Sign(req.v0)
            };

            return res.v0;
        });

        public IAsyncEnumerable<JournalResponse> JournalAsync(JournalRequest request)
        {
            var stream = _innerPOS.Journal(request.ftJournalType, request.From, request.To);
            return stream.ToAsyncEnumerable();
        }

        public Task<EchoResponse> EchoAsync(EchoRequest message) => Task.Run(() => _innerPOS.EchoAsync(message));
    }
}
