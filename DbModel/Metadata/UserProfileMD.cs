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
        [DisplayName("Serial No"), Required, Key]
        public long UserId { get; set; }

        [DisplayName("Email"), Required, RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}")]
        public string UserEmail { get; set; }

        [DisplayName("Company"), Required]
        public long CompanyNo { get; set; }

        [DisplayName("Password"), Required,DataType(DataType.Password)]
        public string UserPwd { get; set; }

        [DisplayName("Confirm Password"), Required,DataType(DataType.Password), Compare("UserPwd")]
        public string CheckPw { get; set; }

        [DisplayName("Activate"), Required]
        public bool IsActive { get; set; }

        [DisplayName("Creator"), Required]
        public Nullable<long> Creater { get; set; }

        [DisplayName("Last Login Date")]
        public Nullable<System.DateTime> LastLoginDate { get; set; }

        [DisplayName("Create Date"), Required]
        public Nullable<System.DateTime> CreateDate { get; set; }

        [DisplayName("Last Update")]
        public Nullable<System.DateTime> LastUpdate { get; set; }

        [DisplayName("Extension")]
        public string ExtensinNo { get; set; }

        [DisplayName("Phone Number"), Required]
        public string PhoneNo { get; set; }

        [DisplayName("Expire Date")]
        public Nullable<System.DateTime> ExpireDate { get; set; }

        [DisplayName("Status"), Required]
        public string Status { get; set; }
    }
}
