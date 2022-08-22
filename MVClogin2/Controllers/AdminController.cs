using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVClogin2.Models;
using MVClogin2.Sql;
using MVClogin2.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVClogin2.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(ILogger<HomeController> logger, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _env = env;
            _roleManager = roleManager;
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
        [HttpGet]
        public IActionResult CreateNewRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("CreateNewRole", "Admin");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
    }
}

