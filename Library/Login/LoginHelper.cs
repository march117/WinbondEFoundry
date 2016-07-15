using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Newtonsoft.Json;
using DbModel.ViewModel.User;
using DbModel.ViewModel.Login;
using DbModel.Helper;
using DbModel.Util;
using DbModel;
using DbModel.ViewModel.SubFunction;

namespace Library.Login
{
    public class LoginHelper
    {
        private LoginVM _loginView;
        private UserProfile _user;
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
        /// 取得加密的Ticket
        /// </summary>
        /// <returns>Encrypt Ticket</returns>
        public string GetEncrptyTicket()
        {
            EncryptTicket();
            return _encryptTicket;
        }

        #region priavete function

        /// <summary>
        /// 取得User
        /// </summary>
        private void GetUser()
        {
            _user = DbHelper.GetItem<UserProfile>(delegate(UserProfile u)
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
                UserProfileVM uVM = new UserProfileVM();
                uVM.UserEmail = _user.UserEmail;
                uVM.UserId = _user.UserId;
                uVM.ProjectNo = _loginView.ProjectNo;
                uVM.SubFunctionList = DbHelper.GetList<ProjectAndSubFunctionView>(delegate(ProjectAndSubFunctionView pasV)
                                        {
                                            return pasV.UserEmail == _user.UserEmail && pasV.ProjectNo == _loginView.ProjectNo;
                                        }).Select(o => new SubFunctionVM
                                        {
                                            MainFunctionId = o.MainFunctionId,
                                            ParentFunctionId = o.ParentFunctionId,
                                            SubFunctionId = o.SubFunctionId,
                                            SubFunctionName = o.SubFunctionName
                                        }).ToList();

                
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                                                            _user.UserEmail,
                                                                            DateTime.Now,
                                                                            DateTime.Now.AddMinutes(30),
                                                                            false,
                                                                            JsonConvert.SerializeObject(uVM));
                _encryptTicket = FormsAuthentication.Encrypt(ticket);
            }
        }

        #endregion
    }
}
