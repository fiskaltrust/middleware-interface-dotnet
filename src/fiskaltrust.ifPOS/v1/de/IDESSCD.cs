using System.ServiceModel;
using System.Threading.Tasks;
#if WCF
using System.ServiceModel.Web;
#endif

namespace fiskaltrust.ifPOS.v1.de
{
    [ServiceContract]
    public interface IDESSCD
    {
        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/starttransactionexportdata", Method = "POST")]
#endif
        Task<StartTransactionResponse> StartTransactionExportDataAsync(StartTransactionRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/updatetransactionexportdata")]
#endif
        Task<UpdateTransactionResponse> UpdateTransactionExportDataAsync(UpdateTransactionRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/finishtransactionexportdata")]
#endif
        Task<FinishTransactionResponse> FinishTransactionExportDataAsync(FinishTransactionRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/tseinfo", Method = "GET")]
#endif
        Task<TseInfo> GetTseInfoAsync();

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/exportdata", Method = "GET")]
#endif
        Task<TseExportDataResult> ExportDataAsync();

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/tsestate", Method = "POST")]
#endif
        Task<TseState> SetTseStateAsync(TseState state);
    }
}