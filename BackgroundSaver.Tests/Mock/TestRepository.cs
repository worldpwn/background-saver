using BackgroundSaver.Core.DataStorage;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundSaver.Tests.Mock
{
    public class TestRepository : IRepository<TestAnalyticsData>
    {
        public List<TestAnalyticsData> Store { get; private set; } = new List<TestAnalyticsData>();

        public async Task AddManyAndSaveAsync(IEnumerable<TestAnalyticsData> analyticsDataDTOs, CancellationToken cancellationToken)
        {
            await Task.Run(() => Store.AddRange(analyticsDataDTOs));
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing");
        }
    }
}
