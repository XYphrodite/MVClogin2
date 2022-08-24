using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVClogin2.Models;
using MVClogin2.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;


namespace MVClogin2.Controllers
{

    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Data()
        {
            return View();
        }
        [Authorize]
        public IActionResult JSON()
        {
            JsonFileProductService ProductService = new JsonFileProductService(_env);
            IEnumerable<Product> products = ProductService.GetProducts();
            ViewData["Products"] = products;
            return View();
        }

        [AllowAnonymous]
        public IActionResult RefreshData()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
