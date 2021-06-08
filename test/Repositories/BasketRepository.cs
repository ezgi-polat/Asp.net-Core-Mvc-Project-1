using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.CustomExtensions;
using test.Entities;
using test.Interfaces;

namespace test.Repositories
{
    public class BasketRepository:IBasketRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BasketRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }
        public void AddBasket(Product product)
        {
           var list= _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("basket");
            if (list == null)
            {
                list = new List<Product>();
                list.Add(product);
            }
            else
            {
                list.Add(product);
            }
            _httpContextAccessor.HttpContext.Session.SetObject<List<Product>>("basket",list);
        }
        public void DeleteBasket(Product product)
        {
            var list = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("basket");
            list.Remove(product);
            _httpContextAccessor.HttpContext.Session.SetObject("basket",list);
        }
        public List<Product> getBasketProducts()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("basket");

        }
        public void EmptyTheBasket()
        {
      _httpContextAccessor.HttpContext.Session.Remove("basket");

        }
    }
}
