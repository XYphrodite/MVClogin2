using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVClogin2.Models;
using MVClogin2.Services;
using System.Collections.Generic;

namespace MVClogin2.Pages
{
    public class JsonModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public JsonModel(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void OnGet()
        {
            JsonFileProductService ProductService = new JsonFileProductService(_env);
            IEnumerable<Product> products = ProductService.GetProducts();
            ViewData["Products"] = products;
        }
    }
}
