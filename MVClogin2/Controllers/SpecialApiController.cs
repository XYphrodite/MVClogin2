using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVClogin2.Sql;

namespace MVClogin2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialApiController : ControllerBase
    {
        private ISqlWorker sqlWorker;
        public SpecialApiController(ISqlWorker sqlWorker)
        {
            this.sqlWorker = sqlWorker;
        }
        [HttpGet]
        public string AddDefault(string password)
        {
            if (password == "12345")
            {
                sqlWorker.AddDefault();
                return "Success";
            }
            return "Error";
        }
    }
}
