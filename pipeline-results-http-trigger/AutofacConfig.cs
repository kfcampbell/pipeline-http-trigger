using Autofac;
using AzureFunctions.Autofac.Configuration;

namespace pipelineresultshttptrigger
{
    public class AutofacConfig
    {
        public AutofacConfig()
        {
            DependencyInjection.Initialize(builder =>
            {
                // just the sake of demonstration, I'm injecting a simple service
                builder.RegisterType<ServiceOne>().As<IServiceOne>();
            }, "pipeline_results_http_trigger");
        }
    }

    public class ServiceOne : IServiceOne
    {
        public string Execute()
        {
            return "Check the oven. Your build's done!";
        }
    }

    public interface IServiceOne { string Execute(); }
}
