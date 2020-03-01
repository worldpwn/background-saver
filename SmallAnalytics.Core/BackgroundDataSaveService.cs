using Microsoft.Extensions.Hosting;
using SmallAnalytics.Core.DataStorage;
using SmallAnalytics.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmallAnalytics.Core
{
    public class BackgroundDataSaveService<TModel> : IHostedService where TModel : IData
    {
        // TODO:
        // 1 - limited max queu size before save
        // 2 - every fix time save

        private readonly IRepository<TModel> _repository;
        private readonly IDataQueue<TModel> _dataQueue;
        public BackgroundDataSaveService(
            IRepository<TModel> repository,
            IDataQueue<TModel> dataQueue)
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
            IEnumerable<TModel> queue = _dataQueue.DeQueueAll();
            await _repository.AddManyAndSaveAsync(queue, cancellationToken);
        }
    }
}
