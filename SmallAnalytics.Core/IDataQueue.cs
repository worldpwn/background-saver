using SmallAnalytics.Core.Models;
using System.Collections.Generic;

namespace SmallAnalytics.Core
{
    public interface IDataQueue<TModel> where TModel : IData
    {
        void AddToQueue(TModel data);
        IEnumerable<TModel> DeQueueAll();
        IReadOnlyList<TModel> ReadQueue();
    }
}
