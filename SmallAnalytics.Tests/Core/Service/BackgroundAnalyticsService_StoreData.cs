using SmallAnalytics.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SmallAnalytics.Tests.Core.Service
{
    public class BackgroundAnalyticsService_StoreData
    {
        [Fact]
        public void AddData_Should_BeInQueue()
        {
            BackgroundAnalyticsService backgroundAnalyticsService = new BackgroundAnalyticsService();
            string content = "some content";

            backgroundAnalyticsService.StoreData(DateTimeOffset.UtcNow, content);

            Assert.NotNull(backgroundAnalyticsService.Queue.FirstOrDefault(d => d.Content == content));
        }

        [Fact]
        public async Task MultiThread_AddData_Should_BeInQueue()
        {
            Task[] Tasks = new Task[12];

            BackgroundAnalyticsService backgroundAnalyticsService = new BackgroundAnalyticsService();
            

            for (int i = 0; i < Tasks.Length; i++)
            {
                string content = $"some content from thread {i}";
                Tasks[i] = new Task(() => backgroundAnalyticsService.StoreData(DateTimeOffset.UtcNow, content));
            }

            await Task.WhenAll(Tasks);

            Assert.Equal(11, backgroundAnalyticsService.Queue.Count);
        }
    }
}
