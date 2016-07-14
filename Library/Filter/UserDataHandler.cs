using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using Newtonsoft.Json;
using DbModel;
namespace Library.Filter
{
    public class UserDataHandler : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //將UserProfile寫入ViewBag
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                HttpCookie cookie = filterContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    filterContext.Controller.ViewBag.UserProfile = JsonConvert.DeserializeObject<UserProfile>(ticket.UserData);
                }
            }             
        }
    }
}
