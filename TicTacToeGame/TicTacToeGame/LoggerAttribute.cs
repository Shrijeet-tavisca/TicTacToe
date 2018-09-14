using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public class LoggerAttribute: ResultFilterAttribute, IActionFilter
    {
        Logger logObject = new Logger();
        DataOfPlayer addingObject = new DataOfPlayer();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                logObject.Request = context.RouteData.Values["action"].ToString() + " " + context.RouteData.Values["controller"].ToString();
                logObject.Response = "Success";
                logObject.Exception = "NULL";
                addingObject.AddLog(logObject);
            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            logObject.Request = context.RouteData.Values["action"].ToString() + " " + context.RouteData.Values["controller"].ToString();
            logObject.Response = "NULL";
            logObject.Exception = "NULL";
            addingObject.AddLog(logObject);

        }
    }
}
