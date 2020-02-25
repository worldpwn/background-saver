using System;
using System.Collections.Generic;
using System.Text;

namespace SmallAnalytics.Core.Data
{
    public class AnalyticData
    {
        public Guid Id { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public string Content { get; private set; }

        public AnalyticData(DateTimeOffset date, string content)
        {
            Id = Guid.NewGuid();
            Date = date;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
    }
}
