using SmallAnalytics.Core;
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
                IDataQueue dataQueue = new DataQueue();
                string content = "some content";

                dataQueue.AddToQueue(DateTimeOffset.UtcNow, content);
            }

            IDataQueue actQueue = new DataQueue();
            actQueue.DeQueueAll();

            Assert.Empty(actQueue.ReadQueue());
        }

        [Fact]
        public void DeQueData_Should_ReturnElements()
        {
            IDataQueue dataQueue = new DataQueue();
            int numberOfNewElements = 78;
            for (int i = 0; i < numberOfNewElements; i++)
            {
             
                string content = "some content";

                dataQueue.AddToQueue(DateTimeOffset.UtcNow, content);
            }

            Assert.Equal(numberOfNewElements, dataQueue.DeQueueAll().Count());
        }
    }
}
