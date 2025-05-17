using Infra.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helpers
{
    public class FileHelper
    {
        public static async Task<List<ProductEntity>> ReaderAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found!");
            }
            var data = await File.ReadAllTextAsync(filePath);
            //deserializer
           return JsonConvert.DeserializeObject<List<ProductEntity>>(data) ?? new List<ProductEntity>();
        }
    }
}
