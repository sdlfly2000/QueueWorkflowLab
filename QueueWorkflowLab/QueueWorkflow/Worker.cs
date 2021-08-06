using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueueSocket;

namespace QueueWorkflow
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _services;
        private ISocketService _socketService;

        public Worker(
            IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _services.CreateScope())
            {
                _socketService = scope.ServiceProvider.GetRequiredService<ISocketService>();
                _socketService.Start();
            }
                
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _socketService.Stop();
        }
    }
}
