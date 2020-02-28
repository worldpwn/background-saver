using SmallAnalytics.Core.DataStorage;
using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmallAnalytics.Tests.Mock
{
    public class TestRepository : IRepository
    {
        public static List<AnalyticsDataDTO> _store = new List<AnalyticsDataDTO>();

        public async Task AddManyAndSaveAsync(IEnumerable<AnalyticsDataDTO> analyticsDataDTOs)
        {
            await Task.Run(() => _store.AddRange(analyticsDataDTOs));
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing");
        }
    }
}
