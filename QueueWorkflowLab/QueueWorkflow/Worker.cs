using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using QueueSocket;

namespace QueueWorkflow
{
    public class Worker : BackgroundService
    {
        private readonly ISocketService _socketService;

        public Worker(
            ISocketService socketService)
        {
            _socketService = socketService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _socketService.Start();
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _socketService.Stop();
        }
    }
}
