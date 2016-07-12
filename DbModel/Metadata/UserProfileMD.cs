using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DbModel
{
    [MetadataType(typeof(UserProfileMD))]
    public partial class UserProfile
    {
        public UserProfile()
        {
            CreateDate = DateTime.Now;
        }
        public string CheckPw { get; set; }
    }

    public class UserProfileMD
    {
        [DisplayName("Email"), Required, RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}"), Key]
        public string UserEmail { get; set; }

        [DisplayName("公司流水號"), Required]
        public long CompanyNo { get; set; }

        [DisplayName("密碼"), Required]
        public string UserPwd { get; set; }

        [DisplayName("確認密碼"), Required, Compare("UserPwd")]
        public string CheckPw { get; set; }

        [DisplayName("是否啟用"), Required]
        public bool IsActive { get; set; }

        [DisplayName("建立者"), Required]
        public string Creater { get; set; }

        [DisplayName("最後登入時間")]
        public Nullable<System.DateTime> LastLoginDate { get; set; }

        [DisplayName("建立時間"), Required]
        public System.DateTime CreateDate { get; set; }

        [DisplayName("最後更新時間")]
        public Nullable<System.DateTime> LastUpdate { get; set; }

        public string ExtensinNo { get; set; }

        [DisplayName("電話號碼"), Required]
        public string PhoneNo { get; set; }

        [DisplayName("是否為內部使用者"), Required]
        public bool IsInternal { get; set; }

        [Required]
        public string ContraceNo { get; set; }

        [DisplayName("帳號過期時間")]
        public Nullable<System.DateTime> ExpireDate { get; set; }

        [DisplayName("承包商"), Required]
        public string Contractor { get; set; }

        [DisplayName("狀態"), Required]
        public string Status { get; set; }
    }
}
