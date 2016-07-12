﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbModel;
using DbModel.Helper;

namespace WinbondEFoundry.Controllers
{
    public class UserProfileController : BaseController<UserProfile>
    {
        public override void ResourceToEditView()
        {
            ViewBag.CompanyList = DbHelper.GetList<CompanyProfile>()
                                    .Select(o => new SelectListItem
                                    {
                                        Text = o.CompanyName,
                                        Value = o.CompanyNo.ToString()
                                    });
        }

        public override void ResourceToIndexView()
        {

        }
    }
}