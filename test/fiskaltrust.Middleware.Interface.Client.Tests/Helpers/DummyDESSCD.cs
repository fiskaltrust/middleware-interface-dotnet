using fiskaltrust.ifPOS.v1.de;
using System;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Tests.Helpers
{
#if WCF
    [System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
#endif
    public class DummyDESSCD : ifPOS.v1.de.IDESSCD
    {
#if WCF
        [System.ServiceModel.Web.WebInvoke(BodyStyle = System.ServiceModel.Web.WebMessageBodyStyle.Bare, UriTemplate = "v1/exportdata", Method = "GET")]
#endif
        public async Task<TseExportDataResult> ExportDataAsync() => await Task.FromResult(new TseExportDataResult());

#if WCF
        [System.ServiceModel.Web.WebInvoke(BodyStyle = System.ServiceModel.Web.WebMessageBodyStyle.Bare, UriTemplate = "v1/finishtransactionexportdata")]
#endif
        public async Task<FinishTransactionResponse> FinishTransactionExportDataAsync(FinishTransactionRequest request) => await Task.FromResult(new FinishTransactionResponse { StartTime = DateTime.Now, EndTime = DateTime.Now });

#if WCF
        [System.ServiceModel.Web.WebInvoke(BodyStyle = System.ServiceModel.Web.WebMessageBodyStyle.Bare, UriTemplate = "v1/starttransactionexportdata", Method = "POST")]
#endif
        public async Task<StartTransactionResponse> StartTransactionExportDataAsync(StartTransactionRequest request) => await Task.FromResult(new StartTransactionResponse { StartTime = DateTime.Now });

#if WCF
        [System.ServiceModel.Web.WebInvoke(BodyStyle = System.ServiceModel.Web.WebMessageBodyStyle.Bare, UriTemplate = "v1/updatetransactionexportdata")]
#endif
        public async Task<UpdateTransactionResponse> UpdateTransactionExportDataAsync(UpdateTransactionRequest request) => await Task.FromResult(new UpdateTransactionResponse { StartTime = DateTime.Now });

#if WCF
        [System.ServiceModel.Web.WebInvoke(BodyStyle = System.ServiceModel.Web.WebMessageBodyStyle.Bare, UriTemplate = "v1/tseinfo", Method = "GET")]
#endif
        public async Task<TseInfo> GetTseInfoAsync() => await Task.FromResult(new TseInfo());

#if WCF
        [System.ServiceModel.Web.WebInvoke(BodyStyle = System.ServiceModel.Web.WebMessageBodyStyle.Bare, UriTemplate = "v1/tsestate", Method = "POST")]
#endif
        public async Task<TseState> SetTseStateAsync(TseState state) => await Task.FromResult(new TseState());
    }
}
