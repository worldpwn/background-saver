using BackgroundSaver.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundSaver.Core.DataStorage
{
    public interface IRepository<TModel> : IDisposable where TModel : IData
    {
        Task AddManyAndSaveAsync(IEnumerable<TModel> analyticsDataDTOs, CancellationToken cancellationToken);
    }
}
