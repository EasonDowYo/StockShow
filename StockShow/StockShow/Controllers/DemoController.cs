using Microsoft.AspNetCore.Mvc;

namespace StockShow.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return RedirectToAction(nameof(Index3)); //切換到Index3的內容
        }
        public IActionResult Index3()
        {
            return View("Index");//切換到Index的內容
        }


        public IActionResult TestAJAX()
        {
            return View();
        }

        public IActionResult returnStr()
        {
            return Content("ResurnString");
        }

        public IActionResult returnStrByInput(string str1,int int1)
        {
            string stri = str1 + int1.ToString();
            return Content(stri);
        }

    }
}
