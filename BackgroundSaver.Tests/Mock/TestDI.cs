using Microsoft.Extensions.DependencyInjection;
using BackgroundSaver.Core.DataStorage;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackgroundSaver.Tests.Mock
{
    public class TestDI : IServiceProvider
    {
        private readonly IRepository<TestAnalyticsData> _repository;
        public TestDI(IRepository<TestAnalyticsData> repository)
        {
            _repository = repository;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IRepository<TestAnalyticsData>)) return _repository;
            return new TestServiceScope(_repository);
        }
    }

    public class TestServiceScope : IServiceScope, IServiceScopeFactory
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private readonly IRepository<TestAnalyticsData> _repository;
        public TestServiceScope(IRepository<TestAnalyticsData> repository)
        {
            this._repository = repository;
            this.ServiceProvider = new TestDI(repository);
        }

        public void Dispose()
        {
            
        }

        public IServiceScope CreateScope()
        {
            return new TestServiceScope(_repository);
        }
    }
}
