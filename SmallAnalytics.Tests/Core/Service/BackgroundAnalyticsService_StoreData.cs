using SmallAnalytics.Core.Service;
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
    public class BackgroundAnalyticsService_StoreData
    {
        public BackgroundAnalyticsService_StoreData()
        {
            BackgroundAnalyticsService.ClearQueue();
        }
 

        [Fact]
        public void AddData_Should_BeInQueue()
        {
            BackgroundAnalyticsService backgroundAnalyticsService = new BackgroundAnalyticsService();
            string content = "some content";

            backgroundAnalyticsService.StoreData(DateTimeOffset.UtcNow, content);

            Assert.NotNull(BackgroundAnalyticsService.ReadQueue().FirstOrDefault(d => d.Content == content));
        }


        [Fact]
        public async Task MultiThread_AddData_Should_BeInQueue()
        {
            int numberOfThreads = 30000;
            Task[] tasks = new Task[numberOfThreads];

            for (int i = 0; i < tasks.Length; i++)
            {
                BackgroundAnalyticsService backgroundAnalyticsService = new BackgroundAnalyticsService();
                string content = $"some content from thread {i}";
                tasks[i] = Task.Factory.StartNew(() => backgroundAnalyticsService.StoreData(DateTimeOffset.UtcNow, content));
            }

            await Task.WhenAll(tasks);

            Assert.Equal(numberOfThreads, BackgroundAnalyticsService.ReadQueue().Count);
        }
    }
}
