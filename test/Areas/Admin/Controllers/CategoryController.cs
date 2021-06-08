using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;
using test.Interfaces;
using test.Models;

namespace test.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View(_categoryRepository.GetList());
        }
        public IActionResult Add()
        {
            return View(new CategoryAddModel());
        }
        [HttpPost]
        public IActionResult Add(CategoryAddModel model)
        {
            if (ModelState.IsValid)
            {

                _categoryRepository.Add(new Category
                {
                    Name = model.Name
                }); ;
                return RedirectToAction("Index");

            }
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var updateCategory = _categoryRepository.getId(id);
            CategoryUpdateModel model = new CategoryUpdateModel
            {
                Id = updateCategory.Id,
                Name = updateCategory.Name
            };
            return View(model);

        }
        [HttpPost]
        public IActionResult Update(CategoryUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var updateCategory1 = _categoryRepository.getId(model.Id);
                updateCategory1.Name = model.Name;
                _categoryRepository.Update(updateCategory1);
                return RedirectToAction("Index");
            }


            return View(model);
        }
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(new Category
            {
                Id = id

            });
            return RedirectToAction("Index");
        }

    }
}

