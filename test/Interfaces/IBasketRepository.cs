using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;

namespace test.Interfaces
{
    public interface IBasketRepository
    {
        void AddBasket(Product product);
        void DeleteBasket(Product product);
        List<Product> getBasketProducts();
        void EmptyTheBasket();
    }
}
