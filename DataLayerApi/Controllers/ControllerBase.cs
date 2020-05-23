using DataLayerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DataLayerApi.Controllers
{
    [Authorize()]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ControllerBase : Controller
    {
        protected readonly ILogger logger;

        protected ControllerBase(ILogger logger)
        {
            using var stopWatch = new StopWatch(logger, LogLevel.Debug, $"Initialize controller");
            this.logger = logger;
            this.logger.LogDebug($"Vytvářím constructor");
        }

        public override AcceptedAtActionResult AcceptedAtAction(string actionName, object routeValues, [ActionResultObjectValue] object value)
        {
            return base.AcceptedAtAction(actionName, routeValues, value);
        }
    }
}
