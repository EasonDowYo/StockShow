using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockShow.Models;
using StockShow.STKDataQuery;
using System.Data;
using static StockShow.Controllers.DemoController;

namespace StockShow.Controllers
{
    public class StockReviewController : Controller
    {
        public IActionResult StockTrend()
        {
            
            return View();
        }

        [HttpPost]
        public JsonResult Get5DaysData_Post([FromBody] Models.StockDataTable input_json)
        {

            if (input_json.StockNo != null)
            {
                StockDataQuery STKDataQuery = new StockDataQuery();
                DataTable dt = STKDataQuery.Get5DaysData(input_json.StockNo);
                string json_output = JsonConvert.SerializeObject(dt);

                return Json(json_output);
            }

            return Json(null);
        }
        [HttpPost]
        public JsonResult GetStockNoData_Post()
        {
            try
            {
                StockNoQuery STKNoQuery = new StockNoQuery();
                DataTable dt = STKNoQuery.GetAllStockNo();
                string json_output = JsonConvert.SerializeObject(dt);

                return Json(json_output);
            }
            catch
            {
                return Json(null);
            }
        }
    }
}
