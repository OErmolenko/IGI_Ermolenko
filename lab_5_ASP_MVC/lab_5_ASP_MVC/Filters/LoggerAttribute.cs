using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace lab_5_ASP_MVC.Filters
{
    public class LoggerAttribute : Attribute, IActionFilter
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "logger.txt");

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            File.AppendAllText(filePath, "response to " + (string)context.RouteData.Values["action"] + "\n");
        }
    }
}
