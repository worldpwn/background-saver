using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmallAnalytics.Core.DataStorage
{
    public interface IRepository : IDisposable
    {
        Task AddManyAndSaveAsync(IEnumerable<AnalyticsDataDTO> analyticsDataDTOs, CancellationToken cancellationToken);
    }
}
