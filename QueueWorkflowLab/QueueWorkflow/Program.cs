using System.Collections.Generic;
using Common.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QueueWorkflow
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddMemoryCache();
                    DIModule.RegisterDomain(services, new List<string>
                    {
                        "QueueSocket",
                        "QueueWorkflow",
                        "Workflow.Services",
                        "TCPServer"
                    });
                });
    }
}
