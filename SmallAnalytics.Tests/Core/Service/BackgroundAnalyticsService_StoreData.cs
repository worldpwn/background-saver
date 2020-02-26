using SmallAnalytics.Core.DataStorage;
using SmallAnalytics.Core.Service;
using SmallAnalytics.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SmallAnalytics.Tests.Core.Service
{
    public class BackgroundAnalyticsService_StoreData
    {
        public BackgroundAnalyticsService_StoreData()
        {
            BackgroundAnalyticsService.ClearQueue();
        }

        [Fact]
        public void AddData_Should_BeSavedInRepository()
        {
            BackgroundAnalyticsService.ClearQueue();
            IRepository testRepository = new TestRepository();
            BackgroundAnalyticsService backgroundAnalyticsService = new BackgroundAnalyticsService(testRepository);
            string content = "some content";

            backgroundAnalyticsService.StoreData(DateTimeOffset.UtcNow, content);

            Assert.NotNull(BackgroundAnalyticsService.ReadQueue().FirstOrDefault(d => d.Content == content));
            BackgroundAnalyticsService.ClearQueue();
        }
    }
}
