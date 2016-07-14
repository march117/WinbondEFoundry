using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Newtonsoft.Json;
using DbModel.ViewModel.Login;
using DbModel.Helper;
using DbModel.Util;
using DbModel;

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
            EncryptTicket();
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
            return _encryptTicket;
        }

        #region priavete function

        /// <summary>
        /// 取得User
        /// </summary>
        private void GetUser()
        {           
            _user = DbHelper.GetItem<UserProfile>(_loginView.UserEmail);
        }

        /// <summary>
        /// 確認密碼
        /// </summary>
        private void CheckPW()
        {
            string pwd = SecureUtil.Decrypt(_user.UserPwd);
            isLogin =  pwd == _loginView.Password;

            //登入失敗清除使用者
            if (!isLogin)
            {
                _user = null;
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
                                                                            JsonConvert.SerializeObject(_user));
                _encryptTicket = FormsAuthentication.Encrypt(ticket);
            }
        }

        #endregion
    }
}
