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
        public async Task AddAsync(AnalyticsDataDTO analyticsDataDTOs)
        {
            await Task.Run(() => _store.Add(analyticsDataDTOs));
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing");
        }

        public async Task SaveChangeAsync()
        {
            await Task.Run(() => Console.WriteLine("Saving"));
        }
    }
}
