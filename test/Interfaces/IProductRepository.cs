using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;

namespace test.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public List<Category> GetCategory(int ProductId);
        void AddCategory(ProductCategory productCategory);
        void DeleteCategory(ProductCategory productCategory);
        List<Product> GetCategoryId(int categoryId);

    }
}
