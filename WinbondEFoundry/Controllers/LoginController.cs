using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel;
using DbModel.Util;
using DbModel.ViewModel.Login;
using Library.Login;
using System.Web.Security;
using DbModel.Util.User;

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

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(UserDataUtil.SessionKey);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 取得使用者關聯的專案(Ajax Api)
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProjectByUser([Bind(Include="UserEmail,Password")]LoginVM lv)
        {
            LoginHelper lh = new LoginHelper(lv);
            if (lh.Login())
            {
                List<ProjectAndUsersView> projects = DataUtil.GetList<ProjectAndUsersView>(delegate(ProjectAndUsersView pauv)
                {
                    return pauv.UserId == lh.User().UserId;
                });

                return Json(projects);
            }

            return null;
        }

        /// <summary>
        /// Login Process
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginVM lv)
        {
            if (ModelState.IsValid)
            {
                LoginHelper lh = new LoginHelper(lv);
                if (lh.Login())
                {
                    string encrypt = lh.GetEncrptyTicket();                    
                    Response.AppendCookie(new HttpCookie(FormsAuthentication.FormsCookieName,encrypt));
                    Session.Add(UserDataUtil.SessionKey, lh.UserViewModel());
                    //Return Url
                    string rURL = Request.QueryString["ReturnUrl"];
                    if (!string.IsNullOrEmpty(lv.ReturnUrl))
                    {
                        return Redirect(lv.ReturnUrl);
                    }
                }
            }            
            return View();
        }
    }
}
