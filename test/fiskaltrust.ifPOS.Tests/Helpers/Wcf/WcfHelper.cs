#if WCF
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;

namespace fiskaltrust.ifPOS.Tests.Helpers.Wcf
{
    public class GlobalErrorBehaviorAttribute : Attribute, IEndpointBehavior, IServiceBehavior
    {
        public void Validate(ServiceEndpoint endpoint) { }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }

        public void Validate(ServiceDescription description, ServiceHostBase serviceHostBase) { }

        public void AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters) { }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                channelDispatcher.ErrorHandlers.Add(new GlobalErrorHandler());
            }
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new GlobalErrorHandler());
        }
    }

    public class GlobalErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var newEx = new FaultException();
            MessageFault msgFault = newEx.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, newEx.Action);
        }
    }

    public static class WcfHelper
    {
        public static T GetProxy<T>(string url)
        {
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            binding.SendTimeout = TimeSpan.FromSeconds(15);
            binding.ReceiveTimeout = TimeSpan.FromDays(14);
            var factory = new ChannelFactory<T>(binding, new EndpointAddress(url));
            return factory.CreateChannel();
        }

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

            host.Description.Behaviors.Add(new GlobalErrorBehaviorAttribute());

            host.Open();
            return host;
        }

        public static ServiceHost StartRestHost<T>(string url, T component)
        {
            var restHost = new WebServiceHost(component, new Uri(url));
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
            restHost.Description.Behaviors.Add(new GlobalErrorBehaviorAttribute());
            sep.Behaviors.Add(new GlobalErrorBehaviorAttribute());
            restHost.Open();
            return restHost;
        }

        public static T GetRestProxy<T>(string url) where T : class
        {
            var binding = new BasicHttpBinding();
            binding.SendTimeout = TimeSpan.FromSeconds(15);
            binding.ReceiveTimeout = TimeSpan.FromDays(14);

            var factory = new WebChannelFactory<T>(new Uri(url));
            return factory.CreateChannel();
        }
    }
}
#endif