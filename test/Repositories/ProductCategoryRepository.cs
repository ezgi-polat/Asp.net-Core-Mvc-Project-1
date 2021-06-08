using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using test.Contexts;
using test.Entities;
using test.Interfaces;


namespace test.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategory getFilter(Expression<Func<ProductCategory, bool>> filter)
        {
            using var context = new TestContext();
            return context.ProductCategories.FirstOrDefault(filter);
        }
    }
}
