using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel.Util.Project;
using DbModel;
using DbModel.Util;
using DbModel.ViewModel.User;

namespace WinbondEFoundry.Controllers
{
    public class AdminController : AuthorizeController
    {
        #region Project Member
        
        public ActionResult ProjectMember()
        {
            ViewBag.SubFuncList = DataUtil.GetList<SubFunction>(delegate(SubFunction model)
            {
                return model.MainFunctionId == 1;
            });
            ProjectDataUtil pdu = new ProjectDataUtil(ViewBag.UserProfile);
            return View(pdu.GetUserListByProject());
        }

        public ActionResult EditProjectMember(string id)
        {
            UserProfileVM uVM = (UserProfileVM)ViewBag.UserProfile;
            UserProfile u = DataUtil.GetItem<UserProfile>(id);
            ProjectAndUsers pau = DataUtil.GetItem<ProjectAndUsers>(delegate(ProjectAndUsers model)
            {
                return model.UserId == u.UserId && model.ProjectId == uVM.ProjectNo;
            });
            ViewBag.Company = DataUtil.GetItem<CompanyProfile>(u.CompanyNo);
            Tuple<UserProfile, ProjectAndUsers> tuple = new Tuple<UserProfile, ProjectAndUsers>(u,pau);
            return View(tuple);
        }

        [HttpPost]
        public ActionResult EditProjectMember(UserProfile Item1,ProjectAndUsers Item2, string UserEmailName, string DomainName)
        {
            Item1.UserEmail = UserEmailName + "@" + DomainName;
            DataUtil.Update<UserProfile>(Item1);
            Item2.UserId = Item1.UserId;
            DataUtil.Update<ProjectAndUsers>(Item2);
            ViewBag.Company = DataUtil.GetItem<CompanyProfile>(Item1.CompanyNo);
            Tuple<UserProfile, ProjectAndUsers> tuple = new Tuple<UserProfile, ProjectAndUsers>(Item1, Item2);
            return View(tuple);
        }

        public ActionResult AddProjectMember()
        {
            UserProfileVM uVM = (UserProfileVM)ViewBag.UserProfile;
            ViewBag.Company = DataUtil.GetItem<CompanyProfile>(uVM.CompanyNo);
            Tuple<UserProfile, ProjectAndUsers> tuple = new Tuple<UserProfile, ProjectAndUsers>(new UserProfile(),new ProjectAndUsers());
            return View(tuple);
        }

        [HttpPost]
        public ActionResult AddProjectMember(UserProfile Item1,ProjectAndUsers Item2, string UserEmailName, string DomainName)
        {
            Item1.UserEmail = UserEmailName + "@" + DomainName;
            DataUtil.Insert<UserProfile>(Item1);
            Item2.UserId = Item1.UserId;
            DataUtil.Insert<ProjectAndUsers>(Item2);
            
            return RedirectToAction("ProjectMember");
        }

        #endregion

        #region Assign Member
        
        public ActionResult GetMemberPermission(long userId)
        {
            UserProfileVM uVM = (UserProfileVM)ViewBag.UserProfile;
            return Json(DataUtil.GetList<ProjectAndSubFunctionView>(delegate(ProjectAndSubFunctionView model)
                        {
                            return model.ProjectNo == uVM.ProjectNo && model.UserId == userId;
                        }
                       )
                    );       
        }

        [HttpPost]
        public ActionResult SetMemberPermission(long userId, long[] subFuncId)
        {
            UserProfileVM uVM = (UserProfileVM)ViewBag.UserProfile;
            //先刪除全部
            DataUtil.Delete<UserAndFunction>(delegate(UserAndFunction model)
            {
                return model.ProjectNo == uVM.ProjectNo && model.UserId == userId;
            });
            List<UserAndFunction> uafList = new List<UserAndFunction>();
            foreach (long id in subFuncId)
            {
                UserAndFunction uaf = new UserAndFunction()
                {
                    SubFuntionNo = id,
                    ProjectNo = uVM.ProjectNo,
                    UserId = userId,
                    Creater = uVM.UserId,
                    Status = "1"
                };
                uafList.Add(uaf);
            }

            DataUtil.Insert<UserAndFunction>(uafList.ToArray());
            return Content("Success");
        }

        #endregion

        public ActionResult ManageGroups()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
    }
}
