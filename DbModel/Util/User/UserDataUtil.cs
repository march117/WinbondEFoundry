using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DbModel.ViewModel.User;
using DbModel.ViewModel.SubFunction;

namespace DbModel.Util.User
{
    public class UserDataUtil
    {
        public static string SessionKey = "loginUser";
        /// <summary>
        /// 使用者角色
        /// </summary>
        public enum Role
        {
            [AdminFunction(13,14,15,16)]
            SystemAdmin =1,
            [AdminFunction(9,11,12)]
            WinbondPM = 2,
            [AdminFunction(7,8,9,10)]
            UserPM = 3,
            [AdminFunction(7,10)]
            User = 4
        }        
    }

    public class AdminFunctionAttribute : Attribute
    {
        private List<long> _funcNo;
        public AdminFunctionAttribute(params long[] no)
        {
            _funcNo = no.ToList();
        }

        public List<long> FuncNo
        {
            get
            {
                return _funcNo;
            }
        }
    }
}
