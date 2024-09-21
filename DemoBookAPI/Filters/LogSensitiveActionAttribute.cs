﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;


namespace DemoBookAPI.Filters
{
    public class LogSensitiveActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Sensitive action executed.");
        }
    }
}
