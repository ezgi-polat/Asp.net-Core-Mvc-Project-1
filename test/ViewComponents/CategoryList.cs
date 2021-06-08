using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Interfaces;

namespace test.ViewComponents 
{
    public class CategoryList : ViewComponent
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryList(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(_categoryRepository.GetList());
        }

    }
}
