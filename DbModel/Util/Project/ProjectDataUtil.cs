using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DbModel;
using DbModel.ViewModel.Project;
using DbModel.ViewModel.User;
namespace DbModel.Util.Project
{
    public class ProjectDataUtil
    {
        UserProfileVM _uVM;

        public ProjectDataUtil(UserProfileVM uVM)
        {
            _uVM = uVM;
        }

        public static void EditProjectAndSubFunction(int[] subFuncIds,string projectNo,string creater){
            List<UserAndFunction> uafList = new List<UserAndFunction>();
            using(EFoundryContext db = new EFoundryContext()){
                ////先刪除所有關聯
                //db.UserAndFunction.RemoveRange(db.UserAndFunction.Where(o=>o.ProjectNo == projectNo));
                //db.SaveChanges();
                ////取得所有跟專案關聯的使用者
                //List<ProjectAndUsersView> pauVList = db.ProjectAndUsersView.Where(o => o.ProjectNo == projectNo).ToList();
                //foreach (int subFuncId in subFuncIds)
                //{
                //    foreach (ProjectAndUsersView pauV in pauVList)
                //    {
                //        UserAndFunction uaf = new UserAndFunction()
                //        {
                //            ProjectNo = projectNo,
                //            SubFuntionNo = subFuncId,
                //            UserEmail = pauV.UserEmail,
                //            CreaterEmail = creater,
                //            Status = "1"
                //        };

                //        uafList.Add(uaf);
                //    }                    
                //}
                //db.UserAndFunction.AddRange(uafList);
                //db.SaveChanges();
            }            
        }
        /// <summary>
        /// 列出同專案使用者
        /// </summary>
        /// <returns>同專案使用者</returns>
        public List<UserProfileVM> GetUserListByProject()
        {
            List<UserProfileVM> uList;
            using (EFoundryContext db = new EFoundryContext())
            {
                uList = db.ProjectAndUsers
                         .Join(db.UserProfile, pau => pau.UserId, u => u.UserId, (pau, u) => new UserProfileVM
                         {
                             UserId = u.UserId,
                             ProjectNo = pau.ProjectId,
                             UserEmail = u.UserEmail,
                             UserRole = pau.IsAdmin ? User.UserDataUtil.Role.UserPM : User.UserDataUtil.Role.User
                         })
                         .Where(o=>o.ProjectNo == _uVM.ProjectNo)
                         .ToList();
            }

            return uList;
        }
    }
}
