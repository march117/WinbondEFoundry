using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbModel.ViewModel.Project
{
    public class ProjectAndUsersVM
    {
        public long PAUId { get; set; }
        public long UserId { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserEmail { get; set; }
        public string ProjectId { get; set; }
    }
}
