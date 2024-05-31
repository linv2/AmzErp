using AmzErp.Common.Exception;
using AmzErp.Common.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AmzErp.Common.Mvc.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException exception)
            {
                context.Result = new OkObjectResult(exception.Result);
            }
            else
            {
                _logger.LogError(context.Exception, "拦截器拦截到未处理异常");
                var result = ResultData.Fail(context.Exception.Message);
                context.Result = new OkObjectResult(result);
            }
        }
    }
}
