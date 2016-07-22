using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbModel.ViewModel.SubFunction;
using DbModel.Util.User;
namespace DbModel.ViewModel.User
{
    public class UserProfileVM
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public string ProjectNo { get; set; }
        public string CompanyNo { get; set; }
        public List<SubFunctionVM> SubFunctionList { get; set; }
        public List<AdminFunctionVM> AdminFunctionList { get; set; }
        public UserDataUtil.Role UserRole { get; set; }
    }
}
