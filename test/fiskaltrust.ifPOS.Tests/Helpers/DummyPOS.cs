using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fiskaltrust.ifPOS.Tests.Helpers
{
#if WCF
    [System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
#endif
    public class DummyPOS : ifPOS.v0.IPOS
    {
        private delegate string Echo_Delegate(string message);
        private delegate ifPOS.v0.ReceiptResponse Sign_Delegate(ifPOS.v0.ReceiptRequest request);
        private delegate Stream Journal_Delegate(long ftJournalType, long from, long to);

        public IAsyncResult BeginEcho(string message, AsyncCallback callback, object state)
        {
            var d = new Echo_Delegate(Echo);
            var r = d.BeginInvoke(message, callback, d);
            return r;
        }

        public string EndEcho(IAsyncResult result)
        {
            var d = (Echo_Delegate)result.AsyncState;
            return d.EndInvoke(result);
        }

        public string Echo(string message) => message;

        public IAsyncResult BeginSign(ifPOS.v0.ReceiptRequest request, AsyncCallback callback, object state)
        {
            var d = new Sign_Delegate(Sign);
            var r = d.BeginInvoke(request, callback, d);
            return r;
        }

        public ifPOS.v0.ReceiptResponse EndSign(IAsyncResult result)
        {
            var d = (Sign_Delegate)result.AsyncState;
            return d.EndInvoke(result);
        }

        public ifPOS.v0.ReceiptResponse Sign(ifPOS.v0.ReceiptRequest request)
        {
            var response = new ifPOS.v0.ReceiptResponse
            {
                ftCashBoxID = request.ftCashBoxID,
                cbTerminalID = request.cbTerminalID,
                cbReceiptReference = request.cbReceiptReference,
                ftQueueID = request.ftQueueID,
                ftReceiptMoment = DateTime.UtcNow
            };

            return response;
        }

        public Stream Journal(long ftJournalType, long from, long to)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);
            bw.Write(ftJournalType);
            bw.Write(from);
            bw.Write(to);
            bw.Flush();
            ms.Position = 0;
            return ms;
        }

        public IAsyncResult BeginJournal(long ftJournalType, long from, long to, AsyncCallback callback, object state)
        {
            var d = new Journal_Delegate(Journal);
            var r = d.BeginInvoke(ftJournalType, from, to, callback, d);
            return r;
        }

        public Stream EndJournal(IAsyncResult result)
        {
            var d = (Journal_Delegate)result.AsyncState;
            return d.EndInvoke(result);
        }
    }
}
