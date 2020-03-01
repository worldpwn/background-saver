using SmallAnalytics.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallAnalytics.MsSQL.Modesl
{
    public class AnalyticsDataModel : IData
    {
        public Guid Id { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public string Content { get; private set; }

        public AnalyticsDataModel(DateTimeOffset date, string content)
        {
            Id = Guid.NewGuid();
            Date = date;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
    }
}
