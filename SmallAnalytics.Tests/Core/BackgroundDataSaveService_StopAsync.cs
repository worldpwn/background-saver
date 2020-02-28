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
            IDataQueue dataQueue = new DataQueue();
            dataQueue.AddToQueue(DateTimeOffset.UtcNow, "some content");

            IRepository repository = new TestRepository();
            BackgroundDataSaveService backgroundDataSaveService = new BackgroundDataSaveService(repository, dataQueue);
            CancellationTokenSource cts = new CancellationTokenSource();

            await backgroundDataSaveService.StopAsync(cts.Token);

            Assert.NotEmpty(((TestRepository)repository).Store);
        }
    }
}
