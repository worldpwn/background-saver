using SmallAnalytics.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SmallAnalytics.Tests.Core.Data
{
    public class AnalyticData_ctor
    {
        [Fact]
        public void PassCorrectData_Should_BeOk()
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            string content = "user registered";
            AnalyticData analyticData = new AnalyticData(date: date, content: content);

            Assert.Equal(date, analyticData.Date);
            Assert.Equal(content, analyticData.Content);
        }

        [Fact]
        public void PassNullContent_Should_Throw()
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            string content = null;
            Action action = () => new AnalyticData(date: date, content: content);

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void MultipleCreationIds_Should_BeDifferent()
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            string content = "user registered";
            AnalyticData analyticDataA = new AnalyticData(date: date.AddMinutes(3), content: content);
            AnalyticData analyticDataB = new AnalyticData(date: date.AddDays(3), content: content);
            AnalyticData analyticDataC = new AnalyticData(date: date.AddHours(3), content: content);

            Assert.NotEqual(analyticDataA, analyticDataB);
            Assert.NotEqual(analyticDataA, analyticDataC);
            Assert.NotEqual(analyticDataB, analyticDataC);
        }
    }
}
