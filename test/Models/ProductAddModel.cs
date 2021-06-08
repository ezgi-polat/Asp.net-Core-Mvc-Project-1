using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class ProductAddModel
    {

        [Required(ErrorMessage = "Name isnt empty")]
        public string Name { get; set; }
        [Range(0,double.MaxValue,ErrorMessage = "Price must be highest 0.")]
        public decimal Price { get; set; }

        public IFormFile İmage { get; set; }

     
    }
}
