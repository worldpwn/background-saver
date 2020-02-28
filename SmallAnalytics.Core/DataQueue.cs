using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallAnalytics.Core
{
    public class DataQueue : IDataQueue
    {
        private static ConcurrentQueue<AnalyticsDataDTO> Queue = new ConcurrentQueue<AnalyticsDataDTO>();

        public void AddToQueue(DateTimeOffset date, string content)
        {
            DataQueue.Queue.Enqueue(new AnalyticsDataDTO(date, content));
        }

        public IEnumerable<AnalyticsDataDTO> DeQueueAll()
        {
            List<AnalyticsDataDTO> dataDTOs = new List<AnalyticsDataDTO>();
            while (DataQueue.Queue.TryDequeue(out AnalyticsDataDTO item))
            {
                dataDTOs.Add(item);
            }
            return dataDTOs;
        }

        public IReadOnlyList<AnalyticsDataDTO> ReadQueue()
        {
            return DataQueue.Queue.ToList();
        }
    }
}
