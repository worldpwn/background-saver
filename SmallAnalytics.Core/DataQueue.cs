using SmallAnalytics.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallAnalytics.Core
{
    public class DataQueue<TModel> : IDataQueue<TModel> where TModel : IData
    {
        private ConcurrentQueue<TModel> Queue = new ConcurrentQueue<TModel>();

        public void AddToQueue(TModel data)
        {
            this.Queue.Enqueue(data);
        }

        public IEnumerable<TModel> DeQueueAll()
        {
            List<TModel> dataDTOs = new List<TModel>();
            while (this.Queue.TryDequeue(out TModel item))
            {
                dataDTOs.Add(item);
            }
            return dataDTOs;
        }

        public IReadOnlyList<TModel> ReadQueue()
        {
            return this.Queue.ToList();
        }
    }
}
