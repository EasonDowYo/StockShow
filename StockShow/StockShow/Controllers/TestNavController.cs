using Microsoft.AspNetCore.Mvc;

namespace StockShow.Controllers
{
    public class TestNavController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
    }
}
