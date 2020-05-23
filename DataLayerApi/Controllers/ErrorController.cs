using DataLayerApi.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DataLayerApi.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        public ErrorController(ILogger<ErrorController> logger) : base(logger)
        {
        }

        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }
            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var exceptionResp = exception as HttpResponseException;

            if (exceptionResp != null)
            {
                var resultObj = this.Problem(
                      detail: exceptionResp.StackTrace,
                      title: exceptionResp.Message,
                      statusCode: exceptionResp.Status,
                      type: exceptionResp.GetType().Name);
                resultObj.StatusCode = exceptionResp.Status;
                return resultObj;
            }
            else
                return this.Problem(
                    detail: exception.StackTrace,
                    title: exception.Message,
                    type: exception.GetType().Name);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var exceptionResp = exception as HttpResponseException;

            if (exceptionResp != null)
            {
                var resultObj = this.Problem(
                      detail: exceptionResp.StackTrace,
                      title: exceptionResp.Message,
                      statusCode: exceptionResp.Status,
                      type: exceptionResp.GetType().Name);
                resultObj.StatusCode = exceptionResp.Status;
                return resultObj;
            }
            else
                return this.Problem();
        }
    }
}
