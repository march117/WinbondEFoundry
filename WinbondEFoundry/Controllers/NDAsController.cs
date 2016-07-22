using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel;
using DbModel.Util;

namespace WinbondEFoundry.Controllers
{
    public class NDAsController : BaseController<NDAs>
    {

        public override void ResourceToEditView()
        {
            ViewBag.CompanyList = DataUtil.GetList<CompanyProfile>();
        }

        public override void ResourceToIndexView()
        {
            
        }
    }
}
