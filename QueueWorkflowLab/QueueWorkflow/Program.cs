using System.Collections.Generic;
using Common.Core.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Workflow.Sql.database;

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
                    var config = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .Build();

                    services.AddDbContext<WorkflowDbContext>(
                        options => options.UseMySql(
                            config.GetSection("ConnectionStrings")["WorkflowDatabase"]));

                    services.AddHostedService<Worker>();
                    services.AddMemoryCache();
                    DIModule.RegisterDomain(services, new List<string>
                    {
                        "QueueSocket",
                        "QueueWorkflow",
                        "Workflow.Services",
                        "TCPServer"
                    });
                })
                .UseSystemd();
    }
}
