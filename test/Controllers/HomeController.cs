using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;
using test.Interfaces;
using test.Models;

namespace test.Controllers
{
    //[Route("ezgi/[action]")] override startup.js route 
    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IProductRepository _productRepository;
        private readonly IBasketRepository _basketRepository;
        public HomeController(IProductRepository productRepository, SignInManager<AppUser> signInManager, IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
            _signInManager = signInManager;
            _productRepository = productRepository;
        }
        public IActionResult Index(int? categoryId)//category null da gelebilir
        {
            ViewBag.CategoryId = categoryId;
            //SetSession("admin", "ezgi");
            //SetCookie("admin", "ezgi");
            return View();
        }

        public IActionResult ProductDetail(int id)
        {
            //ViewBag.Cookie = GetCookie("admin");
            //ViewBag.Session = GetSession("admin");
            return View(_productRepository.getId(id));
        }
        public IActionResult Login()
        {
            return View(new UserLoginModel());
        }
        //formdan gelenleri yakalayıp post eder
        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = _signInManager.PasswordSignInAsync(model.userName, model.password, model.RememberMe, false).Result;
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalıdır.");

            }
            return View(model);
        }
        public IActionResult AddBasket(int id)
        {
            var product = _productRepository.getId(id);
            _basketRepository.AddBasket(product);
            TempData["info"] = "Product added to basket!!";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteBasket(int id)
        {
            var product = _productRepository.getId(id);
            _basketRepository.DeleteBasket(product);
            //TempData["info"] = "Product take out of the to basket!!";
            return RedirectToAction("Index");
        }
        public IActionResult Basket()
        {
            return View(_basketRepository.getBasketProducts());
        }
        public IActionResult EmptyBasket(decimal price)
        {

            _basketRepository.EmptyTheBasket();
            return RedirectToAction("Thanks", new { price = price });
        }
        public IActionResult Thanks(decimal price)
        {
            ViewBag.Price = price;
            return View();

        }
        public IActionResult NotFound(int code)
        {
            ViewBag.Code = code;
            return View();
        }
        [Route("/Error")]
        public  IActionResult Error()
        {
            var errorInfo=HttpContext.Features.Get<IExceptionHandlerPathFeature>();//pathi ile bana ilgili hatayı getir
            return View();
        }

        //Cookie kısmı
        //public string GetCookie(string key)
        //{

        //    HttpContext.Request.Cookies.TryGetValue(key, out string value);
        //    return value;

        //}

        //public void SetCookie(string key, string value)
        //{

        //    HttpContext.Response.Cookies.Append(key, value);

        //}
        ////Session
        //public void SetSession(string key, string value)
        //{

        //    HttpContext.Session.SetString(key, value);

        //}
        ////varsa value ya atar yoksa Null döner
        //public string GetSession(string key)
        //{
        //    return HttpContext.Session.GetString(key);

        //}
    }
}
