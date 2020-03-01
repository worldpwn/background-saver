using SmallAnalytics.Core;
using SmallAnalytics.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SmallAnalytics.Tests.Core
{
    public class DataQueue_DeQueueAll
    {
        [Fact]
        public void DeQueData_Should_BeRemovedFromQueue()
        {
            for (int i = 0; i < 23; i++)
            {
                IDataQueue<TestAnalyticsData> dataQueue = new DataQueue<TestAnalyticsData>();
                string content = "some content";

                dataQueue.AddToQueue(new TestAnalyticsData(DateTimeOffset.UtcNow, content));
            }

            IDataQueue<TestAnalyticsData> actQueue = new DataQueue<TestAnalyticsData>();
            actQueue.DeQueueAll();

            Assert.Empty(actQueue.ReadQueue());
        }

        [Fact]
        public void DeQueData_Should_ReturnElements()
        {
            IDataQueue<TestAnalyticsData> dataQueue = new DataQueue<TestAnalyticsData>();
            int numberOfNewElements = 78;
            for (int i = 0; i < numberOfNewElements; i++)
            {
             
                string content = "some content";

                dataQueue.AddToQueue(new TestAnalyticsData(DateTimeOffset.UtcNow, content));
            }

            Assert.Equal(numberOfNewElements, dataQueue.DeQueueAll().Count());
        }
    }
}
