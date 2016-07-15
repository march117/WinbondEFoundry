using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DbModel
{
    [MetadataType(typeof(ProjectMD))]
    public partial class Project
    {
        public Project()
        {
            CreateDate = DateTime.Now;
            ProjectNo = Prefix + TmpId.ToString();
        }
    }

    public partial class ProjectMD
    {
        [DisplayName("Project No"), Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ProjectNo { get; set; }

        [DisplayName("Project Name"), Required]
        public string ProjectName { get; set; }

        [DisplayName("Create Date"), Required]
        public Nullable<System.DateTime> CreateDate { get; set; }

        [DisplayName("Last Update Date")]
        public Nullable<System.DateTime> LastUpdate { get; set; }

        [DisplayName("Start Date"), Required]
        public System.DateTime StartDate { get; set; }

        [DisplayName("NDA"), Required]
        public Nullable<long> NDAId { get; set; }

        public long TmpId { get; set; }
        public string Prefix { get; set; }
    }
}
