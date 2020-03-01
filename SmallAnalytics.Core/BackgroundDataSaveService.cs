﻿using Microsoft.Extensions.Hosting;
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
        /// <summary>
        /// Time span before each save of queue. Default value is 1 minute.
        /// </summary>
        public TimeSpan TimeBeforeSaves { get; set; } = TimeSpan.FromMinutes(1);
        private Timer? _timer = null;

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
            IEnumerable<TModel> queue = this._dataQueue.DeQueueAll();
            await _repository.AddManyAndSaveAsync(queue, cancellationToken);
        }
    }
}
