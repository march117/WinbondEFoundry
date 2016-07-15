using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DbModel.ViewModel.Login
{
    public class LoginVM
    {
        [DisplayName("User Email"), Required, RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}")]
        public string UserEmail { get; set; }

        [DisplayName("Password"), Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        [DisplayName("Project"),Required]
        public string ProjectNo { get; set; }
    }
}
