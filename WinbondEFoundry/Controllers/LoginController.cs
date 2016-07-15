﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel;
using DbModel.Helper;
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
        public ActionResult GetProjectByUser([Bind(Include="UserEmail,Password")]LoginVM lv)
        {
            LoginHelper lh = new LoginHelper(lv);
            if (lh.Login())
            {
                List<ProjectAndUsersView> projects = DbHelper.GetList<ProjectAndUsersView>(delegate(ProjectAndUsersView pauv)
                {
                    return pauv.UserId == lh.User().UserId;
                });

                return Json(projects);
            }

            return null;
        }

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
                   
                    //Return Url
                    string rURL = Request.QueryString["ReturnUrl"];
                    if (!string.IsNullOrEmpty(lv.ReturnUrl))
                    {
                        return Redirect(lv.ReturnUrl);
                    }
                }
                ViewBag.isLogin = lh.Login();

                
            }            
            return View();
        }
    }
}
