using Microsoft.EntityFrameworkCore;
using SmallAnalytics.MsSQL.Modesl;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallAnalytics.MsSQL
{
    public class Context : DbContext
    {
        public DbSet<AnalyticsDataModel> AnalyticsDatas { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

    }
}
