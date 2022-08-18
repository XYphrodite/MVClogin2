using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVClogin2.Models;
using MVClogin2.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MVClogin2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipiesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public RecipiesController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpGet]
        public JObject GetData()
        {
            JsonFileProductService ProductService = new JsonFileProductService(_env);
            JObject jObject = ProductService.getProductsAsJsonObject();
            return jObject;          
        }
        [HttpPost]
        public IActionResult PostString(string str)
        {
            /*return new View("@{ViewData[\"Title\"] = \"Home Page\";}"+
                    "<div class=\"text-center\">"+
                        "<h1 class=\"display-4\">"+str+"</h1>"+
                    "</div>");*/
            //return new RedirectToPageResult("Index");
            return base.Content("<div class=\"text-center\">" +
                        "<h1 class=\"display-4\">" + str + "</h1>" +
                    "</div>");
        }

    }
}
