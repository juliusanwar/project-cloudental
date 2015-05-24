using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.ViewModel
{
    public class UserWithRoleViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
}