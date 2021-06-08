using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;
using test.Interfaces;

namespace test.Repositories
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
    }
}
