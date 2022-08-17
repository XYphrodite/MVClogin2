using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVClogin2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipiesController : ControllerBase
    {
        [HttpGet]
        public ContentResult GetString()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                //StatusCode = (int)HttpStatusCode.OK,
                Content = "<html>" +
                "<body>Hello World</body>" +
                "</html>"
            };
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
