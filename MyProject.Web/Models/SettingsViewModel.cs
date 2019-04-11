using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Web.Models
{
    public class SettingsViewModel
    {
        public string OldPass { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}