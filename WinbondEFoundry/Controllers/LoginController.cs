using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel.ViewModel.Login;
using Library.Login;
using System.Web.Security;

namespace WinbondEFoundry.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginVM lv)
        {
            if (ModelState.IsValid)
            {
                LoginHelper lh = new LoginHelper(lv);
                if (lh.Login())
                {
                    string encrypt = lh.GetEncrptyTicket();                    
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,encrypt));                    
                }
                ViewBag.isLogin = lh.Login();
            }            
            return View();
        }
    }
}
