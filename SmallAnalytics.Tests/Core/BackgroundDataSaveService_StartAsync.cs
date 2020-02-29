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
    public class BackgroundDataSaveService_StartAsync
    {
        [Fact]
        public async Task OnStartWaitTimeToSave_Should_SaveQueu()
        {
            // Arrange
            TimeSpan timeToSave = TimeSpan.FromSeconds(3);
            IDataQueue dataQueue = new DataQueue();
            dataQueue.AddToQueue(DateTimeOffset.UtcNow, "some content");

            IRepository repository = new TestRepository();
            BackgroundDataSaveService backgroundDataSaveService = new BackgroundDataSaveService(repository, dataQueue) { TimeBeforeSaves = timeToSave };
            CancellationTokenSource cts = new CancellationTokenSource();

            // Act         
            await backgroundDataSaveService.StartAsync(cts.Token);

            await Task.Delay(timeToSave);

            Assert.NotEmpty(((TestRepository)repository).Store);
        }
    }
}
