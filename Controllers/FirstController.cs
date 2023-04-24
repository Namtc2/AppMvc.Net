using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AppMvc.Net.Models;
using System.IO;
using AppMvc.Net.Services;

namespace AppMvc.Net.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly ProductService _productService;

        public FirstController(ILogger<FirstController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public string Index()
        {
            // this.HttpContext
            // this.Request
            // this.Response
            // this.RouteData
            
            // this.User
            // this.ModelState
            // this.ViewData
            // this.ViewBag
            // this.Url
            // this.TempData

            // Console.WriteLine("Index action");
            // _logger.LogWarning("Thông báo");
            // _logger.LogError("Thông báo");
            // _logger.LogDebug("Thong bao");
            // _logger.LogCritical("Thong bao");
            _logger.LogInformation("Index Action");
            return "Tôi là first index";
        }
        public IActionResult Bird(){
            string filePath = Path.Combine(Startup.ContentRootPath, "Files", "birds.jpg" );
            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "image/jpg");
        }
        public IActionResult IPhonePrice(){
            _logger.LogInformation("Get Iphone price");
            return Json(new {
                productName = "Iphone x",
                price = 500
            });
        }
        public IActionResult HelloView(){
            return View();
        }

        [TempData]
        public string StatusMessage {get; set;}
        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.Where(p=>p.Id == id).FirstOrDefault();
            if (product == null){
                //TempData["StatusMessage"] = "San pham khong ton tai";
                StatusMessage = "San pham khong ton tai";
                return Redirect(Url.Action("Index","Home"));
            }                
            return View(product);
        }
    }
}
