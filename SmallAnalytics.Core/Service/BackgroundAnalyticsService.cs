using Microsoft.Extensions.Hosting;
using SmallAnalytics.Core.DataStorage;
using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace SmallAnalytics.Core.Service
{
    public class BackgroundAnalyticsService : IBackgroundAnalyticsService, IHostedService, IDisposable
    {
        private static ConcurrentQueue<AnalyticsDataDTO> Queue = new ConcurrentQueue<AnalyticsDataDTO>();
        private Timer? _timer;

        private readonly IRepository _repository;
        public BackgroundAnalyticsService(IRepository repository)
        {
            _repository = repository;
        }

        public void StoreData(DateTimeOffset date, string content)
        {
            Queue.Enqueue(new AnalyticsDataDTO(date, content));
        }

        public static void ClearQueue()
        {
            while (BackgroundAnalyticsService.Queue.TryDequeue(out AnalyticsDataDTO item))
            {
                // do nothing
            }
        }

        public static IReadOnlyList<AnalyticsDataDTO> ReadQueue()
        {
            return BackgroundAnalyticsService.Queue.ToImmutableList();
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromMinutes(1));
            await SaveQueueToDBAsync();
        }

        private async Task SaveQueueToDBAsync()
        {
            while (BackgroundAnalyticsService.Queue.TryDequeue(out AnalyticsDataDTO item))
            {
                await _repository.AddAsync(item);
            }
            await _repository.SaveChangeAsync();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _repository.Dispose();
        }
    }
}
