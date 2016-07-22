using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel;
using DbModel.ViewModel.User;
using DbModel.Util;
using DbModel.Util.Project;
namespace WinbondEFoundry.Controllers
{
    public class ProjectController : BaseController<Project>
    {
        #region User ActionResult
        
        public ActionResult UserList(string id)
        {
            ViewBag.Project = DataUtil.GetItem<Project>(id);
            return View(DataUtil.GetList<ProjectAndUsersView>(delegate(ProjectAndUsersView pauv)
            {
                return pauv.ProjectId == id;
            }));
        }

        public ActionResult AddUser(string id)
        {
            ViewBag.UserList = DataUtil.GetList<UserProfile>();
            ViewBag.Project = DataUtil.GetItem<Project>(id);
            return View();
        }       

        [HttpPost]
        public ActionResult AddUser(ProjectAndUsers pau)
        {
            DataUtil.Insert<ProjectAndUsers>(pau);

            return RedirectToAction("UserList", new { id = pau.ProjectId });
        }

        public ActionResult EditUser(string id)
        {
            ViewBag.UserList = DataUtil.GetList<UserProfile>();
            return View(DataUtil.GetItem<ProjectAndUsers>(id));
        }

        [HttpPost]
        public ActionResult EditUser(ProjectAndUsers pau)
        {            
            DataUtil.Update<ProjectAndUsers>(pau);
            return View(pau);
        }

        public ActionResult DeleteUser(int[] pauId,string id)
        {
            DataUtil.Delete<ProjectAndUsers>(pauId);
            return RedirectToAction("UserList", new { id = id });
        }

        #endregion

        #region SubFunction ActionResul

        public ActionResult EditSubFunction(string id)
        {
            ViewBag.Project = DataUtil.GetItem<Project>(id);
            ViewBag.SubFuncList = DataUtil.GetList<SubFunction>();
            return View(DataUtil.GetList<ProjectAndSubFunctionView>(delegate(ProjectAndSubFunctionView pas)
            {
                return pas.ProjectNo == id;
            }));
        }

        [HttpPost]
        public ActionResult EditSubFunction(int[] subFuncId, string ProjectNo)
        {
            UserProfileVM uVM = (UserProfileVM)ViewBag.UserProfile;
            ProjectDataUtil.EditProjectAndSubFunction(subFuncId, ProjectNo, uVM.UserEmail);
            return RedirectToAction("EditSubFunction", new { id = ProjectNo });
        }

        #endregion

        public override void ResourceToEditView()
        {
            ViewBag.NDAList = DataUtil.GetList<NDAs>();
        }

        public override void ResourceToIndexView()
        {
            
        }
    }
}
