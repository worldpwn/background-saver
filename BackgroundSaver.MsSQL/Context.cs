using Microsoft.EntityFrameworkCore;
using BackgroundSaver.MsSQL.Modesl;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackgroundSaver.MsSQL
{
    public class Context : DbContext
    {
        public DbSet<AnalyticsDataModel> AnalyticsDatas { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

    }
}
