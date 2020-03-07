using BackgroundSaver.Core.DataStorage;
using BackgroundSaver.MsSQL.Modesl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundSaver.MsSQL.Repository
{
    public class SQLRepository : IRepository<AnalyticsDataModel>
    {
        private readonly Context _context;
        public SQLRepository(Context context)
        {
            _context = context;
        }
        public async Task AddManyAndSaveAsync(IEnumerable<AnalyticsDataModel> analyticsDataDTOs, CancellationToken cancellationToken)
        {
            // TODO bulk incert
            await _context.AddRangeAsync(analyticsDataDTOs, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
