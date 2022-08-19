using Microsoft.AspNetCore.Hosting;
using MVClogin2.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MVClogin2.Services
{
    public class JsonFileSqlConstService
    {
        public JsonFileSqlConstService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data json", "SqlConst.json"); }
        }
        public SqlModel GetSqlConst()
        {
            StreamReader stream = File.OpenText(JsonFileName);   
            return JsonSerializer.Deserialize<SqlModel>(stream.ReadToEnd());

        }
    }
}
