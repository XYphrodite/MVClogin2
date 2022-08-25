using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVClogin2.Services;
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
            var stateTimer = new Timer(MyMethod, null, 200, 1000);
            data = (new JsonFileRandomDataService(_env)).GetData().data;
        }

        private void MyMethod(object state)
        {
            try
            {
                data = (new JsonFileRandomDataService(_env)).GetData().data;
            }
            finally { }
        }
    }
}
