using SmallAnalytics.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
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
            AnalyticsDataDTO analyticsData = new AnalyticsDataDTO(date: date, content: content);

            Assert.Equal(date, analyticsData.Date);
            Assert.Equal(content, analyticsData.Content);
        }

        [Fact]
        public void PassNullContent_Should_Throw()
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            string content = null;
            Action action = () => new AnalyticsDataDTO(date: date, content: content);

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void MultipleCreationIds_Should_BeDifferent()
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            string content = "user registered";
            AnalyticsDataDTO analyticDataA = new AnalyticsDataDTO(date: date.AddMinutes(3), content: content);
            AnalyticsDataDTO analyticDataB = new AnalyticsDataDTO(date: date.AddDays(3), content: content);
            AnalyticsDataDTO analyticDataC = new AnalyticsDataDTO(date: date.AddHours(3), content: content);

            Assert.NotEqual(analyticDataA, analyticDataB);
            Assert.NotEqual(analyticDataA, analyticDataC);
            Assert.NotEqual(analyticDataB, analyticDataC);
        }
    }
}
