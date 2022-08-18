using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVClogin2.Models;
using MVClogin2.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MVClogin2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public ValueController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpGet]
        public IEnumerable<Product> GetData(string id=null)
        {
            JsonFileProductService ProductService = new JsonFileProductService(_env);
            List<Product> products  = ProductService.GetProducts().ToList();
            if (id == null)
                return products;
            else
            {
                int size = products.Count-1;
                int index;
                if ((int.TryParse(id, out index))&&(index<=size))
                {
                    List<Product> one = new List<Product>();
                    one.Add(products[index]);
                    return one;
                }
                return null; 
            }
        }
        [HttpPost]
        public IActionResult PostString(string str)
        {
            return base.Content("<div class=\"text-center\">" +
                        "<h1 class=\"display-4\">" + str + "</h1>" +
                    "</div>");
        }

    }
}
