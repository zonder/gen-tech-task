using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PrintService.Domain.Dtos;
using PrintService.Infrastructure.Redis;

namespace PrintService.Api.Workers
{
    public class PrintTaskWorker : BackgroundService
    {
        private readonly IRedisQueue<PrintAtTaskDto> _tasksToPrint;
        public PrintTaskWorker(IRedisQueue<PrintAtTaskDto> tasksToPrint)
        {
            _tasksToPrint = tasksToPrint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var task = await _tasksToPrint.Pop();
                if (task != null)
                {
                    Console.WriteLine(task.Message);
                    continue;
                }

                // Sleep for 100 milliseconds when no items in queue
                Thread.Sleep(100);
            }
        }
    }
}
