using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVClogin2.Models;
using MVClogin2.Sql;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MVClogin2.Areas.Simple.Pages.Data
{
    public class MyCalibrationsModel : PageModel
    {
        public List<CalibrationModel> models;
        private readonly IWebHostEnvironment _env;
        public MyCalibrationsModel(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void OnGet()
        {
            try
            {
                SqlWorker sqlWorker = new SqlWorker(_env);
                models = sqlWorker.getCalibrations(GetCurrentUser().username);
            }
            finally { }
            
        }
        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                };
            }
            return null;
        }
    }
}
