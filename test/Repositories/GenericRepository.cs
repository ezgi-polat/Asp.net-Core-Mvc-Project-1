using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Contexts;

namespace test.Repositories
{

        public class GenericRepository<TEntity> where TEntity : class, new()
        {
            public void Add(TEntity table)
            {
                using var context = new TestContext();
                context.Set<TEntity>().Add(table);
                context.SaveChanges();

            }
            public void Update(TEntity table)
            {
                using var context = new TestContext();
                context.Set<TEntity>().Update(table);
                context.SaveChanges();

            }
            public void Delete(TEntity table)
            {
                using var context = new TestContext();
                context.Set<TEntity>().Remove(table);
                context.SaveChanges();

            }
            public List<TEntity> GetList()
            {
                using var context = new TestContext();
                return context.Set<TEntity>().ToList();

            }
            public TEntity getId(int id)
            {
                using var context = new TestContext();
                return context.Set<TEntity>().Find(id);
            }
        }
    }
