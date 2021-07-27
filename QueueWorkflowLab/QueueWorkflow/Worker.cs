using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QueueSocket;

namespace QueueWorkflow
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISocketService _socketService;

        public Worker(
            ILogger<Worker> logger,
            ISocketService socketService)
        {
            _logger = logger;
            _socketService = socketService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _socketService.Start();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _socketService.Dispose();
            return Task.CompletedTask;
        }
    }
}
