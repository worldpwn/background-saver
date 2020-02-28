using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallAnalytics.Core
{
    public class DataQueue : IDataQueue
    {
        public void AddToQueue(DateTimeOffset date, string content)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<AnalyticsDataDTO> DeQueueAll()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<AnalyticsDataDTO> ReadQueue()
        {
            throw new NotImplementedException();
        }
    }
}
