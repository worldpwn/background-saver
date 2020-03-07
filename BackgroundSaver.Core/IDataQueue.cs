using BackgroundSaver.Core.Models;
using System.Collections.Generic;

namespace BackgroundSaver.Core
{
    public interface IDataQueue<TModel> where TModel : IData
    {
        void AddToQueue(TModel data);
        IEnumerable<TModel> DeQueueAll();
        IReadOnlyList<TModel> ReadQueue();
    }
}
