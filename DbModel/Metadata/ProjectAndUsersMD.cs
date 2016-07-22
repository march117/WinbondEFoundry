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
        [DisplayName("Serial No"),Key]
        public long PAUId { get; set; }
        
        [DisplayName("User")]
        public long UserId { get; set; }

        [DisplayName("Project")]
        public string ProjectId { get; set; }

        [DisplayName("Create Date")]
        public Nullable<System.DateTime> CreateDate { get; set; }

        [DisplayName("Last Update Date")]
        public Nullable<System.DateTime> LastUpdate { get; set; }

        [DisplayName("Is Admin")]
        public bool IsAdmin { get; set; }

        [DisplayName("In Project Status")]
        public string InProjectStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Start Date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime StartDate { get; set; }

        [DisplayName("End Date"), DisplayFormat(DataFormatString = "yyyy/MM/dd")]
        public System.DateTime EndDate { get; set; }
    }
}
