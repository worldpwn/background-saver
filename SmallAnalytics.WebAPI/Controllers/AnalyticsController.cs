using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmallAnalytics.Core;
using SmallAnalytics.MsSQL;
using SmallAnalytics.MsSQL.Modesl;

namespace SmallAnalytics.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyticsController : ControllerBase
    {

        private readonly ILogger<AnalyticsController> _logger;
        private readonly Context _context;
        private readonly IDataQueue<AnalyticsDataModel> _queue;
        public AnalyticsController(
            ILogger<AnalyticsController> logger,
            Context context,
            IDataQueue<AnalyticsDataModel> queue)
        {
            _logger = logger;
            _context = context;
            _queue = queue;
        }

        [HttpGet]
        public async Task<IEnumerable<AnalyticsDataModel>> Get()
        {
            return await _context.AnalyticsDatas.ToListAsync();
        }

        [HttpPost]
        public ActionResult Post(string data)
        {
            _queue.AddToQueue(new AnalyticsDataModel(DateTimeOffset.UtcNow, data));
            _logger.LogInformation("adding data to queue", data);
            return Ok();
        }
    }
}
