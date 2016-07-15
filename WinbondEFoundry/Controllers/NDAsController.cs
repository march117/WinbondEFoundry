﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel;
using DbModel.Helper;

namespace WinbondEFoundry.Controllers
{
    public class NDAsController : BaseController<NDAs>
    {

        public override void ResourceToEditView()
        {
            ViewBag.CompanyList = DbHelper.GetList<CompanyProfile>();
        }

        public override void ResourceToIndexView()
        {
            
        }
    }
}