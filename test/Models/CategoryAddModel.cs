using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class CategoryAddModel
    {

        [Required(ErrorMessage = "Name isnt empty")]
        public string Name { get; set; }
    }
}
