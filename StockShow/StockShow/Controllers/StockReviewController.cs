using Microsoft.AspNetCore.Mvc;

namespace StockShow.Controllers
{
    public class StockReviewController : Controller
    {
        public IActionResult StockTrend()
        {
            return View();
        }
    }
}
