using Microsoft.Extensions.Hosting;
using SmallAnalytics.Core.DataStorage;
using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmallAnalytics.Core
{
    public class BackgroundDataSaveService : IHostedService
    {
        /// <summary>
        /// Time span before each save of queue. Default value is 1 minute.
        /// </summary>
        public TimeSpan TimeBeforeSaves { get; set; } = TimeSpan.FromMinutes(1);
        private Timer? _timer = null;

        private readonly IRepository _repository;
        private readonly IDataQueue _dataQueue;
        
        public BackgroundDataSaveService(
            IRepository repository,
            IDataQueue dataQueue)
        {
            this._repository = repository;
            this._dataQueue = dataQueue;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(
                callback: async (e) => await SaveAndEmtpyQueueAsync(cancellationToken),
                state: null,
                dueTime: TimeSpan.Zero,
                period: this.TimeBeforeSaves);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await SaveAndEmtpyQueueAsync(cancellationToken);
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
        }

        private async Task SaveAndEmtpyQueueAsync(CancellationToken cancellationToken)
        {
            IEnumerable<AnalyticsDataDTO> queue = this._dataQueue.DeQueueAll();
            await _repository.AddManyAndSaveAsync(queue, cancellationToken);
        }
    }
}
