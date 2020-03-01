using SmallAnalytics.Core;
using SmallAnalytics.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmallAnalytics.Tests.Core
{
    public class DataQueue_AddToQueue
    {

        [Fact]
        public void AddData_Should_BeInQueue()
        {
            IDataQueue<TestAnalyticsData> dataQueue = new DataQueue<TestAnalyticsData>();
            string content = "some content";

            dataQueue.AddToQueue(new TestAnalyticsData(DateTimeOffset.UtcNow, content));

            Assert.NotNull(dataQueue.ReadQueue().FirstOrDefault(d => d.Content == content));
        }


        [Fact]
        public async Task MultiThread_AddData_Should_BeInQueue()
        {
            int numberOfThreads = 30000;
            Task[] tasks = new Task[numberOfThreads];

            IDataQueue<TestAnalyticsData> dataQueue = new DataQueue<TestAnalyticsData>();

            for (int i = 0; i < tasks.Length; i++)
            {
         
                tasks[i] = Task.Factory.StartNew(() =>
                    {
                        
                        string content = $"some content from thread {i}";
                        dataQueue.AddToQueue(new TestAnalyticsData(DateTimeOffset.UtcNow, content));
                    });
            }

            await Task.WhenAll(tasks);

            Assert.Equal(numberOfThreads, dataQueue.ReadQueue().Count);
        }
    }
}
