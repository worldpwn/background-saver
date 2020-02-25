using SmallAnalytics.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public void MultiThread_AddData_Should_BeInQueue()
        {
            Thread[] threads = new Thread[12];

            BackgroundAnalyticsService backgroundAnalyticsService = new BackgroundAnalyticsService();
            

            for (int i = 0; i < threads.Length; i++)
            {
                string content = $"some content from thread {i}";
                threads[i] = new Thread(() => backgroundAnalyticsService.StoreData(DateTimeOffset.UtcNow, content));
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Assert.NotEmpty(backgroundAnalyticsService.Queue);
           // Assert.NotNull(backgroundAnalyticsService.Queue.FirstOrDefault(d => d.Content == content));
        }
    }
}
