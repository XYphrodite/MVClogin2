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
using System.Security.Claims;

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
        [Authorize]
        [HttpGet("Data")]
        public IEnumerable<Product> GetData(string id=null)
        {
            //var currentUser = GetCurrentUser();
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
        [AllowAnonymous]
        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi, you're on public property");
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value
                };
            }
            return null;
        }

    }
}
