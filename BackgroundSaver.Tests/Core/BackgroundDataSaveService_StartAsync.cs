using BackgroundSaver.Core;
using BackgroundSaver.Core.DataStorage;
using BackgroundSaver.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using Microsoft.Extensions.Logging.Abstractions;

namespace BackgroundSaver.Tests.Core
{
    public class BackgroundDataSaveService_StartAsync
    {
        [Fact]
        public async Task OnStartWaitTimeToSave_Should_SaveQueue()
        {
            // Arrange
            TimeSpan timeToSave = TimeSpan.FromSeconds(3);
            IDataQueue<TestAnalyticsData> dataQueue = new DataQueue<TestAnalyticsData>();
            dataQueue.AddToQueue(new TestAnalyticsData(DateTimeOffset.UtcNow, "some content"));

            IRepository<TestAnalyticsData> repository = new TestRepository();
            BackgroundDataSaveService<TestAnalyticsData> backgroundDataSaveService = new BackgroundDataSaveService<TestAnalyticsData>(
                serviceProvider: new TestDI(repository),
                dataQueue: dataQueue,
                logger: NullLogger< BackgroundDataSaveService<TestAnalyticsData>>.Instance) { TimeBeforeSaves = timeToSave };
            CancellationTokenSource cts = new CancellationTokenSource();

            // Act         
            await backgroundDataSaveService.StartAsync(cts.Token);

            await Task.Delay(timeToSave);

            Assert.NotEmpty(((TestRepository)repository).Store);
        }

        [Fact]
        public async Task OnStartWihtoutWaitTimeToSave_Should_NotSaveQueue()
        {
            // Arrange
            TimeSpan timeToSave = TimeSpan.FromMinutes(30);
            IDataQueue<TestAnalyticsData> dataQueue = new DataQueue<TestAnalyticsData>();
            dataQueue.AddToQueue(new TestAnalyticsData(DateTimeOffset.UtcNow, "some content"));

            IRepository<TestAnalyticsData> repository = new TestRepository();
            BackgroundDataSaveService<TestAnalyticsData> backgroundDataSaveService = new BackgroundDataSaveService<TestAnalyticsData>(
                serviceProvider: new TestDI(repository),
                dataQueue: dataQueue,
                logger: NullLogger<BackgroundDataSaveService<TestAnalyticsData>>.Instance) { TimeBeforeSaves = timeToSave };
            CancellationTokenSource cts = new CancellationTokenSource();

            // Act         
            await backgroundDataSaveService.StartAsync(cts.Token);

            Assert.Empty(((TestRepository)repository).Store);
        }

        [Fact]
        public async Task MultipleIntervals_Should_SaveQueue()
        {
            // Arrange
            TimeSpan timeToSave = TimeSpan.FromSeconds(3);
            IDataQueue<TestAnalyticsData> dataQueue = new DataQueue<TestAnalyticsData>();
            dataQueue.AddToQueue(new TestAnalyticsData(DateTimeOffset.UtcNow, "some content"));

            IRepository<TestAnalyticsData> repository = new TestRepository();
            BackgroundDataSaveService<TestAnalyticsData> backgroundDataSaveService = new BackgroundDataSaveService<TestAnalyticsData>(
                serviceProvider: new TestDI(repository),
                dataQueue: dataQueue,
                logger: NullLogger<BackgroundDataSaveService<TestAnalyticsData>>.Instance) { TimeBeforeSaves = timeToSave };

            CancellationTokenSource cts = new CancellationTokenSource();

            // Act         
            await backgroundDataSaveService.StartAsync(cts.Token);

            await Task.Delay(timeToSave);

            Assert.NotEmpty(((TestRepository)repository).Store);
        }
    }
}
