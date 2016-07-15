using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace DbModel
{
    [MetadataType(typeof(ProjectMD))]
    public partial class Project
    {
        public Project()
        {
            
        }
    }

    public partial class ProjectMD
    {
        [DisplayName("專案編號"),Required,Key]
        public string ProjectNo { get; set; }

        [DisplayName("專案名稱"), Required]
        public string ProjectName { get; set; }

        [DisplayName("建立時間"), Required]
        public Nullable<System.DateTime> CreateDate { get; set; }

        [DisplayName("最後更新時間"), Required]
        public Nullable<System.DateTime> LastUpdate { get; set; }

        [DisplayName("開始時間"), Required]
        public System.DateTime StartDate { get; set; }

        [DisplayName("合約編號"), Required]
        public Nullable<long> NDAId { get; set; }

        public long TmpId { get; set; }
        public string Prefix { get; set; }
    }
}
