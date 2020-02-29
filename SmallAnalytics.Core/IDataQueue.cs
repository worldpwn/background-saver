using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallAnalytics.Core
{
    public interface IDataQueue
    {
        void AddToQueue(DateTimeOffset date, string content);
        IEnumerable<AnalyticsDataDTO> DeQueueAll();
        IReadOnlyList<AnalyticsDataDTO> ReadQueue();
    }
}
