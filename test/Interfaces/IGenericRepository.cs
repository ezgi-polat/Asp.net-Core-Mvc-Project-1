using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Interfaces
{
    public interface IGenericRepository<Table> where Table : class, new()
    {
        void Add(Table table);
        void Update(Table table);
        void Delete(Table table);
        public List<Table> GetList();
        public Table getId(int id);

    }
}
