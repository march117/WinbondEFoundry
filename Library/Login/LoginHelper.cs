using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Reflection;
using Newtonsoft.Json;
using DbModel.ViewModel.User;
using DbModel.ViewModel.Login;
using DbModel.Util;
using DbModel.Util.User;
using DbModel;
using DbModel.ViewModel.SubFunction;
using DbModel.Util.Function;

namespace Library.Login
{
    public class LoginHelper
    {
        private LoginVM _loginView;
        private UserProfile _user;
        private UserProfileVM _uVM;
        private Boolean isLogin;
        private string _encryptTicket;

        /// <summary>
        /// 建構子，並且比對使用者資料
        /// </summary>
        /// <param name="lv">使用者輸入資料</param>
        public LoginHelper(LoginVM lv)
        {
            _loginView = lv;
            GetUser();
            CheckPW();            
        }

        /// <summary>
        /// 登入是否成功
        /// </summary>
        /// <returns>登入狀態</returns>
        public bool Login()
        {
            return isLogin;
        }

        /// <summary>
        /// 取得登入使用者
        /// </summary>
        /// <returns>使用者</returns>
        public UserProfile User()
        {
            return _user;
        }

        /// <summary>
        /// 取得使用者View Model
        /// </summary>
        /// <returns></returns>
        public UserProfileVM UserViewModel()
        {
            _uVM = new UserProfileVM();
            _uVM.UserEmail = _user.UserEmail;
            _uVM.UserId = _user.UserId;
            _uVM.CompanyNo = _user.CompanyNo;
            _uVM.ProjectNo = _loginView.ProjectNo;
            //取得登入角色
            GetUserRole();
            //取得次選單
            GetSubFunction();

            return _uVM;
        }

        /// <summary>
        /// 取得加密的Ticket
        /// </summary>
        /// <returns>Encrypt Ticket</returns>
        public string GetEncrptyTicket()
        {
            UserViewModel();
            EncryptTicket();
            return _encryptTicket;
        }

        #region priavete function

        /// <summary>
        /// 取得User
        /// </summary>
        private void GetUser()
        {
            _user = DataUtil.GetItem<UserProfile>(delegate(UserProfile u)
            {
                return u.UserEmail == _loginView.UserEmail;
            });
        }

        /// <summary>
        /// 確認密碼
        /// </summary>
        private void CheckPW()
        {
            if (_user != null && _user.IsActive)
            {
                string pwd = SecureUtil.Decrypt(_user.UserPwd);
                isLogin = pwd == _loginView.Password;
            
                if (!isLogin)
                {
                    //登入失敗清除使用者
                    _user = null;                    
                }
            }
            
        }

        /// <summary>
        /// Create FormAuthenticationTickek並且Encrypt
        /// </summary>
        private void EncryptTicket()
        {
            if (_user != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                                                            _user.UserEmail,
                                                                            DateTime.Now,
                                                                            DateTime.Now.AddMinutes(30),
                                                                            false,
                                                                            JsonConvert.SerializeObject(_loginView));                
                _encryptTicket = FormsAuthentication.Encrypt(ticket);
            }
        }

        /// <summary>
        /// 取得登入角色
        /// </summary>
        void GetUserRole()
        {
            UserDataUtil.Role r;
            ProjectAndUsers pau = DataUtil.GetItem<ProjectAndUsers>(delegate(ProjectAndUsers model)
            {
                return model.ProjectId == _loginView.ProjectNo && model.UserId == _user.UserId;
            });

            if (pau.IsAdmin)
            {
                r = UserDataUtil.Role.UserPM;
            }
            else
            {
                r = UserDataUtil.Role.User;
            }
            _uVM.UserRole = r;
        }

        /// <summary>
        /// 取得登入使用者所有次選單
        /// </summary>
        /// <returns>SubFunction List</returns>
        void GetSubFunction()
        {
            List<SubFunctionVM> sfList;
            if (_uVM.UserRole == UserDataUtil.Role.User)
            {                
                sfList = DataUtil.GetList<ProjectAndSubFunctionView>(delegate(ProjectAndSubFunctionView pasV)
                {
                    return pasV.UserId == _uVM.UserId && pasV.ProjectNo == _uVM.ProjectNo;
                }).Select(o => new SubFunctionVM
                {
                    MainFunctionId = o.MainFunctionId,
                    ParentFunctionId = o.ParentFunctionId,
                    SubFunctionId = o.SubFunctionId,
                    SubFunctionName = o.SubFunctionName
                }).ToList();
            }
            else
            {
                sfList = DataUtil.GetList<SubFunction>(delegate(SubFunction model)
                {
                    return model.MainFunctionId != 3;
                }).Select(o => new SubFunctionVM
                {
                    MainFunctionId = o.MainFunctionId,
                    ParentFunctionId = o.ParentFunctionId,
                    SubFunctionId = o.SubFunctionId,
                    SubFunctionName = o.SubFunctionName
                }).ToList();
            }
            _uVM.SubFunctionList = sfList;
            
            //加上Admin SubFunction
            _uVM.AdminFunctionList = GetAdminSubFunction();
            
        }

        /// <summary>
        /// 透過角色，取得Admin Function
        /// </summary>
        /// <returns>SubFunction List</returns>
        List<AdminFunctionVM> GetAdminSubFunction()
        {
            List<long> funcNo = GetRoleFunctionNo();
            List<AdminFunctionVM> subFunc = DataUtil.GetList<SubFunction>(delegate(SubFunction sf)
            {
                return sf.MainFunctionId == 3 && funcNo.Contains(sf.SubFunctionId);
            }).Select(o => new AdminFunctionVM
            {
                SubFunctionId = o.SubFunctionId,
                SubFunctionName = o.SubFunctionName,
                Url = AdminFunctionDataUtil.GetUrl(o)
            })
            .ToList();
            return subFunc;
        }

        /// <summary>
        /// 從屬性取得Admin Function Id
        /// </summary>
        /// <returns>Admin Function Id</returns>
        List<long> GetRoleFunctionNo()
        {
            FieldInfo fi = typeof(UserDataUtil.Role).GetField(_uVM.UserRole.ToString());
            AdminFunctionAttribute afa = (AdminFunctionAttribute)Attribute.GetCustomAttribute(fi, typeof(AdminFunctionAttribute));
            return afa.FuncNo;
        }

        #endregion
    }
}
