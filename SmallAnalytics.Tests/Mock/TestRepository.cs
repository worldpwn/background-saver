using SmallAnalytics.Core.DataStorage;
using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmallAnalytics.Tests.Mock
{
    public class TestRepository : IRepository
    {
        public List<AnalyticsDataDTO> Store { get; private set; } = new List<AnalyticsDataDTO>();

        public async Task AddManyAndSaveAsync(IEnumerable<AnalyticsDataDTO> analyticsDataDTOs, CancellationToken cancellationToken)
        {
            await Task.Run(() => Store.AddRange(analyticsDataDTOs));
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing");
        }
    }
}
