using fiskaltrust.ifPOS.v1.de;
using System;
using System.Threading.Tasks;

namespace fiskaltrust.ifPOS.Tests.Helpers
{
    //public class DummyDESSCD : IDESSCD
    //{
    //    private Task<T> FromResult<T>(T result) => Task.Factory.StartNew(() => result);

    //    public async Task<TseExportDataResult> ExportDataAsync() => await FromResult(new TseExportDataResult());

    //    public async Task<FinishTransactionResponse> FinishTransactionExportDataAsync(FinishTransactionRequest request) => await FromResult(new FinishTransactionResponse
    //    {
    //        StartTime = DateTime.Now,
    //        EndTime = DateTime.Now
    //    });

    //    public async Task<TseInfo> GetTseInfoAsync() => await FromResult(new TseInfo());

    //    public async Task<StartTransactionResponse> StartTransactionExportDataAsync(StartTransactionRequest request) => await FromResult(new StartTransactionResponse
    //    {
    //        StartTime = DateTime.Now
    //    });

    //    public async Task<UpdateTransactionResponse> UpdateTransactionExportDataAsync(UpdateTransactionRequest request) => await FromResult(new UpdateTransactionResponse
    //    {
    //        StartTime = DateTime.Now,
    //    });

    //    public async Task<TseState> SetTseStateAsync(TseState state) => await FromResult(new TseState());
    //}
}
