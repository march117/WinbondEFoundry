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
        [DisplayName("帳號"), Required, RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}")]
        public string UserEmail { get; set; }

        [DisplayName("密碼"), Required]
        public string Password { get; set; }
    }
}
