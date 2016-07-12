using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DbModel
{
    [MetadataType(typeof(CompanyProfileMD))]
    public partial class CompanyProfile
    {
        public CompanyProfile()
        {
            CreateDate = DateTime.Now;
        }        
    }

    public class CompanyProfileMD
    {
        [DisplayName("公司流水號"), Required, Key]
        public int CompanyNo { get; set; }

        [DisplayName("建立時間"), Required]
        public System.DateTime CreateDate { get; set; }

        [DisplayName("最後更新時間")]
        public Nullable<System.DateTime> LastUpdate { get; set; }

        [DisplayName("公司名稱"), Required]
        public string CompanyName { get; set; }

        [DisplayName("公司電話")]
        public string CompanyTel { get; set; }

        [DisplayName("公司地址")]
        public string CompanyAddr { get; set; }

        [DisplayName("網域")]
        public string DomainName { get; set; }
    }
}
