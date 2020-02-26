using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

namespace SmallAnalytics.Core.Service
{
    public class BackgroundAnalyticsService : IBackgroundAnalyticsService
    {
        private static ConcurrentQueue<AnalyticsDataDTO> Queue = new ConcurrentQueue<AnalyticsDataDTO>();

        public void StoreData(DateTimeOffset date, string content)
        {
            Queue.Enqueue(new AnalyticsDataDTO(date, content));
        }

        public static void ClearQueue()
        {
            while (BackgroundAnalyticsService.Queue.TryDequeue(out AnalyticsDataDTO item))
            {
                // do nothing
            }
        }

        public static IReadOnlyList<AnalyticsDataDTO> ReadQueue()
        {
            return BackgroundAnalyticsService.Queue.ToImmutableList();
        }
    }
}
