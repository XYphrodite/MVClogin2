using Microsoft.AspNetCore.Hosting;
using MVClogin2.Models;
using Newtonsoft.Json.Linq;
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
        public string getProductsAsString()
        {
            StreamReader stream = File.OpenText(JsonFileName);
            return stream.ReadToEnd();
        }
        public JObject getProductsAsJsonObject()
        {
            StreamReader stream = File.OpenText(JsonFileName);
            string str = stream.ReadToEnd();
            JObject jObject = JObject.Parse(str);
            return jObject;
        }

    }
}
