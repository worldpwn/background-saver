using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BackgroundSaver.Core;
using BackgroundSaver.MsSQL;
using BackgroundSaver.MsSQL.Modesl;
using BackgroundSaver.WebAPI.ViewModels;

namespace BackgroundSaver.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {

        private readonly ILogger<SampleController> _logger;
        private readonly Context _context;
        private readonly IDataQueue<AnalyticsDataModel> _queue;
        public SampleController(
            ILogger<SampleController> logger,
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

        [HttpGet("Count")]
        public async Task<int> Count()
        {
            return await _context.AnalyticsDatas.CountAsync();
        }

        [HttpPost]
        public ActionResult Post([FromBody]DataViewModel data)
        {
            _queue.AddToQueue(new AnalyticsDataModel(DateTimeOffset.UtcNow, data.Content));
            _logger.LogInformation("adding data to queue", data);
            return Ok();
        }
    }
}
