using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;

namespace test.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
    }
}
