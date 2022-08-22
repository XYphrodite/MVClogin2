using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVClogin2.Models;
using MVClogin2.Sql;
using System.Collections.Generic;

namespace MVClogin2.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;


        public AdminController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        public IActionResult ListOfMembers()
        {
            SqlWorker sqlWorker = new SqlWorker();
            sqlWorker.initialize(_env);
            sqlWorker.tryConnect();
            List<UserModel> users;
            users = sqlWorker.getListOfMembers();
            ViewData["members"] = users;
            return View();
        }
    }
}
