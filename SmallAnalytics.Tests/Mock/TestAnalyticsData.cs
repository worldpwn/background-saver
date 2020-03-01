using SmallAnalytics.Core.Models;
using System;

namespace SmallAnalytics.Tests.Mock
{
    public class TestAnalyticsData : IData
    {
        public Guid Id { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public string Content { get; private set; }

        public TestAnalyticsData(DateTimeOffset date, string content)
        {
            Id = Guid.NewGuid();
            Date = date;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
    }
}
