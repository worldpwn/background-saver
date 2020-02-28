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
            throw new NotImplementedException();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            IEnumerable<AnalyticsDataDTO> queue = _dataQueue.DeQueueAll();
            await _repository.AddManyAndSaveAsync(queue, cancellationToken);
        }
    }
}
