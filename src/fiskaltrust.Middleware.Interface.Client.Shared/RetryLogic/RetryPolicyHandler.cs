using fiskaltrust.Middleware.Interface.Client.Shared.Options;
using fiskaltrust.Middleware.Interface.Client.Shared.RetryLogic.Interfaces;
using System;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Shared
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
                    var timeoutTimer = new System.Timers.Timer(_options.ClientTimeout.TotalMilliseconds);
                    timeoutTimer.Elapsed += (source, e) => throw new RetryTimeoutException();
                    timeoutTimer.Start();
                    var result = await action(await _proxyConnectionHandler.GetProxyAsync());
                    timeoutTimer.Stop();
                    return result;
                }
                catch (RetryTimeoutException ex)
                {
                    if (trial == _options.Retries - 1)
                    {
                        throw new RetryPolicyException("The host is not reachable!", ex);
                    }
                }
                catch (Exception)
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
    }
}
