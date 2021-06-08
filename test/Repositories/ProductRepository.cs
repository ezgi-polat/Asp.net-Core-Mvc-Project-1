using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Contexts;
using test.Entities;
using test.Interfaces;

namespace test.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductRepository(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }


        //3 Tablo birleşti.İnner Join
        public List<Category> GetCategory(int ProductId)
        {
            using var context = new TestContext();
            return context.Products.Join(context.ProductCategories, Product => Product.Id, ProductCategory => ProductCategory.ProductId, (u, uc) => new
            {
                Product = u,
                ProductCategory = uc
            }).Join(context.Categories, to => to.ProductCategory.CategoryId, category => category.Id, (uc, k) => new
            {
                Product = uc.Product,
                Category = k,
                ProductCategory = uc.ProductCategory


            }).Where(I => I.Product.Id == ProductId).Select(I => new Category
            {
                Name = I.Category.Name,
                Id = I.Category.Id
            }
            ).ToList();
        }


        public void AddCategory(ProductCategory productCategory)
        {
            var control = _productCategoryRepository.getFilter(I => I.CategoryId == productCategory.CategoryId && I.ProductId == productCategory.ProductId);
            if (control == null)
            {
                _productCategoryRepository.Add(productCategory);
            }
        }

        public void DeleteCategory(ProductCategory productCategory)
        {
            var control = _productCategoryRepository.getFilter(I => I.CategoryId == productCategory.CategoryId && I.ProductId == productCategory.ProductId);
            if (control != null)
            {
                _productCategoryRepository.Delete(control);
            }
        }


        public List<Product> GetCategoryId(int categoryId)
        {
            using var context = new TestContext();
           return context.Products.Join(context.ProductCategories, u => u.Id, uc => uc.ProductId, (Product, ProductCategory) => new
            {
                Product = Product,
                ProductCategory = ProductCategory
            }).Where(I => I.ProductCategory.CategoryId == categoryId).Select(I => new Product
            {
                Id=I.Product.Id,
                Name=I.Product.Name,
                Price=I.Product.Price,
                İmage=I.Product.İmage
            }).ToList();
        }
    }
}
