using Microsoft.AspNetCore.Mvc;
using PSecApp.WebUI.Models;
using System.Diagnostics;

namespace PSecApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
                    //private readonly string _fileName = "20240102_D_Daily MTM Report.xls";
        //private readonly string _destination = "C:\\Users\\micki\\Documents\\tst";
        //private readonly string _downloadSource = "https://clientportal.jse.co.za/_layouts/15/DownloadHandler.ashx?FileName=/YieldX/Derivatives/Docs_DMTM";




            return View();
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