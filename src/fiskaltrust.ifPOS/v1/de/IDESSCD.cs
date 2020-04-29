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
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/starttransaction", Method = "POST")]
#endif
        Task<StartTransactionResponse> StartTransactionAsync(StartTransactionRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/updatetransaction")]
#endif
        Task<UpdateTransactionResponse> UpdateTransactionAsync(UpdateTransactionRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/finishtransaction")]
#endif
        Task<FinishTransactionResponse> FinishTransactionAsync(FinishTransactionRequest request);

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