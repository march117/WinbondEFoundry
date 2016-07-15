using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DbModel
{
    [MetadataType(typeof(NDAsMD))]
    public partial class NDAs
    {

    }

    public  class NDAsMD
    {
        [DisplayName("流水號"),Required,Key]
        public long NDAId { get; set; }

        [DisplayName("編號"), Required]
        public string NDANo { get; set; }

        [DisplayName("名稱"), Required]
        public string NDAName { get; set; }

        [DisplayName("開始時間"), Required]
        public System.DateTime StartDate { get; set; }

        [DisplayName("結束時間"), Required]
        public System.DateTime EndDate { get; set; }

        [DisplayName("公司"), Required]
        public string CompanyNo { get; set; }

        [DisplayName("建立時間")]
        public Nullable<System.DateTime> CreateDate { get; set; }

        [DisplayName("最後更新時間")]
        public Nullable<System.DateTime> LastUpdate { get; set; }
    }
}
