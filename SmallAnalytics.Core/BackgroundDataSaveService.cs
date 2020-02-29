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

        private readonly IRepository _repository;
        private readonly IDataQueue _dataQueue; 
        public BackgroundDataSaveService(
            IRepository repository,
            IDataQueue dataQueue)
        {
            this._repository = repository;
            this._dataQueue = dataQueue;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await SaveAndEmtpyQueueAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await SaveAndEmtpyQueueAsync(cancellationToken);
        }

        private async Task SaveAndEmtpyQueueAsync(CancellationToken cancellationToken)
        {
            IEnumerable<AnalyticsDataDTO> queue = _dataQueue.DeQueueAll();
            await _repository.AddManyAndSaveAsync(queue, cancellationToken);
        }
    }
}
