using System;
using System.Collections.Generic;
using System.Text;

namespace SmallAnalytics.Core.DTO
{
    public class AnalyticsDataDTO
    {
        public Guid Id { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public string Content { get; private set; }

        public AnalyticsDataDTO(DateTimeOffset date, string content)
        {
            Id = Guid.NewGuid();
            Date = date;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
    }
}
