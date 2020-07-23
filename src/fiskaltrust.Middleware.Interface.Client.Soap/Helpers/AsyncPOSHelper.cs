using fiskaltrust.ifPOS.v1;
using fiskaltrust.Middleware.Interface.Client.Extensions;
using fiskaltrust.Middleware.Interface.Client.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace fiskaltrust.Middleware.Interface.Client.Soap.Helpers
{
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

        public Stream Journal(long ftJournalType, long from, long to) => _innerPOS.Journal(ftJournalType, from, to);

        public Task<ifPOS.v1.ReceiptResponse> SignAsync(ifPOS.v1.ReceiptRequest request) => Task.Run(() => {
            var requestV0 = ModelConverter<ifPOS.v1.ReceiptRequest, ifPOS.v0.ReceiptRequest>.Convert(request);
            var responseV0 = _innerPOS.Sign(requestV0);
            return ModelConverter<ifPOS.v0.ReceiptResponse, ifPOS.v1.ReceiptResponse>.Convert(responseV0);
        });

        public IAsyncEnumerable<JournalResponse> JournalAsync(JournalRequest request)
        {
            var stream = _innerPOS.Journal(request.ftJournalType, request.From, request.To);
            return stream.ToAsyncEnumerable();
        }

        public Task<EchoResponse> EchoAsync(EchoRequest message) => Task.Run(() => _innerPOS.EchoAsync(message));
    }
}
