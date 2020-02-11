using fiskaltrust.ifPOS.v1.de;
using System.Threading.Tasks;

namespace fiskaltrust.ifPOS.Tests.Helpers
{
#if WCF
    [System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
#endif
    public class DummyDESSCD : IDESSCD
    {
        private Task<T> FromResult<T>(T result) => Task.Factory.StartNew(() => result);

        public Task<TseExportDataResult> ExportDataAsync() => FromResult(new TseExportDataResult());

        public Task<FinishTransactionResponse> FinishTransactionExportDataAsync(FinishTransactionRequest request) => FromResult(new FinishTransactionResponse());

        public Task<TseInfo> GetTseInfoAsync() => FromResult(new TseInfo());

        public Task<StartTransactionResponse> StartTransactionExportDataAsync(StartTransactionRequest request) => FromResult(new StartTransactionResponse());

        public Task<UpdateTransactionResponse> UpdateTransactionExportDataAsync(UpdateTransactionRequest request) => FromResult(new UpdateTransactionResponse());

        public Task<TseState> SetTseStateAsync(TseState state) => FromResult(new TseState());
    }
}
