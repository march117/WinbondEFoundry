using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel;
using DbModel.Helper;

namespace WinbondEFoundry.Controllers
{
    public class SubFunctionController : BaseController<SubFunction>
    {

        #region Ajax Api
        
        public ActionResult GetSubFunction(int MainFuncNo)
        {
            return Json(
                DbHelper.GetList<SubFunction>(delegate(SubFunction model)
                {
                    return model.MainFunctionId == MainFuncNo;
                })
            );
        }

        
        #endregion
        public override void ResourceToEditView()
        {
            ViewBag.MainFunc = DbHelper.GetList<MainFunction>();
        }

        public override void ResourceToIndexView()
        {
            
        }
    }
}
