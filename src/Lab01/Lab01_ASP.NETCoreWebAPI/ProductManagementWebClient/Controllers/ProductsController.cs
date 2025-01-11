using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProductManagementWebClient.Controllers
{
    public class ProductsController : Controller
    {
        HttpClient client;
        string ProductApiUrl = null;
        public ProductsController()
        {
            client = new HttpClient();
            var contenType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contenType);
            ProductApiUrl = "https://localhost:7094/api/Products";
        }
        public async Task<IActionResult> Index(CancellationToken cancellation = default)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl,cancellation);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData,options);
            return View(listProducts);
        }
    }
}
