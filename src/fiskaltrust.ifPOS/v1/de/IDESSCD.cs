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
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/updatetransaction", Method = "POST")]
#endif
        Task<UpdateTransactionResponse> UpdateTransactionAsync(UpdateTransactionRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/finishtransaction", Method = "POST")]
#endif
        Task<FinishTransactionResponse> FinishTransactionAsync(FinishTransactionRequest request);


        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/tseinfo", Method = "GET")]
#endif
        Task<TseInfo> GetTseInfoAsync();


        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/tsestate", Method = "POST")]
#endif
        Task<TseState> SetTseStateAsync(TseState state);


        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/registerclientid", Method = "POST")]
#endif
        Task<ClientIdResponse> RegisterClientId(ClientIdRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/unregisterclientid", Method = "POST")]
#endif
        Task<ClientIdResponse> UnRegisterClientId(ClientIdRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/executesettsetime", Method = "GET")]
#endif
        Task ExecuteSetTseTimeAsync();

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/executeselftest", Method = "GET")]
#endif
        Task ExecuteSelfTestAsync();


        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/startexportsession", Method = "POST")]
#endif
        Task<StartExportSessionResponse> StartExportSessionAsync();

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/startexportsessionbytimestamp", Method = "POST")]
#endif
        Task<StartExportSessionResponse> StartExportSessionByTimeStampAsync(StartExportSessionByTimeStampRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/startexportsessionbytransaction", Method = "POST")]
#endif
        Task<StartExportSessionResponse> StartExportSessionByTransactionAsync(StartExportSessionByTransactionRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/export", Method = "POST")]
#endif
        Task<ExportDataResponse> ExportAsync(StartExportSessionByTransactionRequest request);

        [OperationContract]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/endexportsession", Method = "POST")]
#endif
        Task<EndExportSessionResponse> EndExportSessionAsync(EndExportSessionRequest request);


    }
}