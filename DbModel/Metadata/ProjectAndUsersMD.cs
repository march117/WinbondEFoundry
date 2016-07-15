using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DbModel
{
    [MetadataType(typeof(ProjectAndUsersMD))]
    public partial class ProjectAndUsers
    {
    }

    public class ProjectAndUsersMD
    {
        [DisplayName("流水號"),Key]
        public long PAUId { get; set; }
        
        [DisplayName("使用者")]
        public long UserId { get; set; }

        [DisplayName("專案")]
        public string ProjectId { get; set; }

        [DisplayName("建立時間")]
        public Nullable<System.DateTime> CreateDate { get; set; }

        [DisplayName("最後更新時間")]
        public Nullable<System.DateTime> LastUpdate { get; set; }

        [DisplayName("是否為專案管理者")]
        public bool IsAdmin { get; set; }

        [DisplayName("專案狀況")]
        public string InProjectStatus { get; set; }

        [DisplayName("開始時間")]
        public System.DateTime StartDate { get; set; }

        [DisplayName("結束時間")]
        public System.DateTime EndDate { get; set; }
    }
}
