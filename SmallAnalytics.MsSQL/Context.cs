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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=blogging.db");
    }
}
