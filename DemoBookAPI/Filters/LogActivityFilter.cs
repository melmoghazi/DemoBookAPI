using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace DemoBookAPI.Filters
{
    /*
    * To make your class Action filter:
    * 1- Create a class and implement  IActionFilter or IAsyncActionFilter interface.
    * 2- Register you action filter  builder.Services.AddControllers(Option => {
                Option.Filters.Add<LogActivityFilter>();
            }); 
        and this will be global action filter that means it will be called with each 
        controller action.

    If your class implemented the 2 interfaces IActionFilter and IAsyncActionFilter
    the compiler will call the Async method only.
    */

    public class LogActivityFilter : IActionFilter, IAsyncActionFilter
    {
        private readonly ILogger<LogActivityFilter> _logger;


        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Execute Before Action
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Executing Action {context.ActionDescriptor.DisplayName} on controller {context.Controller} " +
                $"with argument {JsonSerializer.Serialize(context.ActionArguments)}");
            //If context.Result have value this will be short circte and request will not complete.
            //context.Result = new NotFoundResult();

        }

        /// <summary>
        /// Execute After Action
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} executed on controller {context.Controller}");
            //context.Result = new NotFoundResult();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"(Async) Executing Action {context.ActionDescriptor.DisplayName} on controller {context.Controller} " +
                $"with argument {JsonSerializer.Serialize(context.ActionArguments)}");
            await next();
            _logger.LogInformation($"(Async) Action {context.ActionDescriptor.DisplayName} executed on controller {context.Controller}");

        }
    }
}
