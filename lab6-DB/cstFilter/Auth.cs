using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab6_DB.cstFilter
{
   
        public class Auth : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (filterContext.HttpContext.Session["UserName"] == null)
                    filterContext.Result = new RedirectResult("/Students/login");
                //base.OnActionExecuting(filterContext);
            }
        }
    
}