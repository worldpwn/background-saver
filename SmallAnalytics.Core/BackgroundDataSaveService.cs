using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

        private readonly IDataQueue<TModel> _dataQueue;
        private readonly ILogger<BackgroundDataSaveService<TModel>> _logger;
        private readonly IServiceProvider _serviceProvider;
        public BackgroundDataSaveService(
            IServiceProvider serviceProvider,
            IDataQueue<TModel> dataQueue,
            ILogger<BackgroundDataSaveService<TModel>> logger)
        {
            this._dataQueue = dataQueue;
            this._logger = logger;
            this._serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(
                callback: async (e) => await SaveAndEmptyQueueAsync(cancellationToken),
                state: null,
                dueTime: TimeSpan.Zero,
                period: this.TimeBeforeSaves);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await SaveAndEmptyQueueAsync(cancellationToken);
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
        }

        private async Task SaveAndEmptyQueueAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope()) 
                {
                    IRepository<TModel> repository = scope.ServiceProvider.GetRequiredService<IRepository<TModel>>();
                    IEnumerable<TModel> queue = this._dataQueue.DeQueueAll();
                    await repository.AddManyAndSaveAsync(queue, cancellationToken);
                }

                _logger.LogInformation("Queue has been saved.");
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error during execution of method {nameof(SaveAndEmptyQueueAsync)}");
            }

        }
    }
}
