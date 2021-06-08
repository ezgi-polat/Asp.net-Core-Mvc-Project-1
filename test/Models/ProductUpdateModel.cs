using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class ProductUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public IFormFile İmage { get; set; }
    }
}
