using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

using StockShow.STKDataQueryFunc;

namespace StockShow.Controllers
{
    public class StockDataTableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public JsonResult GetStockData_ByStockNo(string StockNo,int days)
        {

            if (StockNo != "" && days !=0 )
            {
                STKDataQuery STKDataQuery = new STKDataQuery();
                DataTable dt = STKDataQuery.GetStockNoData(StockNo,days);
                string json_output = JsonConvert.SerializeObject(dt);

                return Json(json_output);
            }

            return Json(null);
        }
    }
}
