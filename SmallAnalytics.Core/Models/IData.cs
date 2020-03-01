using System;
using System.Collections.Generic;
using System.Text;

namespace SmallAnalytics.Core.Models
{
    public interface IData
    {
        DateTimeOffset Date { get; }
    }
}
