﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.ServiceModel;
#if WCF
using System.ServiceModel.Web;
#endif

namespace fiskaltrust.Middleware.Interface.Client.Tests.Helpers
{
    [ServiceContract]
    public interface IDummyPOS : ifPOS.v0.IPOS
    {
        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "json/echo")]
#endif
        public string EchoVersionless(string message);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "json/sign")]
#endif
        public ifPOS.v0.ReceiptResponse SignVersionless(ifPOS.v0.ReceiptRequest request);


        [OperationContract]
#if WCF
        [WebInvoke(UriTemplate = "json/journal?type={ftJournalType}&from={from}&to={to}")]
#endif
        public Stream JournalVersionless(long ftJournalType, long from, long to);
    }

    public class DummyPOS : IDummyPOS
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

        public string EchoVersionless(string message) => Echo(message);

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

        public ifPOS.v0.ReceiptResponse SignVersionless(ifPOS.v0.ReceiptRequest request) => Sign(request);

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

        public Stream JournalVersionless(long ftJournalType, long from, long to) => Journal(ftJournalType, from, to);

        public Stream Journal(long ftJournalType, long from, long to)
        {
            var json = JsonConvert.SerializeObject(new
            {
                ftJournalType,
                from,
                to
            });
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
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
