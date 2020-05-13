#if WCF
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace fiskaltrust.Middleware.Interface.Client.Tests.Helpers.Grpc
{
    public static class WcfHelper
    {
        public static ServiceHost StartHost<T>(string url, T component)
        {
            var host = new ServiceHost(component, new Uri(url));
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            host.AddServiceEndpoint(typeof(T), binding, url);
            var serviceBehaviour = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            if (serviceBehaviour != null)
            {
                serviceBehaviour.InstanceContextMode = InstanceContextMode.Single;
            }
            host.Open();
            return host;
        }

        public static ServiceHost StartRestHost<T>(string url, T component)
        {
            var restHost = new WebServiceHost(component, new Uri(url));

            var debugBehavior = restHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            debugBehavior.IncludeExceptionDetailInFaults = true;

            var sep = restHost.AddServiceEndpoint(typeof(T), new WebHttpBinding(), "");
            var whb = sep.Behaviors.Find<WebHttpBehavior>();
            if (whb == null)
            {
                whb = new WebHttpBehavior();
                sep.Behaviors.Add(whb);
            }

            whb.AutomaticFormatSelectionEnabled = true;
            whb.DefaultOutgoingRequestFormat = WebMessageFormat.Json;
            whb.DefaultOutgoingResponseFormat = WebMessageFormat.Json;
            var serviceBehaviour = restHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            if (serviceBehaviour != null)
            {
                serviceBehaviour.InstanceContextMode = InstanceContextMode.Single;
            }
            restHost.Open();
            return restHost;
        }
    }
}
#endif