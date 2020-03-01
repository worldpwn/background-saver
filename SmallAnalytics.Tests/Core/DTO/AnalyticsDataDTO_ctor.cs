using SmallAnalytics.Tests.Mock;
using System;
using Xunit;

namespace SmallAnalytics.Tests.Core.DTO
{
    public class AnalyticsDataDTO_ctor
    {
        [Fact]
        public void PassCorrectData_Should_BeOk()
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            string content = "user registered";
            TestAnalyticsData analyticsData = new TestAnalyticsData(date: date, content: content);

            Assert.Equal(date, analyticsData.Date);
            Assert.Equal(content, analyticsData.Content);
        }

        [Fact]
        public void PassNullContent_Should_Throw()
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            string content = null;
            Action action = () => new TestAnalyticsData(date: date, content: content);

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void MultipleCreationIds_Should_BeDifferent()
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            string content = "user registered";
            TestAnalyticsData analyticDataA = new TestAnalyticsData(date: date.AddMinutes(3), content: content);
            TestAnalyticsData analyticDataB = new TestAnalyticsData(date: date.AddDays(3), content: content);
            TestAnalyticsData analyticDataC = new TestAnalyticsData(date: date.AddHours(3), content: content);

            Assert.NotEqual(analyticDataA, analyticDataB);
            Assert.NotEqual(analyticDataA, analyticDataC);
            Assert.NotEqual(analyticDataB, analyticDataC);
        }
    }
}
