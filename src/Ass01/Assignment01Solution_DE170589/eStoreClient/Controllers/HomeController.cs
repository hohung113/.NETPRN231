using BusinessObject.Entity;
using eStoreClient.JsonHelper;
using eStoreClient.Models;
using eStoreClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;
        public HomeController(ILogger<HomeController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string? productName)
        {

            var result = await _apiService.GetFromApiAsync<object>("api/Product");
            if (!String.IsNullOrEmpty(productName))
            {
                result = await _apiService.GetFromApiAsync<object>($"api/Product/name/{productName}");
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var productList = JsonHelperOption.DeserializeList<Product>(result.ToString());

            ViewBag.ProductName = productName;
            return View(productList);
        }
        public async Task<IActionResult> Cate()
        {
            var result = await _apiService.GetFromApiAsync<object>("api/Category");

            return View(result);
        }
        // Login Action
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _apiService.GetFromApiAsync<object>($"api/Member/Login?email={email}&password={password}");

            if (result != null)
            {
                return Json(new { success = true, token = result });
            }
            return Json(new { success = false, message = "Invalid login credentials" });
        }

        public async Task<IActionResult> Logout()
        {
            var result = await _apiService.GetFromApiAsync<object>("api/Product");
            var user = HttpContext.User.Identity.IsAuthenticated ? HttpContext.User : null;
            if (user == null)
            {

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                await _apiService.PostToApiAsync<object>("api/Product", product);
                return RedirectToAction("Index");
            }
            return View(product);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}