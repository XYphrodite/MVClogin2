using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVClogin2.Models;
using MVClogin2.Services;
using MVClogin2.Sql;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace MVClogin2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public ValueController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("Data")]
        public IEnumerable<Product> GetData(string id=null)
        {
            List<Product> products  =(new JsonFileProductService(_env)).GetProducts().ToList();
            if (id == null)
                return products;
            else
            {
                int size = products.Count-1;
                int index;
                if ((int.TryParse(id, out index))&&(index<=size))
                {
                    List<Product> one = new List<Product>();
                    one.Add(products[index]);
                    return one;
                }
                return null; 
            }
        }
        [HttpGet("Name")]
        public string GetName()
        {
            return GetCurrentUser().username;
        }
        [AllowAnonymous]
        [HttpPost("Public")]
        public string Public(string d)
        {
            RandomDataModel randomData = new RandomDataModel
            {
                data = d
            };
            try
            {
                StreamWriter Writer = new StreamWriter(Path.Combine(_env.WebRootPath, "Data json", "RandomData.json"));
                Writer.Write(JsonConvert.SerializeObject(randomData));
                Writer.Close();
                return "Success";
            }
            catch
            {
                return "Error";
            }
        }

        [HttpPost("UploadCalibration")]
        public string UploadCalibration([FromBody] CalibrationModel model)
        {
            SqlWorker sqlWorker = new SqlWorker();
            sqlWorker.initialize(_env);
            sqlWorker.tryConnect();
            model.UserId=sqlWorker.getIdByUsername(GetCurrentUser().username);
            if (sqlWorker.InsertCalibration(model))
                return "Success";
            return "Error";
        }

        [AllowAnonymous]
        [HttpGet("Value")]
        public string GetValue()
        {
            return (new JsonFileRandomDataService(_env)).GetData().data;
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
