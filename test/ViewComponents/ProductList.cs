using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Interfaces;

namespace test.Viewcomponents
{
    public class ProductList : ViewComponent
    {
        private readonly IProductRepository _productRepository;
        public ProductList(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IViewComponentResult Invoke(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                return View(_productRepository.GetCategoryId((int)categoryId));
            }
            return View(_productRepository.GetList());
        }
    }
}
