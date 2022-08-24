using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVClogin2.Models;
using MVClogin2.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection.Emit;
using System.Threading;
using System.Timers;
using Timer = System.Threading.Timer;

namespace MVClogin2.Areas.Simple.Pages.Data
{
    public class PageWithRandomDataModel : PageModel
    {
        public string data { get; set; }
        public readonly IWebHostEnvironment _env;
        
        public PageWithRandomDataModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
            var stateTimer = new Timer(MyMethod, null, 200, 1000);
            ViewData["path"] = Path.Combine(_env.WebRootPath, "Data json", "RandomData.json");
            ViewData["c"] = (new JsonFileRandomDataService(_env)).GetData().data;
            data = (new JsonFileRandomDataService(_env)).GetData().data;
        }

        private void MyMethod(object state)
        {
            ViewData["c"] = (new JsonFileRandomDataService(_env)).GetData().data;
            Console.WriteLine(ViewData["c"]);
            data = (new JsonFileRandomDataService(_env)).GetData().data;
        }
    }
}
