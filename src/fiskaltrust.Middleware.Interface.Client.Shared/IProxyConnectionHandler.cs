using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Shared
{
    public interface IProxyConnectionHandler<T> where T : class
    {
        Task ReconnectAsync();

        Task ForceReconnectAsync();

        Task<T> GetProxyAsync();
    }
}
