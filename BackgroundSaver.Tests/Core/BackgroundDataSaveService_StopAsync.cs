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
    public class BackgroundDataSaveService_StopAsync
    {
        [Fact]
        public async Task BeforeStop_Should_AllQueuSavedInRepository()
        {
            IDataQueue<TestAnalyticsData> dataQueue = new DataQueue<TestAnalyticsData>();
            dataQueue.AddToQueue(new TestAnalyticsData(DateTimeOffset.UtcNow, "some content"));

            IRepository<TestAnalyticsData> repository = new TestRepository();
            BackgroundDataSaveService<TestAnalyticsData> backgroundDataSaveService = new BackgroundDataSaveService<TestAnalyticsData>(
                serviceProvider: new TestDI(repository),
                dataQueue: dataQueue,
                logger: NullLogger<BackgroundDataSaveService<TestAnalyticsData>>.Instance);

            CancellationTokenSource cts = new CancellationTokenSource();

            await backgroundDataSaveService.StopAsync(cts.Token);

            Assert.NotEmpty(((TestRepository)repository).Store);
        }
    }
}
