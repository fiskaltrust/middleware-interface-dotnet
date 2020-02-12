using System.ServiceModel;
using System.Threading.Tasks;
#if STREAMING
using System.Collections.Generic;
#endif
#if WCF
using System.ServiceModel.Web;
#endif

namespace fiskaltrust.ifPOS.v1
{
    [ServiceContract]
    public interface IPOS : v0.IPOS
    {
        [OperationContract(Name = "v1/Sign")]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "v1/sign")]
#endif
        Task<ReceiptResponse> SignAsync(ReceiptRequest request);

#if STREAMING
        [OperationContract(Name = "v1/Journal")]
        IAsyncEnumerable<JournalResponse> JournalAsync(JournalRequest request);
#endif

        [OperationContract(Name = "v1/Echo")]
#if WCF
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,  UriTemplate = "v1/echo")]
#endif
        Task<EchoResponse> EchoAsync(EchoRequest message);
    }
}