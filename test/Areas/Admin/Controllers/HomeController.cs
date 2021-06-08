using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;
using test.Interfaces;
using test.Models;

namespace test.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public HomeController(IProductRepository productRepository, SignInManager<AppUser> signInManager, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View(_productRepository.GetList());
        }
        public IActionResult Add()
        {
            return View(new ProductAddModel());
        }
        [HttpPost]
        public IActionResult Add(ProductAddModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                if (model.İmage != null)//resim yükleme işlemi gerçekleştiricez
                {
                    //benzersiz resim adı yüklenicek yer belirtildi
                    var extension = Path.GetExtension(model.İmage.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var uploadPlace = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + newImageName);
                    var stream = new FileStream(uploadPlace, FileMode.Create);
                    model.İmage.CopyTo(stream);
                    product.İmage = newImageName;


                }

                product.Name = model.Name;
                product.Price = model.Price;

                _productRepository.Add(product);
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            }
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var getProduct = _productRepository.getId(id);
            ProductUpdateModel model = new ProductUpdateModel
            {

                Name = getProduct.Name,
                Price = getProduct.Price,
                Id = getProduct.Id

            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(ProductUpdateModel model)
        {

            if (ModelState.IsValid)
            {
                var Updtproduct = _productRepository.getId(model.Id);

                if (model.İmage != null)//resim yükleme işlemi gerçekleştiricez
                {
                    //benzersiz resim adı yüklenicek yer belirtildi
                    var extension = Path.GetExtension(model.İmage.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var uploadPlace = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + newImageName);
                    var stream = new FileStream(uploadPlace, FileMode.Create);
                    model.İmage.CopyTo(stream);
                    Updtproduct.İmage = newImageName;


                }
                Updtproduct.Name = model.Name;
                Updtproduct.Price = model.Price;


                _productRepository.Update(Updtproduct);
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            _productRepository.Delete(new Product { Id = id });
            return RedirectToAction("Index");
        }
        public IActionResult SelectCategory(int id)
        {

            var productctg = _productRepository.GetCategory(id).Select(I => I.Name);
            var categories = _categoryRepository.GetList();

            TempData["ProductId"] = id;

            List<CategorySelectModel> list = new List<CategorySelectModel>();
            foreach (var item in categories)
            {

                CategorySelectModel model = new CategorySelectModel();
                model.categoryId = item.Id;
                model.categoryName = item.Name;
                model.to_be = productctg.Contains(item.Name);//işaretleyip döndür contains içermek itemoı içeriormu objectte karşılamaz

                list.Add(model);
            }

            return View(list);
        }


        [HttpPost]
        public IActionResult SelectCategory(List<CategorySelectModel> list)
        {
            int productId =(int)TempData["ProductId"];

            foreach (var item in list)
            {
                if (item.to_be)
                {
                    _productRepository.AddCategory(new ProductCategory { 
                    
                        CategoryId=item.categoryId,
                        ProductId=productId
                    });

                }
                else
                {
                    _productRepository.DeleteCategory(new ProductCategory
                    {

                        CategoryId = item.categoryId,
                        ProductId = productId
                    });
                }

            }

            return RedirectToAction("Index");
        }
    }

}

