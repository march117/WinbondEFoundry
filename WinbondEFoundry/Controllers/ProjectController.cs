using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel;
using DbModel.ViewModel.User;
using DbModel.Helper;
using DbModel.Helper.Project;
namespace WinbondEFoundry.Controllers
{
    public class ProjectController : BaseController<Project>
    {
        #region User ActionResult
        
        public ActionResult UserList(string id)
        {
            ViewBag.Project = DbHelper.GetItem<Project>(id);
            return View(DbHelper.GetList<ProjectAndUsersView>(delegate(ProjectAndUsersView pauv)
            {
                return pauv.ProjectId == id;
            }));
        }

        public ActionResult AddUser(string id)
        {
            ViewBag.UserList = DbHelper.GetList<UserProfile>();
            ViewBag.Project = DbHelper.GetItem<Project>(id);
            return View();
        }       

        [HttpPost]
        public ActionResult AddUser(ProjectAndUsers pau)
        {
            DbHelper.Insert<ProjectAndUsers>(pau);

            return RedirectToAction("UserList", new { id = pau.ProjectId });
        }

        public ActionResult EditUser(string id)
        {
            ViewBag.UserList = DbHelper.GetList<UserProfile>();
            return View(DbHelper.GetItem<ProjectAndUsers>(id));
        }

        [HttpPost]
        public ActionResult EditUser(ProjectAndUsers pau)
        {            
            DbHelper.Update<ProjectAndUsers>(pau);
            return View(pau);
        }

        public ActionResult DeleteUser(int[] pauId,string id)
        {
            DbHelper.Delete<ProjectAndUsers>(pauId);
            return RedirectToAction("UserList", new { id = id });
        }

        #endregion

        #region SubFunction ActionResul

        public ActionResult EditSubFunction(string id)
        {
            ViewBag.Project = DbHelper.GetItem<Project>(id);
            ViewBag.SubFuncList = DbHelper.GetList<SubFunction>();
            return View(DbHelper.GetList<ProjectAndSubFunctionView>(delegate(ProjectAndSubFunctionView pas)
            {
                return pas.ProjectNo == id;
            }));
        }

        [HttpPost]
        public ActionResult EditSubFunction(int[] subFuncId, string ProjectNo)
        {
            UserProfileVM uVM = (UserProfileVM)ViewBag.UserProfile;
            ProjectDataHelper.EditProjectAndSubFunction(subFuncId, ProjectNo, uVM.UserEmail);
            return RedirectToAction("EditSubFunction", new { id = ProjectNo });
        }

        #endregion

        public override void ResourceToEditView()
        {
            ViewBag.NDAList = DbHelper.GetList<NDAs>();
        }

        public override void ResourceToIndexView()
        {
            
        }
    }
}
