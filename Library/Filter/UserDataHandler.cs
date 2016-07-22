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
using DbModel.ViewModel.User;
using DbModel.Util.User;
using DbModel.ViewModel.Login;
using Library.Login;
namespace Library.Filter
{
    public class UserDataHandler : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //將UserProfile寫入ViewBag
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                UserProfileVM uVM = (UserProfileVM)filterContext.RequestContext.HttpContext.Session[UserDataUtil.SessionKey];
                FormsAuthenticationTicket ticket = ((FormsIdentity)filterContext.HttpContext.User.Identity).Ticket;
                if (uVM == null)                
                {
                    if ((string)filterContext.RouteData.Values["controller"] != "Login")
                    {
                        
                        LoginVM userData = JsonConvert.DeserializeObject<LoginVM>(ticket.UserData);
                        LoginHelper lh = new LoginHelper(userData);
                        uVM = lh.UserViewModel();
                        filterContext.RequestContext.HttpContext.Session.Add(UserDataUtil.SessionKey, uVM);
                    }
                }

                filterContext.Controller.ViewBag.UserProfile = uVM;
            }             
        }
    }
}
