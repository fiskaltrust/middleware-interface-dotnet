using System;
using System.Threading.Tasks;
using System.Timers;

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
                    var timeoutTimer = new Timer(_options.ClientTimeout.TotalMilliseconds);
                    timeoutTimer.Elapsed += (source, e) => throw new TimeoutException();
                    timeoutTimer.Start();
                    var result = await action(await _proxyConnectionHandler.GetProxyAsync());
                    timeoutTimer.Stop();
                    return result;
                }
                catch (Exception ex)
                {
                    if (trial == _options.Retries - 1)
                    {
                        throw new RetryPolicyException("The host is not reachable!", ex);
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
                    var timeoutTimer = new Timer(_options.ClientTimeout.TotalMilliseconds);
                    timeoutTimer.Elapsed += (source, e) => throw new TimeoutException();
                    timeoutTimer.Start();
                    await action(await _proxyConnectionHandler.GetProxyAsync());
                    timeoutTimer.Stop();
                    return;
                }
                catch (Exception ex)
                {
                    if (trial == _options.Retries - 1)
                    {
                        throw new RetryPolicyException("The host is not reachable!", ex);
                    }
                }

                await _proxyConnectionHandler.ForceReconnectAsync();

                trial++;
                await Task.Delay(_options.DelayBetweenRetries);
            }
        }
    }
}
