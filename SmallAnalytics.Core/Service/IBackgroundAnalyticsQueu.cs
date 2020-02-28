using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmallAnalytics.Core.Service
{
    public interface IBackgroundAnalyticsQueu
    {
        void StoreData(DateTimeOffset date, string content);
    }
}
