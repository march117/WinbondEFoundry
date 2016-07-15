using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbModel.Helper.Function
{
    public class MainFunctionHelper
    {
        private static List<MainFunction> _mfList;
        private static MainFunctionHelper _instance;
        private static object _sync = new object();
        /// <summary>
        /// 取得Helper物件
        /// </summary>
        /// <returns></returns>
        public static MainFunctionHelper Instance()
        {            
            //Singelton
            if (_instance == null)
            {
                lock (_sync)
                {
                    if (_instance == null)
                    {
                        _instance = new MainFunctionHelper();
                    }                    
                }
                
            }

            return _instance;
        }

        /// <summary>
        /// 取得Main Function List
        /// </summary>
        /// <returns></returns>
        public List<MainFunction> MainFunctionList()
        {
            if (_mfList == null)
            {
                GetMainFunction();
            }
               
            return _mfList;
        }

        /// <summary>
        /// 重整列表
        /// </summary>
        public void ReloadList()
        {
            GetMainFunction();
        }

        private void GetMainFunction()
        {
            _mfList = DbHelper.GetList<MainFunction>();
        }
    }
}
