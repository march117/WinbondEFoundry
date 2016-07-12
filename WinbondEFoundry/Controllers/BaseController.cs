using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel;
using DbModel.Helper;

namespace WinbondEFoundry.Controllers
{
    public abstract class BaseController<T> : Controller where T: class
    {
        // GET: /Groups/
        public ActionResult Index()
        {
            ResourceToIndexView();
            return View(DbHelper.GetList<T>());
        }

        public ActionResult Create()
        {
            ResourceToEditView();
            return View();
        }

        [HttpPost]
        public ActionResult Create(T g)
        {
            ViewBag.IsValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                DbHelper.Insert<T>(g);
                return RedirectToAction("Index");
            }
            else
            {
                ResourceToEditView();
                return View();
            }
        }


        public ActionResult Edit(string id)
        {
            T g = DbHelper.GetItem<T>(id);
            ResourceToEditView();
            return View(g);
        }

        [HttpPost]
        public ActionResult Edit(T g)
        {
            if (ModelState.IsValid)
            {
                DbHelper.Update<T>(g);
            }            
            ResourceToEditView();
            return View(g);
        }

        [HttpPost]
        public ActionResult DeleteItem(string[] id)
        {
            DbHelper.Delete<T>(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 將需要的資源加入編輯的ViewBag
        /// </summary>
        public abstract void ResourceToEditView();

        /// <summary>
        /// 將需要的資源加入Index的ViewBag
        /// </summary>
        public abstract void ResourceToIndexView();
    }
}
