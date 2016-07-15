using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DbModel;
using DbModel.ViewModel.Project;
namespace DbModel.Helper.Project
{
    public class ProjectDataHelper
    {
        public static void EditProjectAndSubFunction(int[] subFuncIds,string projectNo,string creater){
            List<UserAndFunction> uafList = new List<UserAndFunction>();
            using(EFoundryContext db = new EFoundryContext()){
                //先刪除所有關聯
                db.UserAndFunction.RemoveRange(db.UserAndFunction.Where(o=>o.ProjectNo == projectNo));
                db.SaveChanges();
                //取得所有跟專案關聯的使用者
                List<ProjectAndUsersView> pauVList = db.ProjectAndUsersView.Where(o => o.ProjectNo == projectNo).ToList();
                foreach (int subFuncId in subFuncIds)
                {
                    foreach (ProjectAndUsersView pauV in pauVList)
                    {
                        UserAndFunction uaf = new UserAndFunction()
                        {
                            ProjectNo = projectNo,
                            SubFuntionNo = subFuncId,
                            UserEmail = pauV.UserEmail,
                            CreaterEmail = creater,
                            Status = "1"
                        };

                        uafList.Add(uaf);
                    }                    
                }
                db.UserAndFunction.AddRange(uafList);
                db.SaveChanges();
            }            
        }
    }
}
