using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmallAnalytics.Core.DataStorage
{
    public interface IRepository
    {
        Task AddAsync(IEnumerable<AnalyticsDataDTO> analyticsDataDTOs);
    }
}
