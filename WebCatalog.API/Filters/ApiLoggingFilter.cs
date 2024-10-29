﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace WebCatalog.API.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("### Executado ----> OnActionExecuted");
            _logger.LogInformation("######################################");
            _logger.LogInformation(DateTime.Now.ToShortTimeString());
            _logger.LogInformation($"Status Code: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("######################################");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("### Executando ----> OnActionExecuting");
            _logger.LogInformation("######################################");
            _logger.LogInformation(DateTime.Now.ToShortTimeString());
            _logger.LogInformation($"Model State: {context.ModelState.IsValid}");
            _logger.LogInformation("######################################");
        }
    }
}
