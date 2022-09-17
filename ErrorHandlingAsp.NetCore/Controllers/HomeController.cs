using ErrorHandlingAsp.NetCore.Filters;
using ErrorHandlingAsp.NetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ErrorHandlingAsp.NetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [CustomHandleExceptionFilterAttribute(ErrorPage = "Error1")]
        public IActionResult Index()
        {
            //throw new Exception("Veri tabanına alanrken bir h edna gedi");

            int value1 = 5;
            int value2 = 0;

            int result = value1 / value2;

            return View();
        }

        [CustomHandleExceptionFilterAttribute(ErrorPage = "Error2")]
        public IActionResult Privacy()
        {
            throw new FileNotFoundException();

            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.path = exception.Path;
            ViewBag.message = exception.Error.Message;

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Error1()
        {
            return View();
        }

        public IActionResult Error2()
        {
            return View();
        }

    }
}