using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DbModel.ViewModel.SubFunction;

namespace DbModel.Util.Function
{
    public class AdminFunctionDataUtil
    {
        public enum AdminFunctionCategory:long
        {           
            [AdminFunctionUrl("/Admin/ProjectMember")]
            ProjectMember = 7,
            [AdminFunctionUrl("/Admin/AssignPermission")]
            AssignPermission = 8,
            [AdminFunctionUrl("/Admin/ManageGroups")]
            ManageGroups = 9,
            [AdminFunctionUrl("/Admin/Search")]
            Search = 10,
            [AdminFunctionUrl("/Admin/QueryDownloadLog")]
            QueryDownloadLog = 11,
            [AdminFunctionUrl("/Admin/ManageCustomer")]
            ManageCustomer = 12,
            [AdminFunctionUrl("/Admin/DocumentAttribute")]
            DocumentAttribute = 13,
            [AdminFunctionUrl("/Admin/ManageProjectManager")]
            ManageProjectManager = 14,
            [AdminFunctionUrl("/Admin/AssignFunctionPermission")]
            AssignFunctionPermission = 15,
            [AdminFunctionUrl("/Admin/QueryStock")]
            QueryStock = 16
        }

        public static string GetUrl(SubFunction subFunc)
        {
            long subFuncId = subFunc.SubFunctionId;
            IEnumerable<long> ids = Enum.GetValues(typeof(AdminFunctionCategory)).Cast<long>();
            AdminFunctionCategory afc;
            if (ids.Any(o => o == subFuncId))
            {
                afc = (AdminFunctionCategory)Enum.ToObject(typeof(AdminFunctionCategory), subFuncId);
                FieldInfo info = typeof(AdminFunctionCategory).GetField(afc.ToString());
                AdminFunctionUrlAttribute urlAttr = (AdminFunctionUrlAttribute)Attribute.GetCustomAttribute(info, typeof(AdminFunctionUrlAttribute));
                return urlAttr.GetUrl;
            }

            return null;
        }
    }

    public class AdminFunctionUrlAttribute : Attribute
    {
        private string _url;
        public AdminFunctionUrlAttribute(string url)
        {
            _url = url;
        }

        public string GetUrl{
            get
            {
                return _url;
            }
        }
    }
}
