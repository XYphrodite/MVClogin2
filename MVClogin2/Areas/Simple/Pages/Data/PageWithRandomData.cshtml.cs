using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVClogin2.Models;
using MVClogin2.Services;

namespace MVClogin2.Areas.Simple.Pages.Data
{
    public class PageWithRandomDataModel : PageModel
    {
        public string data;
        private readonly IWebHostEnvironment _env;
        public PageWithRandomDataModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
            JsonFileRandomDataService service = new JsonFileRandomDataService(_env);
            RandomDataModel model = service.GetData();
            data = model.data;
        }
    }
}
