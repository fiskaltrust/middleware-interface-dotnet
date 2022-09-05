using fiskaltrust.ifPOS.v1;
using fiskaltrust.Middleware.Interface.Client.Extensions;
using fiskaltrust.Middleware.Interface.Client.Common.Helpers;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Common.RetryLogic
{
    public class RetryPolicyHandler<T> : IRetryPolicyHandler<T> where T : class
    {
        private readonly RetryPolicyOptions _options;
        private readonly IProxyConnectionHandler<T> _proxyConnectionHandler;

        public RetryPolicyHandler(RetryPolicyOptions options, IProxyConnectionHandler<T> proxyConnectionHandler)
        {
            _options = options;
            _proxyConnectionHandler = proxyConnectionHandler;
        }

        public async Task<K> RetryFuncAsync<K>(Func<T, Task<K>> action)
        {
            var trial = 0;

            while (trial < _options.Retries)
            {
                try
                {
                    var tokenSource = new CancellationTokenSource(_options.ClientTimeout);
                    var task = action(await _proxyConnectionHandler.GetProxyAsync());
                    return await Task.Run(async () => await (Helpers.RuntimeHelpers.IsMono ? task.WithCancellation(tokenSource.Token) : task), tokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    if (trial == _options.Retries - 1)
                    {
                        throw new RetryPolicyException("The maximum number of retries was reached while sending this request.");
                    }
                }
                catch (ScuException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    if (trial == _options.Retries - 1)
                    {
                        throw;
                    }
                }

                await _proxyConnectionHandler.ForceReconnectAsync();

                trial++;
                await Task.Delay(_options.DelayBetweenRetries);
            }

            return default;
        }

        public async Task RetryFuncAsync(Func<T, Task> action)
        {
            var trial = 0;

            while (trial < _options.Retries)
            {
                try
                {
                    var tokenSource = new CancellationTokenSource(_options.ClientTimeout);
                    var task = action(await _proxyConnectionHandler.GetProxyAsync());
                    await Task.Run(async () => await (Helpers.RuntimeHelpers.IsMono ? task.WithCancellation(tokenSource.Token) : task).WithCancellation(tokenSource.Token), tokenSource.Token);
                    return;
                }
                catch (TaskCanceledException)
                {
                    if (trial == _options.Retries - 1)
                    {
                        throw new RetryPolicyException("The maximum number of retries was reached while sending this request.");
                    }
                }
                catch (ScuException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    if (trial == _options.Retries - 1)
                    {
                        throw;
                    }
                }

                await _proxyConnectionHandler.ForceReconnectAsync();

                trial++;
                await Task.Delay(_options.DelayBetweenRetries);
            }
        }
    }
}
