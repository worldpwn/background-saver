using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmallAnalytics.Core.Service
{
    public class BackgroundAnalyticsService : IBackgroundAnalyticsService
    {
        public List<AnalyticsDataDTO> Queue { get; private set; }

        public BackgroundAnalyticsService()
        {
            this.Queue = new List<AnalyticsDataDTO>();
        }

        public void StoreData(DateTimeOffset date, string content)
        {
            Queue.Add(new AnalyticsDataDTO(date, content));
        }
    }
}
