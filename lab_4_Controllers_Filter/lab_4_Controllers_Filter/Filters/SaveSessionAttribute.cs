using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using lab_4_Controllers_Filter.Extensions;

namespace lab_4_Controllers_Filter.Filters
{
    public class SaveSessionAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Dictionary<string, string> dict = new Dictionary<string, string>();
            // считывание данных из ModelState и запись в сессию
            if (context.ModelState != null)
            {
                foreach (var item in context.ModelState)
                {
                    //dict.Add(item.Key, item.Value.AttemptedValue);
                    context.HttpContext.Session.SetObject(item.Key, item.Value.AttemptedValue);
                }
                //context.HttpContext.Session.GetObject(_name, dict);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;


        }
    }
}
