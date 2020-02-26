using SmallAnalytics.Core.DataStorage;
using SmallAnalytics.Core.Service;
using SmallAnalytics.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SmallAnalytics.Tests.Core.Service
{
    public class BackgroundAnalyticsService_StartAsync
    {
        public BackgroundAnalyticsService_StartAsync()
        {
            BackgroundAnalyticsService.ClearQueue();
        }
 

        [Fact]
        public void AddData_Should_BeInQueue()
        {
            IRepository testRepository = new TestRepository();
            BackgroundAnalyticsService backgroundAnalyticsService = new BackgroundAnalyticsService(testRepository);
            string content = "some content";

            backgroundAnalyticsService.StoreData(DateTimeOffset.UtcNow, content);

            Assert.NotNull(BackgroundAnalyticsService.ReadQueue().FirstOrDefault(d => d.Content == content));
        }


        [Fact]
        public async Task MultiThread_AddData_Should_BeInQueue()
        {
            IRepository testRepository = new TestRepository();
            int numberOfThreads = 30000;
            Task[] tasks = new Task[numberOfThreads];

            for (int i = 0; i < tasks.Length; i++)
            {
                BackgroundAnalyticsService backgroundAnalyticsService = new BackgroundAnalyticsService(testRepository);
                string content = $"some content from thread {i}";
                tasks[i] = Task.Factory.StartNew(() => backgroundAnalyticsService.StoreData(DateTimeOffset.UtcNow, content));
            }

            await Task.WhenAll(tasks);

            Assert.Equal(numberOfThreads, BackgroundAnalyticsService.ReadQueue().Count);
        }
    }
}
