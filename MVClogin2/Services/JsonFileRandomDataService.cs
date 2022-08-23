using Microsoft.AspNetCore.Hosting;
using MVClogin2.Models;
using System.IO;
using System.Text.Json;

namespace MVClogin2.Services
{
    public class JsonFileRandomDataService
    {
        public JsonFileRandomDataService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data json", "RandomData.json"); }
        }
        public RandomDataModel GetData()
        {
            StreamReader stream = File.OpenText(JsonFileName);
            var model = JsonSerializer.Deserialize<RandomDataModel>(stream.ReadToEnd());
            stream.Close();
            return model;
        }
    }
}
