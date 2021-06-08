using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage="Username isnt empty")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Password isnt empty")]
        public string password { get; set; }
        public bool RememberMe { get; set; }

    }
}
