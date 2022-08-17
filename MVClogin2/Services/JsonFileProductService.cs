using Microsoft.AspNetCore.Hosting;
using MVClogin2.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MVClogin2.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get;}
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data json", "products.json"); }
        }
        public IEnumerable<Product> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
            }
        }
        //public string getProducts()
        //{
        //    StreamReader stream = File.OpenText(JsonFileName);
        //    string json = stream.ReadToEnd();
        //    return json;
        //}

    }
}
