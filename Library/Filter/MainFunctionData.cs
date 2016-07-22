using System.Web.Mvc;
using DbModel.Util.Function;
namespace Library.Filter
{
    public class MainFunctionData : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.MainFunction = MainFunctionDataUtil.Instance().MainFunctionList();
        }
    }
}
