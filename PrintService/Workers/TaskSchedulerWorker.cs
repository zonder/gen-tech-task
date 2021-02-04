using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PrintService.Domain.Dtos;
using PrintService.Infrastructure.Redis;

namespace PrintService.Api.Workers
{
    public class TaskSchedulerWorker : BackgroundService
    {
        private readonly IRedisSequence<PrintAtTaskDto> _scheduledTasks;
        private readonly IRedisQueue<PrintAtTaskDto> _tasksToPrint;
        private readonly ILockRegistry _lockRegistry;


        public TaskSchedulerWorker(IRedisSequence<PrintAtTaskDto> scheduledTasks, IRedisQueue<PrintAtTaskDto> tasksToPrint, ILockRegistry lockRegistry)
        {
            _scheduledTasks = scheduledTasks;
            _tasksToPrint = tasksToPrint;
            _lockRegistry = lockRegistry;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var task = await _scheduledTasks.GetNext();
                if (task == null
                    // TODO: change to UTC
                    || DateTime.Now < task.PrintAt)
                {
                    // Sleep for 100 milliseconds when no items in sequence or items are not ready to be printed.
                    Thread.Sleep(100);
                    continue;
                }

                var key = task.Id.ToString();

                // Need to lock items to process message only once when instance is scaled.
                if (await _lockRegistry.AcquireLock(key))
                {
                    await _scheduledTasks.Remove(task);
                    await _tasksToPrint.Push(task);

                    //TODO: in case of failure we need to cleanup outdated locks
                    await _lockRegistry.ReleaseLock(key);
                }

                
            }
        }
    }
}
