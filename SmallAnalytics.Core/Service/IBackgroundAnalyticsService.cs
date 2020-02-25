using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmallAnalytics.Core.Service
{
    public interface IBackgroundAnalyticsService
    {
        void StoreData(DateTimeOffset date, string content);
    }
}
