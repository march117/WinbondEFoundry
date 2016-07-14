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
        [DisplayName("使用者流水號"), Required, Key]
        public long UserId { get; set; }

        [DisplayName("Email"), Required, RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}")]
        public string UserEmail { get; set; }

        [DisplayName("公司"), Required]
        public long CompanyNo { get; set; }

        [DisplayName("密碼"), Required,DataType(DataType.Password)]
        public string UserPwd { get; set; }

        [DisplayName("確認密碼"), Required,DataType(DataType.Password), Compare("UserPwd")]
        public string CheckPw { get; set; }

        [DisplayName("是否啟用"), Required]
        public bool IsActive { get; set; }

        [DisplayName("建立者"), Required]
        public Nullable<long> Creater { get; set; }

        [DisplayName("最後登入時間")]
        public Nullable<System.DateTime> LastLoginDate { get; set; }

        [DisplayName("建立時間"), Required]
        public System.DateTime CreateDate { get; set; }

        [DisplayName("最後更新時間")]
        public Nullable<System.DateTime> LastUpdate { get; set; }

        [DisplayName("分機號碼")]
        public string ExtensinNo { get; set; }

        [DisplayName("電話號碼"), Required]
        public string PhoneNo { get; set; }

        [DisplayName("帳號過期時間")]
        public Nullable<System.DateTime> ExpireDate { get; set; }

        [DisplayName("狀態"), Required]
        public string Status { get; set; }
    }
}
