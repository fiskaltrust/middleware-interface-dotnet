using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterInvoiceResponse
    {
        // ikof md5 + full ikof signature (calculated here, because the cert is only in the scu), see chapter 5.1 in tech specs
    }
}
