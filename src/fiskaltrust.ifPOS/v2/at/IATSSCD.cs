using System.ServiceModel;
using System.Threading.Tasks;

namespace fiskaltrust.ifPOS.v2.at
{
    /// <summary>
    /// This interface is applicable only for the Austrian market and enables direct communication with the signature creation device for own purposes: it can be used for testing if the service is running (“Echo” call), for getting the certificate (“Certificate” call), or signing autono-mously (“Sign” call).
    /// </summary>
    [ServiceContract]
    public interface IATSSCD : v0.IATSSCD
    {
        /// <summary>
        /// Get the certificate of the signature creation device
        /// </summary>
        [OperationContract(Name = "v2/Certificate")]
        Task<CertificateResponse> CertificateAsync();

        /// <summary>
        /// Get the short name of the certificate service provider for RKSV
        /// </summary>
        [OperationContract(Name = "v2/ZDA")]
        Task<ZdaResponse> ZdaAsync();

        /// <summary>
        /// Sign data with the signature creation device
        /// </summary>
        [OperationContract(Name = "v2/Sign")]
        Task<SignResponse> SignAsync(SignRequest signRequest);

        /// <summary>
        /// Function to test communication
        /// </summary>
        [OperationContract]
        Task<EchoResponse> EchoAsync(EchoRequest echoRequest);
    }
}