using SmallAnalytics.Core;
using SmallAnalytics.Core.DataStorage;
using SmallAnalytics.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Threading;

namespace SmallAnalytics.Tests.Core
{
    public class BackgroundDataSaveService_StopAsync
    {
        [Fact]
        public async Task BeforeStop_Should_AllQueuSavedInRepository()
        {
            IDataQueue<TestAnalyticsData> dataQueue = new DataQueue<TestAnalyticsData>();
            dataQueue.AddToQueue(new TestAnalyticsData(DateTimeOffset.UtcNow, "some content"));

            IRepository<TestAnalyticsData> repository = new TestRepository();
            BackgroundDataSaveService<TestAnalyticsData> backgroundDataSaveService = new BackgroundDataSaveService<TestAnalyticsData>(repository, dataQueue);
            CancellationTokenSource cts = new CancellationTokenSource();

            await backgroundDataSaveService.StopAsync(cts.Token);

            Assert.NotEmpty(((TestRepository)repository).Store);
        }
    }
}
