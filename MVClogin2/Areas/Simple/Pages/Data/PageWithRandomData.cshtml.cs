using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVClogin2.Services;
using System;
using System.IO;
using System.Threading;

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
            ViewData["path"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            Console.WriteLine(ViewData["path"]);

        }


    }
}
