using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using test.Entities;

namespace test.Interfaces
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategory>
    {
        //Bunu araştır!!!

        ProductCategory getFilter(Expression<Func<ProductCategory, bool>> filter);
        
    }
}

