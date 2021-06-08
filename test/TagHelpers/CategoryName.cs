using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Interfaces;

namespace test.TagHelpers
{
   
    [HtmlTargetElement("getCategoryName")]
    public class CategoryName : TagHelper
    {
        private readonly IProductRepository _productRepository
;
        public CategoryName(IProductRepository productRepository
)
        {

            _productRepository = productRepository;
            ;
        }
        public int ProductId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //TagBuilder builder = new TagBuilder("ul");
            //StringBuilder builder2 = new StringBuilder();
            //builder2.Append()
            string data = "";
            var buyCategories = _productRepository.GetCategory(ProductId).Select(I => I.Name);
            foreach (var item in buyCategories)
            {
                data += item + " ";
            }
            output.Content.SetContent(data);
        }
    }
}


