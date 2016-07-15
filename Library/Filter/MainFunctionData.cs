using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DbModel.Helper;
using DbModel.Helper.Function;
namespace Library.Filter
{
    public class MainFunctionData : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.MainFunction = MainFunctionHelper.Instance().MainFunctionList();
        }
    }
}
