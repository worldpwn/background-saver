using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmallAnalytics.Core.DataStorage
{
    public interface IRepository : IDisposable
    {
        Task AddAsync(AnalyticsDataDTO analyticsDataDTOs);
        Task SaveChangeAsync();
    }
}
