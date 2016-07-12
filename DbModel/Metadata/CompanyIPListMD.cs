using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace DbModel
{
    [MetadataType(typeof(CompanyIPListMD))]
    public partial class CompanyIPList
    {
        public class CompanyIPListMD
        {
            [DisplayName("公司流水號")]
            [Required]
            public global::System.Int64 CompanyNo { get; set; }
            [DisplayName("IP")]
            [Required]
            public global::System.String Ip { get; set; }
        }
    }
}
