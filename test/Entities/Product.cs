using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;

namespace test.Entities
{
    //[Dapper.Contrib.Extensions.Table("Products")]
    public class Product : IEquatable<Product>
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string İmage { get; set; }

        public decimal Price { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

        public bool Equals([AllowNull] Product other)//eşit olup olmadığına karar verme bir nesneyi başka nesne ile ile kıyaslarken
        {
            return Id == other.Id && Name == other.Name && İmage == other.İmage && Price == other.Price;
        }

    }
}

